using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class GameEvent
{
    [Serializable]
    public enum GameEventType
    {

        //GameProcess
        InitCharacter,

        //Character
        CharacterMove,
        CharacterJump,
        CharacterAim,
        CharacterAimOut,
        CharacterFire,
        CharacterDamage,
        CharacterTryConsume,
        CharacterConsume,

        //DamageLogic
        HitAttack,
    }

    public GameEventType GameEventID;
    public readonly uint Id;
    public Action<GameEvent> OnAction;
    static uint LastId;
    public virtual void Send()
    {
        GameContext.Instance.SendEvent(this);
    }

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

public class EventJsonUtility
{
    public static string EventToBinary(GameEvent eventdata)
    {
        return UnityEngine.JsonUtility.ToJson(eventdata);
    }

    public static GameEvent BinaryToEvent(string eventString)
    {
        switch (UnityEngine.JsonUtility.FromJson<GameEvent>(eventString).GameEventID)
        {
            case GameEvent.GameEventType.InitCharacter:
                return UnityEngine.JsonUtility.FromJson<InitCharacterEvent>(eventString);
            case GameEvent.GameEventType.CharacterMove:
                return UnityEngine.JsonUtility.FromJson<CharacterMoveEvent>(eventString);
            case GameEvent.GameEventType.CharacterJump:
                return UnityEngine.JsonUtility.FromJson<CharacterJumpEvent>(eventString);
            case GameEvent.GameEventType.CharacterFire:
                return UnityEngine.JsonUtility.FromJson<CharacterFireEvent>(eventString);
            case GameEvent.GameEventType.CharacterDamage:
                return UnityEngine.JsonUtility.FromJson<CharacterDamageEvent>(eventString);
            case GameEvent.GameEventType.CharacterAim:
                return UnityEngine.JsonUtility.FromJson<CharacterAimEvent>(eventString);
            case GameEvent.GameEventType.CharacterAimOut:
                return UnityEngine.JsonUtility.FromJson<CharacterAimOutEvent>(eventString);
            case GameEvent.GameEventType.CharacterTryConsume:
                return UnityEngine.JsonUtility.FromJson<CharacterTryConsumeEvent>(eventString);
            case GameEvent.GameEventType.CharacterConsume:
                return UnityEngine.JsonUtility.FromJson<CharacterConsumeEvent>(eventString);
        }

        return UnityEngine.JsonUtility.FromJson<GameEvent>(eventString);
    }
}



