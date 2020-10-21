using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    const float XMinMove = -30;
    
    public float speed;
    public int hitPoints; 
    
    public Transform hitPointLine;
    private int _staticPoint;

    private void Start()
    {
        speed = Random.Range(speed, speed + 3);
        _staticPoint = hitPoints;
    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector2(-1, 0) * (speed * Time.deltaTime), Space.World);
        
        if (transform.position.x < XMinMove)
        {
            if (!GameController.instance.reUse)
            {
                Destroy(gameObject);
            }
            else
            {
                ReturnObjectForPool();
            }
        }
    }

    public void ReturnObjectForPool()
    {
        GameController.queueEnemy.Enqueue(gameObject);
        gameObject.SetActive(false);
    }

    public void TakeDamage(int amount)
    {
        hitPoints -= amount;

        if (hitPoints <= 0)
        {
            if (!GameController.instance.reUse) Destroy(gameObject);
            else
            {
                if (hitPointLine != null)
                {
                    hitPointLine.localScale = new Vector3(1, 1, 1);
                    ReturnObjectForPool();
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
        else if (hitPointLine != null)
        {
            float percent = (float) amount / _staticPoint;
            Vector3 scale = hitPointLine.localScale;
            scale.x -= percent;
            hitPointLine.localScale = scale;
        }
    }
}
