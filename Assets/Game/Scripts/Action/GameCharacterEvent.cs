using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterMoveEvent : GameEvent
{
    public CharacterInfo Info;
    public Vector3 Forward;
    public bool Run;
    public CharacterMoveEvent(GameCharacter caster, Vector3 forward, bool run)
    {
        GameEventID = GameEventType.CharacterMove;
        Info = caster.CharacterInfo;
        Forward = forward;
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
public class CharacterAimEvent : GameEvent
{
    public CharacterInfo Info;
    public Vector3 Forward;
    public CharacterAimEvent(GameCharacter caster, Vector3 forward)
    {
        GameEventID = GameEventType.CharacterAim;
        Info = caster.CharacterInfo;
        Forward = forward;
    }
}

[Serializable]
public class CharacterAimOutEvent : GameEvent
{
    public CharacterInfo Info;
    public CharacterAimOutEvent(GameCharacter caster)
    {
        GameEventID = GameEventType.CharacterAimOut;
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
        if (target == null) return;
        Target = target.CharacterInfo;
    }
}

