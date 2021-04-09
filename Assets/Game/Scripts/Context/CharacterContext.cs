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
    public GameObject PlayerObject;
    public GameObject CamObject;


    public void SubscribeEvent()
    {
        GameContext.Instance.RegisterObserver(GameEvent.GameEventType.InitCharacter, InitCharacter);
        GameContext.Instance.RegisterObserver(GameEvent.GameEventType.CharacterMove, CharacterMove);
    }

    public void InitCharacter(GameEvent data)
    {
        GameObject character = PhotonNetwork.Instantiate(
            PlayerObject.name,
            gameObject.transform.position,
            Quaternion.identity,
            0
        );
        
        CamObject.GetComponent<GameCharacterDriver>().cam = CamObject.transform.GetChild(0);
        CamObject.GetComponent<GameCharacterDriver>().character = character.GetComponent<GameCharacter>();
        CamObject.GetComponent<CameraControl>().camTarget = character;

    }

    public void RigisterCharacter(GameCharacter character)
    {
        GameCharacters.Add(character);
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
        GetSpotCharacter(e.Info.ID)?.CommentMovement(e.Forwad, e.Run);
        
    }

    private void TextFunc(GameEvent eventData)
    {
        Debug.Log("TestGood");
    }
}
