using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour

{
	public float moveSpeed;
	
	private Rigidbody2D myRigidbody;
	//public float raycastMaxDistance= 4f;

	private bool playerMoving; 
	
	private Vector2 lastMove;
	
	public float jumpForce;
	

	
	public bool Grounded;

	public int hpMax = 15;
	public int hpCurrent = 15;
	private bool isAlive = true;
	private bool isInvulnerable = false;
	public float invulnTime = 0.5f;
	private float invulnEndTime;
	
	private Animator anim; 

	
    // Start is called before the first frame update
    void Start()
    {
		Grounded=true;
        myRigidbody = GetComponent<Rigidbody2D>();
		
    }

    // Update is called once per frame
    void Update()
    {
        playerMoving = false;

		if(isInvulnerable)
		{
			if(Time.time > invulnEndTime)
			{
				Debug.Log("Vulnerable again.");
				isInvulnerable = false;
			}
		}

		if(hpCurrent <= 0)
		{
			isAlive = false;
			Debug.Log("You are dead!");
		}
		
		if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw ("Horizontal") < -0.5f ) {

			//transform.Translate (new Vector3 (Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
			myRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, myRigidbody.velocity.y);
			playerMoving = true;
			lastMove = new Vector2 (Input.GetAxisRaw("Horizontal"), 0f); 

		}
		if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f) {
			myRigidbody.velocity = new Vector2(0f, myRigidbody.velocity.y); 
		}
		if(Input.GetKeyDown(KeyCode.Space)&&Grounded){
			myRigidbody.AddForce(Vector2.up*jumpForce,ForceMode2D.Impulse);
			Grounded=false;
		}

		
    }
	void OnCollisionEnter2D (Collision2D col)
    {
        if (col.gameObject.tag == "floor")
        {
            Grounded= true;
        }
		if (col.gameObject.tag == "FOE")
        {
			if(!isInvulnerable)
			{
            	hpCurrent-=2;
				Debug.Log("HP: " + hpCurrent + "/" + hpMax);
				isInvulnerable = true;
				invulnEndTime = Time.time + invulnTime;
			}
        }
		if (col.gameObject.tag=="stairs" && Input.GetKeyDown(KeyCode.UpArrow)) 
		{
			
			if(Input.GetAxisRaw("Horizontal") > 0.5f){
				transform.position= new Vector3(1.0f,1.0f,0.0f);
				myRigidbody.AddForce(transform.up*0.05f);
			//myRigidbody.AddForce(Vector2.right*1.0f,ForceMode2D.Impulse);
				Grounded= true;
			}
			if(Input.GetAxisRaw ("Horizontal") < 0.5f ){
				transform.position= new Vector3(-1.0f,-1.0f,0.0f);
				myRigidbody.AddForce(transform.up*0.05f);
			//myRigidbody.AddForce(Vector2.right*1.0f,ForceMode2D.Impulse);
				Grounded= true;
			}
			
		}
    }

	public int getHpMax()
	{
		return hpMax;
	}

	public int getHpCurrent()
	{
		return hpCurrent;
	}

	// takes positive or negative number and changes current HP
	public void changeHpCurrent(int difference)
	{
		
		if(hpCurrent + difference >= hpMax)
		{
			hpCurrent = hpMax;
		}
		else if(hpCurrent + difference <= 0)
		{
			isAlive = false;
		}
		else{
			hpCurrent += difference;
		}
	}

}
