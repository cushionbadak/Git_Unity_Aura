using UnityEngine;
using System.Collections;

[System.Serializable]
public class WB_Vector3
{

    private float x;
    private float y;
    private float z;

    public WB_Vector3() { }
    public WB_Vector3(Vector3 vec3)
    {
        this.x = vec3.x;
        this.y = vec3.y;
        this.z = vec3.z;
    }

    public static implicit operator WB_Vector3(Vector3 vec3)
    {
        return new WB_Vector3(vec3);
    }
    public static explicit operator Vector3(WB_Vector3 wb_vec3)
    {
        return new Vector3(wb_vec3.x, wb_vec3.y, wb_vec3.z);
    }
}

