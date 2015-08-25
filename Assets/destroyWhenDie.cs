using UnityEngine;
using System.Collections;

public class destroyWhenDie : MonoBehaviour {
	SpriteRenderer s;
	// Use this for initialization
	void Start () {
		s = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (s.color.a == 0)
			Destroy (this.gameObject);
	}
}
