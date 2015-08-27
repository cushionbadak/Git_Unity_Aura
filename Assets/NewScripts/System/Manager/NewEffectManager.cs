using UnityEngine;
using System.Collections;

public class NewEffectManager : SingleTonBehaviour<NewEffectManager> {

	public GameObject attackEffect;
	public GameObject expEffect;
	public GameObject knockback;
	public GameObject hit_short;
	public GameObject hit_long;
	public GameObject EXP;
	public GameObject Fire;
	public GameObject LightBombEffect;
	public GameObject LevelUpEffect;
	public GameObject PressT;
	public GameObject ShockWaveEffect;
	public GameObject DustCloudWithImpactEffect;
	public GameObject DustCloudEffect;
	public GameObject MonsterSummonEffect;
	public GameObject FireBombEffect;
	public GameObject ThunderShoesEffect;
	public GameObject BossThunderEffect;
	public GameObject RedHealEffect;
	public GameObject TeleportEffect;
	public GameObject VortexEffect;
	public GameObject Threehit_SmallEffect;
	public GameObject Threehit_LargeEffect;
	public GameObject CrosshitEffect;


	// Use this for initialization
	void Start () {
//////////////////NEED : NEED to initialize function map to initialize effecs
	}

	public void CreateEffect(GameObject reference, NewEffectData.EffectName effect) {
		CreateEffect(reference, effect, 5.0f);
	}

	public GameObject CreateEffect(Vector3 position, NewEffectData.EffectName effect) {
		GameObject eff = null;

		switch (effect) {
			case NewEffectData.EffectName.LONG_HIT: {
					eff = NewEntityManager.Inst.CreateEffect(hit_long, position, new Vector3(0, 0, 0));
					break;
				}
			case NewEffectData.EffectName.EXP: {
					eff = NewEntityManager.Inst.CreateEffect(expEffect, position, new Vector3(0, 0, 0));
					break;
				}
		}

		return eff;
	}

	public GameObject CreateEffect(GameObject reference, NewEffectData.EffectName effect, float time) {
		GameObject eff = null;


		switch (effect) {
			case NewEffectData.EffectName.ATTACK: {
					eff = NewEntityManager.Inst.CreateEffect(attackEffect, reference.transform.position, new Vector3(90, 0, 0));
					eff.transform.parent = reference.transform;
					break;
				}
			case NewEffectData.EffectName.BOSSTHUNDER: {
					eff = NewEntityManager.Inst.CreateEffect(BossThunderEffect, reference.transform.position, new Vector3(0, 0, 0));
					eff.transform.parent = reference.transform;
					break;
				}
			case NewEffectData.EffectName.CROSSHIT: {
					eff = NewEntityManager.Inst.CreateEffect(CrosshitEffect, reference.transform.position, new Vector3(0, 0, 0));
					eff.transform.parent = reference.transform;
					break;
				}
			case NewEffectData.EffectName.DUSTCLOUD: {
					eff = NewEntityManager.Inst.CreateEffect(Threehit_LargeEffect, reference.transform.position, new Vector3(0, 0, 0));
					eff.transform.parent = reference.transform;
					break;
				}
			case NewEffectData.EffectName.DUSTCLOUDWITHIMPACT: {
					eff = NewEntityManager.Inst.CreateEffect(DustCloudWithImpactEffect, reference.transform.position, new Vector3(0, 0, 0));
					eff.transform.parent = reference.transform;
					break;
				}

		}



		if (eff != null)
		{
			var effscript = eff.GetComponent<NewEffectObject>();
			effscript.SetExistingTime(time);
		}

		return eff;
	}

	public void DestroyEffect(GameObject owner) {
		NewEntityManager.Inst.DestroyEffect(owner);
	}


}
