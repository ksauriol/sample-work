using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using Hashtable = ExitGames.Client.Photon.Hashtable;

public class MathQuestionsMulti : MonoBehaviour {
	public GameObject Qpanel;
	public InputField AnswerIF;
	public string CurrentMenu = "Question";
	public string Log= "";
	public string opponentLog = "";
	public Text QText;
	public Text correctText;
	public Text wrongText; 
	public GameObject StarPanel, Wrongpanel, scorePanel, PictureQuestion;
	public bool copy=false;
	int numberCorrect=0;
	int numberWrong=0; 
	int Round=0;
	string Answer;
	string InputAnswer;
	int i = 1;
	public int diff = 1; //difficulty 1=easy 2=medium 3=hard
	public int operation = 0; // 0=random 1=addition 2=subtraction 3=multiplication 4=division
	GameObject LevelandOp;
	NetworkManager LevelandOpScript;
	PickupDemoGui guiScript; 
	int wins = 0;
	int loss = 0;
	float rates = 0.00f;
	
	public Text Score;
	public Text otherScore;
	public Text outcome;
	public GameObject ScorePanel;
	public GameObject Loading;
	public Text Loadingtext;
	int finalmycheckscore;
	int finalotherscore;


	//public bool thisIsTheEnd;
	//int myScore = 0;
	//int opponentScore = 0;
	
	int otherCheckScore;
	int myCheckScore;
	
	bool isredready;
	bool isblueready;

	
	float loadingtime = 0.0f;
	//GameObject go;
	//NetworkManager manager;
	
	int A;
	int B;
	int C;
	
	// Use this for initialization
	void Start () {
		 isredready = false;
		 isblueready = false;

		//thisIsTheEnd = false;
		Loading.SetActive (false);
		PictureQuestion.SetActive (false);
		scorePanel.SetActive(false);
		StarPanel.SetActive (false);
		doMath ();
		
		
		
		
	} //end start
	
	void Update()
	{
		loadingtime += Time.deltaTime;

		if (loadingtime >= 3.5) {
			StartCoroutine(waiting ());
			loadingtime = 0;
		}

		switch(PhotonNetwork.player.ID){
			
		case 1:
			
			//waitforOther(manager.redIsReady);
			
			foreach (PhotonPlayer player in PunTeams.PlayersPerTeam[PunTeams.Team.blue]) {
				
				
				myCheckScore = player.GetScore ();
				
				
			}//end for each
			
			foreach (PhotonPlayer player in PunTeams.PlayersPerTeam[PunTeams.Team.red]) {
				
				otherCheckScore = player.GetScore ();

				
			}//end for each
			
			if(otherCheckScore >= 10){
				Debug.Log ("Opponents Score: "+ otherCheckScore);
				isredready = true;
				Debug.Log ("Other is ready?:" + isredready);
				otherCheckScore -=10;
			}
			if(myCheckScore >= 10){

				//Debug.Log ("Other:" + isredready);
				myCheckScore -=10;
			}
			
			break;

		case 2:
			
			//waitforOther(manager.blueIsReady);
			
			foreach (PhotonPlayer player in PunTeams.PlayersPerTeam[PunTeams.Team.red]) {
				
				myCheckScore = player.GetScore ();
				
			}//end for each
			
			foreach (PhotonPlayer player in PunTeams.PlayersPerTeam[PunTeams.Team.blue]) {
				
				otherCheckScore = player.GetScore ();
				
			}//end for each
			
			if(otherCheckScore >= 10){

				Debug.Log ("Opponents Score: "+ otherCheckScore);
				isblueready = true;
				Debug.Log ("Other is ready?:" + isblueready);
				otherCheckScore -=10;
			}
			if(myCheckScore >= 10){
	
				//Debug.Log ("Other:" + isredready);
				myCheckScore -=10;
			}
			
			break;
			
		}//end switch
		//Debug.Log (myCheckScore);
		//Debug.Log (otherCheckScore);
		waitforother ();
		
		
		
	}//end update()
	
	
	public void goEasy(){//change game mode difficulty
		diff = 1;
		Debug.Log ("Easy Mode Set");
	}
	public void goMedium(){//change game mode difficulty
		diff = 2;
		Debug.Log ("Medium Mode Set");
	}
	public void goHard(){//change game mode difficulty
		diff = 3;
		Debug.Log ("Hard Mode Set");
	}
	
