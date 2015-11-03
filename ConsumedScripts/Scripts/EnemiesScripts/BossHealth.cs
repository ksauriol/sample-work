using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour {
	public int startingHealth = 10;
	public int currentHealth;
	bool dmg;
	// Use this for initialization
	void Awake () {
		currentHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if (dmg) 
		{
			Debug.Log("dmg");
		}
		dmg = false;
	}
	public void TakeDamage(int amount)
	{
		dmg = true;
		currentHealth -= amount;
	}
}