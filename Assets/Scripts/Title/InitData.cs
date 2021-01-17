using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class InitData : MonoBehaviour
{
    public InputField inputName;

    private void Start()
    {
        inputName.text = PlayerPrefs.GetString("UserName");
        if(inputName.text.Length == 0)
        {
            inputName.text = "Random"+Random.Range(0, 999);
        }
        SetName();


    }

    public void SetName()
    {
        if (inputName.text.Length > 10)
        {
            inputName.text = inputName.text.Substring(0, 10);
        }
            
        PlayerPrefs.SetString("UserName", inputName.text);
        PhotonNetwork.NickName = inputName.text;
        Debug.Log(inputName.text.Length);
    }

    
}
