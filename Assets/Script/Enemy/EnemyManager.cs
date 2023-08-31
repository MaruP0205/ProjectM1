using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<EnemyControl> enemies;
    public Transform posCreateEnemy;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void CreateEnemy()
    {
        int index = UnityEngine.Random.Range(0, enemies.Count);
        EnemyControl enemy = Instantiate(enemies[index]);
        Vector2 circle = UnityEngine.Random.insideUnitCircle;
        circle = circle * UnityEngine.Random.Range(1f, 5f);
        Vector3 pos = posCreateEnemy.position + new Vector3(circle.x, 0, circle.y);
        enemy.transform.position = pos;
        //enemy.Setup();
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3)
        {
            timer = 0;
            CreateEnemy();
        }
    }
}
