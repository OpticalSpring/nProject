﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
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
        GameContext.Instance.RegisterObserver(GameEvent.GameEventType.CharacterAim, CharacterAim);
        GameContext.Instance.RegisterObserver(GameEvent.GameEventType.CharacterAimOut, CharacterAimOut);
    }

    public void InitCharacter(GameEvent data)
    {
        GameObject character = PhotonNetwork.Instantiate(
            CharacterPrefab.name,
            gameObject.transform.position,
            Quaternion.identity,
            0
        );
        
        CamObject.GetComponent<GameCharacterDriver>().Cam = CamObject.transform.GetChild(0);
        CamObject.GetComponent<GameCharacterDriver>().MyCharacter = character.GetComponent<GameCharacter>();
        CamObject.GetComponent<CameraControl>().camTarget = character;

    }

    public void RegisterCharacter(GameCharacter character)
    {
        GameCharacters.Add(character);
        if (character.photonView.IsMine) return;

        GameObject tag = Instantiate(NameTagPrefab);
        tag.GetComponent<NameTag>().Cam = CamObject.transform.GetChild(0).GetChild(0).GetComponent<Camera>();
        tag.GetComponent<NameTag>().Target = character.gameObject;
        tag.transform.parent = UITagGroup.transform;
    }

    public bool FindGameCharacter(int entityID, out GameCharacter character)
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
        GetSpotCharacter(e.Info.ID)?.MovementUpdate(e.Forward, e.Run);
        
    }

    void CharacterJump(GameEvent data)
    {
        CharacterJumpEvent e = (CharacterJumpEvent)data;
        GetSpotCharacter(e.Info.ID)?.Jump();

    }

    void CharacterFire(GameEvent data)
    {
        CharacterFireEvent e = (CharacterFireEvent)data;
        GetSpotCharacter(e.Caster.ID)?.Fire();
        GetSpotCharacter(e.Target.ID)?.GetDamage(e.Damage);

    }

    void CharacterAim(GameEvent data)
    {
        CharacterAimEvent e = (CharacterAimEvent)data;
        GetSpotCharacter(e.Info.ID)?.Aim(e.Forward);

    }

    void CharacterAimOut(GameEvent data)
    {
        CharacterAimOutEvent e = (CharacterAimOutEvent)data;
        GetSpotCharacter(e.Info.ID)?.AimOut();

    }

}
