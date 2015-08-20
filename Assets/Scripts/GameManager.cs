using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;

struct Temp
{
    Player pl;

}

public class GameManager : MonoBehaviour
{
    public List<PlayerSkills.skillSet> skills;
    public GameObject DamageText;
    public MapManager mapM;
    bool isGameMode=false;
    public bool canSave = false;
    public PlayerUnit pl;
    public int index=0;
    public bool isPlayerLive=true;
    //Singleton
    private static GameManager uniqueInstance = null;
    public static GameManager I { get { return uniqueInstance; } }

    public bool getGameMode()
    {
        return isGameMode;
    }

    void Awake()
    {
        if (Game.current == null)
        {
            index = 0;
            SaveLoad.Init();
            
            GameObject player = GameObject.FindWithTag("PlayerBody");
            player.transform.parent.transform.position = new Vector3(0,0,0);
            player.GetComponent<PlayerUnit>().currentHP = 100;
            player.GetComponent<PlayerUnit>().EXP = 0;
            player.GetComponent<PlayerUnit>().level = 1;
            player.GetComponent<PlayerUnit>().damage = 2;
            skills.Add(PlayerSkills.skillSet.Knockback);
        }
        else
        {
            isGameMode = true;
            SaveLoad.LoadAll();
            //Load시 게임 초기화
            mapM.CurrentChapter = Game.current.currentChapter;
            switch (mapM.CurrentChapter)
            {
                case 1:
                    {
                        mapM.mapStatus = Game.current.roomStatus;
                        break;
                    }
            }
            GameObject player = GameObject.FindWithTag("PlayerBody");
            PlayerUnit pl = player.GetComponent<PlayerUnit>();
            Debug.Log(Game.current.playerPosition);
            player.transform.parent.transform.position = Game.current.playerPosition;
            player.GetComponent<PlayerUnit>().currentHP = Game.current.hp;
            player.GetComponent<PlayerUnit>().EXP = Game.current.exp;
            player.GetComponent<PlayerUnit>().level = Game.current.level;
            player.GetComponent<PlayerUnit>().damage = PlayerLevelData.I.Status[Game.current.level].damage;
            pl.powerUpPotion = Game.current.powerUpPotion;
            pl.speedUpPotion = Game.current.speedUpPotion;
            pl.rangeUpPotion = Game.current.rangeUpPotion;
            pl.powerUp(pl.powerUpPotion);
            pl.speedUp(pl.speedUpPotion);
            pl.rangeUp(pl.rangeUpPotion);
            skills = Game.current.skills;

            index = Game.current.dialogIndex;
            Debug.Log(index);
        }
    }

    public List<PlayerSkills.skillSet> getSkills()
    {
        return skills;
    }

    // Use this for initialization
    void Start()
    {
		
		Game.current = new Game();
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
        if (canSave)
        {
            SaveLoad.Save(i);
            SaveLoad.LoadAll();
            SystemMessageManager.I.addMessage("슬롯 " + i + "에 저장되었습니다.");
        }
        else
        {
            SystemMessageManager.I.addMessage("세이브 지역이 아닙니다. ");

        }
    }

    public void setGameMode(bool b)
    {
        isGameMode = b;
    }

    void Update()
    {
        if(!isGameMode)
        {
            if(Input.GetKeyDown(KeyCode.Alpha7))
            {
                ScriptsManager.I.GameModeOn();
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
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
        }
        if (isGameMode)
        {
            if(isPlayerLive==false)
            {
                Time.timeScale = 0.01f;
            }
            if (pl.currentHP <= 0)
            {
                isPlayerLive = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Save(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Save(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Save(2);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
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
                for (int i = 0; i < SaveLoad.savedGames.Count; i++)
                {
                    Debug.Log(i + "번째 슬롯의 pos : " + SaveLoad.savedGames[i].playerPosition);
                }
            }
            if(Input.GetKeyDown(KeyCode.V))
            {
                Debug.Log(skills);
            }

        }
    }

    

    IEnumerator createDamageText(GameObject obj,Attack attk)
    {
        GameObject dmgText = (GameObject)Instantiate(DamageText,obj.transform.position,Quaternion.Euler(90,0,0));
        int tempDmg = (int)attk.damage;
        string damage = tempDmg.ToString();
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
            Character enemy = objectThing.GetComponent<Character>();
            enemy.currentHP -= attk.damage;

            if (attk.damage > 0)
            {
                EffectManager.I.createAttackEffect(objectThing);
                StartCoroutine(createDamageText(objectThing, attk));
            }

            if (enemy.currentHP < 0)
                enemy.Die();
        }
    }

    public void giveStunToEnemy(GameObject enemy,float time)
    {
        enemy.GetComponent<Character>().giveStun(time);
    }

    public void giveSnareToEnemy(GameObject enemy, float time)
    {
        enemy.GetComponent<Character>().giveSnare(time);
    }

    public void giveKnockbackToEnemy(Vector3 force,GameObject enemy)
    {

        enemy.GetComponent<Character>().giveKnockback(force);
         EffectManager.I.createShortHitEffect(enemy);
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
    }

    public void giveStunToPlayer(float time)
    {
        GameObject player = findPlayer().gameObject;
        player.GetComponent<PlayerUnit>().giveStun(time);
    }

    public void giveSnareToPlayer(float time)
    {
        GameObject player = findPlayer().gameObject;
        player.GetComponent<PlayerUnit>().giveSnare(time);
    }

    //attackToPlayer과 같다.
    /*
    float tempHP = character.currentHP - attk.damage;
    character.currentHP = (tempHP <= 0) ? 0.0f : tempHP;
    if (tempHP <= 0)
        character.Die ();
    else {
        if(attk.knockbackVector.magnitude > 0.0f)
            character.giveKnockback(attk.knockbackVector);
        if(attk.stunTime > 0.0f)
            character.giveStun(attk.stunTime);
        else if(attk.snareTime > 0.0f)
            character.giveSnare(attk.snareTime);

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

        SystemMessageManager.I.addMessage("경험치를 획득했습니다. (+"+exp+")");

        player.EXPIncrease(exp);
        if (PlayerLevelData.I.Status[player.level+1].needEXP<=player.EXP)
        {
            SystemMessageManager.I.addMessage("레벨 업!");
            player.level++;
            player.EXP -= PlayerLevelData.I.Status[player.level].needEXP;
            player.maxHP = PlayerLevelData.I.Status[player.level].maxHP;
            player.currentHP += PlayerLevelData.I.Status[player.level].maxHP- PlayerLevelData.I.Status[player.level-1].maxHP;
            player.damage = PlayerLevelData.I.Status[player.level].damage;
            EffectManager.I.createLevelUpEffect(player.gameObject);
        }
    }


    

    public void givePlayerSlow(float time, float ratio)
    {

    }

    public void CreateGameObjectAfter(GameObject obj, Vector3 position, Vector3 roation, float time)
    {
        if (time < 0)
            return;

        StartCoroutine(CreateGameObjectTimer(obj, position, Quaternion.Euler(roation), time));
    }

    IEnumerator CreateGameObjectTimer(GameObject obj, Vector3 position, Quaternion rotation, float time)
    {
        yield return StartCoroutine(DelayedTimer.WaitForCustomDeltaTime(time, GetDeltaTime));

        GameObject.Instantiate(obj, position, rotation);
    }

    float GetDeltaTime()
    {
        //if (false)
        //    return 0;
        return Time.deltaTime;
    }
}