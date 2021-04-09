using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCharacterAnim : MonoBehaviour
{
    public Animator anim;
    public float Move;
    public bool Run;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        Move = Mathf.Lerp(Move, 0, Time.deltaTime);
    }
    // Update is called once per frame
    void LateUpdate()
    {
        anim.SetFloat("Move", Move);
        anim.SetBool("Run", Run);
    }

    public void MoveStateUpdate(float move, bool run)
    {
        Move = move;
        Run = run;
    }
}
