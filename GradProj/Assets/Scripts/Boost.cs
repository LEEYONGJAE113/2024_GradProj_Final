using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    public ItemData.ItemType type;
    public float rate;

    public void Init(ItemData data)
    {
        name = "Boost " + (data.itemId - 50);
        transform.parent = GameManager.instance.player.transform;
        transform.localPosition = Vector3.zero; // 혹시모르니까...

        type = data.itemType;
        rate = data.damages[0];
        ApplyBoost();
    }

    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyBoost();
    }

    void ApplyBoost()
    {
        switch (type)
        {
            case ItemData.ItemType.Zandi:
                CoolDown();
                break;
            case ItemData.ItemType.Hmchair:
                MoveSpeedUp();
                break;
        }
    }

    void CoolDown()
    {
        WeaponManager[] weaponManagers = transform.parent.GetComponentsInChildren<WeaponManager>();
        foreach (WeaponManager weaponManager in weaponManagers)
        {
            weaponManager.coolTime = weaponManager.coolTime * rate * 100;
        }
    }

    void MoveSpeedUp()
    {
        GameManager.instance.playerMoveSpeed += GameManager.instance.playerMoveSpeed * rate;
    }
}
