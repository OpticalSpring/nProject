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

[Serializable]
public class CharacterJumpEvent : GameEvent
{
    public CharacterInfo Info;
    public CharacterJumpEvent(GameCharacter caster)
    {
        GameEventID = GameEventType.CharacterJump;
        Info = caster.CharacterInfo;
    }
}

[Serializable]
public class CharacterFireEvent : GameEvent
{
    public CharacterInfo Caster;
    public CharacterInfo Target;
    public int Damage;
    public CharacterFireEvent(GameCharacter caster, GameCharacter target, int damage)
    {
        GameEventID = GameEventType.CharacterFire;
        Caster = caster.CharacterInfo;
        Damage = damage;
        Target = target.CharacterInfo;
    }
}
