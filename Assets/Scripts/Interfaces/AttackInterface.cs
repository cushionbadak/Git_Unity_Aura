using UnityEngine;
using System.Collections;

public interface AttackInterface {
	float damage	{ get; set;}
	Vector3 knockbackVector { get; set;}//넉백의 크기.
	float stunTime	{ get; set;}	//기절시간. 기절동안에는 오오라가 사라진다
	float snareTime	{ get; set;}	//경직시간. 경직동안에는 오오라가 사라지지 않는다.
	float speed		{ get; set;}	//객체의 이동속력.
}