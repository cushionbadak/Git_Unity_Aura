using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public static class SaveLoad {
    [SerializeField]
    public static List<Game> savedGames = new List<Game>();
    public const int SLOTS=9;
    public static void Init()
    {
        savedGames.Clear();
        for(int i=0;i<SLOTS;i++)
        {
            savedGames.Add(null);
        }
    }

    public static void Save(int i)
    {
        if (i >= 0 && i < SLOTS)
        {
            PlayerUnit p = GameManager.I.findPlayer().gameObject.GetComponent<PlayerUnit>();
            Game.current.hp = p.currentHP;
            Game.current.exp = p.EXP;
            Game.current.level = p.level;
            Game.current.currentChapter = GameObject.Find("Managers").GetComponentInChildren<MapManager>().CurrentChapter;
            Game.current.roomStatus = GameObject.Find("Managers").GetComponentInChildren<MapManager>().getRoomStatus();
            Game.current.powerUpPotion = p.powerUpPotion;
            Game.current.rangeUpPotion = p.rangeUpPotion;
            Game.current.speedUpPotion = p.speedUpPotion;
            Game.current.playerPosition = GameManager.I.findPlayer().gameObject.transform.position;
            Game.current.dialogIndex = ScriptsManager.I.index;
            Game.current.skills = GameManager.I.skills;
            savedGames[i] = Game.current;
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
            bf.Serialize(file, SaveLoad.savedGames);

            file.Close();
        }
        else
        {
            Debug.Log("슬롯이 없습니다.");
        }
    }

    public static void LoadAll()
    {
        if(File.Exists(Application.persistentDataPath+"/savedGames.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            SaveLoad.savedGames = (List<Game>)bf.Deserialize(file);
            file.Close();
        }
    }
    

}

