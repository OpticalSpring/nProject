using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NameTag : MonoBehaviour
{
    public GameObject Target;
    public Camera Cam;
    public TextMeshProUGUI NameText;

    private void Start()
    {
        NameText.text = Target.GetComponent<GameCharacter>().PlayerName;
    }

    private void Update()
    {
        if(Target == null)
        {
            gameObject.SetActive(false);
        }
        FollowTarget();
    }


    void FollowTarget()
    {
        Vector3 screenPos = Cam.WorldToScreenPoint(Target.transform.position + new Vector3(0, 2, 0));
        gameObject.transform.position = screenPos;
    }
}
