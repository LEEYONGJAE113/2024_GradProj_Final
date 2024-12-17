using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Time")]
    public bool isTimeGoing;
    public float currentGameTime;
    public float maxGameTime = 10 * 60f;

    [Header("# GameObject")]
    public PoolManager pool;
    public QuestManager questManager;
    public EnemySpawner enemySpawner;
    public WeaponManager weaponManager;
    public Upgrade upgradeUI;
    public Player player;
    public Result resultUI;
    public GameObject enemyCleaner;
    [Header("# Game Info")]
    public int inGameLevel;
    public int inGameKill;
    public int inGameGold;
    [SerializeField]
    private float _inGameCurrentHp;
    public float inGameCurrentHp
    {
        get => _inGameCurrentHp;
        set => _inGameCurrentHp = Mathf.Clamp(value, 0, playerMaxHP);
    }
    
    [Header("# Player")]
    public float playerMaxHP; // 나중에 스탯 반영해서 지정 미리 해두면 좋음
    public float playerMoveSpeed; // 나중에 스탯 반영해서 지정 미리 해두면 좋음

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void GameStart()
    {
        inGameCurrentHp = playerMaxHP;
        upgradeUI.Select(0); //temp
        questManager.GetQuest(1); //temp
        Resume();
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isTimeGoing = false;

        yield return new WaitForSeconds(0.5f);

        resultUI.gameObject.SetActive(true);
        resultUI.Lose();
        Stop();
    }

    public void GameClear()
    {
        StartCoroutine(GameClearRoutine());
    }

    IEnumerator GameClearRoutine()
    {
        isTimeGoing = false;
        enemyCleaner.transform.position = player.transform.position;
        enemyCleaner.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        resultUI.gameObject.SetActive(true);
        resultUI.Win();
        Stop();
    }

    void Update()
    {
        if (!isTimeGoing) { return; }
        
        currentGameTime += Time.deltaTime;

        if (currentGameTime > maxGameTime)
        { 
            currentGameTime = maxGameTime;
            GameClear();
        }
        if (questManager.currentQuest == QuestManager.Quests.Survive)
        {
            questManager.questProgress += Time.deltaTime;
        }
    }

    public void ClearQuest()
    {
        if (!isTimeGoing) { return; }
        upgradeUI.Show();
        questManager.GetQuest(++inGameLevel);
    }

    public void Stop()
    {
        isTimeGoing = false;
        Time.timeScale = 0;
    }
    public void Resume()
    {
        isTimeGoing = true;
        Time.timeScale = 1;
    }
}
