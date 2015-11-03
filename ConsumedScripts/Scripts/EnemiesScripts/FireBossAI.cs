using UnityEngine;
using System.Collections;

public class FireBossAI : MonoBehaviour {


	public float chaseSpeed = 0f;                           // The nav mesh agent's speed when chasing.
	private Transform myTransform;
	private BossSight bossSight;                          // Reference to the EnemySight script.
	private NavMeshAgent nav;                               // Reference to the nav mesh agent.
	private Transform player;                               // Reference to the player's transform.

	
	
	void Awake ()
	{
		myTransform = transform;
		bossSight = GetComponent<BossSight>();
		nav = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag("Player").transform;

	}
	
	
	void Update ()
	{

				if (bossSight.playerInSight) {
transform.LookAt (player.position);
			Throwing();


				}

		else {

			nav.enabled = false;

				}
	
		}
	
	void Throwing ()
	{

		nav.enabled = true;
//		nav.speed = chaseSpeed;
//		if ((float)Vector3.Distance(player.position, myTransform.position) > 1.5f) {
					
						
						nav.SetDestination (player.position);
						nav.updateRotation = true;
//		} else {
//
//			nav.enabled = false;
//				
//		}

	}

}
