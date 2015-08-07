using UnityEngine;
using System.Collections;

public class PlayerUnit : Player {
    // New Variables
    //private GameObject parent;
    private Rigidbody rigid;
    private Vector3 currentPos;
    private float snare_time_store = 0.0f;
    private float snare_duration = 0.0f;
    private bool isSnare = false;
    private Vector3 dir;
    public GameObject expText;
    public GameObject resultUI;
    bool HorizontalCheck = false;
    bool VerticalCheck = false;
    RaycastHit hit;
    
    // Use this for initialization
    void Start () {

        rigid = gameObject.GetComponent<Rigidbody>();

        maxHP = PlayerLevelData.I.Status[level].maxHP;
        
        /*
        originalSpeed = 10.0f;
        currentSpeed = originalSpeed;

        powerUpPotion = 0;
        powerUpPotionScale = 1.0f;
        speedUpPotion = 0;
        speedUpPotionScale = 1.0f;
        rangeUpPotion = 0;
        rangeUpPotionScale = 1.0f;

        isThunderShoes = false;
        isDraculaBrooch = false;
        isStickyBall = false;
        isCriticalKnuckle = false;
        isSpecialThing = false;
        */

    }
	
	// Update is called once per frame
	void Update () {
        
        currentPos = transform.position;
        float v, h;
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");
        dir = new Vector3(h, 0, v);

        //움직임 보정 코드
        if (Physics.Raycast(currentPos, dir * 10.0f, out hit, 0.5f))
        {
            if (hit.collider.tag == "MapObject")
            {
                if(h>=0&&v>=0)
                {
                    if (Physics.Raycast(currentPos, Vector3.right, out hit, 0.5f))
                    {
                        if (hit.collider.tag == "MapObject")
                        {
                            HorizontalCheck = true;
                        }
                    }
                    if (Physics.Raycast(currentPos, Vector3.forward, out hit, 0.5f))
                    {
                        if (hit.collider.tag == "MapObject")
                        {
                            VerticalCheck = true;
                        }
                    }
                }
                else if(h<=0&&v>=0)
                {
                    if (Physics.Raycast(currentPos, Vector3.left, out hit, 0.5f))
                    {
                        if (hit.collider.tag == "MapObject")
                        {
                            HorizontalCheck = true;
                        }
                    }

                    if (Physics.Raycast(currentPos, Vector3.forward, out hit, 0.5f))
                    {
                        if (hit.collider.tag == "MapObject")
                        {
                            VerticalCheck = true;
                        }
                    }
                }
                else if(h<=0&&v<=0)
                {

                    if (Physics.Raycast(currentPos, Vector3.left, out hit, 0.5f))
                    {
                        if (hit.collider.tag == "MapObject")
                        {
                            HorizontalCheck = true;
                        }
                    }

                    if (Physics.Raycast(currentPos, Vector3.back, out hit, 0.5f))
                    {
                        if (hit.collider.tag == "MapObject")
                        {
                            VerticalCheck = true;
                        }
                    }
                }

                else if(h>=0&&v<=0)
                {

                    if (Physics.Raycast(currentPos, Vector3.right, out hit, 0.5f))
                    {
                        if (hit.collider.tag == "MapObject")
                        {
                            HorizontalCheck = true;
                        }
                    }

                    if (Physics.Raycast(currentPos, Vector3.back, out hit, 0.5f))
                    {
                        if (hit.collider.tag == "MapObject")
                        {
                            VerticalCheck = true;
                        }
                    }
                }
                if (HorizontalCheck && !VerticalCheck)
                    h = 0;
                if (VerticalCheck && !HorizontalCheck)
                    v = 0;

                if (VerticalCheck && HorizontalCheck)
                {
                    h = 0;
                    v = 0;
                }
                    HorizontalCheck = false;
                VerticalCheck = false;
            }
        }
         
        //움직임
        Vector3 speedVec = new Vector3(h, v, 0) * currentSpeed * speedUpPotionScale * Time.deltaTime;
        transform.Translate(speedVec);

        // Snare Check
        if (isSnare)
        {
            currentSpeed = originalSpeed * 0.7f;
            if (snare_duration > snare_time_store)
                snare_time_store += Time.deltaTime;
            else
            {
                snare_time_store = 0.0f;
                snare_duration = 0.0f;
                isSnare = false;
                currentSpeed = originalSpeed;
            }
        }
        // Position Sync
        transform.parent.position = transform.position;
        transform.localPosition = new Vector3(0, 0, 0);
        // Save Function
        if (Input.GetKeyDown(KeyCode.O)) { }    

        // Die Check
        if (currentHP <= 0) { Die(); }
        
	}
    
    public override void giveDamage(float damage)
    {
        currentHP -= damage;
    }
        
    public override void giveKnockback(Vector3 moveVector) { }
    public override void giveStun(float time) { }
    public override void giveSnare(float time)
    {
        isSnare = true;
        snare_duration = time;
    }

    public override void Die() { /*죽었을때 게임정지, UI띄우기*/ }
    public override void pause() { }
    public override void resume() {}

    public void powerUp(int powerUpPotionNum)
    {
        powerUpPotionScale = 1.0f + (0.2f * powerUpPotionNum);
    }
    public void speedUp(int speedUpPotionNum)
    {
        speedUpPotionScale = 1.0f + (0.1f * speedUpPotionNum);
    }
    public void rangeUp(int rangeUpPotionNum)
    {
        rangeUpPotionScale = 1.0f + (0.1f * rangeUpPotionNum);
    }

}
