using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class GameCharacter : MonoBehaviourPunCallbacks
{
    CharacterStatus currentStatus;
    public List<SkinnedMeshRenderer> SkinnedMeshes;

    void Init()
    {
        currentStatus = new CharacterStatus();
    }


}
