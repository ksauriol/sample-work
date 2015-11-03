using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Unmute : MonoBehaviour, IPointerClickHandler {
	
	public void OnPointerClick(PointerEventData eventData){
		
		if(!EventSystem.current.IsPointerOverGameObject ())
			isOK = !isOK;
		if (!isOK){
			AudioListener.volume = 1;
		}
	}
	bool isOK;
}