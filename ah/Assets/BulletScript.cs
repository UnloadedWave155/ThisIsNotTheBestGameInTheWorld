using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{//for a collision between bullet & foe
    // Start is called before the first frame update
	
	public float velX = 8f;
	float velY = 0f;
	private Rigidbody2D rb;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag == "FOE"){
			
			Destroy(col.gameObject);		
		}
		Destroy(gameObject);
		
	}

	void Update()
	{
		rb.velocity = new Vector2(velX, velY);
		Destroy(gameObject, 3f); // destroys the bullet after 3 seconds regardless of if it hits something (performance)
	}
}
