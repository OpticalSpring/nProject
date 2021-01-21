using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public static ColorManager instance = null;
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
    }
    public Color NumToCol(int n)
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
}
