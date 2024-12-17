using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int itemLevel;
    public WeaponManager weaponManager;
    public Boost boost;
    private Image icon;
    private Text textLevel;
    private Text textName;
    private Text textDesc;

    void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1/*Icon*/];
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];

        textName.text = data.itemName;
    }

    void OnEnable()
    {
        textLevel.text = "Lv." + (itemLevel + 1);

        switch (data.itemType)
        {
            case ItemData.ItemType.Token:
            case ItemData.ItemType.Keycap:
                textDesc.text = string.Format(data.itemDesc, data.damages[itemLevel] * 100, data.counts[itemLevel], 100 - (data.coolTimes[itemLevel] * 100));
                break;
            case ItemData.ItemType.Zandi:
                textDesc.text = string.Format(data.itemDesc, 100 - (data.damages[itemLevel] * 100));
                break;
            case ItemData.ItemType.Hmchair:
                textDesc.text = string.Format(data.itemDesc, data.damages[itemLevel] * 100);
                break;
            default:
                textDesc.text = string.Format(data.itemDesc);
                break;
        }
    }


    public void OnClick()
    {
        switch (data.itemType)
        {
            case ItemData.ItemType.Token:
            case ItemData.ItemType.Keycap:
                if (itemLevel == 0)
                {
                    GameObject newWeaponManager = new GameObject();
                    weaponManager = newWeaponManager.AddComponent<WeaponManager>();
                    weaponManager.Init(data);
                }
                else
                {
                    float nextDamage = data.baseDamage;
                    nextDamage += data.baseDamage * data.damages[itemLevel];

                    int nextCount = 0;
                    nextCount += data.counts[itemLevel];

                    float nextCoolTime = data.baseCoolTime * data.coolTimes[itemLevel];

                    float fastRate = 1 + (itemLevel * 0.25f);
                    float nextFast = data.baseFast * fastRate;

                    weaponManager.LevelUp(nextDamage, nextCount, nextCoolTime, nextFast);
                }
                itemLevel++;
                break;
            case ItemData.ItemType.Zandi:
            case ItemData.ItemType.Hmchair:
                if(itemLevel == 0)
                {
                    GameObject newBoost = new GameObject();
                    boost = newBoost.AddComponent<Boost>();
                    boost.Init(data);
                }
                else
                {
                    float nextRate = data.damages[itemLevel];
                    boost.LevelUp(nextRate);
                }
                itemLevel++;
                break;
            case ItemData.ItemType.Coffee:
                GameManager.instance.inGameCurrentHp += GameManager.instance.playerMaxHP / 3;
                break;
        }


        if (itemLevel == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
