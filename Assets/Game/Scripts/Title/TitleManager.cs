using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
public class TitleManager : MonoBehaviourPunCallbacks
{
    public static TitleManager Instance = null;
    void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
        }
        SceneFlowManager.Instance.Init();
        OnLogin();
        StartCoroutine(AutoReflesh());
    }
    private string gameVer = "0.1";
    public GameObject[] uiGroup;
    public GameObject roomGrid;
    public GameObject roomPrefab;
    public GameObject startButton;
    public Text curRoomName;
    public Text curPlayerList;



    // Update is called once per frame
    void Update()
    {
        UpdateRoomInfo();
    }

    void OnLogin()
    {
        if (PhotonNetwork.IsConnected) return;
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = gameVer;
        PhotonNetwork.NickName = PlayerPrefs.GetString("NickName");
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
        PhotonNetwork.QuickResends = 3;
        Debug.Log("LoginServer");
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        Debug.Log("ConnectServer");
    }

    public override void OnJoinedRoom()
    {

    }

    public void InitRoom()
    {
        if (PhotonNetwork.InLobby)
        {
            RoomOptions RO = new RoomOptions();
            RO.MaxPlayers = 8;
            RO.IsOpen = true;
            RO.IsVisible = true;

            PhotonNetwork.JoinOrCreateRoom(PlayerPrefs.GetString("NickName"), RO, TypedLobby.Default);
            OpenUI(3);
            SetPlayerColorNoOverlap();
            Debug.Log("InitRoom");
        }
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName, null);
        OpenUI(3);
        SetPlayerColorNoOverlap();
        Debug.Log("JoinRoom");
    }

    public void UpdateRoomInfo()
    {
        if (PhotonNetwork.InRoom)
        {
            
            curRoomName.text = PhotonNetwork.CurrentRoom.Name + "";
            curPlayerList.text = "";
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                object colorNum;
                PhotonNetwork.PlayerList[i].CustomProperties.TryGetValue("Color", out colorNum);
                curPlayerList.text += PhotonNetwork.PlayerList[i].NickName + colorNum + "\n";

            }

            if (PhotonNetwork.IsMasterClient)
            {
                startButton.SetActive(true);
            }
            else
            {
                startButton.SetActive(false);
            }

        }
    }

    IEnumerator AutoReflesh()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (PhotonNetwork.InRoom)
            {
                if (!uiGroup[3].activeSelf)
                {
                    OpenUI(3);
                }

                IsOverlapPlayerColor();
            }
        }
    }

    public bool GetPlayerColor(int n)
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if (ColorManager.GetPlayerNameToCol(PhotonNetwork.PlayerList[i].NickName) == n)
            {
                return true;
            }
        }
        return false;
    }

    public void SetPlayerColor(int n)
    {
        if (GetPlayerColor(n))
        {
            return;
        }
        PhotonNetwork.NickName = ColorManager.SetPlayerColToName(PhotonNetwork.NickName, n);
        SceneFlowManager.Instance.NickName = ColorManager.GetPlayerNameOutCol(PhotonNetwork.NickName);
        SceneFlowManager.Instance.ColorNum = n;
        IngameChatManager.Instance.PlayerColor = n;
    }

    void SetPlayerColorNoOverlap()
    {
        int n = SceneFlowManager.Instance.ColorNum;
        if(n == 0) n = Random.Range(1, 13);
        while (true)
        {
            if (!GetPlayerColor(n))
            {
                break;
            }
            n = Random.Range(1, 13);
        }
        SetPlayerColor(n);
    }

    void IsOverlapPlayerColor()
    {
        string oldName = PhotonNetwork.NickName;
        int n = ColorManager.GetPlayerNameToCol(oldName);
        if (n == 0)
        {
            SetPlayerColorNoOverlap();
            return;
        }
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if(PhotonNetwork.PlayerList[i].NickName == oldName) continue;
            if (ColorManager.GetPlayerNameToCol(PhotonNetwork.PlayerList[i].NickName) == n)
            {
                SetPlayerColorNoOverlap();
                return;
            }
        }
    }


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        for (int i = 0; i < roomGrid.transform.childCount; i++)
        {
            Destroy(roomGrid.transform.GetChild(i).gameObject);
        }

        foreach (RoomInfo roomInfo in roomList)
        {
            if (roomInfo.IsOpen)
            {

                GameObject _room = Instantiate(roomPrefab, roomGrid.transform);
                RoomData roomData = _room.GetComponent<RoomData>();
                roomData.roomName = roomInfo.Name;
                roomData.maxPlayer = roomInfo.MaxPlayers;
                roomData.curPlayer = roomInfo.PlayerCount;
                roomData.UpdateInfo();
            }

        }
    }


    public void LeftRoom()
    {
        OpenUI(1);
        if (PhotonNetwork.InRoom)
        {

            PhotonNetwork.LeaveRoom();
            PhotonNetwork.JoinLobby();

            Debug.Log("LeftRoom");
        }
    }

    public void OpenUI(int i)
    {
        CloseAllUI();
        uiGroup[i].SetActive(true);
    }


    void CloseAllUI()
    {
        for (int i = 0; i < uiGroup.Length; i++)
        {
            uiGroup[i].SetActive(false);
        }
    }

    public void StartGame()
    {
        photonView.RPC("ShardGame", RpcTarget.All);
    }

    [PunRPC]
    public void ShardGame()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        SceneFlowManager.Instance.TitleToIngameFromGameStart();
    }

    public void EndGame()
    {
        PhotonNetwork.CurrentRoom.IsOpen = true;

        OpenUI(3);
        SetPlayerColorNoOverlap();
        
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        IngameChatManager.Instance.SendNotifyMessage(otherPlayer.NickName + "님이 퇴장했습니다.", false);


    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        IngameChatManager.Instance.SendNotifyMessage(newPlayer.NickName + "님이 입장했습니다.", false);

    }
    void OnDisconnectedFromServer()
    {
        PhotonNetwork.ReconnectAndRejoin();
    }
    void OnPlayerDisconnected()
    {
        PhotonNetwork.RemoveRPCs(PhotonNetwork.LocalPlayer);
    }

    void OnFailedToConnect()
    {
        PhotonNetwork.ReconnectAndRejoin();
    }

    void OnFailedToConnectToMasterServer()
    {
        PhotonNetwork.ReconnectAndRejoin();
    }
    void OnApplicationPause(bool paused)
    {
        if (paused)
        {
            PhotonNetwork.ReconnectAndRejoin();
        }
    }


}
