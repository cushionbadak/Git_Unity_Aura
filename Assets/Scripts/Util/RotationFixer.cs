using UnityEngine;
using System.Collections;

public class RotationFixer : MonoBehaviour
{
    public Vector3 fixangle;
    [System.Serializable]
    public struct fixable
    {
        public bool x, y, z;
    }

    [SerializeField]
    public fixable fix;


    // Use this for initialization
    void Start()
    {

    }

    void LateUpdate()
    {
        Vector3 angle = transform.localEulerAngles;
        if (fix.x)
            angle.x = fixangle.x;
        if (fix.y)
            angle.y = fixangle.y;
        if (fix.z)
            angle.z = fixangle.z;

        transform.localEulerAngles = angle;
    }
}
