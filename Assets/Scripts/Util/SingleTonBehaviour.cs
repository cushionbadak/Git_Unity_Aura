using UnityEngine;
using System.Collections;

public class SingleTonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{

    public static T Inst
    {
        get
        {

            if (inst == null)
            {
                inst = (T)FindObjectOfType(typeof(T));

                if (inst != null)
                {
                    inst.gameObject.name = typeof(T).ToString();
                }
                else
                {
                    var managerCategory = GameObject.Find("Managers");
                    var newObject = new GameObject();
                    newObject.name = typeof(T).ToString();
                    inst = newObject.AddComponent<T>();

                    DontDestroyOnLoad(newObject);
                }
            }
            return inst;
        }
    }

    private static T inst;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
