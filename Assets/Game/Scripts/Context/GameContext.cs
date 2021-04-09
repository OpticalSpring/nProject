using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContext : MonoBehaviourPunCallbacks
{
    public static GameContext Instance;
    private void Awake()
    {
        Instance = this;
    }

    private readonly Dictionary<GameEvent.GameEventType, GameEventAction> _gameEvents = new Dictionary<GameEvent.GameEventType, GameEventAction>();
    

    #region GameEvent
    public void InitGameEvent()
    {
        _gameEvents.Clear();
        foreach (GameEvent.GameEventType value in Enum.GetValues(typeof(GameEvent.GameEventType)))
        {
            _gameEvents.Add(value, new GameEventAction(value));
        }
    }

    public void RegisterObserver(GameEvent.GameEventType gameEventType, GameEventAction.GameEventDelegate gameEvent)
    {
        if (_gameEvents.ContainsKey(gameEventType) == true)
        {
            _gameEvents[gameEventType].GameEvent += gameEvent;
        }
    }

    public void DeleteObserver(GameEvent.GameEventType gameEventType, GameEventAction.GameEventDelegate gameEvent)
    {
        if (_gameEvents.ContainsKey(gameEventType) == true)
        {
            _gameEvents[gameEventType].GameEvent -= gameEvent;
        }
    }

    public void SendEvent(GameEvent data)
    {
        
        PhotonView photonView = PhotonView.Get(this);
        
        photonView.RPC("SendEventRPC", RpcTarget.All, EventJsonUtility.EventToBinary(data));
    }
    [PunRPC]
    public void SendEventRPC(string sdata)
    {
        GameEvent data = EventJsonUtility.BinaryToEvent(sdata);
        if (_gameEvents.ContainsKey(data.GameEventID) == true)
        {
            _gameEvents[data.GameEventID].OnGameEvent(data);
        }
    }

    #endregion
}
