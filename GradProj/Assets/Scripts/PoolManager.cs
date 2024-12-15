using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PrefabsCategory
{
    [SerializeField]
    private string categoryName;
    public List<GameObject> prefabs;
}
public class PoolManager : MonoBehaviour
{
    /// <summary> <item> <description> 0 = Enemy, </description> </item>
    /// <item> <description> 1 = Weapons, </description> </item>
    /// <item> <description> 2 = Drop Items </description> </item> </summary>
    public PrefabsCategory[] prefabsCategories;
    private List<GameObject>[][] _pools;

    void Awake()
    {
        InitPools();
    }

    void InitPools()
    {
        _pools = new List<GameObject>[prefabsCategories.Length][];

        for (int index = 0; index < _pools.Length; index++)
        {
            _pools[index] = new List<GameObject>[prefabsCategories[index].prefabs.Count];
            for (int jndex = 0; jndex < _pools[index].Length; jndex++)
            {
                _pools[index][jndex] = new List<GameObject>();
            }
        }
    }

    public GameObject Get(int categoryIndex, int prefabIndex)
    {
        GameObject select = null;

        foreach (GameObject target in _pools[categoryIndex][prefabIndex])
        {
            if(!target.activeSelf)
            {
                select = target;
                select.SetActive(true);
                break;
            }
        }

        if (select == null)
        {
            select = Instantiate(prefabsCategories[categoryIndex].prefabs[prefabIndex], transform);
            _pools[categoryIndex][prefabIndex].Add(select);
        }

        return select;
    }

}

