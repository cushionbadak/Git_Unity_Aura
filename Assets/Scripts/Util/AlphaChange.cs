using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class AlphaChange : MonoBehaviour
{
    float timeSum = 0;
    Color c1;
    Color c2;
    // Use this for initialization
    void Start()
    {

        c1 = GetComponent<Image>().color;
        c1.a = 0;
        c2 = GetComponent<Image>().color;
        c2.a = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (enabled)
        {
            timeSum += Time.deltaTime;
            GetComponent<Image>().color = Color.Lerp(c1, c2, timeSum * 1.2f);
        }
    }
}