using UnityEngine;
using System.Collections;

public class SetDifficulty : MonoBehaviour {
	GameObject LevelandOp;
	LevelandOperation LevelandOpScript;
	void Start(){
		LevelandOp = GameObject.Find ("LevelandOperationSelector");
		LevelandOpScript = LevelandOp.GetComponent<LevelandOperation>();
	}
	public void easy(){
		LevelandOpScript.Difficulty = 1;
		Application.LoadLevel ("ChooseOperation");
	}

	public void medium(){
		LevelandOpScript.Difficulty = 2;
		Application.LoadLevel ("ChooseOperation");
	}

	public void hard(){
		LevelandOpScript.Difficulty = 3;
		Application.LoadLevel ("ChooseOperation");
	}
}
