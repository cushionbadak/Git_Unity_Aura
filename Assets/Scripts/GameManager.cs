using UnityEngine;
using System.Collections;
using System.IO;
using System;

struct Temp
{
    Player pl;

}

public class GameManager : MonoBehaviour
{
    public GameObject DamageText;
    public MapManager mapM;
    //Singleton
    private static GameManager uniqueInstance = null;
    public static GameManager I { get { return uniqueInstance; } }

    void Awake()
    {
        if (Game.current == null)
        {
            SaveLoad.Init();
            Game.current = new Game();

        }
        else
        {
            SaveLoad.LoadAll();
            //Load시 게임 초기화
            mapM.CurrentChapter = Game.current.currentChapter;
            switch (mapM.CurrentChapter)
            {
                case 1:
                    {
                        mapM.chap1 = Game.current.roomStatus;
                        break;
                    }
            }
            GameObject player = GameManager.I.findPlayer().gameObject;
            player.transform.parent.transform.position = Game.current.playerPosition;
            player.GetComponent<PlayerUnit>().currentHP = Game.current.hp;
            player.GetComponent<PlayerUnit>().EXP = Game.current.exp;
            player.GetComponent<PlayerUnit>().level = Game.current.level;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        //Singleton
        if (uniqueInstance == null)
            uniqueInstance = this;
        else
            Destroy(this.gameObject);
    }

    public void Load(int i)
    {
        SaveLoad.LoadAll();
        Game.current = SaveLoad.savedGames[i];
        Time.timeScale = 1.0f;
       
        Application.LoadLevel(0);
    }

    public void Save(int i)
    {
        SaveLoad.Save(i-1);
        SaveLoad.LoadAll();
        Debug.Log("Slot " + i + " Saved");
    }

    void Update()
    {
        if(findPlayer().gameObject.GetComponent<PlayerUnit>().currentHP<0)
        {
            Time.timeScale = 0.01f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Save(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Save(1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            Save(2);
        }

        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            Load(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Load(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Load(2);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            SaveLoad.LoadAll();
            Debug.Log("Loaded");
            Debug.Log("Current Max Slot : " + SaveLoad.savedGames.Count);
            for(int i=0;i< SaveLoad.savedGames.Count;i++)
            {
                Debug.Log(i + "번째 슬롯의 pos : " + SaveLoad.savedGames[i].playerPosition);
            }
        }
    }

    public void attackToPlayer(Attack attk, GameObject objectThing)
    {
        //player가 효과를 받는 attack이 일어났을 때 일어날 일들 중
        //데미지, 넉백, 기절, 경직 정보를 읽어서 player에게 넘겨준다.
        //만약 데미지를 받은 뒤 플레이어가 죽을 체력이면 Die함수를 호출한다.
        //스턴과 경직 둘 다 있는 경우 스턴만 적용된다.
        if (objectThing.tag == "PlayerBody")
        {
            Player player = objectThing.GetComponent<Player>();
            player.currentHP -= attk.damage;

            if(attk.damage>0)
            {
                StartCoroutine(createDamageText(objectThing,attk));
                EffectManager.I.createAttackEffect(objectThing);
            }

            if (attk.isknockbackVectorNeed)
            {
                player.giveKnockback(attk.knockbackVector * attk.knockbackForce);
            }
            else
            {
                Vector3 temp = player.transform.position - attk.transform.position;
                temp.Normalize();
                player.giveKnockback(temp * attk.knockbackForce);
            }
            if (attk.stunTime > 0.0f)
                player.giveStun(attk.stunTime);
            else if (attk.snareTime > 0.0f)
            {
                StartCoroutine(applySnare(player, attk));
            }

        }
    }//attackToPlayer End.

    

    IEnumerator createDamageText(GameObject obj,Attack attk)
    {
        GameObject dmgText = (GameObject)Instantiate(DamageText,obj.transform.position,Quaternion.Euler(90,0,0));
        string damage = attk.damage.ToString();
        dmgText.GetComponent<TextMesh>().text = damage;
        if(obj.tag=="PlayerBody")
        {
            dmgText.GetComponent<TextMesh>().color = Color.yellow;
        }
        Destroy(dmgText, 1.0f);
        yield return null;
    }

    IEnumerator applySnare(Character character, Attack attk)
    {
        character.currentSpeed = 0;
        yield return new WaitForSeconds(attk.snareTime);
        character.currentSpeed = character.originalSpeed;
    }
    
    
    //오류있음 !!! 이 부분은 바뀌어야 한다.
    public void attckToEnemy(Attack attk, GameObject objectThing)
    {
        if (objectThing.tag == "EnemyBody")
        {
            Character character = objectThing.GetComponent<Character>();
            character.currentHP -= attk.damage;

            if (attk.damage > 0)
            {
                EffectManager.I.createAttackEffect(objectThing);
                StartCoroutine(createDamageText(objectThing, attk));
            }
            character.giveDamage(attk.damage);
            if (attk.knockbackForce > 0)
            {
                if (attk.isknockbackVectorNeed)
                {
                    character.giveKnockback(attk.knockbackVector * attk.knockbackForce);
                    EffectManager.I.createShortHitEffect(objectThing);
                }
                else
                {
                    EffectManager.I.createShortHitEffect(objectThing);
                    Vector3 temp = character.transform.position - attk.transform.position;
                    temp.Normalize();
                    // character.giveKnockback(temp * attk.knockbackForce);
                }
            }
            if (attk.stunTime > 0.0f)
                character.giveStun(attk.stunTime);
            else if (attk.snareTime > 0.0f)
                StartCoroutine(applySnare(character, attk));
        }
    }
    public void makeKnockbackEffect()
    {
        EffectManager.I.createKnockbackEffect(GameManager.I.findPlayer().gameObject);
    }
    public void attackToPlayer(Attack attk)
    {
        GameObject playerobject = GameObject.FindWithTag("PlayerBody");
        Player player = playerobject.GetComponent<PlayerUnit>();
        player.currentHP -= attk.damage;

        if (attk.damage > 0)
        {
            EffectManager.I.createAttackEffect(player.gameObject);
            StartCoroutine(createDamageText(player.gameObject, attk));
        }
        if (attk.isknockbackVectorNeed)
        {
            player.giveKnockback(attk.knockbackVector * attk.knockbackForce);
        }
        else
        {
            Vector3 temp = player.transform.position - attk.transform.position;
            temp.Normalize();
            player.giveKnockback(temp * attk.knockbackForce);
        }
        if (attk.stunTime > 0.0f)
            player.giveStun(attk.stunTime);
        else if (attk.snareTime > 0.0f)
            StartCoroutine(applySnare(player, attk));
    }

    //attackToPlayer과 같다.
    /*
    float tempHP = character.currentHP - attk.damage;
    character.currentHP = (tempHP <= 0) ? 0.0f : tempHP;
    if (tempHP <= 0)
        character.Die ();
    else {
        if(attk.knockbackVector.magnitude > 0.0f)
            character.haveKnockback(attk.knockbackVector);
        if(attk.stunTime > 0.0f)
            character.haveStun(attk.stunTime);
        else if(attk.snareTime > 0.0f)
            character.haveSnare(attk.snareTime);

    }
    */
    //attackToEnemy End.

    public void savePlayerInfo(int slot)
    {
        if (slot < 1 || slot > 3)
        {
            Debug.Log("TotalManager : savePlayerInfo : inappropriate slot number.");
            return;
        }
        Player player = findPlayer();
        string path = Application.dataPath + "/StreamingAssets/PlayerSlot" + slot.ToString() + ".dat";
        StreamWriter sw = new StreamWriter(path, false);
        //글을 적는 순서는 다음과 같다.
        //TotalManager class에서:
        //getCurrentScene
        //
        //Player class에서:
        //level, currentHP, originalSpeed,
        //powerUpPotion, speedUpPotion, rangeUpPotion,
        //powerUpPotionScale, speedUpPotionScale, rangeUpPotionScale,
        //EXP, 
        //isThunderShoes, isDraculaBrooch, isStickyBall, isCriticalKnuckle, isSpecialThing
        //이 순서는 아직 변경 가능성이 있으며, 특히 맵 관련해서는 더욱 변경될 것이다.
        sw.WriteLine(TotalManager.I.getCurrentScene());
        sw.WriteLine(player.level.ToString());
        sw.WriteLine(player.currentHP.ToString());
        sw.WriteLine(player.originalSpeed.ToString());
        sw.WriteLine(player.powerUpPotion.ToString());
        sw.WriteLine(player.speedUpPotion.ToString());
        sw.WriteLine(player.rangeUpPotion.ToString());
        sw.WriteLine(player.powerUpPotionScale.ToString());
        sw.WriteLine(player.speedUpPotionScale.ToString());
        sw.WriteLine(player.rangeUpPotionScale.ToString());
        sw.WriteLine(player.EXP.ToString());

        sw.WriteLine(player.isThunderShoes.ToString());
        sw.WriteLine(player.isDraculaBrooch.ToString());
        sw.WriteLine(player.isStickyBall.ToString());
        sw.WriteLine(player.isCriticalKnuckle.ToString());
        sw.WriteLine(player.isSpecialThing.ToString());

        sw.Close();
    }

    public Player getPlayerInfo(int slot, Player player)
    {
        //slot num이 1~3 이 아니면 에러가 날 가능성이 높습니다.
        string path = Application.dataPath + "/StreamingAssets/PlayerSlot" + slot.ToString() + ".dat";

        string[] trash = new string[1];
        StreamReader sr = new StreamReader(path);

        trash[0] = sr.ReadLine();                                   //1
        player.level = Convert.ToInt32(sr.ReadLine());              //2
        player.currentHP = Convert.ToSingle(sr.ReadLine());         //3
        player.originalSpeed = Convert.ToSingle(sr.ReadLine());     //4
        player.powerUpPotion = Convert.ToInt32(sr.ReadLine());      //5
        player.speedUpPotion = Convert.ToInt32(sr.ReadLine());      //6
        player.rangeUpPotion = Convert.ToInt32(sr.ReadLine());      //7
        player.powerUpPotionScale = Convert.ToSingle(sr.ReadLine());    //8
        player.speedUpPotionScale = Convert.ToSingle(sr.ReadLine());    //9
        player.rangeUpPotionScale = Convert.ToSingle(sr.ReadLine());    //10

        player.EXP = Convert.ToInt32(sr.ReadLine());                    //11
        player.isThunderShoes = Convert.ToBoolean(sr.ReadLine());       //12
        player.isDraculaBrooch = Convert.ToBoolean(sr.ReadLine());      //13
        player.isStickyBall = Convert.ToBoolean(sr.ReadLine());         //14
        player.isCriticalKnuckle = Convert.ToBoolean(sr.ReadLine());    //15
        player.isSpecialThing = Convert.ToBoolean(sr.ReadLine());       //16

        sr.Close();

        //파일에 들어있지 않은 값들은 이 밑에서 계산한다.
        player.maxHP = PlayerLevelData.I.Status[player.level].maxHP;
        //speedUpPotion에 따른 currentSpeed변화를 여기서 전부 계산한다.
        player.currentSpeed = player.originalSpeed * Convert.ToSingle(Math.Pow(Convert.ToDouble(player.rangeUpPotionScale), Convert.ToDouble(player.rangeUpPotion)));

        return player;
    }

    public Player findPlayer()
    {
        return GameObject.FindWithTag("PlayerBody").GetComponent<Player>();
    }

    public void EXPIncrease(int num,Vector3 pos) {
        int exp = num;
        Player player = findPlayer();
        EffectManager.I.createEXPEffect(pos);
        Debug.Log(exp);
        player.EXPIncrease(exp);
    }


    

    public void givePlayerSlow(float time, float ratio)
    {

    }
}