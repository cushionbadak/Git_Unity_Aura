using UnityEngine;
using System.Collections;

public class PlayerUnit : Player {
    // New Variables
    //private GameObject parent;
    private Rigidbody rigid;
    private Vector3 currentPos;
    private Vector3 dir;
    public GameObject expText;
    public GameObject resultUI;
    bool HorizontalCheck = false;
    bool VerticalCheck = false;
    RaycastHit hit;

    public GameObject _pAura;

    private bool on_stun = false;
    private float t_stun = .0f;
    private float cd_stun;

    //private bool on_snare = false;
    private float ratio_snare = 1.0f;
    

    // Use this for initialization
    void Start () {

        rigid = gameObject.GetComponent<Rigidbody>();

        maxHP = PlayerLevelData.I.Status[level].maxHP;
        
    }
	
	// Update is called once per frame
	void Update () {
        
        currentPos = transform.position;
        float v, h;
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");
        dir = new Vector3(h, 0, v);
        dir.Normalize();

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
        Vector3 speedVec = new Vector3(h, 0, v) * currentSpeed * speedUpPotionScale * ratio_snare * Time.deltaTime;
        transform.position += speedVec;

        // Stun Check
        if (on_stun)
        {
            currentSpeed = .0f;
            t_stun += Time.deltaTime;
            if (t_stun >= cd_stun)
            {
                on_stun = false;
                t_stun = .0f;
                currentSpeed = originalSpeed;
                _pAura.SetActive(true);
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
    public override void giveStun(float time)
    {
        on_stun = true;
        t_stun = .0f;
        cd_stun = time;
        _pAura.SetActive(false);
    }
    public override void giveSnare(float ratio)
    {
        ratio_snare = ratio;
    }
    public void removeSnare()
    {
        ratio_snare = 1.0f;
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
