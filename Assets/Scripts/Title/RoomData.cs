using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomData : MonoBehaviour
{
    public string roomName;
    public int curPlayer;
    public int maxPlayer;
    public int gameState;
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
        if(gameState == 1)
        {
            roomDataText.text += " 게임중";
        }
        else
        {
            roomDataText.text += " 대기중";
        }
    }

    public void JoinRoom()
    {
        TitleManager.instance.JoinRoom(roomName);
    }
}
