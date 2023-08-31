using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHub : MonoBehaviour
{
    public Image fg_image;
    [SerializeField]
    public GameObject objectDisplay;
    public RectTransform anchor;
    private RectTransform parent;
    private Transform anchor3D;
    private Camera cam;
    private float timeCount;
    private float val;
    private float cur_val;
    public void Setup(RectTransform parent, Transform anchor3D)
    {
        gameObject.SetActive(true);
        cur_val = val = 1;
        timeCount = 0;
        cam = Camera.main;
        this.parent = parent;
        this.anchor3D = anchor3D;
        anchor.SetParent(parent, false);
        anchor.anchoredPosition = Vector2.zero;
    }
    public void ShowEffect(int hp, int maxhp)
    {
        timeCount = 0.5f;
        val = (float) hp/ (float)maxhp;
    }
    // Start is called before the first frame update
    void Start()
    {
        timeCount -= Time.deltaTime;
        objectDisplay.gameObject.SetActive(timeCount > 0);
        cur_val = math.lerp(cur_val, val, Time.deltaTime * 2);
        fg_image.fillAmount = cur_val;
    }

    private void Update()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector3 worldPos = anchor3D.position;
        Vector2 screenPos = cam.WorldToScreenPoint(worldPos);
        Vector2 localUIPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parent, screenPos, null, out localUIPoint);
        anchor.anchoredPosition = localUIPoint;
    }
}
