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



    public void CommentMovement(Vector3 forwad, bool run)
    {
        Debug.Log("ㄹㅇ");
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
            //앞에 장애물이 타밎될 경우 슬라이딩 벡터를 통해 벽에 미끄러지는 것을 구현하여 답답하지 않은 조작을 의도
            Vector3 S;
            Vector3 V = Vector3.Normalize(gameObject.transform.forward);
            Vector3 nPoint = gameObject.transform.position;
            if (Physics.SphereCast(gameObject.transform.position, 0.2f, gameObject.transform.forward, out rayHit, 0.8f, mask))
            {
                S = V - rayHit.normal * (Vector3.Dot(V, rayHit.normal));
                if (Physics.Raycast(gameObject.transform.position, S, out rayHit, 0.5f, mask))
                {

                }
                else
                {

                    nPoint += S * 10;
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, nPoint, Mathf.Abs(S.x + S.y + S.z) * speed * Time.deltaTime * 1.0f);
                }
            }
        }
    }
}
