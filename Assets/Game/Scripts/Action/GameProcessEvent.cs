using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InitCharacterEvent : GameEvent
{
    public string ID;
    public InitCharacterEvent(string id)
    {
        GameEventID = GameEventType.InitCharacter;
        ID = id;
    }
}

[Serializable]
public class GameProcessChangeEvent : GameEvent
{
    public GameProcess.GameProcessState State;

    public GameProcessChangeEvent(GameProcess.GameProcessState state)
    {
        GameEventID = GameEventType.GameProcessChange;
        State = state;
    }
}



