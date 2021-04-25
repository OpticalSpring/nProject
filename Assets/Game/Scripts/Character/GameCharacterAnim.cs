using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCharacterAnim : MonoBehaviour
{
    public Animator anim;
    public float Move;
    float movement;
    Vector2 movementWalk8Way;
    public Vector2 Walk8Way;
    public bool Run;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        movement = Mathf.Lerp(movement, Move, Time.deltaTime * 10);
        movementWalk8Way = Vector2.Lerp(movementWalk8Way, Walk8Way, Time.deltaTime * 10);
    }
    // Update is called once per frame
    void LateUpdate()
    {
        anim.SetFloat("Move", movement);
        anim.SetFloat("XVelocity", movementWalk8Way.x);
        anim.SetFloat("YVelocity", movementWalk8Way.y);
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

    public void Walk8WayStateUpdate(Vector2 walk8Way)
    {
        Walk8Way = walk8Way;
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
