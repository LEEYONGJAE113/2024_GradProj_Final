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
    private SpriteRenderer _spriter;
    private Animator _anim;
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriter = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        if (!_isLive ) { return; }
        Vector2 dirVec = player.position - _rb.position;
        Vector2 nextVec = dirVec.normalized * moveSpeed * Time.fixedDeltaTime;
        _rb.MovePosition(_rb.position + nextVec);
        _rb.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        if (!_isLive) { return; }
        _spriter.flipX = player.position.x < _rb.position.x;
    }

    void OnEnable()
    {
        player = GameManager.instance.player.GetComponent<Rigidbody2D>();
        _isLive = true;
        health = maxHealth;
    }

    public void Init(EnemyData data)
    {
        _anim.runtimeAnimatorController = animCon[data.spriteType];
        moveSpeed = data.moveSpeed;
        maxHealth = data.health;
        health = data.health;
    }
}