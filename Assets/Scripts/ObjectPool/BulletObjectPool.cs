using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : Singleton<BulletObjectPool>
{
    [SerializeField] private ObjectPool bulletPools;

    private void Awake()
    {
        bulletPools.Initialize();
    }

    public GameObject GetBullet()
    {
        return bulletPools.GetObject();
    }

    public void ReturnBullet(GameObject obj)
    {
        bulletPools.ReturnToPool(obj);
    }
}
