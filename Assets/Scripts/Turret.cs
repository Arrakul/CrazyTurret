using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Turret : MonoBehaviour
{
    public SpawnEnemy spawnEnemy;
    public GameObject bulletStorage;
    public GameObject bulletPrefab;
    
    public Transform[] firePoints;
    
    public float interval;

    private int countEnemy;
    
    void Start()
    {
        if (GameController.Instance.reUse)
        {
            StartCoroutine(ReUseSpawnBullet());
        }
        else
        {
            StartCoroutine(DontReUseSpawnBullet());
        }
    }

    private void Update()
    {
        countEnemy = spawnEnemy.transform.childCount;
    }

    IEnumerator DontReUseSpawnBullet()
    {
        while (true)
        {
            if (countEnemy > 0)
            {
                for (int i = 0; i < firePoints.Length; i++)
                {
                    ToTarget(firePoints[i]);
                }
            }

            yield return new WaitForSeconds(interval);
            yield return  new WaitForFixedUpdate();
        }
    }

    void ToTarget(Transform point)
    {
        var bullet = Instantiate(bulletPrefab, point.position, point.rotation);
        bullet.transform.parent = bulletStorage.transform;
        bullet.transform.LookAt(spawnEnemy.transform.GetChild(Random.Range(0, countEnemy)));
    }
    
    IEnumerator ReUseSpawnBullet()
    {
        while (true)
        {
            int index = Random.Range(0, spawnEnemy.massEnemyPrefab.Length);

            for (int i = 0; i < firePoints.Length; i++)
            {
                var target = PoolManager.GetActiveObject(spawnEnemy.massEnemyPrefab[index].name);
                var bullet = PoolManager.GetObject(bulletPrefab.name, firePoints[i].position, Quaternion.identity);
                
                //bullet.transform.LookAt(spawnEnemy.transform);
                //bullet.GetComponent<Bullet>().targer = target.transform;
                bullet.transform.LookAt((target != null)? target.transform : spawnEnemy.transform);
                bullet.GetComponent<Bullet>().SettingsMove();
            }

            yield return new WaitForSeconds(interval);
        }
    }
}
