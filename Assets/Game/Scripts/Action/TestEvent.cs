using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent : GameEvent
{
    public TestEvent() 
    {
        GameEventID = GameEventType.Test;
        Debug.Log("TestStart");
    }
}
