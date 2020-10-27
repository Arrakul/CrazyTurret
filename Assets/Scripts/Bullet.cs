using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int damage;

    void Start()
    {
        speed = 10;//GameController.instance.settingsBullet.speed;
        damage = 1;//GameController.instance.settingsBullet.damage;
        
        //GetComponent<Rigidbody2D>().velocity = transform.forward * Random.Range(speed, speed+7);
        SettingsMove();
    }

    public void SettingsMove()
    {
        GetComponent<Rigidbody2D>().velocity = transform.forward * Random.Range(speed, speed+7);
        var rotation = transform.rotation;
        rotation = new Quaternion(rotation.x, 0, rotation.z, rotation.w);

        transform.rotation = rotation;
    }

    private void Update()
    {
        if ((transform.position.x > 50 || transform.position.x < -50) 
            || (transform.position.y > 50 || transform.position.y < -50))
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
