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

    public GameProcessState GameState;

    public void SubscribeEvent()
    {
        GameContext.Instance.RegisterObserver(GameEvent.GameEventType.GameProcessChange, GameProcessChange);
    }

    private void Start()
    {
        GameContext.Instance.InitGameEvent();
        CharacterContext.Instance.SubscribeEvent();
        UIContext.Instance.SubscribeEvent();
        FXContext.Instance.SubscribeEvent();
        SubscribeEvent();
        StartCoroutine(GameStart());
    }
    IEnumerator GameStart()
    {
        IngameChatManager.Instance.SendNotifyMessage("Enter Scene", false);
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        yield return new WaitForSeconds(1);
        if (!PhotonNetwork.IsMasterClient)
        {
            yield break;
        }
        IngameChatManager.Instance.SendNotifyMessage("Character Set", true);
        var spy = PhotonNetwork.CurrentRoom.Players[Random.Range(1, PhotonNetwork.CurrentRoom.PlayerCount+1)];
        new InitCharacterEvent(spy.NickName).Send();
        yield return new WaitForSeconds(5);
        IngameChatManager.Instance.SendNotifyMessage("Game Start", true);
        new GameProcessChangeEvent(GameProcessState.Ingame).Send();

    }


    private void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        if (GameState == GameProcessState.Start || GameState == GameProcessState.End)
        {
            return;
        }
        if(CharacterContext.Instance.GetAlliveCount() <= 1)
        {
            new GameProcessChangeEvent(GameProcessState.End).Send();
            if (CharacterContext.Instance.GetAlliveGameCharacter(0).CharacterInfo.IsSpy)
            {
                IngameChatManager.Instance.SendNotifyMessage("Spy Win", true);
            }
            else
            {
                IngameChatManager.Instance.SendNotifyMessage("Human Win", true);
            }
        }
        else
        {
            bool isSpy = false;
            foreach(var character in CharacterContext.Instance.GameCharacters)
            {
                if(character.CharacterInfo.IsSpy == true)
                {
                    isSpy = true;
                }
            }
            if(isSpy == false)
            {
                new GameProcessChangeEvent(GameProcessState.End).Send();
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

    void GameProcessChange(GameEvent data)
    {
        GameProcessChangeEvent e = (GameProcessChangeEvent)data;
        GameState = e.State;
    }
}


