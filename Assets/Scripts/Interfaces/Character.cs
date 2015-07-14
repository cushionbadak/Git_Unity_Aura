using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour{

	public float maxHP;		
	public float currentHP;
	public int level;
	public float originalSpeed;
	public float currentSpeed;

	public virtual void haveDamage(float damage){}
    public virtual void haveKnockback(Vector3 moveVector) { }
    public virtual void haveStun(float time) { }
    public virtual void haveSnare(float time) { }
    public virtual void Die() { }

    public virtual void pause() { }
    public virtual void resume() { }

}
