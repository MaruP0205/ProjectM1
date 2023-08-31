using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleOOP : MonoBehaviour
{
    public ZombieControl zombieControl_;
    public OgreControl ogreControl_;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Zombie: " + zombieControl_.damage);
        Debug.Log("Ogre: " + ogreControl_.damage);
        zombieControl_.Attack();
        ogreControl_.Attack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
