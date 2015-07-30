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


        // Player Status
        level = 1;  // Player의 현재 레벨 받아오기 필요. 게임매니저에 저장해서 받아오기
        EXP = 0;

        maxHP = PlayerLevelData.I.Status[level].maxHP;
        
        
        currentHP = maxHP;
        
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

        // Others
        //parent = transform.parent.gameObject;
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
        Vector3 speedVec = new Vector3(h, v, 0) * currentSpeed *Time.deltaTime;
        transform.Translate(speedVec);
       
        

        //rigid.velocity = speedVec;
        
        // Movement xDir = x-coord, yDir = z-coord
        /*bool Key_left = Input.GetKey(KeyCode.LeftArrow);
        bool Key_right = Input.GetKey(KeyCode.RightArrow);
        bool Key_up = Input.GetKey(KeyCode.UpArrow);
        bool Key_down = Input.GetKey(KeyCode.DownArrow);
        float xDir = 0.0f;
        float yDir = 0.0f;

        if (Key_right) { xDir = 1.0f; }
        else if (Key_left) { xDir = -1.0f; }

        if (Key_up) { yDir = 1.0f; }
        else if (Key_down) { yDir = -1.0f; }

        Move(xDir, yDir);
        */
        
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


    private void Move(float xDir, float yDir)
    {
        Vector3 start = transform.position;
        Vector3 end = start + new Vector3(xDir * currentSpeed * Time.deltaTime, 0, yDir * currentSpeed * Time.deltaTime);

        transform.position = end;
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

    public override void Die() {
        //죽었을때 게임정지, UI띄우기
    }

    void reGame()
    {
    }

    public override void pause() { }
    public override void resume() {}
}
