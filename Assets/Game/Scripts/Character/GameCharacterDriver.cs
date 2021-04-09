using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCharacterDriver : MonoBehaviourPunCallbacks
{
    public GameCharacter character;
    public int ID;
    public Transform cam;
    public Vector3 camForward;
    Vector3 moveVector;

    




    void Update()
    {
        if (character == null)
        {
            return;
        }



        // read inputs
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (Mathf.Abs(h) + Mathf.Abs(v) == 0)
        {
            return;
        }
        // calculate move direction to pass to character
        if (cam != null)
        {
            // calculate camera relative direction Kto move:
            camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
            moveVector = v * camForward + h * cam.right;
        }
        bool run = Input.GetKey(KeyCode.LeftShift);

        new CharacterMoveEvent(character, moveVector, run).Send();



    }
}
