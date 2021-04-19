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
        CommentMovement(CurrentStatus.ForwadVector, CurrentStatus.RunState);
        GravityProcess();
    }

    void Init()
    {
        CharacterContext.Instance.RigisterCharacter(this);
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

    public void MovementUpdate(Vector3 forwad, bool run)
    {
        CurrentStatus.ForwadVector = forwad;
        CurrentStatus.RunState = run;
    }
    public void CommentMovement(Vector3 forwad, bool run)
    {
        GetComponent<GameCharacterAnim>().MoveStateUpdate(forwad.magnitude, run);
        if(forwad.magnitude == 0)
        {
            return;
        }
        Turn(forwad);
        if (run == true)
        {
            Move(CurrentStatus.MoveFastSpeed);
        }
        else
        {
            Move(CurrentStatus.MoveSpeed);
        }
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
    void Move(float speed)
    {
        RaycastHit rayHit;
        int mask = 1 << 2;
        mask = ~mask;

        if (GameCharacterLogic.CheckObstacle(gameObject) == false)
        {
            gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            gameObject.transform.position = GameCharacterLogic.GetSlidingVector(gameObject, speed);
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
