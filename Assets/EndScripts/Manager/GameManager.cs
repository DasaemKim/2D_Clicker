using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEditor.Experimental.RestService;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerData playerData;
    public Stat stat;
    public GoldManager goldManager;
    public CharacterData characterData;

    private Enemy enemy;
    private EnemyStat enemyStat;

    public event Action OnUpdateEnemy;

    private string savePath;

    private void Awake()
    {
        Instance = this;
        savePath = Path.Combine(Application.persistentDataPath,"save.json");
    }

    public void NewGame()
    {
        playerData = new PlayerData()
        {
            stage = 1,
            gold = 0,
            criticalDamageLevel = 0,
            autoAttackLevel = 0,
            goldBonusLevel = 0,
            equippedWeaponLevel = 0,
            selectedCharacter = "default",
            equippedWeaponName = "³ª¹«°Ë"
        };
        SaveGame();
    }
    public void LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            playerData = JsonUtility.FromJson<PlayerData>(json);
        }
        else
        {
            NewGame();
        }
    }
    public void SaveGame()
    {
        string json = JsonUtility.ToJson(playerData,true);
        File.WriteAllText(savePath, json);
    }
    public Enemy Enemy
    {
        get { return enemy; }
        set { enemy = value; }
    }
    public void SetEquippedWeapon(string weaponName, int level)
    {
        playerData.equippedWeaponName = weaponName;
        playerData.equippedWeaponLevel = level;
        SaveGame();
    }
    public int GetEquippedWeaponLevel()
    {
        return playerData.equippedWeaponLevel;
    }
}
