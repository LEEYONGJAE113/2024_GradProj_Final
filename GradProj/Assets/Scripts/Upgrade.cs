using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    private RectTransform _rect;
    private Item[] _items;

    void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _items = GetComponentsInChildren<Item>(true);
    }

    public void Show()
    {
        Choice();
        _rect.localScale = Vector3.one;
        GameManager.instance.Stop();
    }
    public void Hide()
    {
        _rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
    }

    public void Select(int idx)
    {
        _items[idx].OnClick();
    }

    void Choice()
    {
        foreach (Item item in _items)
        {
            item.gameObject.SetActive(false);
        }

        int[] ranInt = new int[3];
        while (true)
        {
            ranInt[0] = Random.Range(0, _items.Length);
            ranInt[1] = Random.Range(0, _items.Length);
            ranInt[2] = Random.Range(0, _items.Length);

            if (ranInt[0] != ranInt[1] && ranInt[1] != ranInt[2] && ranInt[0] != ranInt[2]) { break; }
        }

        for (int idx = 0; idx < ranInt.Length; idx++)
        {
            Item ranItem = _items[ranInt[idx]];

            if (ranItem.itemLevel == ranItem.data.damages.Length)
            {
                _items[4].gameObject.SetActive(true); //temp
            }
            else
            {
                ranItem.gameObject.SetActive(true);
            }
        }
    }
}
