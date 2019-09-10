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


}
