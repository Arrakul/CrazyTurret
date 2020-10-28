using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Turret : MonoBehaviour
{
    public SpawnEnemy spawnEnemy;
    public GameObject bulletStorage;
    public GameObject bulletPrefab;
    
    public float interval;
    
    public Transform[] firePoints;

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
                    var bullet = Instantiate(bulletPrefab, firePoints[i].position, firePoints[i].rotation);
                    bullet.transform.parent = bulletStorage.transform;
                    bullet.transform.LookAt(spawnEnemy.transform.GetChild(Random.Range(0, countEnemy)));
                }
            }

            yield return new WaitForSeconds(interval);
            yield return  new WaitForFixedUpdate();
        }
    }
    
    IEnumerator ReUseSpawnBullet()
    {
        while (true)
        {
            int index = Random.Range(0, spawnEnemy.massEnemyPrefab.Length);
            var target = PoolManager.GetActiveObject(spawnEnemy.massEnemyPrefab[index].name);

            for (int i = 0; i < firePoints.Length; i++)
            {
                var bullet = PoolManager.GetObject(bulletPrefab.name, firePoints[i].position, Quaternion.identity);

                bullet.transform.LookAt((target != null)? target.transform : spawnEnemy.transform);
                bullet.GetComponent<Bullet>().SettingsMove();
            }

            yield return new WaitForSeconds(interval);
        }
    }
}
