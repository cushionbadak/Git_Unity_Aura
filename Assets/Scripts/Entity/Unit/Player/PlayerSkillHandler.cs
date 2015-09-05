using UnityEngine;
using System.Collections;

public class PlayerSkillHandler : MonoBehaviour
{
    public KeyCode[] skillKeys = new KeyCode[3];

    public PlayerUnit player = null;

    // Use this for initialization
    void Start()
    {
        if (player == null)
        {
            player = GetComponent<PlayerUnit>();
            if (player == null)
            {
                Debug.LogError(name + "I am Not Player!!!");
                Application.Quit();
            }
        }

        if (player.skill.Length != skillKeys.Length)
        {
            Debug.LogError(name + "Cannot Bind Skill to SkillKey. Size of Skill : " + player.skill.Length + "  Size of SkillKey : " + skillKeys.Length);
            Application.Quit();
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < skillKeys.Length; ++i)
        {
            if(Input.GetKeyDown(skillKeys[i]))
            {
                HandleSkill(player.skill[i]);
            }
        }
    }

    void HandleSkill(SkillData.SkillName skill)
    {
        switch(skill)
        {
            case SkillData.SkillName.InstallTower:
                {
                    break;
                }

            case SkillData.SkillName.Knockback:
                {
                    break;
                }

            case SkillData.SkillName.Laser:
                {
                    break;
                }

            case SkillData.SkillName.ShugokuOokiidesu:
                {
                    break;
                }

            case SkillData.SkillName.SpinningCross:
                {
                    break;
                }

            case SkillData.SkillName.Teleport:
                {
                    break;
                }

            case SkillData.SkillName.TripleShock:
                {
                    break;
                }

            case SkillData.SkillName.WindBitingSnowBall:
                {
                    break;
                }
        }
    }
}