	public void doMath(){
		if (copy) {
			string[] Section = opponentLog.Split(' ');
			string[] Instruction = Section[Round].Split (',');
			doMathWithInput (int.Parse(Instruction[0]),int.Parse(Instruction[1]),int.Parse(Instruction[2]));
			Round++;
		} else {
			if (operation>0){
				switch (operation) {
				case 1:
					Addition ();
					break;
				case 2:
					Subtraction ();
					break;
				case 3:
					Multiplication ();
					break;
				case 4:
					Division ();
					break;
				default:
					Debug.Log ("Wrong Mode");
					break;
				}
			} else if (operation==0){
				int mode = Random.Range (1, 5);
				switch (mode) {
				case 1:
					Addition ();
					break;
				case 2:
					Subtraction ();
					break;
				case 3:
					Multiplication ();
					break;
				case 4:
					Division ();
					break;
				default:
					//Debug.Log ("Wrong Mode");
					break;
				}
			}
		}
	}// end do math
	
	public void setQuiz(string doThis){//sets the quiz to be taken
		opponentLog = doThis;
		copy = true;
	}
	
	public void doMathWithInput(int mode, int first, int second){
		int result=0;
		//double division=0;
		string operation="";
		switch (mode) {
		case 1:result=first+second;operation=" + ";break;
		case 2:result=first-second;operation=" - ";break;
		case 3:result=first*second;operation=" * ";break;
		case 4:result=first*second;operation=" / ";break;
		default: Debug.Log ("Bad Input in doMathWithInput");break;
		}
		if ((mode > 0) && (mode < 4)) {//modes 1 through 3
			InputAnswer = result.ToString ();
			QText.text = first + operation + second + " = ";
			Log += mode +","+ first +","+ second + " ";
		} else if (mode == 4) {//division
			//InputAnswer = string.Format ("{0:0.00}", division);
			InputAnswer = second.ToString ();
			//Debug.Log ("Must input in format 0.00 For Example: 2 must be put as 2.00");
			QText.text = result + " / " + first + " = ";
			Log += "4," + first +","+ second + " ";
		}
	}
	
	
	void Division(){
		int n = 0;
		int m = 0;
		//double D;
		switch (diff) {
		case 1: n=10;m=1;break;
		case 2: n=15;m=5;break;
		case 3: n=20;m=10;break;
		}
		A = Random.Range(m,n);
		B = Random.Range(m,n);
		C = A * B;
		//D = (double)A / B;
		//InputAnswer = string.Format ("{0:0.00}", D);
		//Debug.Log ("Must input in format 0.00 For Example: 2 must be put as 2.00");
		QText.text = C + " / " + A + " = ";
		Log += "4," + A +","+ B + " ";
		InputAnswer = B.ToString ();
		//Debug.Log (Log);
	}
	
	void Multiplication(){
		int n = 0;
		int m = 0;
		switch (diff) {
		case 1: n=10;m=1;break;
		case 2: n=15;m=5;break;
		case 3: n=20;m=10;break;
		}
		A = Random.Range(m,n);
		B = Random.Range(m,n);
		C = A * B;
		InputAnswer = C.ToString ();
		QText.text = A + " * " + B + " = ";
		Log += "3," + A +","+ B + " ";
		//Debug.Log (Log);
	}
	
	void Subtraction(){
		int n = 0;
		int m = 0;
		switch (diff) {
		case 1: n=10;m=1;break;
		case 2: n=100;m=10;break;
		case 3: n=1000;m=100;break;
		}
		A = Random.Range(m,n);
		B = Random.Range(m,n);
		C = A - B;
		InputAnswer = C.ToString ();
		QText.text = A + " - " + B + " = ";
		Log += "2," + A +","+ B + " ";
		//Debug.Log (Log);
	}
	
	void Addition(){
		int n = 0;
		int m = 0;
		switch (diff) {
		case 1: n=10;m=1;break;
		case 2: n=100;m=10;break;
		case 3: n=1000;m=100;break;
		}
		A = Random.Range(m,n);
		B = Random.Range(m,n);
		C = A + B;
		InputAnswer = C.ToString ();
		QText.text = A + " + " + B + " = ";
		Log += "1," + A +","+ B + " ";
		//Debug.Log (Log);
	}//end Addition
	
	public void Submit(){
		if (i < 5) {	
			Answer = AnswerIF.text;
			if (Answer == InputAnswer) {
				//Debug.Log ("Correct");
				numberCorrect++;
				StartCoroutine (Correct ());
			} else {
				//Debug.Log ("Wrong");
				numberWrong++;
				StartCoroutine (Wrong ());
			}//end else
			i++;
			doMath ();
		} else {
			Answer = AnswerIF.text;
			if (Answer == InputAnswer) {
				//Debug.Log ("Correct");
				numberCorrect++;
				StartCoroutine (Correct ());
			} else {
				//Debug.Log ("Wrong");
				numberWrong++;
				StartCoroutine (Wrong ());
			}//end else

			Log += numberCorrect;
			float time = Timer.getTimeNow ();
			Timer.setTimeNow ();
			Timer.stopNow ();
			Log += " " + time;

			Log = "";
			opponentLog = "";
			copy = false;
			operation = 0;
			correctText.text = "My Correct: " + numberCorrect;
			wrongText.text = "Opponent Correct: " + numberWrong;

			//PhotonNetwork.player.AddScore(-(int)time);
			
			if(PhotonNetwork.player.ID == 1){

				PhotonNetwork.player.AddScore(10);
				isblueready = true;
				Loading.SetActive(true);
				
			}else{
				PhotonNetwork.player.AddScore(10);
				isredready = true;
				Loading.SetActive(true);
			}
			

		}//end else
		AnswerIF.text = "";
	}// end Submit
	
