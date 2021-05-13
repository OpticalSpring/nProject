using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class IngameChatManager : MonoBehaviourPunCallbacks
{
    public static IngameChatManager Instance = null;
    void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
        }
    }

    public Text mainText;
    public InputField chatField;
    public bool ChatEnabled;
    public Color playerColor;
    public string playerName;
    public int ChatLevel = 1;

    void Start()
    {
        playerName = PhotonNetwork.NickName;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (ChatEnabled)
            {
                Chat();
                chatField.gameObject.SetActive(false);
                ChatEnabled = false;
            }
            else
            {
                chatField.gameObject.SetActive(true);
                chatField.ActivateInputField();
                ChatEnabled = true;
            }
        }

    }


    


    void Chat()
    {
        string c = chatField.text;
        if (c.Length == 0)
        {
            return;
        }
        if (c.Length > 20)
        {
            c = c.Substring(0, 20);
        }
        string result = string.Format("<color=#{0}>[{1}]</color> ", ChatLevel == 2 ? ColorManager.ColToStr(ColorManager.NumToCol(0)) : ColorManager.ColToStr(playerColor), playerName);
        result += c;
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("SendChatMessage", RpcTarget.All, result, ChatLevel);
        chatField.text = "";
    }

    [PunRPC]
    public void SendChatMessage(string inputLine, int level = 1)
    {
        if (ChatLevel < level) return;
        if (string.IsNullOrEmpty(inputLine))
        {
            return;
        }
        mainText.text += "\n" + inputLine;
    }
}
