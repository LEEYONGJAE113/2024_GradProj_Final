using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float moveSpeed;//temp >> gamemanager
    private Rigidbody2D _rb;
    private SpriteRenderer _spriter;
    private Animator _anim;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriter = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }

    void OnMove(InputValue iValue)
    {
        inputVec = iValue.Get<Vector2>();
    }

    void FixedUpdate()
    {
        Vector2 nextVec = inputVec * moveSpeed * Time.fixedDeltaTime;
        Vector2 nextPos = _rb.position + nextVec;
        _rb.MovePosition(nextPos);
        _rb.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        _anim.SetFloat("Speed", inputVec.magnitude);
        if (inputVec.x != 0)
        {
            _spriter.flipX = inputVec.x < 0;
        }
    }
}
