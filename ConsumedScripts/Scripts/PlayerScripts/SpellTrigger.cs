using UnityEngine;
using System.Collections;

public class SpellTrigger : MonoBehaviour {
	public string triggerId = "";
	SpellGestureControl spell;
	public Rigidbody rocketPrefab;
	public Transform barrelEnd;
	// Use this for initialization
	void Start () {
		spell = GetComponentInParent<SpellGestureControl> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other){
		if (other.tag.Equals ("Hand")) {
						if (triggerId.Equals ("Left")) {
							Rigidbody rocketInstance;
							rocketInstance = Instantiate(rocketPrefab, barrelEnd.position, barrelEnd.rotation) as Rigidbody;
							rocketInstance.AddForce(barrelEnd.forward * 300);
								other.GetComponent<Renderer>().material.color = Color.yellow;
								spell.SetSpellGestureState ("Left");
						}
						if (triggerId.Equals ("Right")) {
								Input.GetButtonDown("1");
								other.GetComponent<Renderer>().material.color = Color.red;
								spell.SetSpellGestureState ("Right");
						}
						/*if (triggerId.Equals ("Top")) {
								other.renderer.material.color = Color.green;
								spell.SetSpellGestureState ("Top");
						}
						if (triggerId.Equals ("Bottom")) {
								other.renderer.material.color = Color.blue;
								spell.SetSpellGestureState ("Bottom");
						}*/
		   

				}
		   }

}
