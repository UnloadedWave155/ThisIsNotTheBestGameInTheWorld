using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubWeapon : MonoBehaviour
{
	public enum WeaponTypes {pistol, shotgun}; //2 concepts of sub weapons
	public int myPower = 0;
	public NewPlayerController player;
	private AudioSource soundEffect;
	private AudioClip aClip;
	private bool activated = false;
	//private Sprite sprite;

	//Sub Weapon Script based on ypur PowerUp script.

    // Start is called before the first frame update
    void Start()
    {
		soundEffect = GetComponent<AudioSource>();
		aClip = soundEffect.clip;
		if(myPower >= 0 && myPower < System.Enum.GetNames(typeof(SubWeapon.WeaponTypes)).Length) // makes sure the number is within the scope of PowerUpTypes
		{
			
		}
    }


	public int myPowerType()
	{
		return myPower;
	}

	public string displayPowerType()
	{
		return System.Enum.GetName(typeof(WeaponTypes), myPower);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.name == "player" && !activated)
		{
			activated = true; // added this to prevent being able to get the power up twice while the sound is playing.
			Debug.Log("We have picked up a " + Enum.GetName(typeof(WeaponTypes), myPower) + " powerup!");

			if(myPower == 0) // pistol
			{
				Debug.Log("Player has a pistol " );
				player.setSubWeapon(myPower);  
				Debug.Log("Player has a sub weapon.");
			}
	
			soundEffect.PlayOneShot(aClip);
			GetComponent<SpriteRenderer>().enabled = false; // hide the object in the scene so it looks destroyed
			Destroy(gameObject, aClip.length); // destroy when sound is done playing
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}

