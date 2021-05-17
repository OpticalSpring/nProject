using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCharacterDriver : MonoBehaviourPunCallbacks
{
    public GameCharacter MyCharacter;
    public int ID;
    public Vector3 CamForward;
    Vector3 SecondCamForward;
    Vector3 SecondMoveVector;
    bool SecondRunBool;
    int spectatorIndex = 0;

    void Update()
    {
        if(GameProcess.Instance.gameProcess == GameProcess.GameProcessState.Start)
        {
            return;
        }

        if (MyCharacter == null)
        {
            if(CameraControl.Instance.CamTarget == null)
            {
                CameraControl.Instance.CamTarget = CharacterContext.Instance.GetAlliveGameCharacter(spectatorIndex).gameObject;
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                spectatorIndex--;
                if (spectatorIndex < 0)
                {
                    spectatorIndex = CharacterContext.Instance.GetAlliveCount() - 1;
                }
                CameraControl.Instance.CamTarget = CharacterContext.Instance.GetAlliveGameCharacter(spectatorIndex).gameObject;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                spectatorIndex++;
                if (spectatorIndex > CharacterContext.Instance.GetAlliveCount() - 1) 
                {
                    spectatorIndex = 0;
                }
                CameraControl.Instance.CamTarget = CharacterContext.Instance.GetAlliveGameCharacter(spectatorIndex).gameObject;
            }
            return;
        }


        // read inputs
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

       
        CamForward = Vector3.Scale(CameraControl.Instance.CamPivot.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveVector = v * CamForward + h * CameraControl.Instance.CamPivot.transform.right;

        bool run = Input.GetKey(KeyCode.LeftShift);


        if (Input.GetMouseButton(1) && SecondCamForward != CamForward)
        {
            SecondCamForward = CamForward;
            new CharacterAimEvent(MyCharacter.CharacterInfo, CamForward, CameraControl.Instance.CamPivot.transform.localEulerAngles.x).Send();
        }

        if (Input.GetMouseButtonUp(1))
        {
            new CharacterAimOutEvent(MyCharacter.CharacterInfo).Send();
        }

        if (Input.GetMouseButton(1) && Input.GetMouseButtonDown(0))
        {
            new CharacterFireEvent(MyCharacter.CharacterInfo).Send();
        }

        if(IngameChatManager.Instance.ChatEnabled == true)
        {
            moveVector = Vector3.zero;
            run = false;
        }

        if (SecondMoveVector != moveVector || SecondRunBool != run)
        {
            SecondMoveVector = moveVector;
            SecondRunBool = run;
            new CharacterMoveEvent(MyCharacter.CharacterInfo, moveVector, new Vector2(h,v), run).Send();
        }

        if (IngameChatManager.Instance.ChatEnabled == true)
        {
            return;
        }


        if (Input.GetKeyDown(KeyCode.C))
        {
            new CharacterTryConsumeEvent(MyCharacter.CharacterInfo).Send();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            new CharacterJumpEvent(MyCharacter.CharacterInfo).Send();
        }
    }
}
