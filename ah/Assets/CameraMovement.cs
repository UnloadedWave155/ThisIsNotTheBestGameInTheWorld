using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public NewPlayerController player;
	private Vector3 offset;
	
    // Start is called before the first frame update
    void Start()
    {
        offset=transform.position- player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		if(!(player.isInPit()))
		{
        	transform.position = player.transform.position+offset;
		}
    }
}
