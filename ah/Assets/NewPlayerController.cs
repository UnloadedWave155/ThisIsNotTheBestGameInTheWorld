using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewPlayerController : MonoBehaviour
//new script becaues the renamed one wouldn't register/ attach to the player sprite
{
	public float moveSpeed;
	//public float bulletSpeed=10f;
		
	private Rigidbody2D myRigidbody;
	//public float raycastMaxDistance= 4f;

	private bool playerMoving; 
	public  int ammo;
	
	private Vector2 lastMove;
	
	public float jumpForce;

	public bool Grounded;

	public int hpMax = 15;
	public int hpCurrent = 15;
	public int subWeaponType = -1;
	private bool isAlive = true;
	private bool isInvulnerable = false;
	public float invulnTime = 0.5f;
	private float invulnEndTime;
	public GameObject playerStart;
	private GUIController myGUI;
	public int lives = 5;
	public Text livesGUI;

	public GameObject bulletRight;
	public GameObject bulletLeft;
	Vector2 bulletPos;
	public float fireRate = .5f;
	float nextFire = 0f;
	bool facingRight = true;
	
	public Animator animator; 
	
    // Start is called before the first frame update
    void Start()
    {

		Grounded=true;
		lives = PlayerPrefs.GetInt("lives");
		if(lives <= 0)
		{
			PlayerPrefs.SetInt("lives", 5);
		}
		else{
			PlayerPrefs.SetInt("lives", lives);
		}
		livesGUI.text = lives.ToString();
        myRigidbody = GetComponent<Rigidbody2D>();
		gameObject.transform.position = playerStart.transform.position;
		myGUI = GetComponent<GUIController>();
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
			if(isAlive)
			{
				lives-=1;
				PlayerPrefs.SetInt("lives", lives);
				Debug.Log("You are dead!");
				myGUI.showDeathUI();
			}
			isAlive = false;
		}

		if (Input.GetAxisRaw("Horizontal") > 0.5f ) {

			myRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, myRigidbody.velocity.y);
			playerMoving = true;
			lastMove = new Vector2 (Input.GetAxisRaw("Horizontal"), 0f); 
			facingRight = true;
		}
		if (Input.GetAxisRaw("Horizontal") < -0.5f ) {

			myRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, myRigidbody.velocity.y);
			playerMoving = true;
			lastMove = new Vector2 (Input.GetAxisRaw("Horizontal"), 0f); 
			facingRight = false;
		}


		/*if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw ("Horizontal") < -0.5f ) {

			//transform.Translate (new Vector3 (Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
			myRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, myRigidbody.velocity.y);
			playerMoving = true;
			lastMove = new Vector2 (Input.GetAxisRaw("Horizontal"), 0f); 
			

		}*/
		if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f) {
			myRigidbody.velocity = new Vector2(0f, myRigidbody.velocity.y); 
		}
		if(Input.GetKeyDown(KeyCode.Space)&&Grounded){
			myRigidbody.AddForce(Vector2.up*jumpForce,ForceMode2D.Impulse);
			Grounded=false;
		}
		if(subWeaponType==0 && Input.GetKeyDown(KeyCode.E)  && (ammo > 0) && (Time.time > nextFire)){
			nextFire = Time.time + fireRate; // delay between firing shots
			fire();
		}
		
		animator.SetFloat("Horizontal",Input.GetAxis("Horizontal"));//for animations

		
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
				isInvulnerable = true;
				invulnEndTime = Time.time + invulnTime; 
			}

        }
		if (col.gameObject.tag=="stairs" && Input.GetKeyDown(KeyCode.UpArrow)) 
		{
			
			if(Input.GetAxisRaw("Horizontal") > 0.5f){
				transform.position= new Vector3(1.0f,1.0f,0.0f);
				myRigidbody.AddForce(transform.up*0.25f);
			//myRigidbody.AddForce(Vector2.right*1.0f,ForceMode2D.Impulse);
				Grounded= true;
			}
			if(Input.GetAxisRaw ("Horizontal") < 0.5f ){
				transform.position= new Vector3(-1.0f,-1.0f,0.0f);
				myRigidbody.AddForce(transform.up*0.25f);
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
    public void setSubWeapon(int type){  
		
		
	}

	public void respawn()
	{
		gameObject.transform.position = playerStart.transform.position;
		hpCurrent = hpMax;
		myGUI.showLevelUI();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void fire()
	{
		bulletPos = transform.position;
		if(facingRight)
		{
			bulletPos += new Vector2(+.5f, +0.2f);
			Instantiate(bulletRight, bulletPos, Quaternion.identity);
		}
		else{
			bulletPos += new Vector2(-.5f, +0.2f);
			Instantiate(bulletLeft, bulletPos, Quaternion.identity);
		}
	}

}
