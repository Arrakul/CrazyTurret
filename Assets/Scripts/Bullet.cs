using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int damage;

    void Start()
    {
        speed = GameController.instance.settingsBullet.speed;
        damage = GameController.instance.settingsBullet.damage;
        
        GetComponent<Rigidbody2D>().velocity = transform.forward * Random.Range(speed, speed+7);
        var rotation = transform.rotation;
        rotation = new Quaternion(rotation.x, 0, rotation.z, rotation.w);
        transform.rotation = rotation;
    }

    private void Update()
    {
        if (transform.position.x > GameController.instance.settingsBullet.X || 
            transform.position.x < -GameController.instance.settingsBullet.X)
        {
            ReturnObjectForPool();
        }
        else if (transform.position.y > GameController.instance.settingsBullet.Y || 
                 transform.position.y < -GameController.instance.settingsBullet.Y)
        {
            ReturnObjectForPool();
        }
    }
    
    void HitObject(GameObject theObject)
    {
        theObject.GetComponent<Enemy>()?.TakeDamage(damage);
        ReturnObjectForPool();
    }
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        HitObject(collider.gameObject);
    }

    public void ReturnObjectForPool()
    {
        if (!GameController.instance.reUse)
        {
            Destroy(gameObject);
        }
        else
        {
            GameController.queueBullet.Enqueue(gameObject);
            gameObject.SetActive(false);
        }
    }
}
