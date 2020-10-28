using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public bool reUse = true;

    public SettingsBullet settingsBullet;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
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

[Serializable]
public class SettingsBullet
{
    public int Speed = 10;
    public int Damage = 1;

    public int X = 50;
    public int Y = 50;
}
