using UnityEngine;
using System.Collections;

public class PlayerUnit : Player {
    // New Variables
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        // Player Status
        level = 1;
        EXP = .0f;

        maxHP = PlayerLevelData.I.Status[level].maxHP;
        currentHP = maxHP;
        
        originalSpeed = 100.0f;
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
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	    
        // Movement xDir = x-coord, yDir = z-coord
        bool Key_left = Input.GetKey(KeyCode.LeftArrow);
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
	}

    protected void Move(float xDir, float yDir)
    {
        Vector3 start = rb.position;
        Vector3 end = start + new Vector3(xDir * currentSpeed, 0, yDir * currentSpeed);
        Vector3 newPos = end * Time.deltaTime;
        rb.MovePosition(newPos);
    }
    
}
