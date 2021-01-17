using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
public class TitleManager : MonoBehaviourPunCallbacks
{
    private string gameVer = "0.1";
    public GameObject[] uiGroup;
    public GameObject roomGrid;
    public GameObject roomPrefab;
    public GameObject startButton;
    // Start is called before the first frame update
    void Awake()
    {
        OnLogin();
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedRoom()
    {
        
    }

    public void InitRoom()
    {
        OpenUI(3);
        startButton.SetActive(true);
        RoomOptions RO = new RoomOptions();
        RO.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();
        RO.MaxPlayers = 8;
        RO.IsOpen = true;
        RO.IsVisible = true;

       // RO.CustomRoomPropertiesForLobby = new string[1];
       // RO.CustomRoomPropertiesForLobby[0] = "map";
        //int count1 = Random.Range(0, 10000);
       // RO.CustomRoomProperties.Add("map", count1.ToString());

        PhotonNetwork.JoinOrCreateRoom(PlayerPrefs.GetString("NickName"), RO, TypedLobby.Default);

    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("ROOM"))
        {
            Destroy(obj);
        }
        foreach(RoomInfo roomInfo in roomList)
        {
            GameObject _room = Instantiate(roomPrefab, roomGrid.transform);
            RoomData roomData = _room.GetComponent<RoomData>();
            roomData.roomName = roomInfo.Name;
            roomData.maxPlayer = roomInfo.MaxPlayers;
            roomData.curPlayer = roomInfo.PlayerCount;
            roomData.UpdateInfo();
            roomData.GetComponent<Button>().onClick.AddListener(
                delegate
            {
                OnClickRoom(roomData.roomName);
            }
            );
        }
    }

    void OnClickRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName, null);
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
}
