using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Youtube : MonoBehaviour, IPointerClickHandler {
	
	public void OnPointerClick(PointerEventData eventData){
		
		if(!EventSystem.current.IsPointerOverGameObject ())
			isOK = !isOK;
		if (!isOK){
			Application.OpenURL ("https://www.youtube.com/channel/UCIEjMgYlCFR_Ld8y0Cc931Q/feed");
		}
	}
	bool isOK;
}
