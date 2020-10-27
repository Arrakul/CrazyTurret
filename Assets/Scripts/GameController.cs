using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public bool reUse = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
