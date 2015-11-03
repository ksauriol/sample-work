using UnityEngine;
using System.Collections;
//using System.Collections.Generic;

public class UI : MonoBehaviour {
	
	public int fastForwardSpeed=3;

	public bool alwaysEnableNextButton=true;
	
	public string nextLevel="";
	public string mainMenu="";

	private bool enableSpawnButton=true;

	private bool paused=false;
	
	
	private static UI ui;
	void Awake(){
		ui=this;
	}
	
	private string gameMessage="";
	private float lastMsgTime=0;
	public static void ShowMessage(string msg){
		ui.gameMessage=ui.gameMessage+msg+"\n";
		ui.lastMsgTime=Time.time;
	}
	IEnumerator MessageCountDown(){
		while(true){
			if(gameMessage!=""){
				while(Time.time-lastMsgTime<3){
					yield return null;
				}
				gameMessage="";
			}
			yield return null;
		}
	}
	
	// Use this for initialization
	void Start () {		
		
		//init the rect to be used for drawing ui box
	//	topPanelRect=new Rect(-3, -3, Screen.width+6, 28);
		
	//	bottomPanelRect=new Rect(-3, Screen.height-25, Screen.width+6, 28);
		//this two lines are now obsolete
		//initiate sample menu, so player can preview the tower in pointNBuild buildphase
		//if(buildMode==_BuildMode.PointNBuild && showBuildSample) BuildManager.InitiateSampleTower();
		
		StartCoroutine(MessageCountDown());
	}
	
	//caleed when SpawnManager clearFor Spawing event is detected, enable spawnButton
	void OnClearForSpawning(bool flag){
		enableSpawnButton=flag;
	}
	
	//call to enable/disable pause
	void TogglePause(){
		paused=!paused;
		if(paused){
			Time.timeScale=0;
		}
		else Time.timeScale=1;
	}
	
	// Update is called once per frame
	void Update () {

		//if escape key is pressed, toggle pause
		if(Input.GetKeyUp(KeyCode.Escape)){
			TogglePause();
		}
	}
		
//	private Rect topPanelRect=new Rect(-3, -3, Screen.width+6, 28);
//	private Rect bottomPanelRect;
//	private Rect buildListRect;
//	private Rect towerUIRect;
	private Rect[] scatteredRectList=new Rect[0];	//for piemenu
	
	//check for all UI screen space, see if user cursor is within any of them, return true if yes
	//this is to prevent user from being able to interact with in game object even when clicking on UI panel and buttons
/*	public bool IsCursorOnUI(Vector3 point){
		Rect tempRect=new Rect(0, 0, 0, 0);
		
		tempRect=topPanelRect;
		tempRect.y=Screen.height-tempRect.y-tempRect.height;
		if(tempRect.Contains(point)) return true;
		
		tempRect=bottomPanelRect;
		tempRect.y=Screen.height-tempRect.y-tempRect.height;
		if(tempRect.Contains(point)) return true;
		
		tempRect=buildListRect;
		tempRect.y=Screen.height-tempRect.y-tempRect.height;
		if(tempRect.Contains(point)) return true;
		
		tempRect=towerUIRect;
		tempRect.y=Screen.height-tempRect.y-tempRect.height;
		if(tempRect.Contains(point)) return true;
		
		for(int i=0; i<scatteredRectList.Length; i++){
			tempRect=scatteredRectList[i];
			tempRect.y=Screen.height-tempRect.y-tempRect.height;
			if(tempRect.Contains(point)) return true;
		}
		
		return false;
	}
*/
	//calculate the position occupied by each individual button based on number of button, position on screen and button size
	public Vector2[] GetPieMenuPos(int num, Vector3 pos, int size){
		//first off calculate the radius require to accomodate all buttons
		float radius=(num*size*1.8f)/(2*Mathf.PI);
		
		//create the rect array and the angle spacing required for the number of button
		Vector2[] piePos=new Vector2[num];
		float angle=200/(Mathf.Max(1, num-1));
		
		//loop through and calculate the button's position
		for(int i=0; i<num; i++){
			float x = pos.x+radius*Mathf.Sin((80)*Mathf.Deg2Rad+i*angle*Mathf.PI/180);
			float y = pos.y+radius*-Mathf.Cos((80)*Mathf.Deg2Rad+i*angle*Mathf.PI/180);
			
			piePos[i]=new Vector2(x, y);
		}
		
		return piePos;
	}

	//draw GUI
	public GUISkin skin;
	void OnGUI(){
				GUI.depth = 100;
		
				GUI.skin = skin;
		
				//general infobox
				//draw top panel
/*				GUI.BeginGroup (topPanelRect);
				for (int i=0; i<2; i++)
						GUI.Box (new Rect (0, 0, topPanelRect.width, topPanelRect.height), "");
		
				int buttonX = 8;
	*/	
				//if not pause, draw the spawnButton and fastForwardButton
		/*		if (!paused) {
						//if SpawnManager is ready to spawn
						if (enableSpawnButton) {
								if (GUI.Button (new Rect (buttonX, 5, 60, 20), "Button1")) {
										enableSpawnButton = false;
								}
						}
						buttonX += 65;
				}
				
				//display the fastforward button based on current time scale
				if (Time.timeScale == 1) {
						if (GUI.Button (new Rect (buttonX, 5, 60, 20), "Timex" + fastForwardSpeed.ToString ())) {
								Time.timeScale = fastForwardSpeed;
						}
				} else {
						if (GUI.Button (new Rect (buttonX, 5, 60, 20), "Timex1")) {
								Time.timeScale = 1;
						}
				}
*/
			//shift the cursor to where the next element will be drawn
/*			buttonX +=65;
			buttonX +=130;
			
			//pause button
			if(GUI.Button(new Rect(topPanelRect.width-30, 5, 25, 20), "II")) {
				TogglePause();
			}
		GUI.EndGroup ();
			
		//draw bottom panel
		GUI.BeginGroup (bottomPanelRect);
			for(int i=0; i<2; i++) GUI.Box(new Rect(0, 0, bottomPanelRect.width, bottomPanelRect.height), "");

			float subStartX=10; float subStartY=0;
			
			GUI.Label(new Rect(subStartX, subStartY+2, 250f, 25f), "Add Here anything you like! ");
			subStartX+=70;
		GUI.EndGroup ();
*/
			//if paused, draw the pause menu
			if(paused){
				float startX=Screen.width/2-100;
				float startY=Screen.height*0.35f;
				
				for(int i=0; i<4; i++) GUI.Box(new Rect(startX, startY, 200, 150), "Game Paused");
				
				startX+=50;
				
				if(GUI.Button(new Rect(startX, startY+=30, 100, 30), "Resume Game")){
					TogglePause();
				}
				if(GUI.Button(new Rect(startX, startY+=35, 100, 30), "Start again")){
				Application.LoadLevel(Application.loadedLevel);
			}
				if(GUI.Button(new Rect(startX, startY+=35, 100, 30), "Main Menu")){
					Application.LoadLevel(0);
				}
			}
}
}