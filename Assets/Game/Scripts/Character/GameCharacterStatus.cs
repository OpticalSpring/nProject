using System;
using UnityEngine;

[Serializable]
public struct CharacterInfo
{
    public int ID;
}

public class CharacterStatus
{
    public int HP_NOW;
    public int HP_MAX = 1000;
    public float MoveSpeed = 3;
    public float MoveFastSpeed = 6;
    public Vector3 DirectionVector;
    public Vector3 CamVector;
    public Vector2 InputVector;
    public bool RunState;
    public float Velocity;
    public bool Aim;

    public CharacterStatus()
    {
        HP_NOW = HP_MAX;
        Velocity = 0;
    }
}
