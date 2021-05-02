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
        GameContext.Instance.RegisterObserver(GameEvent.GameEventType.CharacterDamage, CharacterDamage);
        GameContext.Instance.RegisterObserver(GameEvent.GameEventType.CharacterAim, CharacterAim);
        GameContext.Instance.RegisterObserver(GameEvent.GameEventType.CharacterAimOut, CharacterAimOut);
        GameContext.Instance.RegisterObserver(GameEvent.GameEventType.CharacterTryConsume, CharacterTryConsume);
        GameContext.Instance.RegisterObserver(GameEvent.GameEventType.CharacterConsume, CharacterConsume);
    }

    public void InitCharacter(GameEvent data)
    {
        GameObject character = PhotonNetwork.Instantiate(
            Path.Combine("Game", CharacterPrefab.name),
            gameObject.transform.position,
            Quaternion.identity,
            0
        );

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
        GameCharacters.Remove(character);
        Destroy(character.gameObject);
    }

    bool FindGameCharacter(int entityID, out GameCharacter character)
    {
        character = GameCharacters.Find(x => x.CharacterInfo.ID == entityID);
        return (character != null);
    }

    public GameCharacter GetSpotCharacter(int cid)
    {
        if (FindGameCharacter(cid, out GameCharacter character))
        {
            return character;
        }

        return null;
    }

    void CharacterMove(GameEvent data)
    {
        CharacterMoveEvent e = (CharacterMoveEvent)data;
        GetSpotCharacter(e.Caster.ID)?.MovementUpdate(e.Direction, e.Input, e.Run);

    }

    void CharacterJump(GameEvent data)
    {
        CharacterJumpEvent e = (CharacterJumpEvent)data;
        GetSpotCharacter(e.Caster.ID)?.Jump();

    }

    void CharacterFire(GameEvent data)
    {
        CharacterFireEvent e = (CharacterFireEvent)data;
        GetSpotCharacter(e.Caster.ID)?.Fire();
    }

    void CharacterDamage(GameEvent data)
    {
        CharacterDamageEvent e = (CharacterDamageEvent)data;
        GetSpotCharacter(e.Target.ID)?.GetDamage(e.Damage);
    }

    void CharacterAim(GameEvent data)
    {
        CharacterAimEvent e = (CharacterAimEvent)data;
        GetSpotCharacter(e.Caster.ID)?.Aim(e.Forward, e.RootRatation);

    }

    void CharacterAimOut(GameEvent data)
    {
        CharacterAimOutEvent e = (CharacterAimOutEvent)data;
        GetSpotCharacter(e.Caster.ID)?.AimOut();

    }

    void CharacterTryConsume(GameEvent data)
    {
        CharacterTryConsumeEvent e = (CharacterTryConsumeEvent)data;
        GetSpotCharacter(e.Caster.ID)?.TryConsume();

    }

    void CharacterConsume(GameEvent data)
    {
        CharacterConsumeEvent e = (CharacterConsumeEvent)data;
        GetSpotCharacter(e.Caster.ID)?.ConsumeCaster(GetSpotCharacter(e.Target.ID));
        GetSpotCharacter(e.Target.ID)?.ConsumeTarget();
    }
}
