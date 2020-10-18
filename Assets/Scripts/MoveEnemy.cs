using UnityEngine;
using Random = UnityEngine.Random;

public class MoveEnemy : MonoBehaviour
{
    public float xMin;
    public float speed;

    private void Start()
    {
        xMin = -30;
        speed = Random.Range(1, 3);
    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector2(-1, 0) * (speed * Time.deltaTime), Space.World);
        
        if (transform.position.x < xMin)
        {
            if (!Menu.instance.reUse) Destroy(gameObject);
            else
            {
                ShowObject();
            }
        }
    }

    public void ShowObject()
    {
        Enemy obj = gameObject.GetComponent<LowEnemy>();
        
        if (obj != null)
        {
            SpawnEnemy.queueLowEnemy.Enqueue((LowEnemy)obj);
        }
        else
        {
            obj = gameObject.GetComponent<MidleEnemy>();
            if (obj != null)
            {
                SpawnEnemy.queueMidleEnemy.Enqueue((MidleEnemy)obj);
            }
            else
            {
                obj = gameObject.GetComponent<HighEnemy>();
                if (obj != null)
                {
                    SpawnEnemy.queueHighEnemy.Enqueue((HighEnemy)obj);
                }
            }
        }

        transform.parent = SpawnEnemy.instance.showStorage.transform;
        obj.gameObject.SetActive(false);
    }
}
