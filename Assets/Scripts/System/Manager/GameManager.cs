using UnityEngine;
using System.Collections;

public class GameManager : SingleTonBehaviour<GameManager>
{
    enum state
    {
        IDLE,
        GAME, GAMEPAUSE,
        SCENARIO, SCENARIOPAUSE,
        END,
    }

    StateMachine<System.Object> mvStateMachine;

    PlayerUnit player = null;

    // Use this for initialization
    void Awake()
    {
        //initialize state machine
        mvStateMachine = new StateMachine<System.Object>();
        mvStateMachine.AddState(state.IDLE, () => { });
        mvStateMachine.AddState(state.GAME, () => { });
        mvStateMachine.AddState(state.GAMEPAUSE, () => { });
        mvStateMachine.AddState(state.SCENARIO, () => { });
        mvStateMachine.AddState(state.SCENARIOPAUSE, () => { });
        mvStateMachine.AddState(state.END, () => { });
        mvStateMachine.SetInitState(state.IDLE);
    }

    void Start()
    {

        var playerbody = GameObject.FindGameObjectWithTag("PlayerBody");
        player = playerbody.GetComponent<PlayerUnit>();
        if (player == null)
        {
            Debug.LogError(name + "No Player is Found");
            Application.Quit();
        }
    }

    // Update is called once per frame
    void Update()
    {
        mvStateMachine.UpdateState();
    }

    void LateUpdate()
    {
        mvStateMachine.LateUpdateState();
    }

    public void GiveAttackTo(UnitObject target, UnitObject from, AttackParameters attack)
    {
        //check validation
        if ((state)mvStateMachine.GetCurrentState() != state.GAME)
            return;


        // calculate damage
        if (attack.damage != 0)
        {
            Debug.Log(from.name + " attacks " + target.name + " : " + attack.damage);
            target.GiveDamage(attack.damage);
        }
        // give snare
        if (attack.isSnareNeed)
        {
            target.GiveSnare(attack.snareTime, attack.snareRate);
        }
        // give stun
        if (attack.isStunNeed)
        {
            target.GiveStun(attack.stunTime);
        }
        // give knockback
        if (attack.isKnockBackNeed)
        {
            target.GiveKnockBack(attack.knockbackVector);
        }
    }

    public void GameStart()
    {
        if ((state)mvStateMachine.GetCurrentState() != state.IDLE)
            return;

        mvStateMachine.ChangeState(state.GAME);
        player.Spawn();
    }

    public void OnMenuOn()
    {
        if ((state)mvStateMachine.GetCurrentState() == state.GAME)
        {
            mvStateMachine.ChangeState(state.GAMEPAUSE);

            // pause animation
            PauseAnimationAll();
            // pause object
            PauseObjectAll();
        }
        else if ((state)mvStateMachine.GetCurrentState() == state.SCENARIO)
        {
            mvStateMachine.ChangeState(state.SCENARIOPAUSE);

            // pause object
            PauseObjectAll();
        }

    }

    public void OnMenuOff()
    {
        if ((state)mvStateMachine.GetCurrentState() == state.GAMEPAUSE)
        {
            mvStateMachine.ChangeState(state.GAME);

            // pause animation
            ResumeAnimationAll();
            // pause object
            ResumeObjectAll();
        }
        else if ((state)mvStateMachine.GetCurrentState() == state.SCENARIOPAUSE)
        {
            mvStateMachine.ChangeState(state.SCENARIO);

            // pause object
            ResumeObjectAll();
        }
    }

    public void OnScenarioOn()
    {
        if ((state)mvStateMachine.GetCurrentState() == state.GAME)
        {
            mvStateMachine.ChangeState(state.SCENARIO);

            PauseObjectAll();
        }
        else if ((state)mvStateMachine.GetCurrentState() == state.GAMEPAUSE)
        {
            mvStateMachine.ChangeState(state.SCENARIOPAUSE);
        }
    }

    public void OnScenarioOff()
    {
        if ((state)mvStateMachine.GetCurrentState() == state.SCENARIO)
        {
            mvStateMachine.ChangeState(state.GAME);

            ResumeObjectAll();
        }
        else if ((state)mvStateMachine.GetCurrentState() == state.SCENARIOPAUSE)
        {
            mvStateMachine.ChangeState(state.GAMEPAUSE);
        }
    }

    public void OnUnitDied(GameObject owner, UnitObject unit)
    {
        if (unit.tag == "PlayerBody")
        {
            EndGame();
        }
        else if (unit.tag == "EnemyBody")
        {
            EXPIncrease(player, unit.Exp, unit.transform.position);
        }

        Destroy(owner);
    }

    private void PauseObjectAll()
    {
        var pausables = EntityManager.Inst.FindAllPausable();
        foreach (var obj in pausables)
        {
            obj.PauseObject();
        }
    }

    private void ResumeObjectAll()
    {
        var pausables = EntityManager.Inst.FindAllPausable();
        foreach (var obj in pausables)
        {
            obj.ResumeObject();
        }
    }

    private void PauseAnimationAll()
    {
        var pausables = EntityManager.Inst.FindAllPausable();
        foreach (var obj in pausables)
        {
            obj.PauseAnimation();
        }
    }

    private void ResumeAnimationAll()
    {
        var pausables = EntityManager.Inst.FindAllPausable();
        foreach (var obj in pausables)
        {
            obj.ResumeAnimation();
        }
    }

    private void EndGame()
    {
        Debug.LogError("End Game");
    }

    public void EXPIncrease(PlayerUnit player, int amount, Vector3 pos)
    {
        player.GiveExp(amount);

        EffectManager.Inst.CreateEffect(pos, EffectData.EffectName.EXP);
        //SystemMessageManager.I.addMessage("경험치를 획득했습니다. (+" + amount + ")");

        if (player.Exp >= PlayerData.Inst.LevelData[player.Level + 1].ExpRequired)
        {
            LevelUp(player);
        }
    }

    public void LevelUp(PlayerUnit player)
    {
        player.OnLevelUp();

        var levelData = PlayerData.Inst.LevelData[++player.Level];

        player.Exp = 0;
        player.HPMax = levelData.HPMax;
        player.HPCurrent += 40;
        if (player.HPCurrent > player.HPMax)
            player.HPCurrent = player.HPMax;
        player.Damage = levelData.Damage;

        EffectManager.Inst.CreateEffect(player.gameObject, EffectData.EffectName.EXP);
    }

    public PlayerUnit FindPlayer()
    {
        return player;
    }
}
