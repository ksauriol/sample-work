using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {
	public GameObject theKey;
	private bool playerNextToKey = false;
	private bool drawGUI = false;
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.E) && playerNextToKey == true)
		{
			theKey.SetActive (false);
		}
	}

	void OnTriggerEnter(Collider theCollider)
	{
		if (theCollider.tag == "Player")
		{
			playerNextToKey = true;
			drawGUI = true;
		}
	}
	
	void OnTriggerExit(Collider theCollider)
	{
		if (theCollider.tag == "Player")
		{
			playerNextToKey = false;
			drawGUI = false;
		}
	}
	
	void OnGUI()
	{
		if (drawGUI == true)
		{
			GUI.Box (new Rect((float)(Screen.width * .5f - 51), 175, 102, 22),"E to pick up");
		}
	}
}
