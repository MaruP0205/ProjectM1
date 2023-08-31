using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static Vector3 add_move_dir;
    public static Vector3 move_dir;
   // public AudioSource audioSource_;
  //  public AudioClip sfx_run;

    public static bool isJump;
    public static bool isFire;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public static void AddMoveDir(Vector3 val)
    {
        add_move_dir = val;
    }
    public static void OnFire(bool isfire)
    {
        isFire = isfire;
    }
    // Update is called once per frame
    void Update()
    {
        move_dir = Vector3.zero;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        move_dir = new Vector3(x,0,z)+add_move_dir;
      //  audioSource_.PlayOneShot(sfx_run);
        /*
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            isJump = true;
        }else
        {
            isJump = false;
        }*/

        if (Input.GetKeyDown(KeyCode.F))
        {
            OnFire(true);
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            OnFire(false);
        }
        
    }
}
