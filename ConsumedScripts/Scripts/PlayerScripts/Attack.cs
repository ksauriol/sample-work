using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	public int dmg = 50;
	public float dist;
	public float maxDist = 1.5f;
	public Animator anim;
	public float dmgDelay = .6f;
	
	public int rightAtk = 0;
	public int leftAtk = 0;
	private hitInfo info;

	void Start () {
		info = new hitInfo();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")) {
			DoAttack();
		}
			
	}
	IEnumerator WaitDmg() {
		yield return new WaitForSeconds(dmgDelay);
	}
	void DoAttack() {
		if (Random.value >= 0.5 && rightAtk <= 2) {
			anim.SetBool ("Right", true);
			rightAtk++;
			leftAtk = 0;
		}
		else {
			if (leftAtk <= 2) {
				anim.SetBool ("Left", true);
				rightAtk = 0;
				leftAtk++;
			}
			else {
				anim.SetBool ("Right", true);
				rightAtk++;
				leftAtk = 0;
			}
		}
		
		WaitDmg ();
		
		RaycastHit attack;
		Ray ray = Camera.main.ScreenPointToRay (new Vector3(Screen.width/2, Screen.height/2, 0));
		
		if (Physics.Raycast (ray, out attack)) {
			dist = attack.distance;
			if(dist < maxDist) {
				attack.transform.SendMessage ("applyForce", info, SendMessageOptions.DontRequireReceiver);
			}
		}
		
		anim.SetBool ("Right", false);
		anim.SetBool ("Left", false);
	}
	
}