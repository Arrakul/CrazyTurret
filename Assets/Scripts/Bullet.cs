using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int damage;

    void Start()
    {
        speed = GameController.Instance.settingsBullet.Speed;
        damage = GameController.Instance.settingsBullet.Damage;

        SettingsMove();
    }

    public void SettingsMove()
    {
        GetComponent<Rigidbody2D>().velocity = transform.forward * speed;
        var rotation = transform.rotation;
        rotation = new Quaternion(rotation.x, 0, rotation.z, rotation.w);

        transform.rotation = rotation;
    }
    
    private void FixedUpdate()
    {
        transform.Translate(new Vector2(1, 0) * (speed * Time.deltaTime), Space.World);
        
        if ((transform.position.x > GameController.Instance.settingsBullet.X || transform.position.x < -GameController.Instance.settingsBullet.X) 
            || (transform.position.y > GameController.Instance.settingsBullet.Y || transform.position.y < -GameController.Instance.settingsBullet.Y))
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
