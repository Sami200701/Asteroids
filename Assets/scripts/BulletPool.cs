using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool instance;
    public List<GameObject> bullets;
    public GameObject bullet;

    public int size;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        bullets = new List<GameObject>();
        GameObject newBullet;

        for (int i = 0; i < size; i++)
        {
            newBullet = Instantiate(bullet);
            newBullet.SetActive(false);
            bullets.Add(newBullet);
        }
    }

    public GameObject GetPooledBullet()
    {
        for (int i = 0; i < size; i++)
        {
            if (!bullets[i].activeInHierarchy)
            {
                return bullets[i];
            }
        }

        return null;
    }
    
}
