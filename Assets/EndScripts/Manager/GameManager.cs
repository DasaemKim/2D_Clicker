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

    //public Weapon weapon;

    private Enemy enemy;

    public string savePath;

    private void Awake()
    {
        Instance = this;
        savePath = Path.Combine(Application.persistentDataPath, "save.json");
        Debug.Log(savePath);
    }

    public void NewGame()
    {
        player.Init();
    }
    public void LoadGame()
    {
        string json = File.ReadAllText(savePath);
        player.playerData = JsonUtility.FromJson<PlayerData>(json);

        Debug.Log(player.playerData);
    }

    public void SaveGame()
    {
        string json = JsonUtility.ToJson(player.playerData,true);
        File.WriteAllText(savePath, json);

        Debug.Log("저장");
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
