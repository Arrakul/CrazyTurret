using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Turret : MonoBehaviour
{
    public GameObject spawnEnemy;
    public GameObject bulletPrefab;
    public GameObject bulletStorage;

    public Transform firePoint1;
    public Transform firePoint2;
    
    public float interval;

    private int countEnemy;
    
    void Start()
    {
        if (!GameController.instance.reUse)
        {
            StartCoroutine(DontReUseSpawnBullet());
        }
        else
        {
            StartCoroutine(ReUseSpawnBullet());
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
                var bullet = Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);
                bullet.transform.parent = bulletStorage.transform;
                bullet.transform.LookAt(spawnEnemy.transform.GetChild(Random.Range(0, countEnemy)));
                
                var bullet2 = Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
                bullet2.transform.parent = bulletStorage.transform;
                bullet2.transform.LookAt(spawnEnemy.transform.GetChild(Random.Range(0, countEnemy)));
            }

            yield return new WaitForSeconds(interval);
            yield return new WaitForFixedUpdate();
        }
    }
    
    IEnumerator ReUseSpawnBullet()
    {
        while (true)
        {
            var childCount = GameController.instance.enemyStorage.childCount;
            
            if (GameController.queueBullet.Count > 0 && childCount > 0)
            {
                ToTarget(childCount, firePoint1);
                ToTarget(childCount, firePoint2);
            }

            yield return new WaitForSeconds(interval);
            yield return new WaitForFixedUpdate();
        }
    }
    
    private void ToTarget (int childCount, Transform firePoint)
    {
        var bullet = GameController.queueBullet.Dequeue();
        bullet.transform.position = firePoint.position;
        bullet.transform.parent = GameController.instance.bulletStorage;
        bullet.transform.LookAt(GameController.instance.enemyStorage.GetChild(Random.Range(0, childCount)));
        bullet.SetActive(true);
    }
}
