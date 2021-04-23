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
    public Vector3 ForwardVector;
    public Vector3 CamVector;
    public bool RunState;
    public float Velocity;
    public bool Aim;

    public CharacterStatus()
    {
        HP_NOW = HP_MAX;
        Velocity = 0;
    }
}
