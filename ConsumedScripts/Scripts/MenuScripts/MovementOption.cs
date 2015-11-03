using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MovementOption : MonoBehaviour, IPointerClickHandler {
	
	public void OnPointerClick(PointerEventData eventData){
		
		if(!EventSystem.current.IsPointerOverGameObject ())
			isOK = !isOK;
		if (!isOK){
			Application.LoadLevel(2);
		}
	}
	bool isOK;
}
