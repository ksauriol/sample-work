using UnityEngine;
using System.Collections;

public class SetOperation : MonoBehaviour {

	GameObject LevelandOp;
	LevelandOperation LevelandOpScript;
	void Start(){
		LevelandOp = GameObject.Find ("LevelandOperationSelector");
		LevelandOpScript = LevelandOp.GetComponent<LevelandOperation>();
	}
	public void Random(){
		LevelandOpScript.Operation = 0;
		Application.LoadLevel ("Game");
	}
	public void Addition(){
		LevelandOpScript.Operation = 1;
		Application.LoadLevel ("Game");
	}
	
	public void Subtraction(){
		LevelandOpScript.Operation = 2;
		Application.LoadLevel ("Game");
	}
	
	public void Multiplication(){
		LevelandOpScript.Operation = 3;
		Application.LoadLevel ("Game");
	}
	public void Division(){
		LevelandOpScript.Operation = 4;
		Application.LoadLevel ("Game");
	}
	public void Patterns(){
		Application.LoadLevel ("Patterns");
	}
}
