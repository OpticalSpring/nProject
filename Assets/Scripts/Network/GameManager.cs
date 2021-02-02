using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance = null;
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        Invoke("CountPlayerInRoom", 5f);
    }

    public GameObject playerObject;
    public GameObject camObject;

    public int curPlayer;
    public int maxPlayer;

    private void Start()
    {
        InitPlayer();
    }
    void CountPlayerInRoom()
    {
        curPlayer = PhotonNetwork.PlayerList.Length;
        maxPlayer = PhotonNetwork.CurrentRoom.MaxPlayers;
    }

    void InitPlayer()
    {
        GameObject player = PhotonNetwork.Instantiate(
            playerObject.name,
            gameObject.transform.position,
            Quaternion.identity,
            0
        );
        
        player.GetComponent<PlayerControl>().cam = camObject.transform.GetChild(0);
        camObject.GetComponent<CameraControl>().camTarget = player;
        
    }

    public void CheckPlayerList()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Player");
        
    }

    public GameObject GetPlayerObject(int ID)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < objects.Length; i++)
        {
            if(objects[i].GetComponent<PhotonView>().Owner.ActorNumber == ID)
            {
                return objects[i];
            }
        }
        return null;
    }
}
