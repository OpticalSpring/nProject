using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    private static GameManager instance = null;
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        Invoke("CountPlayerInRoom", 5f);
    }

    public int curPlayer;
    public int maxPlayer;
    void CountPlayerInRoom()
    {
        curPlayer = PhotonNetwork.PlayerList.Length;
        maxPlayer = PhotonNetwork.CurrentRoom.MaxPlayers;
    }
}
