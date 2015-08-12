using UnityEngine;
using System.Collections;

public class SetSize : MonoBehaviour {
    PlayerUnit pl;
	// Use this for initialization
	void Start () {
        pl = GameObject.FindWithTag("PlayerBody").GetComponent<PlayerUnit>();
        this.GetComponent<ParticleSystem>().startSize = pl.AuraRange*pl.rangeUpPotionScale/10;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
