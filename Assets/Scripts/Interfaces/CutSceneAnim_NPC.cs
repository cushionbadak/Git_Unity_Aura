using UnityEngine;
using System.Collections;

public class CutSceneAnim_NPC : MonoBehaviour {
    public string name_monster;//에디터 창에서 몬스터의 이름과 타입을 지정해 주어야 한다.
    public int type;
    AnimControl_Monster anim;
    GameObject plDum;
    private Transform curTr;
    private Vector3 prevPos;
    // Use this for initialization
    void Start()
    {
        plDum = this.gameObject;
        anim = new AnimControl_Monster(name_monster, type, this.gameObject);
        curTr = plDum.transform;
        prevPos = curTr.position;
        GetComponent<NavMeshAgent>().updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {

        bool isMoving;

        if (curTr.position != prevPos)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (isMoving)
            anim.UpdateAnim(STATE_MONSTER.RUN);
        else
            anim.UpdateAnim(STATE_MONSTER.IDLE);

        prevPos = curTr.position;
    }
}
