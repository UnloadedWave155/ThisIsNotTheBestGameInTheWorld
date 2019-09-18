using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour

{
	public float moveSpeed;
	
	private Rigidbody2D myRigidbody;
	//public float raycastMaxDistance= 4f;

	private bool playerMoving; 
	
	private Vector2 lastMove;
	
	public float jumpForce;
	
	public bool Grounded;

	public int hpMax = 10;
	public int hpCurrent = 7;
	
	
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
    }

	// Finds out what kind of power up this is, applies it, and destroys the powerup.
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.name == "PowerUp")
		{
			PowerUp p = col.gameObject.GetComponent(typeof (PowerUp)) as PowerUp;
			int type = p.myPowerType(); // type is the type of powerup we just picked up
			Debug.Log("We have picked up a " + p.displayPowerType() + " powerup!");

			if(type == 0) // small heal
			{
				if(hpMax != hpCurrent)
				{
					Debug.Log("HP: " + hpCurrent + "/" + hpMax);
					hpCurrent += 5;  // just test numbers
					if(hpCurrent > hpMax)
					{
						hpCurrent = hpMax;
					}
					Debug.Log("HP: " + hpCurrent + "/" + hpMax);
				}
			}
			else if(type == 1) // full heal
			{
				Debug.Log("HP: " + hpCurrent + "/" + hpMax);
				hpCurrent = hpMax;
				Debug.Log("HP: " + hpCurrent + "/" + hpMax);
			}

			Destroy(col.gameObject); // destroy when done
		}
	}


}
