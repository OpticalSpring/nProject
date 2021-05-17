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

    public GameProcessState gameProcess;

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
        IngameChatManager.Instance.SendNotifyMessage("Enter Scene", false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        yield return new WaitForSeconds(1);
        gameProcess = GameProcessState.Ingame;
        if (!PhotonNetwork.IsMasterClient)
        {
            yield break;
        }
        IngameChatManager.Instance.SendNotifyMessage("Character Set", true);
        new InitCharacterEvent(Random.Range(0, PhotonNetwork.CurrentRoom.PlayerCount)).Send();

    }


    private void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        if (gameProcess == GameProcessState.Start || gameProcess == GameProcessState.End)
        {
            return;
        }
        if(CharacterContext.Instance.GetAlliveCount() <= 1)
        {
            gameProcess = GameProcessState.End;
            if (CharacterContext.Instance.GetAlliveGameCharacter(0).CharacterInfo.IsSpy)
            {
                IngameChatManager.Instance.SendNotifyMessage("Spy Win", true);
            }
            else
            {
                IngameChatManager.Instance.SendNotifyMessage("Human Win", true);
            }
        }
    }

    public enum GameProcessState
    {
        Start,
        Ingame,
        End
    }
}


