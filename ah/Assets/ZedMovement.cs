﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZedMovement : MonoBehaviour
{
	public float moveSpeed;
	public int moveX;
	public Spawner spawn;
	public bool canSpawn;
	
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
		if(gameObject.transform.position.y<-50){
			transform.gameObject.SetActive(false);
			canSpawn=true;
		}
		if(canSpawn==true){
			gameObject.transform.position = transform.position;
			transform.gameObject.SetActive(true);
		}
    }

	
		
	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag == "bullet"){
			
			
			transform.gameObject.SetActive(false);
			canSpawn=true;
			
		
		}
	}
}
