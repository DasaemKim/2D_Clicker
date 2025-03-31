using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEditor.Experimental.RestService;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GoldManager goldManager;

    public Player player;


    private Enemy enemy;

    private string savePath;

    private void Awake()
    {
        Instance = this;
        savePath = Path.Combine(Application.persistentDataPath,"save.json");
        Debug.Log(savePath);
    }

    public void NewGame()
    {
        player.Init();
    }
    public void LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            player.playerData = JsonUtility.FromJson<PlayerData>(json);

        }
        else
        {
            NewGame();
        }
    }
    public void SaveGame()
    {
        string json = JsonUtility.ToJson(player.playerData,true);
        File.WriteAllText(savePath, json);
    }
    public Enemy Enemy
    {
        get { return enemy; }
        set { enemy = value; }
    }
    public void SetEquippedWeapon(string weaponName, int level)
    {
        player.playerData.equippedWeaponName = weaponName;
        player.playerData.equippedWeaponLevel = level;
        SaveGame();
    }
    public int GetEquippedWeaponLevel()
    {
        return player.playerData.equippedWeaponLevel;
    }
}
