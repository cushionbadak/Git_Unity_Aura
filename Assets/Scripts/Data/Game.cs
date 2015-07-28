using UnityEngine;
using System.Collections;
[System.Serializable]
public class Game  {
    public static Game current;
    public float hp;
    public int level;
    public int exp;
    [System.NonSerialized]
    public Vector3 playerPosition;
    public float playerPositionX
    {
            get{return playerPosition.x;}
            set { playerPosition.x = value; }
    }
    public float playerPositionY
    {
            get{ return playerPosition.y;}
            set { playerPosition.y = value; }
    }

    public float playerPositionZ
    {
        get { return playerPosition.z; }
        set { playerPosition.z = value; }
    }
 
}
