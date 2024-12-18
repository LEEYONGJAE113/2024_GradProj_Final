using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponData
{
    public const int INFINITY_PER = -500; // 무한관통력
    public const float TOKEN_AND_PLAYER_GAP = 1.5f;
    public const float GCTRUCK_SPAWN_RADIUS = 10f;
}
public class Weapon : MonoBehaviour
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

        if (!IsMelee())
        {
            _rb.velocity = dir * fast;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == WeaponData.INFINITY_PER) { return; }

        per--;

        if (per < 0)
        {
            _rb.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area") || IsMelee()) { return; }
        gameObject.SetActive(false);
    }

    bool IsMelee()
    {
        if (gameObject.transform.parent != null && gameObject.transform.parent.name == "Weapon 0")
        { return true; }
        else
        { return false; }
    }
}
