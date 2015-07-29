using UnityEngine;
using System.Collections;

public class PlayerLevelData : MonoBehaviour
{
    //Singleton
    private static PlayerLevelData uniqueInstance = null;
    public static PlayerLevelData I { get { return uniqueInstance; } }

    //Data.
    //level, maxHP, attack, needexp, needexpsum 가 있다.
    public struct StatusStruct
    {
        public int level;
        public float maxHP, damage;
        public int needEXP, needEXPSum;

        public StatusStruct(int a, float b, float c, int d, int e)
        {
            level = a;
            maxHP = b;
            damage = c;
            needEXP = d;
            needEXPSum = e;
        }
    }

    public StatusStruct[] Status;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        //Singleton
        if (uniqueInstance == null)
            uniqueInstance = this;
        else
            Destroy(this.gameObject);

        //Datas.
        Status = new StatusStruct[31];
        Status[0] = new StatusStruct(0, 10000.0f, 10000.0f, 2, 3);
        Status[1] = new StatusStruct(1, 100.0f, 10.0f, 0, 0);
        Status[2] = new StatusStruct(2, 110.0f, 12.0f, 20, 20);
        for (int i = 3; i < 31; i++)
        {
            float tmaxHP, tdamage;
            int tneedEXP, tneedEXPSum;
            //tmaxHP
            if (i <= 10)
            {
                tmaxHP = 2 * Status[i - 1].maxHP - Status[i - 2].maxHP + 5;
            }
            else if (i <= 20)
            {
                tmaxHP = 2 * Status[i - 1].maxHP - Status[i - 2].maxHP + 10;
            }
            else
            {
                tmaxHP = 2 * Status[i - 1].maxHP - Status[i - 2].maxHP + 30;
            }
            //tdamage
            if (i <= 9)
            {
                tdamage = 2 * Status[i - 1].damage - Status[i - 2].damage + 1;
            }
            else if (i <= 19)
            {
                tdamage = 2 * Status[i - 1].damage - Status[i - 2].damage + 5;
            }
            else
            {
                tdamage = 2 * Status[i - 1].damage - Status[i - 2].damage + 12;
            }
            //tneedEXP
            if (i <= 10)
            {
                tneedEXP = 2 * Status[i - 1].needEXP - Status[i - 2].needEXP + 20;
            }
            else if (i <= 19)
            {
                tneedEXP = 2 * Status[i - 1].needEXP - Status[i - 2].needEXP + 40;
            }
            else
            {
                tneedEXP = 2 * Status[i - 1].needEXP - Status[i - 2].needEXP + 60;
            }
            //tneedEXPSum
            tneedEXPSum = 0;
            for (int j = 1; j < i; j++)
                tneedEXPSum += Status[j].needEXP;
            tneedEXPSum += tneedEXP;
            Status[i] = new StatusStruct(i, tmaxHP, tdamage, tneedEXP, tneedEXPSum);
        }
        //Datas End.

    }//Awake End.
}
