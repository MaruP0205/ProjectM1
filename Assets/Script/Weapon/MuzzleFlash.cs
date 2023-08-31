using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    public Transform model;
    private float timeShow;
    public ParticleSystem particleSystem_;
    [SerializeField]
    private Transform root_aim;
    [SerializeField]
    private Transform aim;
    // Start is called before the first frame update
    void Start()
    {
        model.gameObject.SetActive(false);
    }
    public void Ready()
    {
        root_aim.SetParent(GameObject.FindObjectOfType<CharacterControl>().transform, true);
        root_aim.localScale = Vector3.one;

    }
    public void Hide()
    {
        root_aim.SetParent(transform, false);
        root_aim.localPosition = Vector3.zero;
        root_aim.localScale = Vector3.one;
        root_aim.localRotation = Quaternion.identity;

    }
    public Vector3 GetPosShoot()
    {
        return root_aim.position;
    }
    public Vector3 GetDirShoot()
    {
        return (aim.position - root_aim.position).normalized;
    }
    // Update is called once per frame
    void Update()
    {
        timeShow -= Time.deltaTime;
        if (timeShow <= 0) 
        {
            model.gameObject.SetActive(false);
        }
    }

    public void Fire()
    {
        timeShow = 0.05f;
        model.gameObject.SetActive(true);
        transform.localRotation = Quaternion.Euler(UnityEngine.Random.Range(-180,180),0,0);
        particleSystem_.Simulate(0);
        particleSystem_.Play();
    }
}
