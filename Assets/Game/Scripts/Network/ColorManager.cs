﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    
    static public Color NumToCol(int n)
    {
        Color c = Color.gray;
        switch (n)
        {
            case 0:
                c = Color.black;
                break;
            case 1:
                c = Color.white;
                break;
            case 2:
                c = Color.red;
                break;
            case 3:
                c = Color.blue;
                break;
            case 4:
                c = Color.green;
                break;
            case 5:
                c = Color.yellow;
                break;
            case 6:
                c = Color.cyan;
                break;
            case 7://핑크
                c = new Vector4(1, 0, 1, 1);
                break;
            case 8://주황
                c = new Vector4(255f / 255f, 102f / 255f, 0, 1);
                break;
            case 9://보라
                c = new Vector4(139f / 255f, 0, 1, 1);
                break;
            case 10://갈색
                c = new Vector4(139f / 255f, 69f / 255f, 19f / 255f, 1);
                break;
            case 11://연두
                c = new Vector4(0, 128f / 255f, 0, 1);
                break;
            default:
                c = Color.gray;
                break;
        }
        return c;
    }

    static public string SetPlayerColToName(string name, int col)
    {
        string str = name;
        if (str.IndexOf("#") != -1)
        {
            str = str.Substring(0, str.IndexOf("#"));
        }
        if (col < 10)
        {
            str = str + "#0" + col;
        }
        else
        {

            str = str + "#" + col;
        }
        return str;
    }
    static public int GetPlayerNameToCol(string name)
    {
        return int.Parse(name.Substring(name.IndexOf("#") + 1, 2));
    }
    static public string SetPlayerNameToCol(string name)
    {
        return name.Substring(0, name.IndexOf("#"));
    }
    
}
