using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContext : MonoBehaviour
{
    public static GameContext Instance;

    private void Awake()
    {
        Instance = this;
        InitGameEvent();
    }

    private readonly Dictionary<GameEvent.GameEventType, GameEventAction> _gameEvents = new Dictionary<GameEvent.GameEventType, GameEventAction>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region GameEvent
    void InitGameEvent()
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
        if (_gameEvents.ContainsKey(data.GameEventID) == true)
        {
            _gameEvents[data.GameEventID].OnGameEvent(data);
        }
    }

    #endregion
}
