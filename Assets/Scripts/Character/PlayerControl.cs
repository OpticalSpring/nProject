﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerControl : MonoBehaviourPun
{
    PlayerCharacter character;
    public Transform cam;                  
    public Vector3 camForward;
    Vector3 moveVector;
    
    // Start is called before the first frame update
    void Start()
    {
        if (Camera.main != null)
        {
            cam = Camera.main.transform.parent;
        }
        else
        {
            Debug.LogWarning("카메라가 없는데?", gameObject);
            
        }

       
        character = GetComponent<PlayerCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void FixedUpdate()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        // read inputs
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        if(Mathf.Abs(h) + Mathf.Abs(v) == 0)
        {
            return;
        }
        // calculate move direction to pass to character
        if (cam != null)
        {
            // calculate camera relative direction Kto move:
            camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
            moveVector = v * camForward + h *cam.right;
        }
        bool run = Input.GetKey(KeyCode.LeftShift); 

        
        character.CommentMovement(moveVector,run);
        
    }
}