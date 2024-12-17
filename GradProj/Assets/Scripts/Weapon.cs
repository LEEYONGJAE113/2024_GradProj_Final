using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponData : MonoBehaviour
{
    public float damage;
    public int per; // ==count
    public float coolTime;
    public float fast;
    private Rigidbody2D _rb;
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    public void Init(float damage, int per, Vector3 dir, float coolTime, float fast)
    {
        this.damage = damage;
        this.per = per;
        this.coolTime = coolTime;

        if (per >= 0)
        {
            _rb.velocity = dir * fast;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -500) { return; }

        per--;

        if (per < 0)
        {
            _rb.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area") || per == -500) { return; }
        gameObject.SetActive(false);
    }
}
