﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIContext : MonoBehaviour
{
    public static UIContext Instance;

    private void Awake()
    {
        Instance = this;
    }

    public List<NameTag> UINameTags;

    public void SubscribeEvent()
    {
        GameContext.Instance.RegisterObserver(GameEvent.GameEventType.CharacterDamage, NameTagUpdate);
    }


    public void RegisterUI(NameTag ui)
    {
        UINameTags.Add(ui);
    }

    public bool FindNameTag(int entityID, out NameTag ui)
    {
        ui = UINameTags.Find(x => x.Target.GetComponent<GameCharacter>().CharacterInfo.ID == entityID);
        return (ui != null);
    }

    public NameTag GetNameTag(int cid)
    {
        if (FindNameTag(cid, out NameTag ui))
        {
            return ui;
        }

        return null;
    }

    void NameTagUpdate(GameEvent data)
    {
        CharacterDamageEvent e = (CharacterDamageEvent)data;
        GetNameTag(e.Target.ID)?.UpdateNameTag();
    }
}
