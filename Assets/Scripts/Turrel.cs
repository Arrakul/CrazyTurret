using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Turrel : MonoBehaviour
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
        StartCoroutine(SpawnBullet());
    }

    private void Update()
    {
        countEnemy = spawnEnemy.transform.childCount;
    }

    IEnumerator SpawnBullet()
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

            yield return  new WaitForSeconds(interval);
            yield return  new WaitForFixedUpdate();
        }
    }
}
