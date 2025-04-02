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

    public string savePath;

    private void Awake()
    {
        Instance = this;
        savePath = Path.Combine(Application.persistentDataPath, "save.json");
    }

    //새게임 시작시 초기화
    public void NewGame()
    {
        player.Init();
    }

    //저장된 게임 불러오기
    public void LoadGame()
    {
        string json = File.ReadAllText(savePath);
        player.playerData = JsonUtility.FromJson<PlayerData>(json);
        UIBtnManager.Instance.uiBtnController.RefreshUI();
    }
    
    //현재 게임상태 저장
    public void SaveGame()
    {
        string json = JsonUtility.ToJson(player.playerData,true);
        File.WriteAllText(savePath, json);
    }

    //현재 적 참조 관리
    public Enemy Enemy
    {
        get { return enemy; }
        set { enemy = value; }
    }

    //장착 무기 설정 및 저장
    public void SetEquippedWeapon(string weaponName, int level)
    {
        player.playerData.equippedWeaponName = weaponName;
        player.playerData.equippedWeaponLevel = level;
        SaveGame();
    }

    //장착 무기 레벨 반환
    public int GetEquippedWeaponLevel()
    {
        return player.playerData.equippedWeaponLevel;
    }
}
