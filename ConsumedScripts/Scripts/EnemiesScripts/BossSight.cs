using UnityEngine;
using System.Collections;

public class BossSight : MonoBehaviour
{
	Transform playerpos;
	public float fieldOfViewAngle = 110f;           // Number of degrees, centred on forward, for the enemy see.
	public bool playerInSight;                      // Whether or not the player is currently sighted.
	public Vector3 personalLastSighting;            // Last place this enemy spotted the player.
	
	
	private NavMeshAgent nav;                       // Reference to the NavMeshAgent component.
	private SphereCollider col;                     // Reference to the sphere collider trigger component.
	private Animator anim;                          // Reference to the Animator.

	private GameObject player;                      // Reference to the player.
	private Animator playerAnim;                    // Reference to the player's animator component.
	private PlayerHealth playerHealth;              // Reference to the player's health script.
	public static bool Isdead;
	
	
	void Awake ()
	{

		nav = GetComponent<NavMeshAgent>();
		col = GetComponent<SphereCollider>();
		anim = GetComponent<Animator>();
	
		player = GameObject.FindGameObjectWithTag("Player");
		playerpos = GameObject.FindGameObjectWithTag("Player").transform ;
		playerAnim = player.GetComponent<Animator>();
		playerHealth = player.GetComponent<PlayerHealth>();

	}
	
	
	void Update ()
	{

				if (playerInSight) {
						
						//set the animator parameter to whether the player is in sight or not.
						nav.enabled = true;
						anim.SetBool ("BossThrow", true);
						//fieldOfViewAngle = 360f;
						nav.SetDestination (playerpos.position);
			
				} else {
						//set the animator parameter to false.
						anim.SetBool ("BossThrow", false);
				}
			if (Isdead) {
				Death ();
				}
		}
	
	void OnTriggerStay (Collider other)
	{
		// If the player has entered the trigger sphere...
		if(other.gameObject == player)
		{
			// By default the player is not in sight.
			playerInSight = false;
			
			// Create a vector from the enemy to the player and store the angle between it and forward.
			Vector3 direction = other.transform.position - transform.position;
			float angle = Vector3.Angle(direction, transform.forward);
			
			// If the angle between forward and where the player is, is less than half the angle of view...
			if(angle < fieldOfViewAngle * 0.5f)
			{
				RaycastHit hit;
				
				// ... and if a raycast towards the player hits something...
				if(Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius))
				{
					// ... and if the raycast hits the player...
					if(hit.collider.gameObject == player)
					{
						// ... the player is in sight.
						playerInSight = true;
						

					}
				}
			}
			
		
		}
	}
	
	
	void OnTriggerExit (Collider other)
	{
		// If the player leaves the trigger zone...
		if(other.gameObject == player)
			// ... the player is not in sight.
			playerInSight = false;
	}
	void Death(){
		anim.SetTrigger("Dead");
		Destroy(gameObject, 2f);
	}
}
