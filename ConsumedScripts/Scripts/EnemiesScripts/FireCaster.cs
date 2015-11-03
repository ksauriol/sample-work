using UnityEngine;
using System.Collections;

public class FireCaster : MonoBehaviour
{
	public Rigidbody rocketPrefab;
	public Transform barrelEnd;
	private Transform player; 
	

	void Update (){

		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	public void Castfire()			
	{
		Rigidbody rocketInstance;
		rocketInstance = Instantiate(rocketPrefab, barrelEnd.position, barrelEnd.rotation) as Rigidbody;
		//		rocketInstance.AddForce(barrelEnd.up * 300);				// * Time.smoothDeltaTime
		rocketInstance.AddForce ((player.position + transform.up*2 - barrelEnd.position).normalized * 600 );


	}

}