using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCharacterDriver : MonoBehaviourPunCallbacks
{
    public GameCharacter MyCharacter;
    public int ID;
    public Transform Cam;
    public Vector3 CamForward;
    Vector3 SecondMoveVector;
    bool SecondRunBool;


    void Update()
    {
        if (MyCharacter == null)
        {
            return;
        }
        if (Cam == null)
        {
            return;
        }


        // read inputs
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (Mathf.Abs(h) + Mathf.Abs(v) == 0)
        {
            //return;
        }
        // calculate move direction to pass to character

        // calculate camera relative direction Kto move:
        CamForward = Vector3.Scale(Cam.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveVector = v * CamForward + h * Cam.right;

        bool run = Input.GetKey(KeyCode.LeftShift);


        if (SecondMoveVector != moveVector || SecondRunBool != run)
        {
            SecondMoveVector = moveVector;
            SecondRunBool = run;
            new CharacterMoveEvent(MyCharacter, SecondMoveVector, run).Send();
        }



    }
}
