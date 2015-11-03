using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Mute : MonoBehaviour, IPointerClickHandler {
	
	public void OnPointerClick(PointerEventData eventData){
		
		if(!EventSystem.current.IsPointerOverGameObject ())
			isOK = !isOK;
		if (!isOK){
			AudioListener.volume = 0;
		}
	}
	bool isOK;
}