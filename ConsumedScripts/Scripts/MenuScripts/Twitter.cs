using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Twitter : MonoBehaviour, IPointerClickHandler {
	
	public void OnPointerClick(PointerEventData eventData){
		
		if(!EventSystem.current.IsPointerOverGameObject ())
			isOK = !isOK;
		if (!isOK){
			Application.OpenURL ("https://twitter.com/consumedgame");
		}
	}
	bool isOK;
}
