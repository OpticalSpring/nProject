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
        if(SceneFlowManager.Instance.NickName?.Length > 0)
        {
            inputName.text = SceneFlowManager.Instance.NickName;
        }
        else
        {
            inputName.text = PlayerPrefs.GetString("NickName");
        }
        SetName();
    }

    public void SetName()
    {
        if (inputName.text.Length == 0)
        {
            inputName.text = "Random" + Random.Range(0, 999);
        }
        if (inputName.text.Length > 10)
        {
            inputName.text = inputName.text.Substring(0, 10);
        }
        inputName.text = inputName.text.Replace('#', ' ');


        PlayerPrefs.SetString("NickName", inputName.text);
        PhotonNetwork.NickName = inputName.text;
    }

    
}
