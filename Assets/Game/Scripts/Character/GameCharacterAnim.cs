using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCharacterAnim : MonoBehaviour
{
    public Animator Anim;
    public GameObject RootBone;
    public float Move;
    float movement;
    Vector2 movementWalk8Way;
    public Vector2 Walk8Way;
    public bool Run;
    public float RootRotation;
    float rootRotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        movement = Mathf.Lerp(movement, Move, Time.deltaTime * 10);
        movementWalk8Way = Vector2.Lerp(movementWalk8Way, Walk8Way, Time.deltaTime * 10);
        rootRotation = Mathf.Lerp(rootRotation, RootRotation, Time.deltaTime * 10);
    }
    // Update is called once per frame
    void LateUpdate()
    {
        Anim.SetFloat("Move", movement);
        Anim.SetFloat("XVelocity", movementWalk8Way.x);
        Anim.SetFloat("YVelocity", movementWalk8Way.y);
       // Root();
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

    public void RootBoneUpdate(float root)
    {
        rootRotation = root;
    }

    public void FireStateUpdate(bool aim)
    {
        Anim.SetBool("Aim", aim);
    }

    public void Fire()
    {
        Anim.SetTrigger("Fire");
    }

    public void Root()
    {
        Vector3 newAngles = RootBone.transform.localEulerAngles;
        newAngles.x = rootRotation;
        RootBone.transform.localEulerAngles = newAngles;
    }
}
