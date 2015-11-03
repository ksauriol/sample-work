using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ExitGame : MonoBehaviour, IPointerClickHandler {
	
	public void OnPointerClick(PointerEventData eventData){
		
		if(!EventSystem.current.IsPointerOverGameObject ())
			isOK = !isOK;
		if (!isOK){
			Application.Quit();
		}
	}
	bool isOK;
}
