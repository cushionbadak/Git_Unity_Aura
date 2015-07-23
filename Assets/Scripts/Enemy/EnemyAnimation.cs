using UnityEngine;
using System.Collections;

public class EnemyAnimation : MonoBehaviour {
    public string name_monster;//에디터 창에서 몬스터의 이름과 타입을 지정해 주어야 한다.
    public int type;
    AnimControl_Monster anim;
	// Use this for initialization
	void Start () {
        anim = new AnimControl_Monster(name_monster, type, this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void applyState(STATE_MONSTER st)
    {
      //  anim.UpdateAnim(st);
    }
}
