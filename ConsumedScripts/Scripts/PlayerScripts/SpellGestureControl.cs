using UnityEngine;
using System.Collections;

public class SpellGestureControl : MonoBehaviour {
	string fireSpell = "Left";
	string currentSpell = "";
	string lastSpellTrigger = "";
	float lastSpellTriggerStateChange = 0f;
	public float spellTimeOut = 5f;
	public GameObject fireBall;
	public float fireBallSpeed = 500f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Time.time > lastSpellTriggerStateChange + spellTimeOut) {
						currentSpell = "";
						lastSpellTrigger = "";
				}
		if (currentSpell.Equals (fireSpell) /*|| Input.GetButtonDown("Fire1")*/) 
		{
						GameObject fireProj = (GameObject)Instantiate (fireBall, transform.position+transform.forward, transform.rotation);
						fireProj.GetComponent<Rigidbody>().AddForce(transform.forward*fireBallSpeed);
						currentSpell = "";
						lastSpellTrigger = "";
		}      
			                       }

	public void SetSpellGestureState(string triggerName){
		if(!triggerName.Equals(lastSpellTrigger)){
			lastSpellTrigger = triggerName;
			lastSpellTriggerStateChange = Time.time;
			currentSpell += triggerName;
			print (currentSpell);
		}

	}
}
