using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public Scanner scanner;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriter;
    private Animator _anim;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriter = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
    }

    

    void FixedUpdate()
    {
        if (!GameManager.instance.isTimeGoing) { return; }

        Vector2 nextVec = inputVec * GameManager.instance.playerMoveSpeed * Time.fixedDeltaTime;
        Vector2 nextPos = _rb.position + nextVec;

        _rb.MovePosition(nextPos);

        if (GameManager.instance.questManager.currentQuest == QuestManager.Quests.Move)
        {
            float distance = Vector2.Distance(_rb.position, nextPos);
            GameManager.instance.questManager.questProgress += distance;
        }
        
        _rb.velocity = Vector2.zero;
    }
    void OnMove(InputValue iValue)
    {
        inputVec = iValue.Get<Vector2>();
    }
    void LateUpdate()
    {
        if (!GameManager.instance.isTimeGoing) { return; }
        
        _anim.SetFloat("Speed", inputVec.magnitude);
        if (inputVec.x != 0)
        {
            _spriter.flipX = inputVec.x < 0;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.isTimeGoing || !collision.transform.CompareTag("Enemy")) { return; }
        
        GameManager.instance.inGameCurrentHp -= Time.deltaTime * 10;

        if (GameManager.instance.inGameCurrentHp <= 0)
        {
            for (int idx = 2; idx < transform.childCount; idx++)
            {
                transform.GetChild(idx).gameObject.SetActive(false);
            }
            _anim.SetTrigger("Die");
            GameManager.instance.GameOver();
        }
    }
}
