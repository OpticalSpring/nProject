using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerCharacter : MonoBehaviourPunCallbacks
{
    public int HP_NOW;
    public int HP_MAX;
    public float moveSpeed;
    public float moveFastSpeed;

    public int colorNum;
    public Color playerColor;
    public string playerName;
    public SkinnedMeshRenderer colorMat;
    GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        HP_NOW = HP_MAX;
        playerName = photonView.Owner.NickName;
        int a = int.Parse(playerName.Substring(playerName.IndexOf("#") + 1, 2));
        playerColor = ColorManager.instance.NumToCol(a);
        colorMat.material.color = playerColor;
        playerName = playerName.Substring(0, playerName.IndexOf("#"));
        gameObject.name = playerName;
        cam = GameManager.instance.camObject.transform.GetChild(0).GetChild(0).gameObject;
        

        if (!photonView.IsMine)
        {
            return;
        }
        gameObject.layer = 2;
        IngameChatManager.instance.playerName = playerName;
        IngameChatManager.instance.playerColor = playerColor;
    }

    // Update is called once per frame
    void Update()
    {

    }

    bool CheckObstacle()
    {
        RaycastHit rayHit;
        int mask = 1 << 2 | 1 << 8 | 1 << 9;
        mask = ~mask;

        if (Physics.SphereCast(gameObject.transform.position + new Vector3(0, 1, 0), 0.2f, gameObject.transform.forward, out rayHit, 0.3f, mask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CommentMovement(Vector3 forwad, bool run)
    {
        Turn(forwad);
        if (run == true)
        {
            Move(moveFastSpeed);
        }
        else
        {
            Move(moveSpeed);
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

        if (CheckObstacle() == false)
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

    public void TakeOtherPlayer()
    {
        
        RaycastHit rayHit;
        int mask = 1 << 2;
        mask = ~mask;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out rayHit, 100f, mask))
        {
            Debug.Log(rayHit.collider.gameObject.name);
            
            if (!rayHit.collider.gameObject.GetComponent<PlayerCharacter>())
            {
                return;
            }
            
            if (rayHit.collider.gameObject.GetComponent<PlayerCharacter>().photonView.IsMine == true)
            {
                return;
            }
            
            photonView.RPC("TakeOtherData", RpcTarget.All, rayHit.collider.gameObject.GetComponent<PhotonView>().Owner.ActorNumber);
        }
    }
    [PunRPC]
    public void TakeOtherData(int ID)
    {
        GameObject other = GameManager.instance.GetPlayerObject(ID);
        playerName = other.GetComponent<PlayerCharacter>().playerName;
        playerColor = other.GetComponent<PlayerCharacter>().playerColor;
        colorMat.material.color = playerColor;
        if (photonView.IsMine)
        {
            IngameChatManager.instance.playerName = playerName;
            IngameChatManager.instance.playerColor = playerColor;
        }
    }



    public void Shot()
    {
    }
}