	private IEnumerator Correct(){
		PhotonNetwork.player.AddScore(1);
		StarPanel.SetActive (true);
		yield return new WaitForSeconds (2);
		StarPanel.SetActive (false);
	}
	private IEnumerator Wrong(){
		Wrongpanel.SetActive (true);
		yield return new WaitForSeconds (2);
		Wrongpanel.SetActive (false);
	}
	

	private IEnumerator restart(){
		
		yield return new WaitForSeconds (5);
		Application.LoadLevel(2);
		
	}
	
	private IEnumerator waiting(){
		
		Loadingtext.text = "Waiting";
		yield return new WaitForSeconds (0.5f);
		Loadingtext.text = "Waiting.";
		yield return new WaitForSeconds (0.5f);
		Loadingtext.text = "Waiting..";
		yield return new WaitForSeconds (0.5f);
		Loadingtext.text = "Waiting...";
		yield return new WaitForSeconds (0.5f);
		
	}
	
	
	public void showTheScore(){

		ScorePanel.SetActive(true);
		Loading.SetActive (false);

		if (!PhotonNetwork.inRoom) {
			return;
		}//end if not in network
		else{
			
			switch(PhotonNetwork.player.ID){
				
			case 1:
					finalmycheckscore = myCheckScore;
					Score.text = "Your Score: " + finalmycheckscore;
					
					
				foreach (PhotonPlayer player in PunTeams.PlayersPerTeam[PunTeams.Team.red]) {
					finalotherscore = otherCheckScore;
					otherScore.text = player.name + "'s score: "+ finalotherscore;


				}//end for each
				
				whoWon(myCheckScore, otherCheckScore);
				//PhotonNetwork.player.SetScore(0);
				
				break;
				
			case 2:

				finalmycheckscore = myCheckScore;
				Score.text = "Your Score: " + finalmycheckscore;

				
				foreach (PhotonPlayer player in PunTeams.PlayersPerTeam[PunTeams.Team.blue]) {

					finalotherscore = otherCheckScore;
					otherScore.text = player.name + "'s score: "+ finalotherscore;


				}//end for each
				
				whoWon(myCheckScore, otherCheckScore);
				//PhotonNetwork.player.SetScore(0);
				break;
				
			}//end switch
			
			isredready = false;
			isblueready = false;
			StartCoroutine(returnToLobby());

			
		}//end else
	}//end method
	
	void whoWon(int mine, int opponent){
		if (mine > opponent) {
			wins = PlayerPrefs.GetInt("wins",0);
			wins+=1;
			PlayerPrefs.SetInt("wins", wins);
			loss = PlayerPrefs.GetInt("loss",1);
			rates = (float)wins/(float)loss;
			PlayerPrefs.SetFloat("rates", rates);
			Debug.Log("wins" + wins);
			Debug.Log("loss" + loss);
			Debug.Log ("rates" + rates);
			//you win
			outcome.text = "You WIN!";
		} else if (opponent == mine) {
			//tie game
			outcome.text = "You TIED!";
		} else {
			loss = PlayerPrefs.GetInt("loss",0);
			loss +=1;
			PlayerPrefs.SetInt("loss", loss);
			wins = PlayerPrefs.GetInt("wins");
			rates = (float)wins/(float)loss;
			PlayerPrefs.SetFloat("rates", rates);
			Debug.Log ("loss" + loss);
			Debug.Log ("wins" + wins);
			Debug.Log ("rates" + rates);
			//you lost
			outcome.text = "You LOST!";
		}
	}//end whowon
	
	void waitforother (){
		if (isredready == true && isblueready == true) {

			showTheScore ();
		}//if showscoer
	}
	private IEnumerator returnToLobby()
	{

			yield return new WaitForSeconds (2);
			PhotonNetwork.player.SetScore (0);
			PhotonNetwork.LeaveRoom ();


	}//end returnToLobby
	

	void OnMasterClientSwitched()
	{

		PhotonNetwork.player.SetScore (0);
		PhotonNetwork.LeaveRoom ();

	}
	

	
	
	
	
	
}//end class