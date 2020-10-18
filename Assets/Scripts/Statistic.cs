using UnityEngine;
using UnityEngine.UI;

public class Statistic : MonoBehaviour
{
    public Text bulletText;
    public Text enemyText;

    public GameObject spawnEnemy;
    public GameObject bulletStorage;

    void Update()
    {
        bulletText.text = "bullet : " + bulletStorage.transform.childCount.ToString();
        enemyText.text = "enemy : " + spawnEnemy.transform.childCount.ToString();
    }
}
