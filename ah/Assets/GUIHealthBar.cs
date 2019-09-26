using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIHealthBar : MonoBehaviour
{
	public NewPlayerController player;
	public Text hpCurrentGUI;
	public Text hpMaxGUI;
	public Slider hpBar;
	private int hpCurrent;
	private int hpMax;

	void FixedUpdate()
	{
		hpCurrent = player.getHpCurrent();
		hpMax = player.getHpMax();
		hpCurrentGUI.text = hpCurrent.ToString();
		hpMaxGUI.text = hpMax.ToString();
		hpBar.maxValue = hpMax;
		hpBar.value = hpCurrent;
	}
		
}
