using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
	public enum PowerUpTypes {smallHeal, fullHeal, dmgIncrease, haste, invincibility, hpIncrease, barrier};
	public int myPower = 0;
	//private Sprite sprite;


    // Start is called before the first frame update
    void Start()
    {
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


    // Update is called once per frame
    void Update()
    {
        
    }
}
