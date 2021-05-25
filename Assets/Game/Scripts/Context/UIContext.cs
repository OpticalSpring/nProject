using System.Collections;
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
    public HUDControl HUD;
    public void SubscribeEvent()
    {
        GameContext.Instance.RegisterObserver(GameEvent.GameEventType.CharacterShot, NameTagUpdate);
        GameContext.Instance.RegisterObserver(GameEvent.GameEventType.GameCountChange, CountUpdate);
    }


    public void RegisterUI(NameTag ui)
    {
        UINameTags.Add(ui);
    }

    public void RemoveUI(NameTag ui)
    {
        UINameTags.Remove(ui);
        Destroy(ui.gameObject);
    }

    public bool FindNameTag(int entityID, out NameTag ui)
    {
        ui = UINameTags.Find(x => x?.Target?.GetComponent<GameCharacter>().CharacterInfo.ID == entityID);
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
        CharacterShotEvent e = (CharacterShotEvent)data;
        GetNameTag(e.Target.ID)?.UpdateNameTag();
        var target = CharacterContext.Instance.GetGameCharacter(e.Target.ID);
        if (target != null && target.photonView.IsMine)
        {
            HUD.UpdateNameTag();
        }
        var caster = CharacterContext.Instance.GetGameCharacter(e.Caster.ID);
        if (caster != null && caster.photonView.IsMine)
        {
            HUD.UpdateAmmo();
        }
    }

    void CountUpdate(GameEvent data)
    {
        GameCountChangeEvent e = (GameCountChangeEvent) data;
        HUD.UpdateCount(e.Count);
    }
}
