using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCharacterAnim : MonoBehaviour
{
    public Animator anim;
    public float Move;
    float movement;
    public bool Run;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        movement = Mathf.Lerp(movement, Move, Time.deltaTime * 10);
    }
    // Update is called once per frame
    void LateUpdate()
    {
        anim.SetFloat("Move", movement);
    }

    public void MoveStateUpdate(float move, bool run)
    {
        if (run)
        {
            Move = 1;
        }
        else
        {
            Move = 0.2f;
        }
        if(move == 0)
        {
            Move = 0;
        }
    }

    public void FireStateUpdate(bool aim)
    {
        anim.SetBool("Aim", aim);
    }

    public void Fire()
    {
        anim.SetTrigger("Fire");
    }
}
