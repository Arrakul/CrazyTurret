using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    const float XMinMove = -30;
    
    public float speed;
    public int _staticPoint; 
    
    public Transform hitPointLine;
    private int hitPoints;

    private void Start()
    {
        speed = Random.Range(speed, speed * speed);
        hitPoints = _staticPoint;
    }

    public void RecoveryHP()
    {
        hitPoints = _staticPoint;
    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector2(-1, 0) * (speed * Time.deltaTime), Space.World);
        
        if (transform.position.x < XMinMove)
        {
            if (GameController.Instance.reUse)
            {
                gameObject.GetComponent<PoolObject>().ReturnToPool();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void TakeDamage(int amount)
    {
        hitPoints -= amount;

        if (hitPoints <= 0)
        {
            if (GameController.Instance.reUse)
            {
                if (hitPointLine != null)
                {
                    hitPointLine.localScale = new Vector3(1, 1, 1);
                    gameObject.GetComponent<PoolObject>().ReturnToPool();
                }
            }
            else
            {
                Destroy(gameObject);
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
