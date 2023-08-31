using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataBinding : MonoBehaviour
{   
    public Animator animator;
    private int Key_X;
    private int Key_Z;
    private int Key_Speed;
    private int Key_Jump;
    private int Key_Ground;
    private Vector3 cur_Move;
    private Vector3 moveDir;
    public Vector3 MoveDir
    {
        set
        {
            moveDir = value;
        }
    }

    public float SpeedMove
    {
        set
        {
            animator.SetFloat(Key_Speed, value);
        }
    }
    public bool Jump
    {
        set
        {
            if (value)
            {
                animator.SetTrigger(Key_Jump);
            }
        }
    }

    public bool Ground
    {
        set
        {
            if (value)
            {
                animator.SetBool(Key_Ground,value);
            }
        }
    }
    public void Draw()
    {
        animator.Play("Draw", 2,0);
    }
    public void Fire()
    {
        animator.Play("Fire", 1, 0);
    }

    public void Reload()
    {
        animator.Play("Reload", 1, 0);
    }
    // Start is called before the first frame update
    void Start()
    {
        Key_X = Animator.StringToHash("x");
        Key_Z = Animator.StringToHash("z");
        Key_Speed = Animator.StringToHash("Speed");
        Key_Jump = Animator.StringToHash("Jump");
        Key_Ground = Animator.StringToHash("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        cur_Move = Vector3.Lerp(cur_Move, moveDir, Time.deltaTime * 5);
        animator.SetFloat(Key_X, cur_Move.x);
        animator.SetFloat(Key_Z, cur_Move.z);
    }
}
