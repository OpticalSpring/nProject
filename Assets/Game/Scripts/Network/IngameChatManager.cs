using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.Serialization;

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

    public Text MainText;
    public InputField ChatField;
    public bool ChatEnabled;
    public int PlayerColor;
    public string PlayerName;
    public int ChatLevel = 1;

    void Start()
    {
        PlayerName = PhotonNetwork.NickName;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (ChatEnabled)
            {
                Chat();
                ChatField.gameObject.SetActive(false);
                ChatEnabled = false;
            }
            else
            {
                ChatField.gameObject.SetActive(true);
                ChatField.ActivateInputField();
                ChatEnabled = true;
            }
        }

    }


    


    void Chat()
    {
        string c = ChatField.text;
        if (c.Length == 0)
        {
            return;
        }
        if (c.Length > 20)
        {
            c = c.Substring(0, 20);
        }
        string result = string.Format("<color=#{0}>[{1}]</color> ", ChatLevel == 2 ? ColorManager.ColToStr(ColorManager.NumToCol(0)) : ColorManager.ColToStr(ColorManager.NumToCol(PlayerColor)), PlayerName);
        result += c;
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("SendChatMessage", RpcTarget.All, result, ChatLevel);
        ChatField.text = "";
    }

    [PunRPC]
    public void SendChatMessage(string inputLine, int level = 1)
    {
        if (ChatLevel < level) return;
        if (string.IsNullOrEmpty(inputLine))
        {
            return;
        }
        MainText.text += "\n" + inputLine;
    }

    public void SendNotifyMessage(string inputLine, bool other)
    {
        if (other)
        {
            photonView.RPC("SendChatMessage", RpcTarget.All, inputLine, 0);
        }
        else
        {
            SendChatMessage(inputLine, 0);
        }
    }
}
