using UnityEngine;
using System.Collections;

public class EnemyDamage : MonoBehaviour {
	public float timeBetweenAttacks = 1.5f;
	public int attackdmg = 10;
	GameObject Player;
	HealthPlayer playerHealth;
	bool playerInRange;
	float timer;
	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = Player.GetComponent <HealthPlayer> ();
	}
	void OnTriggerEnter (Collider other)
	{
		// If the entering collider is the player...
		if (other.gameObject  == GameObject.FindGameObjectWithTag ("Player") )
		{
			// ... the player is in range.
			playerInRange = true;
		}
	}
	
	
	void OnTriggerExit (Collider other)
	{
		// If the exiting collider is the player...
		if(other.gameObject  == GameObject.FindGameObjectWithTag ("Player") )
		{
			// ... the player is no longer in range.
			playerInRange = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (playerInRange && timer >= timeBetweenAttacks) 
		{
			Attack ();
		}

	
	}
	void Attack ()
	{
		// Reset the timer.
		timer = 0f;
		
		// If the player has health to lose...
		if(playerHealth.currentHealth > 0)
		{
			// ... damage the player.
			playerHealth.TakeDamage (attackdmg);
		}
	}
}
