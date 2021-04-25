using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterMoveEvent : GameEvent
{
    public CharacterInfo Info;
    public Vector3 Direction;
    public Vector2 Input;
    public bool Run;
    public CharacterMoveEvent(GameCharacter caster, Vector3 direction,Vector2 input, bool run)
    {
        GameEventID = GameEventType.CharacterMove;
        Info = caster.CharacterInfo;
        Direction = direction;
        Input = input;
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
    public float RootRatation;
    public CharacterAimEvent(GameCharacter caster, Vector3 forward, float rootRatation)
    {
        GameEventID = GameEventType.CharacterAim;
        Info = caster.CharacterInfo;
        Forward = forward;
        RootRatation = rootRatation;
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

