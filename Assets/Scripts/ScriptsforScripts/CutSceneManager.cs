using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Cameras;
public class CutSceneManager : MonoBehaviour {

    private static CutSceneManager uniqueInstance = null;
    public static CutSceneManager I { get { return uniqueInstance; } }
    public GameObject plDum;
    public GameObject q;
	public GameObject ImageUIForm;
    public GameObject BB;
    NavMeshAgent nav;
    public GameObject e;
    Vector3 destination;
	bool isWaiting=false;
    bool isMoving=false;
    bool isEmotioning = false;
    bool isBlack = false;
    bool isScene = false;
	bool isDestroying=false;
	bool isDead=false;
	bool isShaking=false;
	bool isImaging=false;
	bool isOffImaging=false;
	public GameObject black;
	float moveTime;
	float deadTime;
	float waitTime;
    Vector3 curPos;
    Vector3 prevPos;
	float timeSumforWait=0;
	float timeSumforImage=0;
	float timeSumforOffImage=0;
	float timeSumforShake=0;
	float timeSumforDead=0;
    float timeSumforEmotion = 0;
    float timeSumforSceneStart = 0;
    float timeSumforMove = 0;
    float timeSumforBlack = 0;
    float timeSumforDestroy = 0;
	GameObject curImage;

    // Use this for initialization
    void Start () {

        //Singleton
        if (uniqueInstance == null)
            uniqueInstance = this;
        else
            Destroy(this.gameObject);
        nav = plDum.GetComponent<NavMeshAgent>();
		
		nav.updateRotation = false;

        Vector3 curPos = plDum.transform.position;
        Vector3 prevPos = plDum.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if(isMoving)
        {
            curPos = plDum.transform.position;
            if ( curPos==prevPos)
            {
                timeSumforMove += Time.deltaTime;
                if (timeSumforMove > moveTime)
                {
                    isMoving = false;
                    timeSumforMove = 0.0f;
                    DoScript();
                }
            }
            prevPos =curPos;
        }

		if (isShaking) {
			timeSumforShake += Time.deltaTime;
			if (timeSumforShake > 1f)
			{
				isShaking = false;
				timeSumforShake = 0;
				GameObject.Find("Camera").GetComponent<CameraShake>().enabled=false;
				DoScript();
			}
		
		}

		if (isWaiting) {
			timeSumforWait += Time.deltaTime;
			if (timeSumforWait > waitTime)
			{
				isWaiting = false;
				timeSumforWait = 0;
				DoScript();
			}
		}
		if (isImaging) {
			timeSumforImage += Time.deltaTime;
			if (timeSumforImage > 3f)
			{
				isImaging = false;
				timeSumforImage = 0;
				DoScript();
			}
		}

		if (isOffImaging) {
			timeSumforOffImage += Time.deltaTime;
			if (timeSumforOffImage > 2f)
			{
				isOffImaging = false;
				timeSumforOffImage = 0;
				DoScript();
			}
		}

        if(isEmotioning)
        {
            timeSumforEmotion += Time.deltaTime;
            if (timeSumforEmotion > 2.5f)
            {
                isEmotioning = false;
                timeSumforEmotion = 0;
                DoScript();
            }
        }

		if(isDestroying)
		{
			timeSumforDestroy += Time.deltaTime;
			if (timeSumforDestroy > 0.2f)
			{
				isDestroying = false;
				timeSumforDestroy = 0;
				DoScript();
			}
		}

        if (isBlack)
        {
            timeSumforBlack += Time.deltaTime;
            if (timeSumforBlack > 3f)
            {

                isBlack = false;
                timeSumforBlack = 0;
                DoScript();
            }
        }
        if (isScene)
        {
            timeSumforSceneStart += Time.deltaTime;
            if (timeSumforSceneStart > 0.0f)
            {

                isScene = false;
                timeSumforSceneStart = 0;
                DoScript();
            }
        }

		if (isDead) {timeSumforDead += Time.deltaTime;
			if (timeSumforDead > 0.0f)
			{
				
				isDead = false;
				timeSumforDead = 0;
				DoScript();
			}
		}
    }

