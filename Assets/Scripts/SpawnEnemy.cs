using System;
using System.Collections;
using System.Collections.Generic;
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
    public static Queue<GameObject> queueEnemy;

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
        if (!GameController.instance.reUse)
        {
            StartCoroutine(DontReUseSpawnEnemys());
        }
        else
        {
            StartCoroutine(ReUseSpawnEnemys());
        }
    }
    
    IEnumerator ReUseSpawnEnemys()
    {
        GameObject enemy = null;
        
        while (true)
        {
            if (GameController.queueEnemy.Count > 0)
            {
                int y = Random.Range(yMin, yMax);

                enemy = GameController.queueEnemy.Dequeue();
                enemy.transform.position = new Vector3(transform.position.x, y);
                enemy.transform.parent = GameController.instance.enemyStorage;
                enemy.gameObject.SetActive(true);
            }

            if (!endlessMode) yield return  new WaitForSeconds(interval);
            yield return  new WaitForFixedUpdate();
        }
    }

    IEnumerator DontReUseSpawnEnemys()
    {
        while (true)
        {
            int y = Random.Range(yMin, yMax);

            Instantiate(GameController.instance.massEnemyPrefab[Random.Range(0, massEnemyPrefab.Length)], 
                new Vector3(transform.position.x, y), Quaternion.identity).transform.parent = transform;
            
            if (!endlessMode) yield return  new WaitForSeconds(interval);
            yield return  new WaitForFixedUpdate();
        }
    }
}
