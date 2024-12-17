using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D player;
    private bool _isLive;
    private Rigidbody2D _rb;
    private Collider2D _coll;
    private SpriteRenderer _spriter;
    private Animator _anim;
    private WaitForFixedUpdate _wait;
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _coll = GetComponent<Collider2D>();
        _spriter = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        _wait = new WaitForFixedUpdate();
    }
    void FixedUpdate()
    {
        if (!GameManager.instance.isTimeGoing) { return; }
        if (!_isLive || _anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) { return; }
        Vector2 dirVec = player.position - _rb.position;
        Vector2 nextVec = dirVec.normalized * moveSpeed * Time.fixedDeltaTime;
        _rb.MovePosition(_rb.position + nextVec);
        _rb.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        if (!_isLive || !GameManager.instance.isTimeGoing) { return; }
        _spriter.flipX = player.position.x < _rb.position.x;
    }

    void OnEnable()
    {
        player = GameManager.instance.player.GetComponent<Rigidbody2D>();
        SetLive(true);
        health = maxHealth;
    }

    public void Init(EnemyData data)
    {
        _anim.runtimeAnimatorController = animCon[data.spriteType];
        moveSpeed = data.moveSpeed;
        maxHealth = data.health;
        health = data.health;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Weapon") || !_isLive) { return; }
        health -= collision.GetComponent<WeaponData>().damage;
        StartCoroutine(Knockback());

        if (health > 0)
        {
            _anim.SetTrigger("Hit");
        }
        else
        {
            SetLive(false);
            GameManager.instance.inGameKill++;
            if (GameManager.instance.questManager.currentQuest == QuestManager.Quests.Kill)
            {
                GameManager.instance.questManager.questProgress++;
            }
        }
    }

    IEnumerator Knockback()
    {
        yield return _wait;
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        _rb.AddForce(dirVec.normalized * 2/*넉백사이즈*/, ForceMode2D.Impulse);
    }

    void Die()
    {
        gameObject.SetActive(false);
    }

    void SetLive(bool live)
    {
        _isLive = live;
        _coll.enabled = live;
        _rb.simulated = live;
        _spriter.sortingOrder = live ? 2 : 1;
        _anim.SetBool("Die", !live);
    }
}