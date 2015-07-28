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
        Debug.Log(GameManager.I.findPlayer().gameObject.transform.position);
        Game.current.playerPosition.x = GameManager.I.findPlayer().gameObject.transform.position.x;

        Game.current.playerPosition.y = GameManager.I.findPlayer().gameObject.transform.position.y;

        Game.current.playerPosition.z = GameManager.I.findPlayer().gameObject.transform.position.z;
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

