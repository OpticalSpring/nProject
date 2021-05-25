using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public static Color NumToCol(int n)
    {
        Color c = Color.gray;
        switch (n)
        {
            case 1:
                c = Color.red;
                break;
            case 2:
                c = Color.blue;
                break;
            case 3: //Teal
                c = new Vector4(0, 128f / 255f, 128f / 255f, 1);
                break;
            case 4: //Purple
                c = new Vector4(139f / 255f, 0, 1, 1);
                break;
            case 5: //Orange
                c = new Vector4(255f / 255f, 102f / 255f, 0, 1);
                break;
            case 6: //Brown
                c = new Vector4(139f / 255f, 69f / 255f, 19f / 255f, 1);
                break;
            case 7:
                c = Color.white;
                break;
            case 8:
                c = Color.yellow;
                break;
            case 9: //Green
                c = new Vector4(65f / 255f, 117f / 255f, 5f / 255f, 1);
                break;
            case 10: //Pale Yellow
                c = new Vector4(252f / 255f, 238f / 255f, 167f / 255f, 1);
                break;
            case 11:
                c = Color.cyan;
                break;
            case 12:
                c = Color.black;
                break;
            default:
                c = Color.gray;
                break;
        }

        return c;
    }

    public static string SetPlayerColToName(string name, int col)
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

    public static string GetPlayerNameOutCol(string name)
    {
        string str = name;
        if (str.IndexOf("#") != -1)
        {
            str = str.Substring(0, str.IndexOf("#"));
        }
        return str;
    }
    
    public static int GetPlayerNameToCol(string name)
    {
        if (!name.Contains("#"))
        {
            return 0;
        }

        return int.Parse(name.Substring(name.IndexOf("#") + 1, 2));
    }

    public static string SetPlayerNameToCol(string name)
    {
        return name.Substring(0, name.IndexOf("#"));
    }

    public static string ColToStr(Color color)
    {
        string r = ((int) (color.r * 255)).ToString("X2");
        string g = ((int) (color.g * 255)).ToString("X2");
        string b = ((int) (color.b * 255)).ToString("X2");
        string a = ((int) (color.a * 255)).ToString("X2");
        string result = string.Format("{0}{1}{2}{3}", r, g, b, a);
        return result;
    }
}