    void DoScript()
    {
        ScriptsManager.I.scriptMove();
    }

    public void question(GameObject obj)
    {
        isEmotioning = true;
        Transform[] objs = obj.GetComponentsInChildren<Transform>();
        foreach (Transform tr in objs)
        {
            if (tr.gameObject.GetInstanceID() != obj.GetInstanceID()) 
                Instantiate(q, tr.position + Vector3.forward * 0.1f + Vector3.right * 0.3f, Quaternion.Euler(90, 0, 0));
        }
    }
    public void question()
    {
        isEmotioning = true;
        Instantiate(q, plDum.transform.position + Vector3.forward * 0.1f + Vector3.right * 0.3f, Quaternion.Euler(90, 0, 0));
    }
    public void exclamation(GameObject obj)
    {
        isEmotioning = true;
        Transform[] objs = obj.GetComponentsInChildren<Transform>();
        foreach (Transform tr in objs)
        {
            if (tr.gameObject.GetInstanceID() != obj.GetInstanceID())
                Instantiate(e, tr.position + Vector3.forward * 0.1f + Vector3.right * 0.3f, Quaternion.Euler(90, 0, 0));
        }
    }
    public void exclamation()
    {
        isEmotioning = true;
        Instantiate(e, plDum.transform.position + Vector3.forward * 0.1f + Vector3.right * 0.3f, Quaternion.Euler(90, 0, 0));
    }
    public void doBlack()
    {
        isBlack = true;
		black = (GameObject)Instantiate (BB);
        black.transform.parent = GameObject.Find("BlackBoard").transform;
    }

	public void createImage(Sprite s)
	{
		isImaging = true;
		
		curImage = (GameObject)Instantiate (ImageUIForm);
		curImage.GetComponent<Image> ().sprite = s;
		curImage.GetComponent<ChangeAlpha_ImageUI> ().alpha1 ();
		curImage.transform.parent=GameObject.Find("BlackBoard").transform;
		
		curImage.GetComponent<RectTransform>().anchoredPosition3D=new Vector3(0,0,0);

	}

	public void DestroyBlack()
	{
		isDestroying = true;
		Destroy(black);
	}

    public void SceneStart(Vector3 pos)
    {
        Debug.Log(pos);
        isScene = true;
		plDum.GetComponent<NavMeshAgent> ().enabled = false;
        plDum.transform.position = pos;
		
		plDum.GetComponent<NavMeshAgent> ().enabled = true;
    }


	public void wait(float t)
	{
		isWaiting = true;
		waitTime = t;
	}

	public void shake()
	{

		isShaking = true;
		GameObject.Find ("Camera").GetComponent<CameraShake> ().enabled = true;
	}

    public void Move(Vector3 pos,float time)
    {
		moveTime = time;
        isMoving = true;
        nav.SetDestination(pos);
        destination = pos;

    }

    public void Move(GameObject obj,Vector3 pos,float time)
    {
		moveTime = time;
        isMoving = true;
        Transform[] objs = obj.GetComponentsInChildren<Transform>();
        foreach (Transform tr in objs)
        {
            if(tr.gameObject.GetInstanceID()!=obj.GetInstanceID())
                tr.gameObject.GetComponent<NavMeshAgent>().SetDestination(tr.position + pos);
        }
    }

	public void dead(GameObject obj, float time)
	{
		deadTime = time;
		isDead = true;
		Transform[] objs = obj.GetComponentsInChildren<Transform>();
		foreach (Transform tr in objs)
		{
			if(tr.gameObject.GetInstanceID()!=obj.GetInstanceID())
				tr.gameObject.GetComponent<changeAlpha_sprite>().alpha0();
		}
	}

    public void CameraOn()
    {
        GameObject cam=GameObject.Find("Camera");
        cam.GetComponent<AutoCam>().SetTarget(plDum.transform);
    }

	public void offImage()
	{
		isOffImaging = true;
		curImage.GetComponent<ChangeAlpha_ImageUI> ().alpha0 ();
	}
}
