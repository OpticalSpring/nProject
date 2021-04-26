using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterMoveEvent : GameEvent
{
    public CharacterInfo Caster;
    public Vector3 Direction;
    public Vector2 Input;
    public bool Run;
    public CharacterMoveEvent(CharacterInfo caster, Vector3 direction,Vector2 input, bool run)
    {
        GameEventID = GameEventType.CharacterMove;
        Caster = caster;
        Direction = direction;
        Input = input;
        Run = run;
    }
}


[Serializable]
public class CharacterJumpEvent : GameEvent
{
    public CharacterInfo Caster;
    public CharacterJumpEvent(CharacterInfo caster)
    {
        GameEventID = GameEventType.CharacterJump;
        Caster = caster;
    }
}

[Serializable]
public class CharacterAimEvent : GameEvent
{
    public CharacterInfo Caster;
    public Vector3 Forward;
    public float RootRatation;
    public CharacterAimEvent(CharacterInfo caster, Vector3 forward, float rootRatation)
    {
        GameEventID = GameEventType.CharacterAim;
        Caster = caster;
        Forward = forward;
        RootRatation = rootRatation;
    }
}

[Serializable]
public class CharacterAimOutEvent : GameEvent
{
    public CharacterInfo Caster;
    public CharacterAimOutEvent(CharacterInfo caster)
    {
        GameEventID = GameEventType.CharacterAimOut;
        Caster = caster;
    }
}

[Serializable]
public class CharacterFireEvent : GameEvent
{
    public CharacterInfo Caster;
    public CharacterFireEvent(CharacterInfo caster)
    {
        GameEventID = GameEventType.CharacterFire;
        Caster = caster;
    }
}

[Serializable]
public class CharacterDamageEvent : GameEvent
{
    public CharacterInfo Caster;
    public CharacterInfo Target;
    public int Damage;
    public CharacterDamageEvent(CharacterInfo caster, CharacterInfo target, int damage)
    {
        GameEventID = GameEventType.CharacterDamage;
        Caster = caster;
        Target = target;
        Damage = damage;
    }
}

