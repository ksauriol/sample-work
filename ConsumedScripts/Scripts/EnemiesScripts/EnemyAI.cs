using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {


	public float chaseSpeed = 2f;                           // The nav mesh agent's speed when chasing.
	private Transform myTransform;
	private EnemySight enemySight;                          // Reference to the EnemySight script.
	private NavMeshAgent nav;                               // Reference to the nav mesh agent.
	private Transform player;                               // Reference to the player's transform.

	
	
	void Awake ()
	{
		myTransform = transform;
		enemySight = GetComponent<EnemySight>();
		nav = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag("Player").transform;

	}
	
	
	void Update ()
	{

				if (enemySight.playerInSight) {

			Chasing ();


				}

		else {

			nav.enabled = false;

				}
	
		}
	
	void Chasing ()
	{

		nav.enabled = true;
		nav.speed = chaseSpeed;
		if ((float)Vector3.Distance(player.position, myTransform.position) > 1.5f) {
					
						
						nav.SetDestination (player.position);
						nav.updateRotation = true;

		} else {

			nav.enabled = false;
				
		}

	}

}
