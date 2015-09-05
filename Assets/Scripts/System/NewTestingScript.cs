using UnityEngine;
using System.Collections;

public class NewTestingScript : MonoBehaviour
{
    bool isMenuOn = false;

    public GameObject stununit;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            GameManager.Inst.GameStart();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isMenuOn)
            {
                isMenuOn = false;
                GameManager.Inst.OnMenuOff();
            }
            else
            {
                isMenuOn = true;
                GameManager.Inst.OnMenuOn();
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            stununit.GetComponent<UnitObject>().GiveStun(3);
        }
    }
}