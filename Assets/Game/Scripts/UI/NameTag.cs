﻿using System.Collections;
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

    private void Start()
    {
        UpdateName(Target.GetComponent<GameCharacter>().PlayerName);
        UpdateHP(Target.GetComponent<GameCharacter>().CurrentStatus.HP_NOW, Target.GetComponent<GameCharacter>().CurrentStatus.HP_MAX);
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
    }

    public void UpdateHP(int curHP, int maxHP)
    {
        HPNowText.text = curHP + "";
        HPMaxText.text = maxHP + "";
    }

    public void UpdateName(string name)
    {
        NameText.text = name;
    }

    bool IsVisible()
    {
        if(Target == null || Target.activeSelf == false)
        {
            return false;
        }
        
        if(screenPos.z < 0)// || screenPos.x < 0 || screenPos.y < 0 || screenPos.x > Screen.width || screenPos.y > Screen.height)
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
