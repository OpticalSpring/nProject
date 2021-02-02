using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
public class TitleManager : MonoBehaviourPunCallbacks
{
    public static TitleManager instance = null;
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        OnLogin();
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
        PhotonNetwork.GameVersion = gameVer;
        PhotonNetwork.NickName = PlayerPrefs.GetString("NickName");
        if (PhotonNetwork.IsConnected == false)
            PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
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

            PhotonNetwork.JoinOrCreateRoom(PlayerPrefs.GetString("UserName"), RO, TypedLobby.Default);
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
                PhotonNetwork.PlayerList[i].CustomProperties.TryGetValue("Color",out colorNum);
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

    public bool GetPlayerColor(int n)
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
           if(ColorManager.GetPlayerNameToCol(PhotonNetwork.PlayerList[i].NickName) == n)
            {
                return true;
            }
        }
        return false;
    }

    public void SetPlayerColor(int n)
    {
        if (GetPlayerColor(n) == true)
        {
            return;
        }
        PhotonNetwork.NickName = ColorManager.SetPlayerColToName(PhotonNetwork.NickName, n);
    }

    void SetPlayerColorNoOverlap()
    {
        int n;
        while (true)
        {
            n = Random.Range(0, 13);
            if (GetPlayerColor(n) == false)
            {
                break;
            }
        }
        SetPlayerColor(n);
    }


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        for (int i = 0; i < roomGrid.transform.childCount; i++)
        {
            Destroy(roomGrid.transform.GetChild(i).gameObject);
        }

        foreach (RoomInfo roomInfo in roomList)
        {
            if (roomInfo.IsOpen == true)
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
        if (PhotonNetwork.InRoom)
        {

            PhotonNetwork.LeaveRoom();
            PhotonNetwork.JoinLobby();

            OpenUI(1);
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
        PhotonNetwork.CurrentRoom.IsOpen = false;
        SceneManager.LoadSceneAsync(1);
    }



    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        IngameChatManager.instance.SendChatMessage(otherPlayer.NickName + "님이 퇴장했습니다.");

    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        IngameChatManager.instance.SendChatMessage(newPlayer.NickName + "님이 입장했습니다.");

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
