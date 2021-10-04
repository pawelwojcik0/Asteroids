using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class ObjectPool
{
    public GameObject prefab;
    public int poolInitCount;
    public Transform poolTransform;

    private List<GameObject> pooledObjects = new List<GameObject>();

    public void Initialize()
    {
        for(int i = 0; i < poolInitCount; i++)
        {
            GameObject obj = GameObject.Instantiate(prefab, poolTransform);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public void Initialize(GameObject _prefab, int _InitCount)
    {
        prefab = _prefab;
        poolInitCount = _InitCount;
        Initialize();
    }

    public GameObject GetObject()
    {
        GameObject toReturn = pooledObjects.LastOrDefault();
        if (toReturn != null)
        {
            pooledObjects.Remove(toReturn);
            toReturn.SetActive(true);
            return toReturn;
        }
        else
        {
            toReturn = GameObject.Instantiate(prefab, poolTransform);
            toReturn.SetActive(true);
            return toReturn;
        }
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(poolTransform);
    }
}
