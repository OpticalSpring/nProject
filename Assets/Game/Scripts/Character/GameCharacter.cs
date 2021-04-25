using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class GameCharacter : MonoBehaviourPunCallbacks
{
    public int ColorNum;
    public Color PlayerColor;
    public string PlayerName;
    public CharacterInfo CharacterInfo;
    public CharacterStatus CurrentStatus;
    public List<SkinnedMeshRenderer> SkinnedMeshes;


    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        CommentMovement();
        GravityProcess();
    }

    void Init()
    {
        CharacterContext.Instance.RegisterCharacter(this);
        CharacterInfo = new CharacterInfo();
        CurrentStatus = new CharacterStatus();
        CharacterInfo.ID = GetComponent<PhotonView>().ViewID;

        PlayerName = photonView.Owner.NickName;
        int a = ColorManager.GetPlayerNameToCol(PlayerName);
        PlayerColor = ColorManager.NumToCol(a);
        SetPlayerColor(PlayerColor);
        PlayerName = ColorManager.SetPlayerNameToCol(PlayerName);
        gameObject.name = PlayerName;



        if (!photonView.IsMine)
        {
            return;
        }
        gameObject.layer = 2;
        IngameChatManager.instance.playerName = PlayerName;
        IngameChatManager.instance.playerColor = PlayerColor;
    }

    public void SetPlayerColor(Color color)
    {
        foreach (SkinnedMeshRenderer mesh in SkinnedMeshes)
        {
            mesh.material.color = color;
        }
    }
    public void Aim(Vector3 forward, float rootRotation)
    {
        CurrentStatus.Aim = true;
        CurrentStatus.CamVector = forward;
        CurrentStatus.RootRotation = rootRotation;
        GetComponent<GameCharacterAnim>().FireStateUpdate(true);
        GetComponent<GameCharacterAnim>().MoveStateUpdate(0, false);
    }

    public void AimOut()
    {
        CurrentStatus.Aim = false;
        GetComponent<GameCharacterAnim>().FireStateUpdate(false);
    }

    public void MovementUpdate(Vector3 direction, Vector2 input, bool run)
    {
        CurrentStatus.DirectionVector = direction;
        CurrentStatus.InputVector = input;
        CurrentStatus.RunState = run;
    }
    public void CommentMovement()
    {
        if (CurrentStatus.Aim == true)
        {
            Turn(CurrentStatus.CamVector);
            Move(CurrentStatus.DirectionVector, CurrentStatus.MoveSpeed);
            GetComponent<GameCharacterAnim>().Walk8WayStateUpdate(CurrentStatus.InputVector);
            GetComponent<GameCharacterAnim>().RootBoneUpdate(CurrentStatus.RootRotation);

        }
        else
        {
            GetComponent<GameCharacterAnim>().MoveStateUpdate(CurrentStatus.DirectionVector.magnitude, CurrentStatus.RunState);
            GetComponent<GameCharacterAnim>().RootBoneUpdate(0);
            if (CurrentStatus.DirectionVector.magnitude == 0)
            {
                return;
            }

            Turn(CurrentStatus.DirectionVector);

            if (CurrentStatus.RunState == true)
            {
                Move(CurrentStatus.DirectionVector, CurrentStatus.MoveFastSpeed);
            }
            else
            {
                Move(CurrentStatus.DirectionVector, CurrentStatus.MoveSpeed);
            }
        }
    }

    public void Fire()
    {
        GetComponent<GameCharacterAnim>().Fire();
    }

    public void GetDamage(int damage)
    {
        CurrentStatus.HP_NOW = Mathf.Max(CurrentStatus.HP_NOW - damage, 0);
    }

    void Turn(Vector3 targetPoint)
    {
        targetPoint += gameObject.transform.position;
        float dz = targetPoint.z - gameObject.transform.position.z;
        float dx = targetPoint.x - gameObject.transform.position.x;

        float rotateDegree = Mathf.Atan2(dx, dz) * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.Euler(0, rotateDegree, 0), 10 * Time.deltaTime);

    }
    void Move(Vector3 directionVector, float speed)
    {
        if (GameCharacterLogic.CheckObstacle(gameObject, directionVector) == false)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, gameObject.transform.position + directionVector, speed * Time.deltaTime);
        }
        else
        {
            gameObject.transform.position = GameCharacterLogic.GetSlidingVector(gameObject, directionVector, speed);
        }
    }
    float jumpTime;
    public void Jump()
    {
        CurrentStatus.Velocity = -0.12f;
        jumpTime = 0.1f;
    }

    void GravityProcess()
    {
        Vector3 newPos;
        if (GameCharacterLogic.CheckGround(gameObject, out newPos) && jumpTime < 0)
        {
            gameObject.transform.position = newPos;
            CurrentStatus.Velocity = 0;
        }
        else
        {
            CurrentStatus.Velocity = Mathf.Clamp(CurrentStatus.Velocity + (0.5f * Time.deltaTime), -1, 1);
            newPos = gameObject.transform.position;
            newPos.y -= CurrentStatus.Velocity;
            gameObject.transform.position = newPos;
            jumpTime -= Time.deltaTime;
        }
    }
}
