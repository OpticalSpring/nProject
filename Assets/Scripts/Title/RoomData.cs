using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomData : MonoBehaviour
{
    public string roomName;
    public int curPlayer;
    public int maxPlayer;
    public Text roomDataText;
    // Start is called before the first frame update
    void Awake()
    {
        roomDataText = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateInfo()
    {
        roomDataText.text = roomName + "[" + curPlayer + "/"+maxPlayer+"]";
    }

    public void JoinRoom()
    {
        TitleManager.instance.JoinRoom(roomName);
    }
}
