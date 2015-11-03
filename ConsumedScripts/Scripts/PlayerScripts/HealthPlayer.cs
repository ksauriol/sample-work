using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthPlayer : MonoBehaviour {
	public int startingHealth = 100;
	public int currentHealth;
    bool dmg;
	public Slider health;
	AudioSource playerAudio;
	// Use this for initialization
	void Awake () {
		playerAudio = GetComponent<AudioSource>();
		currentHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if (dmg) 
		{
			Debug.Log("ouch");
		}
		dmg = false;
	}
	public void TakeDamage(int amount)
	{
		dmg = true;
		currentHealth -= amount;
		health.value = currentHealth;
		playerAudio.Play ();
	}
}
