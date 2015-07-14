using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {
	public float damage;				//데미지의 크기.
	public Vector3 knockbackVector;		//넉백의 크기.
	public float stunTime;				//기절시간. 기절동안에는 오오라가 사라진다
	public float snareTime;				//경직시간. 경직동안에는 오오라가 사라지지 않는다.
	public float speed;					//객체의 이동속력

	public virtual void pause(){}				//일시정지
	public virtual void resume(){}				//일시정지 해제
}
