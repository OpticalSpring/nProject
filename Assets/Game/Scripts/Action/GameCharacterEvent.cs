using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterMoveEvent : GameEvent
{
    public CharacterInfo Info;
    public Vector3 Forwad;
    public bool Run;
    public CharacterMoveEvent(GameCharacter caster, Vector3 forwad, bool run)
    {
        GameEventID = GameEventType.CharacterMove;
        Info = caster.CharacterInfo;
        Forwad = forwad;
        Run = run;
    }
}
