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
    Vector3 SecondCamForward;
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            new CharacterJumpEvent(MyCharacter).Send();
        }

        // read inputs
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

       
        CamForward = Vector3.Scale(Cam.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveVector = v * CamForward + h * Cam.right;

        bool run = Input.GetKey(KeyCode.LeftShift);

        if (SecondMoveVector != moveVector || SecondRunBool != run)
        {
            SecondMoveVector = moveVector;
            SecondRunBool = run;
            new CharacterMoveEvent(MyCharacter, SecondMoveVector, run).Send();
        }

        if (Input.GetMouseButton(1) && SecondCamForward != CamForward)
        {
            SecondCamForward = CamForward;
            new CharacterAimEvent(MyCharacter, CamForward).Send();
        }

        if (Input.GetMouseButtonUp(1))
        {
            new CharacterAimOutEvent(MyCharacter).Send();
        }

        if (Input.GetMouseButton(1) && Input.GetMouseButtonDown(0))
        {
            GameObject target = GameCameraLogic.CheckObject(Cam.GetChild(0).gameObject);
            if (target?.GetComponent<GameCharacter>())
            {
                new CharacterFireEvent(MyCharacter, target.GetComponent<GameCharacter>(), 1).Send();
            }
            else
            {
                new CharacterFireEvent(MyCharacter, null, 1).Send();
            }
        }



    }
}
