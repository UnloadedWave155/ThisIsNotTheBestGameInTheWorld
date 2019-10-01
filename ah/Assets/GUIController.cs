using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{

	public Canvas healthBar;
	public Canvas deadUI;

    // Start is called before the first frame update
    void Start()
    {
		deadUI.GetComponent<Canvas>().enabled = false;
		healthBar.GetComponent<Canvas>().enabled = true;
    }

	public void showDeathUI()
	{
		deadUI.GetComponent<Canvas>().enabled = true;
		healthBar.GetComponent<Canvas>().enabled = false;
	}

	public void showLevelUI()
	{
		deadUI.GetComponent<Canvas>().enabled = false;
		healthBar.GetComponent<Canvas>().enabled = true;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
