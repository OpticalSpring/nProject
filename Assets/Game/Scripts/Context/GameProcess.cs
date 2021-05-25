using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameProcess : MonoBehaviourPunCallbacks
{
    public static GameProcess Instance;
    private void Awake()
    {
        Instance = this;
    }

    public GameProcessState GameState;
    public int GameCount;

    public void SubscribeEvent()
    {
        GameContext.Instance.RegisterObserver(GameEvent.GameEventType.GameProcessChange, GameProcessChange);
        GameContext.Instance.RegisterObserver(GameEvent.GameEventType.GameCountChange, SyncGameCount);
    }

    private void Start()
    {
        GameContext.Instance.InitGameEvent();
        CharacterContext.Instance.SubscribeEvent();
        UIContext.Instance.SubscribeEvent();
        FXContext.Instance.SubscribeEvent();
        SubscribeEvent();
        StartCoroutine(GameStart());
        StartCoroutine(GameCountDown());
    }
    IEnumerator GameStart()
    {
        IngameChatManager.Instance.SendNotifyMessage("Enter Scene", false);
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        yield return new WaitForSeconds(2);
        if (!PhotonNetwork.IsMasterClient)
        {
            yield break;
        }
        IngameChatManager.Instance.SendNotifyMessage("Character Set", true);
        var spy = PhotonNetwork.CurrentRoom.Players[Random.Range(1, PhotonNetwork.CurrentRoom.PlayerCount + 1)];
        new InitCharacterEvent(spy.NickName).Send();
        yield return new WaitForSeconds(5);
        IngameChatManager.Instance.SendNotifyMessage("Game Start", true);
        new GameProcessChangeEvent(GameProcessState.Ingame).Send();

    }

    IEnumerator GameCountDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (PhotonNetwork.IsMasterClient && GameState == GameProcessState.Ingame)
            {
                GameCount--;
                new GameCountChangeEvent(GameCount).Send();
                if (GameCount <= 0)
                {
                    new GameProcessChangeEvent(GameProcessState.End).Send();
                    IngameChatManager.Instance.SendNotifyMessage("Count is Zero - Human Win", true);
                }
            }
        }
    }

    void SyncGameCount(GameEvent data)
    {
        GameCountChangeEvent e = (GameCountChangeEvent)data;
        GameCount = e.Count;
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
        if (CharacterContext.Instance.GetAlliveCount() <= 1)
        {
            new GameProcessChangeEvent(GameProcessState.End).Send();
            if (CharacterContext.Instance.GetAlliveGameCharacter(0).CharacterInfo.IsSpy)
            {
                IngameChatManager.Instance.SendNotifyMessage("No Survival - Spy Win", true);
            }
            else
            {
                IngameChatManager.Instance.SendNotifyMessage("No Survival - Human Win", true);
            }
        }
        else
        {
            bool isSpy = false;
            foreach (var character in CharacterContext.Instance.GameCharacters)
            {
                if (character.CharacterInfo.IsSpy)
                {
                    isSpy = true;
                }
            }
            if (isSpy == false)
            {
                IngameChatManager.Instance.SendNotifyMessage("Find The Spy - Human Win", true);
                new GameProcessChangeEvent(GameProcessState.End).Send();
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

        if (e.State == GameProcessState.End)
        {
            StartCoroutine(GameEnd());
        }
    }

    IEnumerator GameEnd()
    {
        yield return new WaitForSeconds(1);
        IngameChatManager.Instance.SendNotifyMessage("Back Lobby Scene", false);
        yield return new WaitForSeconds(1);
        SceneFlowManager.Instance.IngameToTitleFromEndGame();
    }
}


