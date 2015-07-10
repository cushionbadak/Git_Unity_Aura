using UnityEngine;
using System.Collections;

public class Buff{

	public float damageBuff;
	public float knockbackBuff;
	public float stunTimeBuff;	//기절시간. 기절동안에는 오오라가 사라진다
	public float snareTimeBuff;	//경직시간. 경직동안에는 오오라가 사라지지 않는다.
	public float speedBuff;

	public void pause(){}
	public void resume(){}
}
