using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {
	public float damage = 0;				//데미지의 크기.
    public float speed = 0;					//객체의 이동속력
    
    public Vector3 knockbackVector = new Vector3(0, 0, 0);		//넉백의 크기.
    public float knockbackForce;		//넉백 방향에 곱해줄 힘 크기 (상수)
    public bool isknockbackVectorNeed;	//true면 맞는쪽만, false값이면 양쪽 모두 knockback을 준다.
    public float stunTime = 0;				//기절시간. 기절동안에는 오오라가 사라진다
    public float snareTime = 0;				//경직시간. 경직동안에는 오오라가 사라지지 않는다.

	public virtual void pause(){}				//일시정지
	public virtual void resume(){}				//일시정지 해제
}
