using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectColor : MonoBehaviour
{
    public int colorNum;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().color = ColorManager.instance.NumToCol(colorNum);
    }

    public void Select()
    {
        
        TitleManager.instance.SetPlayerColor(colorNum);
    }
}
