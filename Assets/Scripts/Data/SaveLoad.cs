using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public static class SaveLoad {
    [SerializeField]
    public static List<Game> savedGames = new List<Game>();

    public static void Save()
    {
        PlayerUnit p=GameManager.I.findPlayer().gameObject.GetComponent<PlayerUnit>();
        Game.current.hp = p.currentHP;
        Game.current.exp = p.EXP;
        Game.current.level = p.level;
        Game.current.currentChapter = GameObject.Find("Managers").GetComponentInChildren<MapManager>().CurrentChapter;
        Game.current.roomStatus= GameObject.Find("Managers").GetComponentInChildren<MapManager>().getRoomStatus();
        Debug.Log(GameManager.I.findPlayer().gameObject.transform.position);
        Game.current.playerPosition = GameManager.I.findPlayer().gameObject.transform.position;
        savedGames.Add(Game.current);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
        Debug.Log(Application.persistentDataPath);
        bf.Serialize(file, SaveLoad.savedGames);
        file.Close();
    }

    public static void Load()
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

