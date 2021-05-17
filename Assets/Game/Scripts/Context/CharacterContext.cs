using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CharacterContext : MonoBehaviourPunCallbacks
{
    public static CharacterContext Instance;

    private void Awake()
    {
        Instance = this;
    }
    public List<GameCharacter> GameCharacters;
    public GameObject CharacterPrefab;
    public GameObject CamObject;
    public GameObject UITagGroup;
    public GameObject NameTagPrefab;

    public void SubscribeEvent()
    {
        GameContext.Instance.RegisterObserver(GameEvent.GameEventType.InitCharacter, InitCharacter);
        GameContext.Instance.RegisterObserver(GameEvent.GameEventType.CharacterMove, CharacterMove);
        GameContext.Instance.RegisterObserver(GameEvent.GameEventType.CharacterJump, CharacterJump);
        GameContext.Instance.RegisterObserver(GameEvent.GameEventType.CharacterFire, CharacterFire);
        GameContext.Instance.RegisterObserver(GameEvent.GameEventType.CharacterShot, CharacterDamage);
        GameContext.Instance.RegisterObserver(GameEvent.GameEventType.CharacterAim, CharacterAim);
        GameContext.Instance.RegisterObserver(GameEvent.GameEventType.CharacterAimOut, CharacterAimOut);
        GameContext.Instance.RegisterObserver(GameEvent.GameEventType.CharacterTryConsume, CharacterTryConsume);
        GameContext.Instance.RegisterObserver(GameEvent.GameEventType.CharacterConsume, CharacterConsume);
    }

    public void InitCharacter(GameEvent data)
    {
        InitCharacterEvent e = (InitCharacterEvent)data;

        bool isSpy = (e.ID == PhotonNetwork.LocalPlayer.NickName);

            GameObject character = PhotonNetwork.Instantiate(
            Path.Combine("Game", CharacterPrefab.name),
            gameObject.transform.position + new Vector3(1,0,1) * Random.Range(-10,10),
            Quaternion.identity,
            0
        );
        
        if (isSpy)
        {
            character.GetComponent<GameCharacter>().CharacterInfo.IsSpy = true;
            IngameChatManager.Instance.SendNotifyMessage("You Are Spy", false);
        }
        else
        {
            IngameChatManager.Instance.SendNotifyMessage("You Are Human", false);
        }
        CamObject.GetComponent<GameCharacterDriver>().MyCharacter = character.GetComponent<GameCharacter>();
        CamObject.GetComponent<CameraControl>().CamTarget = character;

    }

    public void RegisterCharacter(GameCharacter character)
    {
        GameCharacters.Add(character);
        if (character.photonView.IsMine)
        {
            UIContext.Instance.HUD.Target = character.gameObject;
            UIContext.Instance.HUD.UpdateNameTag();
            UIContext.Instance.HUD.UpdateAmmo();
        }
        else
        {
            GameObject tag = Instantiate(NameTagPrefab);
            tag.GetComponent<NameTag>().Cam = CamObject.transform.GetChild(0).GetChild(0).GetComponent<Camera>();
            tag.GetComponent<NameTag>().Target = character.gameObject;
            tag.transform.parent = UITagGroup.transform;
        }
    }

    public void RemoveCharacter(GameCharacter character)
    {
        if (character.photonView.IsMine)
        {
            IngameChatManager.Instance.ChatLevel = 2;
        }
        GameCharacters.Remove(character);
        Destroy(character.gameObject);
    }

    bool FindGameCharacter(int entityID, out GameCharacter character)
    {
        character = GameCharacters.Find(x => x.CharacterInfo.ID == entityID);
        return (character != null);
    }

    public GameCharacter GetGameCharacter(int cid)
    {
        if (FindGameCharacter(cid, out GameCharacter character))
        {
            return character;
        }

        return null;
    }

    public int GetAlliveCount()
    {
        return GameCharacters.Count;
    }
    
    public GameCharacter GetAlliveGameCharacter(int index)
    {
        index = Mathf.Clamp(index, 0, GameCharacters.Count - 1);
        return GameCharacters[index];
    }


    void CharacterMove(GameEvent data)
    {
        CharacterMoveEvent e = (CharacterMoveEvent)data;
        GetGameCharacter(e.Caster.ID)?.MovementUpdate(e.Direction, e.Input, e.Run);

    }

    void CharacterJump(GameEvent data)
    {
        CharacterJumpEvent e = (CharacterJumpEvent)data;
        GetGameCharacter(e.Caster.ID)?.Jump();

    }

    void CharacterFire(GameEvent data)
    {
        CharacterFireEvent e = (CharacterFireEvent)data;
        GetGameCharacter(e.Caster.ID)?.Fire();
    }

    void CharacterDamage(GameEvent data)
    {
        CharacterShotEvent e = (CharacterShotEvent)data;
        GetGameCharacter(e.Target.ID)?.GetDamage(e.Caster, e.Damage);
    }

    void CharacterAim(GameEvent data)
    {
        CharacterAimEvent e = (CharacterAimEvent)data;
        GetGameCharacter(e.Caster.ID)?.Aim(e.Forward, e.RootRatation);

    }

    void CharacterAimOut(GameEvent data)
    {
        CharacterAimOutEvent e = (CharacterAimOutEvent)data;
        GetGameCharacter(e.Caster.ID)?.AimOut();

    }

    void CharacterTryConsume(GameEvent data)
    {
        CharacterTryConsumeEvent e = (CharacterTryConsumeEvent)data;
        GetGameCharacter(e.Caster.ID)?.TryConsume();

    }

    void CharacterConsume(GameEvent data)
    {
        CharacterConsumeEvent e = (CharacterConsumeEvent)data;
        GetGameCharacter(e.Caster.ID)?.ConsumeCaster(GetGameCharacter(e.Target.ID));
        GetGameCharacter(e.Target.ID)?.ConsumeTarget();
        new CharacterDeadEvent(e.Caster, e.Target, DeadCause.Consume).Send();
    }

    
}
