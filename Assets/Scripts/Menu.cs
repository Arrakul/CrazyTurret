using UnityEngine;
using  UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static Menu instance;

    [HideInInspector] public bool reUse = true;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ReUse()
    {
        reUse = true;
        SceneManager.LoadScene(1);
    }
    
    public void DontReUse()
    {
        reUse = false;
        SceneManager.LoadScene(1);
    }
}
