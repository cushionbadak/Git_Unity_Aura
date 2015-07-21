using UnityEngine;
using System.Collections;

public class EnemyAnimation : MonoBehaviour {
    public string name_monster;
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
        anim.UpdateAnim(st);
    }
}
