using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NameTag : MonoBehaviour
{
    public GameObject Root;
    public GameObject Target;
    public Camera Cam;
    public Vector3 screenPos;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI HPNowText;
    public TextMeshProUGUI HPMaxText;
    public float remainTimeNow;
    public float remainTimeMax;
    private void Start()
    {
        UIContext.Instance.RegisterUI(this);
    }

    private void Update()
    {
        if(IsVisible())
        {
            Root.SetActive(true);
        }
        else
        {
            Root.SetActive(false);
        }
        FollowTarget();
        UpdateHP(Target.GetComponent<GameCharacter>().CurrentStatus.HP_NOW, Target.GetComponent<GameCharacter>().CurrentStatus.HP_MAX);
    }

    public void UpdateNameTag()
    {
        remainTimeNow = remainTimeMax;
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

    bool IsVisible()
    {
        if(remainTimeNow > 0)
        {
            remainTimeNow -= Time.deltaTime;
        }
        else
        {
            return false;
        }

        if(Target == null || Target.activeSelf == false)
        {
            return false;
        }
        
        if(screenPos.z < 0)
        {
            return false;
        }
        return true;
    }


    void FollowTarget()
    {
        screenPos = Cam.WorldToScreenPoint(Target.transform.position + new Vector3(0, 2, 0));
        gameObject.transform.position = screenPos;
    }
}
