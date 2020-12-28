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

    


    void Chat()
    {
        
            string c = chatField.text;
            if(c.Length > 20)
            {
            c.Substring(0, 20);
            }
           // SendChatMessage(c);
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("SendChatMessage", RpcTarget.All, c);
            chatField.text = "";
        
    }

    [PunRPC]
    private void SendChatMessage(string inputLine)
	{
		if (string.IsNullOrEmpty(inputLine))
		{
			return;
		}
        mainText.text += "\n"+ inputLine;
    }
}
