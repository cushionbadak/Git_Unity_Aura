using UnityEngine;
using System.Collections;
using UnityStandardAssets.Cameras;
public class CutSceneManager : MonoBehaviour {

    private static CutSceneManager uniqueInstance = null;
    public static CutSceneManager I { get { return uniqueInstance; } }
    public GameObject plDum;
    public GameObject q;
    public NavMeshAgent nav;
    public GameObject e;
    Vector3 destination;
    bool isMoving=false;
    bool isEmotioning = false;
    Vector3 curPos;
    Vector3 prevPos;
    float timeSumforEmotion = 0;
    float timeSumforMove = 0;
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
                Debug.Log(timeSumforMove);
                if (timeSumforMove > 2.0f)
                {
                    timeSumforMove = 0.0f;
                    StartCoroutine(DoScript(0.0f));
                }
            }
            prevPos =curPos;
        }

        if(isEmotioning)
        {
            timeSumforEmotion += Time.deltaTime;
            if (timeSumforEmotion > 2.5f)
            {
                timeSumforEmotion = 0;
                StartCoroutine(DoScript(0.0f));
            }
        }
    }

    IEnumerator DoScript(float time)
    {
        isMoving = false;
        isEmotioning = false;
        yield return new WaitForSeconds(time);
        
        ScriptsManager.I.scriptModeON();
        yield return null;
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

    public void Move(Vector3 pos)
    {
        isMoving = true;
        nav.SetDestination(pos);
        destination = pos;
    }

    public void dialogOn()
    {
        
    }

    public void CameraOn()
    {
        GameObject cam=GameObject.Find("Camera");
        cam.GetComponent<AutoCam>().SetTarget(plDum.transform);
    }
}
