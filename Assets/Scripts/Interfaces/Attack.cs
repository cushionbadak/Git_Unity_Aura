using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {
	public float damage;				//데미지의 크기.
	public Vector3 knockbackVector;		//넉백의 방향.
	public float stunTime;				//기절시간. 기절동안에는 오오라가 사라진다
	public float snareTime;				//경직시간. 경직동안에는 오오라가 사라지지 않는다.
	public float speed;					//객체의 이동속력
	public float knockbackForce;		//넉백 방향에 곱해줄 힘 크기 (상수)
	public bool isknockbackVectorNeed;	//true값이면 맞은쪽이 knockbackVector방향으로 
										//false값이면 knockbackVector 상관없이 position값을 빼서 knockback을 준다.

	public void pause(){}				//일시정지
	public void resume(){}				//일시정지 해제시 사용.
}
