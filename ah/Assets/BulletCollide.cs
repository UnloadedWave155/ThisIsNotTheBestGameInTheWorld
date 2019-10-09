using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollide : MonoBehaviour
{//for a collision between bullet & foe
    // Start is called before the first frame update
	
	
	private Rigidbody2D rb2d;
	
	void start(){
		rb2d = GetComponent<Rigidbody2D>();
	}




    // Update is called once per frame
    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(20,0);
    }
	void OnCollisionEnter2D (Collision2D col){
		if (col.gameObject.tag == "FOE"){
			
			Destroy(col.gameObject);
			Destroy(gameObject);
		
		}
		
	}
}
