using UnityEngine;
using System.Collections;

public class Door2 : MonoBehaviour {
	public AnimationClip doorClip;
	public GameObject key;
	private bool door = false;
	private bool drawGUI = false;
	// Use this for initialization
	
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown (KeyCode.E) && door == true && key.activeSelf == false) 
		{
			GameObject.Find ("doors_up").GetComponent<Animation>().Play ("Door2Open");
		}
	}
	
	void OnTriggerEnter(Collider theCollider)
	{
		if (theCollider.tag == "Player")
		{
			door = true;
			drawGUI = true;
		}
	}
	
	void OnTriggerExit(Collider theCollider)
	{
		if (theCollider.tag == "Player")
		{
			door = false;
			drawGUI = false;
		}
	}
	
	void OnGUI()
	{
		if (drawGUI == true)
		{
			GUI.Box (new Rect((float)(Screen.width * .5f - 51), 175, 135, 22),"Requires key/Press E");
		}
	}
}
