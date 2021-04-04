using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectColor : MonoBehaviour
{
    public int colorNum;
    public GameObject xImage;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().color = ColorManager.NumToCol(colorNum);
    }

    public void Select()
    {
        TitleManager.instance.SetPlayerColor(colorNum);
    }

    private void Update()
    {
        if(TitleManager.instance.GetPlayerColor(colorNum) == true)
        {
            xImage.SetActive(true);
        }
        else
        {
            xImage.SetActive(false);
        }
    }
}
