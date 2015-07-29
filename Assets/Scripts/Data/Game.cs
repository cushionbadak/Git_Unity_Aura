using UnityEngine;
using System.Collections;
[System.Serializable]
public class Game  {
    public static Game current;
    public float hp;
    public int level;
    public int exp;
    private WB_Vector3 _someVector3Info;
    public Vector3 playerPosition
    {
        get
        {
            if (_someVector3Info == null)
            {
                return Vector3.zero;
            }
            else
            {
                return (Vector3)_someVector3Info;
            }
        }
        set
        {
            _someVector3Info = (WB_Vector3)value;
        }
    }

}
