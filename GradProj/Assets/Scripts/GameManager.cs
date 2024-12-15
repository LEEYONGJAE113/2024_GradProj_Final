using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Time")]
    public bool isTimeGoing;
    public float currentGameTime;
    public float maxGameTime = 2 * 10f;

    [Header("# GameObject")]
    public PoolManager pool;
    public Player player;
    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        currentGameTime += Time.deltaTime;

        if (currentGameTime > maxGameTime) { currentGameTime = maxGameTime; }
    }
}
