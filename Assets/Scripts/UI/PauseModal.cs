using UnityEngine;
using System.Collections;

public class PauseModal : MonoBehaviour {

	
	Rect background;
	Rect replayBtn;
	Rect resumeBtn;

	bool isEnabled = false;
	// Use this for initialization
	void Start () {
		float screenWidth = Screen.width;
		float screenHeight = Screen.height;

		background = new Rect (screenWidth * .3f, Screen.height * .3f, screenWidth * .4f, screenHeight * .4f);
		replayBtn = new Rect (background.min.x + background.width * .25f - 30, 
		                     background.min.y + background.height * .75f - 30, 60, 60);
		resumeBtn = new Rect (background.min.x + background.width * .75f - 30, 
		                      background.min.y + background.height * .75f - 30, 60, 60);

		NotificationCenter.DefaultCenter.AddObserver (this, "Pause");
		StartCoroutine (Test_Pause ());
	}

	IEnumerator Test_Pause()
	{
		yield return new WaitForSeconds (5);
		NotificationCenter.DefaultCenter.PostNotification (this, "Pause");
	}
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		if (!isEnabled)
			return;

		GUI.Box (background, "");

		if (GUI.Button (replayBtn, "Replay")) {

		}
		
		if (GUI.Button (resumeBtn, "Resume")) {
			NotificationCenter.DefaultCenter.PostNotification(this, "Resume");
			isEnabled = false;
		}
	}

	void Pause()
	{
		isEnabled = true;
	}
}
