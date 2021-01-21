using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameTag : MonoBehaviour
{
    public PlayerCharacter player;
    public Text nameTag;
    GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        nameTag.text = player.playerName;
        cam = GameManager.instance.camObject.transform.GetChild(0).GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
            gameObject.transform.LookAt(cam.transform.position);
        
       
    }
}
