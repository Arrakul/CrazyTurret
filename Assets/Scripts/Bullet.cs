using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    public float speed = 10;
    
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.forward * Random.Range(speed, speed+7);
        var rotation = transform.rotation;
        rotation = new Quaternion(rotation.x, 0, rotation.z, rotation.w);
        transform.rotation = rotation;
    }

    private void Update()
    {
        if (transform.position.x > 50 || transform.position.x < -50)
        {
            Destroy(gameObject);
        }
        else if (transform.position.y > 50 || transform.position.y < -50)
        {
            Destroy(gameObject);
        }
    }
}
