using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZedMovement : MonoBehaviour
{
	public float moveSpeed;
	public int moveX;
	
	private Rigidbody2D myRigidbody;
	//later implement the animation later.
	private Animator anim; 
	
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
		moveX= -1;
    }

    // Update is called once per frame
    void Update()
    {
		//case for zombie moving left, works like a charm
        if(moveX<=0){
			myRigidbody.velocity = new Vector2( 1f * -moveSpeed, myRigidbody.velocity.y);
		}
    }
}
