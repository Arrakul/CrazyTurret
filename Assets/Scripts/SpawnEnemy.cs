using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEnemy : MonoBehaviour
{
    public static SpawnEnemy instance;

    public float interval;
    public int yMin;
    public int yMax;
    
    public bool endlessMode = false;

    public GameObject[] massEnemyPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    void Start()
    {
        if (GameController.Instance.reUse)
        {
            StartCoroutine(ReUseSpawnEnemys());
        }
        else
        {
            StartCoroutine(DontReUseSpawnEnemys());
        }
    }
    
    IEnumerator ReUseSpawnEnemys()
    {
        while (true)
        {
            int y = Random.Range(yMin, yMax);
            int i = Random.Range(0, massEnemyPrefab.Length);
            var enemy = PoolManager.GetObject(massEnemyPrefab[i].name, new Vector3(transform.position.x, y), Quaternion.identity);
            enemy.GetComponent<Enemy>().RecoveryHP();
            
            if (!endlessMode) yield return  new WaitForSeconds(interval);
            yield return  new WaitForFixedUpdate();
        }
    }

    IEnumerator DontReUseSpawnEnemys()
    {
        while (true)
        {
            int y = Random.Range(yMin, yMax);

            Instantiate(massEnemyPrefab[Random.Range(0, massEnemyPrefab.Length)], 
                new Vector3(transform.position.x, y), Quaternion.identity).transform.parent = transform;
            
            if (!endlessMode) yield return  new WaitForSeconds(interval);
            yield return  new WaitForFixedUpdate();
        }
    }
}
