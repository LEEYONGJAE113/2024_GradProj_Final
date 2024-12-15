using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public EnemyData[] enemyData;
    private float timer;
    private int level;
    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.currentGameTime / 10f), enemyData.Length - 1);
        

        if (timer > 0.2f)
        {
            timer = 0f;
            Spawn();
        }
    }
    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0, 0);
        enemy.transform.position = spawnPoint[Random.Range(1/*point start*/, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(enemyData[level]);
    }
}

[System.Serializable]
public class EnemyData
{
    public float spawnTime;
    public int spriteType;
    public float health;
    public float moveSpeed;
}