using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsObjectPool : Singleton<AsteroidsObjectPool>
{
    [SerializeField] private ObjectPool mediumAsteroidsPools;

    private void Awake()
    {
        mediumAsteroidsPools.Initialize();
    }

    public GameObject GetAsteroid()
    {
        return mediumAsteroidsPools.GetObject();
    }

    public void ReturnAsteroid(GameObject obj)
    {
        mediumAsteroidsPools.ReturnToPool(obj);
    }
}
