using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private List<GameObject> _items = new List<GameObject>();

    [SerializeField] string prefix;

    [SerializeField] private GameObject _itemPrefab;

    private GameObject _tempPoolItem;

    public GameObject GetItem()
    {
        _tempPoolItem = _items.Find(x => !x.activeInHierarchy);

        if (!_tempPoolItem)
        {
            _tempPoolItem = InstantiatePrefab();
        }

        _tempPoolItem.SetActive(true);
        _tempPoolItem.GetComponent<IPoolItem>().Enable();

        return _tempPoolItem;
    }

    private GameObject InstantiatePrefab()
    {
        GameObject tempGO = Instantiate(_itemPrefab);
        tempGO.SetActive(false);
        tempGO.transform.name = $"{prefix}_{_items.Count}";
        tempGO.transform.SetParent(transform);
        _items.Add(tempGO);

        return tempGO;
    }
}