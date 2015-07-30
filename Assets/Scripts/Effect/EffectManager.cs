using UnityEngine;
using System.Collections;

public class EffectManager : MonoBehaviour {

    public GameObject attackEffect;
    public GameObject expEffect;
    public GameObject knockback;
    public GameObject hit_short;
    public GameObject hit_long;
    public GameObject EXP;
    public GameObject Fire;
	public GameObject PressT;

    private static EffectManager uniqueInstance = null;
    public static EffectManager I { get { return uniqueInstance; } }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    // Use this for initialization  
    void Start () {

        //Singleton
        if (uniqueInstance == null)
            uniqueInstance = this;
        else
            Destroy(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void createAttackEffect(GameObject obj)
    {
        StartCoroutine(AttackEffect(obj));
    }

    public void createKnockbackEffect(GameObject obj)
    {
        StartCoroutine(KnockbackEffect(obj));
    }
    public void createShortHitEffect(GameObject obj)
    {
        StartCoroutine(shortHitEffect(obj));
    }
    public void createLongHitEffect(GameObject obj)
    {
        StartCoroutine(longHitEffect(obj));
    }

    public void createEXPEffect(Vector3 pos)
    {
        StartCoroutine(EXPEffect(pos));
    }

    public void createFireBallEffect(GameObject obj)
    {
        StartCoroutine(createFireBall(obj));
    }

	public void createPressTEffect(GameObject obj)
	{
		//this is for test effect temporarily.
		//used only in PressT_forTest.cs
		StartCoroutine (createPressT (obj));
	}

	IEnumerator createPressT(GameObject obj)
	{
		//this is for test effect temporarily.
		//used for createPressTEffect method above.
		GameObject eff = (GameObject)Instantiate (PressT, obj.transform.position, Quaternion.identity);
		eff.transform.parent = obj.transform;
		Destroy (eff, 5.0f);
		yield return null;
	}

    IEnumerator createFireBall(GameObject obj)
    {
        
        GameObject eff = (GameObject)Instantiate(Fire, obj.transform.position, Quaternion.Euler(90, 0, 0));
        eff.transform.parent = obj.transform;
        Destroy(eff, 5.0f);
        yield return null;
    }


    IEnumerator AttackEffect(GameObject obj)
    {
        GameObject eff = (GameObject)Instantiate(attackEffect, obj.transform.position, Quaternion.Euler(90, 0, 0));
        eff.transform.parent = obj.transform;
        Destroy(eff, 5.0f);
        yield return null;
    }

    IEnumerator KnockbackEffect(GameObject obj)
    {
        GameObject eff = (GameObject)Instantiate(knockback, obj.transform.position, Quaternion.identity);
        eff.transform.parent = obj.transform;
        Destroy(eff, 5.0f);
        yield return null;
    }

    IEnumerator shortHitEffect(GameObject obj)
    {
        GameObject eff = (GameObject)Instantiate(hit_short, obj.transform.position, Quaternion.identity);
        eff.transform.parent = obj.transform;
        Destroy(eff, 5.0f);
        yield return null;
    }

    IEnumerator longHitEffect(GameObject obj)
    {
        GameObject eff = (GameObject)Instantiate(hit_long, obj.transform.position, Quaternion.identity);

        Destroy(eff, 5.0f);
        yield return null;
    }

    IEnumerator EXPEffect(Vector3 pos)
    {
        GameObject expEff = (GameObject)Instantiate(expEffect, pos, Quaternion.identity);
        Destroy(expEff, 5.0f);
        yield return null;
    }
}
