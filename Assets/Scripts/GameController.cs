using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    [HideInInspector] public bool reUse = true;

    public Transform bulletStorage;
    public Transform enemyStorage;

    public GameObject bulletPrefab;
    public GameObject[] massEnemyPrefab;
    public static Queue<GameObject> queueEnemy;
    public static Queue<GameObject> queueBullet;
    
    public SettingsBullet settingsBullet;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }

    public void ReUse()
    {
        reUse = true;
        
        queueEnemy = new Queue<GameObject>();
        queueBullet = new Queue<GameObject>();
        
        for (int i = 0; i < 2000; i++)
        {
            var obj = Instantiate(massEnemyPrefab[Random.Range(0, massEnemyPrefab.Length)],
                transform, true);
            queueEnemy.Enqueue(obj);
            obj.SetActive(false);
            
            obj = Instantiate(bulletPrefab, transform, true);
            queueBullet.Enqueue(obj);
            obj.SetActive(false);
        }
        
        SceneManager.LoadScene(1);
    }
    
    public void DontReUse()
    {
        reUse = false;
        SceneManager.LoadScene(1);
    }
}

[Serializable]
public class SettingsBullet
{
    public float speed = 10;
    public int damage = 1;

    public int Y = 50;
    public int X = 50;
}
