using UnityEngine;
using System.Collections;
using UnityStandardAssets.Cameras;
public class CutSceneManager : MonoBehaviour {

    private static CutSceneManager uniqueInstance = null;
    public static CutSceneManager I { get { return uniqueInstance; } }
    public GameObject plDum;
    public GameObject q;
    public GameObject BB;
    NavMeshAgent nav;
    public GameObject e;
    Vector3 destination;
    bool isMoving=false;
    bool isEmotioning = false;
    bool isBlack = false;
    bool isScene = false;
    Vector3 curPos;
    Vector3 prevPos;
    float timeSumforEmotion = 0;
    float timeSumforSceneStart = 0;
    float timeSumforMove = 0;
    float timeSumforBlack = 0;
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
                if (timeSumforMove > 2.0f)
                {
                    isMoving = false;
                    timeSumforMove = 0.0f;
                    DoScript();
                }
            }
            prevPos =curPos;
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

        if (isBlack)
        {
            timeSumforBlack += Time.deltaTime;
            if (timeSumforBlack > 5f)
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
    }

    void DoScript()
    {
        ScriptsManager.I.scriptModeON();
    }

    public void question(GameObject obj)
    {
        isEmotioning = true;
        Instantiate(q, obj.transform.position+Vector3.forward*0.1f+Vector3.right*0.3f, Quaternion.Euler(90,0,0));
    }
    public void question()
    {
        isEmotioning = true;
        Instantiate(q, plDum.transform.position + Vector3.forward * 0.1f + Vector3.right * 0.3f, Quaternion.Euler(90, 0, 0));
    }
    public void exclamation(GameObject obj)
    {
        isEmotioning = true;
        Instantiate(e, obj.transform.position + Vector3.forward * 0.1f + Vector3.right * 0.3f, Quaternion.Euler(90, 0, 0));
    }
    public void exclamation()
    {
        isEmotioning = true;
        Instantiate(e, plDum.transform.position + Vector3.forward * 0.1f + Vector3.right * 0.3f, Quaternion.Euler(90, 0, 0));
    }
    public void doBlack()
    {
        isBlack = true;
        GameObject bb = (GameObject)Instantiate(BB);
        bb.transform.parent = GameObject.Find("BlackBoard").transform;
        Destroy(bb, 5.0f);
    }

    public void SceneStart(Vector3 pos)
    {
        Debug.Log(pos);
        isScene = true;
        plDum.transform.position = pos;

    }

    public void Move(Vector3 pos)
    {
        isMoving = true;
        nav.SetDestination(pos);
        destination = pos;
    }
    
    public void CameraOn()
    {
        GameObject cam=GameObject.Find("Camera");
        cam.GetComponent<AutoCam>().SetTarget(plDum.transform);
    }
}
