using UnityEngine;
using System.Collections;

public interface BuffInterface {
	float damageBuff	{ get; set;}
	float knockbackBuff	{ get; set;}
	float stunTimeBuff	{ get; set;}	//기절시간. 기절동안에는 오오라가 사라진다
	float snareTimeBuff	{ get; set;}	//경직시간. 경직동안에는 오오라가 사라지지 않는다.
	float speedBuff		{ get; set;}
}
