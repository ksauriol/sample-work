using UnityEngine;
using System.Collections;

public class DoorLogic : MonoBehaviour 
{
	private bool drawGUI = false;
	private bool doorIsClosed = true;
	public Transform theDoor;
	private Animator anim;
	
	
	void Awake()
	{
		anim = GetComponent<Animator>();
	}
	
	
	
	// Update is called once per frame
	void Update () 
	{
		if (drawGUI == true && Input.GetKeyDown (KeyCode.E))
		{
			
			changeDoorState();
		}
	}
	
	
	void OnTriggerEnter(Collider theCollider)
	{
		if (theCollider.tag == "Player")
		{
			drawGUI = true;
		}
	}
	
	void OnTriggerExit(Collider theCollider)
	{
		if (theCollider.tag == "Player")
		{
			drawGUI = false;
		}
	}
	
	void OnGUI()
	{
		if (drawGUI == true)
		{
		GUI.Box (new Rect((float)(Screen.width * .5f - 51), 175, 102, 22),"Press E to open");
		}
	}
	
	IEnumerator changeDoorState()
	{
		if (doorIsClosed == true)
		{
			anim.SetTrigger ("open");
			doorIsClosed = false;
			yield return new WaitForSeconds(3);
			anim.SetTrigger ("close");
			doorIsClosed = true;
		}
	}
}
