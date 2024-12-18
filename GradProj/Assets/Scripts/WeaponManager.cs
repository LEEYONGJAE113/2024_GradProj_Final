using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    /// <summary> <item> <description> 
    /// 0 = 보안토큰, 1 = 키캡, 2 = 가비지컬렉트럭
    /// </description> </item> </summary>
    public int id;
    public float damage;
    public int count;
    public float coolTime;
    public float fast;
    private Player _player;

    private float _timer;
    void Awake()
    {
        _player = GameManager.instance.player;
    }
    
    void Update()
    {
        if (!GameManager.instance.isTimeGoing) { return; }
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * fast * Time.deltaTime);
                break;
            case 1:
                _timer += Time.deltaTime;

                if (_timer > coolTime)
                {
                    _timer = 0f;
                    FireKeyCap();
                }
                break;
            case 2:
                _timer += Time.deltaTime;

                if (_timer > coolTime)
                {
                    _timer = 0f;
                    SpawnTruck();
                }
                break;
            default:
                break;
        }
    }

    public void LevelUp(float damage, int count, float coolTime, float fast)
    {
        this.damage = damage;
        this.count += count;
        this.coolTime = coolTime;
        this.fast = fast;

        if (id == 0) { PlaceToken(); }
        
        _player.BroadcastMessage("ApplyBoost", SendMessageOptions.DontRequireReceiver);
    }

    public void Init(ItemData data)
    {
        Setting(data);
        switch (id)
        {
            case 0:
                PlaceToken();
                break;
            default:
                break;
        }
        _player.BroadcastMessage("ApplyBoost", SendMessageOptions.DontRequireReceiver);
    }

    void Setting(ItemData data)
    {
        name = "Weapon " + data.itemId;
        transform.parent = _player.transform;
        transform.localPosition = Vector3.zero;

        id = data.itemId;
        damage = data.baseDamage;
        count = data.baseCount;
        coolTime = data.baseCoolTime;
        fast = data.baseFast;
    }

    void PlaceToken()
    {
        for (int idx = 0; idx < count; idx++)
        {
            Transform weapon;
            
            if (idx < transform.childCount)
            {
                weapon = transform.GetChild(idx);
            }
            else
            {
                weapon = GameManager.instance.pool.Get(1, id).transform;
                weapon.parent = transform;
            }

            weapon.localPosition = Vector3.zero;
            weapon.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * idx / count;
            weapon.Rotate(rotVec);
            weapon.Translate(weapon.up * WeaponData.TOKEN_AND_PLAYER_GAP, Space.World);
            weapon.GetComponent<Weapon>().Init(damage, WeaponData.INFINITY_PER, Vector3.zero, coolTime, fast);
        }
    }

    void FireKeyCap()
    {
        if(!_player.scanner.nearestTarget) { return; }

        Vector3 targetPos = _player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform weapon = GameManager.instance.pool.Get(1, id).transform;
        weapon.position = transform.position;
        weapon.rotation = Quaternion.FromToRotation(Vector3.right, dir);
        weapon.GetComponent<Weapon>().Init(damage, count, dir, coolTime, fast);
    }

    void SpawnTruck()
    {
        for (int idx = 0; idx < count; idx++)
        {
            float ranAngle = Random.Range(0f, 2 * Mathf.PI);

            Vector3 ranPoint = new Vector3(
                _player.transform.position.x + Mathf.Cos(ranAngle) * WeaponData.GCTRUCK_SPAWN_RADIUS,
                _player.transform.position.y + Mathf.Sin(ranAngle) * WeaponData.GCTRUCK_SPAWN_RADIUS,
                _player.transform.position.z
                );
            Vector3 dir = _player.transform.position - ranPoint;
            Transform weapon = GameManager.instance.pool.Get(1, id).transform;
            weapon.position = ranPoint;
            weapon.rotation = Quaternion.FromToRotation(Vector3.right, dir);
            weapon.GetComponent<Weapon>().Init(damage, WeaponData.INFINITY_PER, dir, coolTime, fast);
        }
    }
}
