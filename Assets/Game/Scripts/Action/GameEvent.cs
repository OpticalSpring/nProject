using System;
using System.Collections;
using System.Collections.Generic;


public class GameEvent
{
    public enum GameEventType
    {
        Test,
        Test2
    }

    public virtual void Send()
    {
        GameContext.Instance.SendEvent(this);
    }
    

    public GameEventType GameEventID { get; set; }
    public readonly uint Id;
    public Action<GameEvent> OnAction { get; }
    static uint LastId;

    public GameEvent()
    {
        Id = ++LastId;
    }

    public GameEvent(Action<GameEvent> onAction)
    {
        OnAction = onAction;
        Id = ++LastId;
    }

    public bool Equals(GameEvent other)
    {
        return other != null && Id == other.Id;
    }
}
    public class GameEventAction
    {
        public GameEvent.GameEventType GameEventID { get; protected set; }
        public delegate void GameEventDelegate(GameEvent eventdata);
        public event GameEventDelegate GameEvent;

        public GameEventAction(GameEvent.GameEventType gameEventID)
        {
            GameEventID = gameEventID;
        }

        public void OnGameEvent(GameEvent eventdata)
        {
            GameEvent?.Invoke(eventdata);
        }
    }



