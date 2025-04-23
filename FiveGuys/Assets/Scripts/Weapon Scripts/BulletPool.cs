using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int poolSize = 20;

    private Queue<GameObject> bulletPool = new Queue<GameObject>();

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(bulletPrefab);
            obj.SetActive(false);
            bulletPool.Enqueue(obj);
        }
    }

    public GameObject GetObject()
    {
        if (bulletPool.Count > 0)
        {
            GameObject obj = bulletPool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(bulletPrefab);
            return obj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        bulletPool.Enqueue(obj);
    }
}
