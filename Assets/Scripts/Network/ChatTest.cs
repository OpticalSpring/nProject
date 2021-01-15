using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ChatTest : MonoBehaviourPunCallbacks
{
    public Text mainText;
    public InputField chatField;
    bool chatEnabled;
    public Color nameColor;
    public string namePlayer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
            c.Substring(0, 20);
        }
        string result = string.Format("<color=#{0}>[{1}]</color> ", ColorToStr(nameColor), namePlayer);
        result += c;
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("SendChatMessage", RpcTarget.All, result);
        chatField.text = "";
    }

    [PunRPC]
    private void SendChatMessage(string inputLine)
    {
        if (string.IsNullOrEmpty(inputLine))
        {
            return;
        }
        mainText.text += "\n" + inputLine;
    }
}
