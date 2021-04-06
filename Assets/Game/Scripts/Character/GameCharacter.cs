using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class GameCharacter : MonoBehaviourPunCallbacks
{
    public int ID;
    public int ColorNum;
    public Color PlayerColor;
    public string PlayerName;
    public CharacterStatus CurrentStatus;
    public List<SkinnedMeshRenderer> SkinnedMeshes;

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        CharacterContext.Instance.RigisterCharacter(this);
        ID = GetComponent<PhotonView>().ViewID;
        CurrentStatus = new CharacterStatus();

        PlayerName = photonView.Owner.NickName;
        int a = ColorManager.GetPlayerNameToCol(PlayerName);
        PlayerColor = ColorManager.NumToCol(a);
        SetPlayerColor(PlayerColor);
        PlayerName = ColorManager.SetPlayerNameToCol(PlayerName);
        gameObject.name = PlayerName;



        if (!photonView.IsMine)
        {
            return;
        }
        gameObject.layer = 2;
        IngameChatManager.instance.playerName = PlayerName;
        IngameChatManager.instance.playerColor = PlayerColor;
    }

    public void SetPlayerColor(Color color)
    {
        foreach (SkinnedMeshRenderer mesh in SkinnedMeshes)
        {
            mesh.material.color = color;
        }
    }
}
