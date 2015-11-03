using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Facebook : MonoBehaviour, IPointerClickHandler {
	
	public void OnPointerClick(PointerEventData eventData){
		
		if(!EventSystem.current.IsPointerOverGameObject ())
			isOK = !isOK;
		if (!isOK){
			Application.OpenURL ("https://www.facebook.com/consumedgame");
		}
	}
	bool isOK;
}
