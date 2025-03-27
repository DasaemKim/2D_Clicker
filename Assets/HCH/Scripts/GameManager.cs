using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.RestService;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerData playerData;
    public Stat stat;
    public GoldManager goldManager;


    private void Awake()
    {
        Instance = this;
    }

    public void NewGame()
    {

    }
    public void LoadGame()
    {

    }
    public void SaveGame()
    {

    }
}
