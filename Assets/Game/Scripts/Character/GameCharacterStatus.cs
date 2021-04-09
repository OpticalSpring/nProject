﻿using System;
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
    public Vector3 ForwadVector;
    public bool RunState;


    public CharacterStatus()
    {
        HP_NOW = HP_MAX;
        
    }
}
