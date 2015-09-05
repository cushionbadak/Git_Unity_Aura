using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EntityManager : SingleTonBehaviour<EntityManager>
{


    public GameObject EnemyCategory;
    public GameObject PlayerCategory;
    public GameObject AttackCategory;


    void Start()
    {
        FindCategory(EnemyCategory, "NewEntity/NewEnemies");
        FindCategory(PlayerCategory, "NewEntity/NewPlayers");
        FindCategory(AttackCategory, "NewEntity/NewAttacks");

    }

    public GameObject CreateEffect(GameObject prefab, Vector3 position, Vector3 angle)
    {
        GameObject result = null;

        result = GameObject.Instantiate(prefab, position, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;

        return result;
    }

    public GameObject CreateUnit(GameObject prefab, Vector3 position, string category)
    {

        GameObject result = null;


        switch (category)
        {
            case "Enemy":
                {
                    result = GameObject.Instantiate(prefab, position, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                    break;
                }
        }


        return result;
    }

    void FindCategory(GameObject obj, string name)
    {
        if (obj == null)
        {
            obj = GameObject.Find(name);
        }
    }


    public PauseAbleObject[] FindAllPausable()
    {
        return FindObjectsOfType<PauseAbleObject>();
    }

    public void DestroyEffect(GameObject effect)
    {
        Destroy(effect);
    }
}
