using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProcess : MonoBehaviourPunCallbacks
{
    public static GameProcess Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameContext.Instance.InitGameEvent();
        CharacterContext.Instance.SubscribeEvent();
        UIContext.Instance.SubscribeEvent();
        FXContext.Instance.SubscribeEvent();
        StartCoroutine(GameStart());
    }
    IEnumerator GameStart()
    {
        Debug.LogWarning("Enter Scene");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        yield return new WaitForSeconds(1);
        if (!PhotonNetwork.IsMasterClient)
        {
            yield break;
        }
        Debug.LogWarning("Init Character");
        new InitCharacterEvent().Send();
    }

}
