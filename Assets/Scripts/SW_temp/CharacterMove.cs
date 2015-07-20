using UnityEngine;
using System.Collections;

public class CharacterMove : MonoBehaviour {

    public GameObject ef;
    public GameObject AnimControl;
    public bool isPlayer = true;
    public float speed=10;
    Vector3 dir;
     bool HorizontalCheck=false;
     bool VerticalCheck=false;
    Rigidbody rigid;
    AnimControl_Player anim_player;
    AnimControl_Monster anim_monster;
    Vector3 currentPos;
    RaycastHit hit;
    // Use this for initialization
    void Start() {
        rigid = gameObject.GetComponent<Rigidbody>();
            anim_player = new AnimControl_Player(this.gameObject);

            anim_monster = new AnimControl_Monster("monla", 1, this.gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {



        /*
        if(Physics.Raycast(currentPos, dir * 10.0f,out hit, 0.5f))
        {
            if (hit.collider.tag == "MapObject")
            {
                if (Physics.Raycast(currentPos, Vector3.left, out hit, 0.5f))
                {
                    if (hit.collider.tag == "MapObject")
                    {
                        HorizontalCheck = true;
                    }
                }
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
                if (Physics.Raycast(currentPos, Vector3.back, out hit, 0.5f))
                {
                    if (hit.collider.tag == "MapObject")
                    {
                        VerticalCheck = true;
                    }
                }

                if (HorizontalCheck&&!VerticalCheck)
                    h = 0;
                if (VerticalCheck && !HorizontalCheck)
                    v = 0;
              
                HorizontalCheck = false;
                VerticalCheck = false;

            }

           

        }
        */
        currentPos = transform.position;
        float v, h;
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");
        dir = new Vector3(h, 0, v);
    
        Vector3 speedVec = new Vector3(h,0 , v) * Time.deltaTime * speed;
        // transform.Translate(speedVec);

        rigid.velocity = speedVec;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            makeEffect();
        }

        if(!isPlayer)
        if (Input.GetKeyDown(KeyCode.K))
        {
            anim_monster.UpdateAnim(STATE_MONSTER.ATTACK1);
        }


        if (isPlayer)
        {
            if (v == 0 && h == 0)
                anim_player.UpdateAnim(STATE_PLAYER.IDLE);
            else
                anim_player.UpdateAnim(STATE_PLAYER.RUN);
        }
        else
        {
            if (v == 0 && h == 0)
                anim_monster.UpdateAnim(STATE_MONSTER.IDLE);
            else
                anim_monster.UpdateAnim(STATE_MONSTER.RUN);
        }

    }

    void makeEffect()
    {
        StartCoroutine(this.effect());
    }

    IEnumerator effect()
    {
        Object obj = Instantiate(ef, transform.position, transform.rotation);
        Destroy(obj, 2.0f);
        yield return null;
    }
    
}
