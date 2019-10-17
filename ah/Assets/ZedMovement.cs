using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZedMovement : MonoBehaviour
{
	public float moveSpeed;
	public float zedSpeed;
	public int moveX;
	private bool alive;
	//public GameObject Spawn;
	//public GameObject Foe;
	
	private Rigidbody2D myRigidbody;
	//implement the animation later.
	private Animator anim; 
	
    // Start is called before the first frame update
    void Start()
    {
		
		alive = true;
        myRigidbody = GetComponent<Rigidbody2D>();
		//gameObject.transform.position = Spawn.transform.position;
		moveX= -1;
    }

    // Update is called once per frame
    void Update()
    {
		//case for zombie moving left, works like a charm
        if(moveX<=0 ){
			myRigidbody.velocity = new Vector2( zedSpeed * -moveSpeed, myRigidbody.velocity.y);
		}
		if(gameObject.transform.position.y<50){
			Destroy(gameObject);
			alive=false;
		}
		/*if(alive==false){
			respawn();
			
		}
    }
	void respawn(){
		Instantiate(Foe,Spawn.transform.position,Quaternion.identity);
		alive = true;*/
       
	}
	
		
	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag == "bullet"){
			
			
			Destroy(gameObject);
			alive=false;
		
		}
	}
}
