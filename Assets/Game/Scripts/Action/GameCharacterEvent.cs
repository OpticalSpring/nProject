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
public class CharacterShotEvent : GameEvent
{
    public CharacterInfo Caster;
    public CharacterInfo Target;
    public int Damage;
    public CharacterShotEvent(CharacterInfo caster, CharacterInfo target, int damage)
    {
        GameEventID = GameEventType.CharacterShot;
        Caster = caster;
        Target = target;
        Damage = damage;
    }
}

[Serializable]
public class CharacterTryConsumeEvent : GameEvent
{
    public CharacterInfo Caster;
    public CharacterTryConsumeEvent(CharacterInfo caster)
    {
        GameEventID = GameEventType.CharacterTryConsume;
        Caster = caster;
    }
}

[Serializable]
public class CharacterConsumeEvent : GameEvent
{
    public CharacterInfo Caster;
    public CharacterInfo Target;
    public CharacterConsumeEvent(CharacterInfo caster, CharacterInfo target)
    {
        GameEventID = GameEventType.CharacterConsume;
        Caster = caster;
        Target = target;
    }
}

[Serializable] 
public class SpawnFXEvent : GameEvent
{
    public string Prefab;
    public Vector3 Position;
    public Quaternion Rotation;
    public SpawnFXEvent(string prefab, Vector3 position, Quaternion rotation)
    {
        GameEventID = GameEventType.SpawnFX;
        Prefab = prefab;
        Position = position;
        Rotation = rotation;
    }
}