using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour 
{
	[SerializeField] Text connectionText;
	[SerializeField] Camera sceneCamera;
	[SerializeField] GameObject serverWindow;
	//[SerializeField] InputField username;
	[SerializeField] string username;
	[SerializeField] InputField roomName;
	[SerializeField] InputField roomList;
	[SerializeField] InputField messageWindow;
	public static int playerid = 0;
	Queue<string> messages;
	const int messageCount = 6;
	PhotonView photonView;
	public Text Loadingtext;
	public GameObject LoadingPanel;

	public GameObject GamePanel;
	public GameObject OperationsPanel;
	public GameObject DifficultyPanel;
	public GameObject MainPanel;

	public string gameType;
	public int difficulty;
	public int operation;
	public int newoperation;
	public int newdifficulty;
	//bool endOfGame;


	string createdRoomName;
	bool creategame = false;
 
	int playercount;
	float loadingtime;

	GameObject loginGameobject;
	DontDestroyLogin unamescript;

	void Start () 
	{
		loginGameobject = GameObject.Find ("LoginKeeper");
		unamescript = loginGameobject.GetComponent<DontDestroyLogin>();

		//endOfGame = false;
		photonView = GetComponent<PhotonView> ();
		messages = new Queue<string> (messageCount);
		username = unamescript.Username;
		PhotonNetwork.player.name = username;


		PhotonNetwork.logLevel = PhotonLogLevel.Full;
		PhotonNetwork.ConnectUsingSettings ("1.1");
		StartCoroutine ("UpdateConnectionString");
	}

	void Update()
	{

			loadingtime += Time.deltaTime;
			
			if (loadingtime >= 3.5) {
				StartCoroutine(waiting ());
				loadingtime = 0;
			}//if loadingtime


		
		
		
	}//end update
	
	IEnumerator UpdateConnectionString () 
	{
		while(true)
		{
			connectionText.text = PhotonNetwork.connectionStateDetailed.ToString ();
			yield return null;
		}
	}
	
	void OnJoinedLobby()
	{
		serverWindow.SetActive (true);
	}
	
	void OnReceivedRoomListUpdate()
	{
		roomList.text = "";
		RoomInfo[] rooms = PhotonNetwork.GetRoomList ();
		foreach(RoomInfo room in rooms)
			roomList.text += room.name + "\n";
		 
	}
	
	public void JoinRoom()
	{
		//PhotonNetwork.player.name = username.text;
		if(creategame)
		{
			createdRoomName = gameType + "[" + difficulty + "]" + "[" + operation + "]";
			RoomOptions roomOptions = new RoomOptions(){ isVisible = true, maxPlayers = 2 };PhotonNetwork.JoinOrCreateRoom (createdRoomName, roomOptions, TypedLobby.Default);
			creategame = false;
		}
		else
		{

			RoomOptions roomOptions = new RoomOptions(){ isVisible = true, maxPlayers = 2 };PhotonNetwork.JoinRoom(roomName.text);
		
		}//end else
	}
	
	void OnJoinedRoom()
	{

	
		serverWindow.SetActive (false);
		StopCoroutine ("UpdateConnectionString");
		connectionText.text = "";
		StartSpawnProcess (0f);

	}
	
	void StartSpawnProcess(float respawnTime)
	{
		playerid = PhotonNetwork.player.ID;
		sceneCamera.enabled = true;

		if (playerid == 1) {

			PhotonNetwork.player.SetTeam(PunTeams.Team.blue);
			//inGame = true;
			LoadingPanel.SetActive (true);


		} else {

			PhotonNetwork.player.SetTeam(PunTeams.Team.red);
		
		}
	}//end startSpawnProcess


	void AddMessage(string message)
	{
		photonView.RPC ("AddMessage_RPC", PhotonTargets.All, message);
	}
	
	[PunRPC]
	void AddMessage_RPC(string message)
	{
		messages.Enqueue (message);
		if(messages.Count > messageCount)
			messages.Dequeue();
		
		messageWindow.text = "";
		foreach(string m in messages)
			messageWindow.text += m + "\n";
	}

	private IEnumerator waiting(){
		
		Loadingtext.text = "Waiting on other player";
		yield return new WaitForSeconds (0.5f);
		Loadingtext.text = "Waiting on other player.";
		yield return new WaitForSeconds (0.5f);
		Loadingtext.text = "Waiting on other player..";
		yield return new WaitForSeconds (0.5f);
		Loadingtext.text = "Waiting on other player...";
		yield return new WaitForSeconds (0.5f);
		
	}


	void OnPhotonPlayerConnected( PhotonPlayer other )
	{
		Debug.Log( "OnPhotonPlayerConnected() " + other.name ); // not seen if you're the player connecting
		//instantiateThisGame (gameType);
		LoadingPanel.SetActive (false);

		switch(gameType){
		case "Math":
			switch(operation){
			case 1:
				switch(difficulty){
				case 1:
					instantiateThisGame("Easy Math Addition");
					break;
				case 2:
					instantiateThisGame("Medium Math Addition");
					break;
				case 3:
					instantiateThisGame("Hard Math Addition");
					break;
				}
				break;
			case 2:
				switch(difficulty){
				case 1:
					instantiateThisGame("Easy Math Subtraction");
					break;
				case 2:
					instantiateThisGame("Medium Math Subtraction");
					break;
				case 3:
					instantiateThisGame("Hard Math Subtraction");
					break;
				}
				
				break;
			case 3:
				switch(difficulty){
				case 1:
					instantiateThisGame("Easy Math Multiplication");
					break;
				case 2:
					instantiateThisGame("Medium Math Multiplication");
					break;
				case 3:
					instantiateThisGame("Hard Math Multiplication");
					break;
				}
				break;
			case 4:
				switch(difficulty){
				case 1:
					instantiateThisGame("Easy Math Division");
					break;
				case 2:
					instantiateThisGame("Medium Math Division");
					break;
				case 3:
					instantiateThisGame("Hard Math Division");
					break;
				}
				break;
			}
			
			break;
		
			//operation = null;
			//difficulty = null;
			//gameType = null;
		
		
		
		
		
		case "Pattern":
			switch(difficulty){
			case 1:
				instantiateThisGame("Easy Pattern");
				break;
			case 2:
				instantiateThisGame("Medium Pattern");
				break;
			case 3:
				instantiateThisGame("Hard Pattern");
				break;
			}

			break;
		case "TorF":
			instantiateThisGame("TorFPrefab");
			break;
		case "Scroll":
			instantiateThisGame("TapPrefab");
			break;
		default:
			Debug.Log("The Game Type was not selected");
			break;
		

		

		}
	}

	public void createGameButton()
	{
		creategame = true;
		GamePanel.SetActive (true);


	}//end creategamebutton

	public void setgameButton(string gametype)
	{

		gameType = gametype;

		if (gametype == "Math") {
			GamePanel.SetActive (false);
			OperationsPanel.SetActive (true);
		}else if(gametype == "TorF" || gametype == "Scroll"){
			GamePanel.SetActive(false);
			JoinRoom();
		} else {
			GamePanel.SetActive (false);
			DifficultyPanel.SetActive (true);
		}

	}
	public void setOperationButton(int opNum)
	{
		
		operation = opNum;
		OperationsPanel.SetActive (false);
		DifficultyPanel.SetActive (true);
		
	}
	public void setDiffButton(int diffNum)
	{
		
		difficulty = diffNum;

		DifficultyPanel.SetActive(false);

		JoinRoom ();
		
	}

	public void instantiateThisGame(string gamename)
	{
		PhotonNetwork.Instantiate (gamename, new Vector3 (0, 0, 0), Quaternion.identity, 0);
	}

	public void backgamebutton(){
		GamePanel.SetActive (false);
	}
	public void backopbutton(){
		OperationsPanel.SetActive (false);
		GamePanel.SetActive (true);
	}
	public void backdiffbutton(){
		if (gameType != "Math") {
			DifficultyPanel.SetActive (false);
			GamePanel.SetActive (true);
		} else {
			DifficultyPanel.SetActive (false);
			OperationsPanel.SetActive (true);
		}
	}




	

}//end class
