using UnityEngine;
using System.Collections;

public class NewEffectObject : PauseAbleObject {

	public float existingTime = 5.0f;

	public void SetExistingTime(float time) {
		existingTime = time;
	}

	IEnumerator DestroySelf() {
		IEnumerator timer = DelayedTimer.WaitForCustomDeltaTime(existingTime, GetDeltaTime);
		yield return StartCoroutine(timer);

		NewEffectManager.Inst.DestroyEffect(gameObject);
	}
}
