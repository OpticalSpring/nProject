using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCharacterDriver : MonoBehaviourPunCallbacks
{
    GameCharacter character;
    public Transform cam;
    public Vector3 camForward;
    Vector3 moveVector;

    // Start is called before the first frame update
    void Start()
    {

        character = GetComponent<GameCharacter>();
    }




    void Update()
    {
        if (!photonView.IsMine)
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




    }
}
