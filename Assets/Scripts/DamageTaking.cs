using System;
using UnityEngine;

public class DamageTaking : MonoBehaviour
{
    public Transform hitPointLine;
    public int hitPoints = 10; 
    
    private int staticPoint;

    private void Start()
    {
        staticPoint = hitPoints;
    }
    
    public void TakeDamage(int amount)
    {
        //Debug.Log(gameObject.name + " damaged!");
        hitPoints -= amount;

        if (hitPoints <= 0)
        {
            //Debug.Log(gameObject.name + " destroyed!");
            if(!Menu.instance.reUse) Destroy(gameObject);
            else
            {
                if (hitPointLine != null)
                {
                    hitPointLine.localScale = new Vector3(1, 1, 1);
                    gameObject.GetComponent<MoveEnemy>().ShowObject();
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
        else if (hitPointLine != null)
        {
            float percent = (float)amount / staticPoint;
            Vector3 scale = hitPointLine.localScale;
            scale.x -= percent;
            hitPointLine.localScale = scale;
        }
    }
}
