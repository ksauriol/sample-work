using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {
	int wins;
	int loss;
	float rates=0.00f;
	string username;
	public Text winning;
	public Text losing;
	public Text rating;
	public Text user;
	GameObject loginGameobject;
	DontDestroyLogin unamescript;

	// Use this for initialization
	void Start () {
		loginGameobject = GameObject.Find ("LoginKeeper");
		unamescript = loginGameobject.GetComponent<DontDestroyLogin>();
		username = unamescript.Username;
		wins = PlayerPrefs.GetInt ("wins");
		loss = PlayerPrefs.GetInt ("loss");
		rates = PlayerPrefs.GetFloat ("rates");
		winning.text = ("Wins: " + wins);
		losing.text = ("Losses: " + loss);
		rating.text = ("Win/Loss: " + rates);
		user.text = ("Username: " + username);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
