using UnityEngine;
using System.Collections;

public class killenemy : MonoBehaviour {
	
	
	public int enemyhealth = 30;
	public int attackdamage = 10;
	bool Damage = false;
	int currenthealth;
	public GameObject prefab;
	public Transform hit;
	//Detonator Boom;
	//BossSight bosssight;
	//GameObject fire;
	
	
	void Awake ()
	{
		//Boom = GetComponent<Detonator> ();
		currenthealth = enemyhealth;
		//fire = GameObject.FindGameObjectWithTag ("Fire");
		
	}
	
	
	void Update (){
		//Debug.Log (currenthealth);
		//	enemyhealth = currenthealth;
		if (Damage) {
			
			takedamage(attackdamage);
			
		}
		
		
	}
	
	void OnTriggerEnter (Collider other)
	{
		//Debug.Log ("This code is working");
		//	if(Collider == GetComponent<BoxCollider>()){
		if (other.gameObject  == GameObject.FindGameObjectWithTag ("Fire") ) {
			//Debug.Log("you hit me");
			Damage = true;
			Debug.Log (currenthealth);
			
		}
	}
	public void takedamage(int amount){
		if(currenthealth <= 0){
			//Debug.Log ("dead");
			Destroy (transform.parent.gameObject);
			Instantiate(prefab,hit.position,hit.rotation);
			//Boom.Explode();
			//anim.SetTrigger ("Dead");
			//BossSight.Isdead = true;
			
		}else{
			//Debug.Log ("TakeDamage");
			currenthealth -= amount;
			Damage = false;
			
		}
		
	}
	
}