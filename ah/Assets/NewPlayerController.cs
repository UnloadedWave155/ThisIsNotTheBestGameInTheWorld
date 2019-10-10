using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewPlayerController : MonoBehaviour
//new script becaues the renamed one wouldn't register/ attach to the player sprite
{
	public float moveSpeed;
	
	
	
	
	private Rigidbody2D myRigidbody;
	//public float raycastMaxDistance= 4f;

	private bool playerMoving; 
	public  int ammo;
	
	private Vector2 lastMove;
	
	public float jumpForce;

	public bool Grounded;

	public int hpMax = 15;
	public int hpCurrent = 15;
	public int subWeaponType;
	private bool isAlive = true;
	private bool isInvulnerable = false;
	public float invulnTime = 0.5f;
	private float invulnEndTime;
	public GameObject playerStart;
	private GUIController myGUI;
	public int lives = 5;
	public Text livesGUI;
	
	public Animator animator; 
	
	
	public GameObject prefab; // a bullet a player can shoot
	//public Rigidbody2D bullRB; // bullet rigidbody
	//public float bulletSpeed = 20f; //the speed which a bullet can travel

	
    // Start is called before the first frame update
    void Start()
    {
		prefab = Resources.Load("Bullet") as GameObject;
		
		
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
		if(subWeaponType==0 && Input.GetKeyDown(KeyCode.E)  && ammo>0){
			GameObject bullet = (GameObject)Instantiate(prefab);
			bullet.transform.position = new Vector3(transform.position.x + 0.4f, transform.position.y,0f); 
			ammo-=1;
			
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
		subWeaponType=type;
		//if(subWeaponType==0){ //tried to get the bullet to work, haven't set up the collision between the bullet yet.
			//if( Input.GetKeyDown(KeyCode.E)){
				
				GameObject Bullet = Instantiate(prefab) as GameObject;
				/*//Bullet.transform.position = transform.position * 2 ; 
				Rigidbody2D rb = Bullet.GetComponent<Rigidbody2D>();
				Bullet.transform.position = transform.position * 2 ;
				rb.velocity =lastMove;
				/*GameObject Bullet = Instantiate(prefab) as GameObject;
				var BulletInst = Instantiate(Bullet, transform.position, Quaternion.Euler(new Vector2(0, 0))); 
				BulletInst.velocity=new Vector2(bulletSpeed,0);*/
			//}
			
		//}
		
	}

	public void respawn()
	{
		gameObject.transform.position = playerStart.transform.position;
		hpCurrent = hpMax;
		myGUI.showLevelUI();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}


}
