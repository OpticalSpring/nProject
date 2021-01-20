using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class IngameChatManager : MonoBehaviourPunCallbacks
{
    public static IngameChatManager instance = null;
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
    }

    public Text mainText;
    public InputField chatField;
    bool chatEnabled;
    public Color playerColor;
    public string playerName;

    void Start()
    {
        playerName = PhotonNetwork.NickName;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (chatEnabled)
            {
                Chat();
                chatField.gameObject.SetActive(false);
                chatEnabled = false;
            }
            else
            {
                chatField.gameObject.SetActive(true);
                chatField.ActivateInputField();
                chatEnabled = true;
            }
        }

    }


    public static string ColorToStr(Color color)
    {
        string r = ((int)(color.r * 255)).ToString("X2");
        string g = ((int)(color.g * 255)).ToString("X2");
        string b = ((int)(color.b * 255)).ToString("X2");
        string a = ((int)(color.a * 255)).ToString("X2");
        string result = string.Format("{0}{1}{2}{3}", r, g, b, a);
        return result;
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
        string result = string.Format("<color=#{0}>[{1}]</color> ", ColorToStr(playerColor), playerName);
        result += c;
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("SendChatMessage", RpcTarget.All, result);
        chatField.text = "";
    }

    [PunRPC]
    public void SendChatMessage(string inputLine)
    {
        if (string.IsNullOrEmpty(inputLine))
        {
            return;
        }
        mainText.text += "\n" + inputLine;
    }
}
