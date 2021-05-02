using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDControl : MonoBehaviour
{
    public GameObject Root;
    public GameObject Target;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI HPNowText;
    public TextMeshProUGUI HPMaxText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsVisible())
        {
            Root.SetActive(true);
        }
        else
        {
            Root.SetActive(false);
        }
    }

    bool IsVisible()
    {
        if(Target == null)
        {
            return false;
        }
        return true;
    }

    public void UpdateNameTag()
    {
        UpdateName(Target.GetComponent<GameCharacter>().PlayerName);
        UpdateHP(Target.GetComponent<GameCharacter>().CurrentStatus.HP_NOW, Target.GetComponent<GameCharacter>().CurrentStatus.HP_MAX);
    }

    void UpdateHP(int curHP, int maxHP)
    {
        HPNowText.text = curHP + "";
        HPMaxText.text = maxHP + "";
    }

    void UpdateName(string name)
    {
        NameText.text = name;
    }
}
