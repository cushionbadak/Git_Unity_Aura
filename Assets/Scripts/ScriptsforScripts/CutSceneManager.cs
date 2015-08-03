using UnityEngine;
using System.Collections;
using UnityStandardAssets.Cameras;
public class CutSceneManager : MonoBehaviour {
    public GameObject plDum;
    public NavMeshAgent nav;
    // Use this for initialization
    void Start () {
        CameraOn();
        nav = plDum.GetComponent<NavMeshAgent>();
        nav.updateRotation = false;
        Move(new Vector3(10,0,10));
	}
	
	// Update is called once per frame
	void Update () {
	   
	}

    void Move(Vector3 pos)
    {
        nav.SetDestination(pos);
    }

    void dialogOn()
    {
        
    }

    void CameraOn()
    {
        GameObject cam=GameObject.Find("Camera");
        cam.GetComponent<AutoCam>().SetTarget(plDum.transform);
    }
}
