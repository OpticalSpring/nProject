using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitCharacterEvent : GameEvent
{
    public int Key;
    public InitCharacterEvent(int key)
    {
        GameEventID = GameEventType.InitCharacter;
        Key = key;
    }
}



