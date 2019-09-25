using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
	public enum PowerUpTypes {smallHeal, fullHeal, dmgIncrease, haste, invincibility, hpIncrease, barrier};
	public int myPower = 0;
	public PlayerController player;
	private AudioSource soundEffect;
	private AudioClip aClip;
	//private Sprite sprite;


    // Start is called before the first frame update
    void Start()
    {
		soundEffect = GetComponent<AudioSource>();
		aClip = soundEffect.clip;
		if(myPower >= 0 && myPower < System.Enum.GetNames(typeof(PowerUp.PowerUpTypes)).Length) // makes sure the number is within the scope of PowerUpTypes
		{
			
		}
    }


	public int myPowerType()
	{
		return myPower;
	}

	public string displayPowerType()
	{
		return System.Enum.GetName(typeof(PowerUpTypes), myPower);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.name == "player")
		{
			
			Debug.Log("We have picked up a " + Enum.GetName(typeof(PowerUpTypes), myPower) + " powerup!");

			if(myPower == 0) // small heal
			{
				Debug.Log("HP: " + player.getHpCurrent() + "/" + player.getHpMax());
				player.changeHpCurrent(3);  // just test numbers
				Debug.Log("HP: " + player.getHpCurrent() + "/" + player.getHpMax());
			}
			else if(myPower == 1) // full heal
			{
				Debug.Log("HP: " + player.getHpCurrent() + "/" + player.getHpMax());
				player.changeHpCurrent(999);
				Debug.Log("HP: " + player.getHpCurrent() + "/" + player.getHpMax());
			}
			soundEffect.PlayOneShot(aClip);
			GetComponent<SpriteRenderer>().enabled = false;
			Destroy(gameObject, aClip.length); // destroy when done
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
