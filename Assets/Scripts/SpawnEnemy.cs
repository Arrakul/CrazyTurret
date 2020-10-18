using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEnemy : MonoBehaviour
{
    public static SpawnEnemy instance;
    
    public GameObject showStorage;
        
    public LowEnemy lowEnemy;
    public MidleEnemy midleEnemy;
    public HighEnemy highEnemy;

    public float interval;
    public int yMin;
    public int yMax;
    
    public bool endlessMode = false;

    public static Queue<LowEnemy> queueLowEnemy;
    public static Queue<MidleEnemy> queueMidleEnemy;
    public static Queue<HighEnemy> queueHighEnemy;

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
        if(!Menu.instance.reUse) StartCoroutine(DontReUseSpawnEnemys());
        else
        {
            queueLowEnemy = new Queue<LowEnemy>();
            queueMidleEnemy = new Queue<MidleEnemy>();
            queueHighEnemy = new Queue<HighEnemy>();
            
            for (int i = 0; i < 1000; i++)
            {
                var obj = Instantiate(lowEnemy, showStorage.transform, true);
                queueLowEnemy.Enqueue(obj);
                obj.gameObject.SetActive(false);

                if (i < 500)
                {
                    var obj2 = Instantiate(midleEnemy, showStorage.transform, true);
                    queueMidleEnemy.Enqueue(obj2);
                    obj2.gameObject.SetActive(false);
                    
                    var obj3 = Instantiate(highEnemy, showStorage.transform, true);
                    queueHighEnemy.Enqueue(obj3);
                    obj3.gameObject.SetActive(false);
                }
            }

            StartCoroutine(ReUseSpawnEnemys());
        }
    }
    
    IEnumerator ReUseSpawnEnemys()
    {
        GameObject enemy = null;
        
        while (true)
        {
            int y = Random.Range(yMin, yMax);
            
            /*Debug.Log("queueLowEnemy.Count : " + queueLowEnemy.Count);
            Debug.Log("queueMidleEnemy.Count : " + queueMidleEnemy.Count);
            Debug.Log("queueHighEnemy.Count : " + queueHighEnemy.Count);*/

            switch (Random.Range(0,10))
            {
                case 0:
                    if(queueMidleEnemy.Count > 0) enemy = queueMidleEnemy.Dequeue().gameObject;
                    break;
                
                case 9:
                    if(queueHighEnemy.Count > 0) enemy = queueHighEnemy.Dequeue().gameObject;
                    break;
                
                default: 
                    if(queueLowEnemy.Count > 0) enemy = queueLowEnemy.Dequeue().gameObject;
                    break;
            }
            
            enemy.transform.position = new Vector3(transform.position.x, y);
            enemy.transform.parent = transform;
            enemy.gameObject.SetActive(true);
            
            if (!endlessMode) yield return  new WaitForSeconds(interval);
            yield return  new WaitForFixedUpdate();
        }
    }

    IEnumerator DontReUseSpawnEnemys()
    {
        while (true)
        {
            int y = Random.Range(yMin, yMax);
            
            switch (Random.Range(0,10))
            {
                case 0:
                    Instantiate(midleEnemy, new Vector3(transform.position.x, y), Quaternion.identity).transform.parent = transform;
                    break;
                
                case 9:
                    Instantiate(highEnemy, new Vector3(transform.position.x, y), Quaternion.identity).transform.parent = transform;
                    break;
                default:
                    Instantiate(lowEnemy, new Vector3(transform.position.x, y), Quaternion.identity).transform.parent = transform;
                    break;
            }
            
            if (!endlessMode) yield return  new WaitForSeconds(interval);
            yield return  new WaitForFixedUpdate();
        }
    }
}
