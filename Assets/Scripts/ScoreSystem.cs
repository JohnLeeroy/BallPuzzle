using UnityEngine;
using System.Collections;

public class ScoreSystem : MonoBehaviour {

	public int baseBubbleValue = 100;
	public int[] chainComboMultipliers;

	int chainCounter = 0;	//number of chained bubbles per SINGLE touch
	int touchIndex = 0;  //counts the number of spawning taps

	public int score = 0;

	IEnumerator Start () {
		NotificationCenter.DefaultCenter.AddObserver(this, "OnSpawnPlayerBubble");
		NotificationCenter.DefaultCenter.AddObserver(this, "OnBubblePop");
		NotificationCenter.DefaultCenter.AddObserver (this, "OnPlayerBubblePop");

		yield return 0;
		setScore (score); // update UI
	}

	void OnSpawnPlayerBubble()
	{
		touchIndex++;
		chainCounter = 0;
	}

	void OnPlayerBubblePop()
	{

	}

	void OnBubblePop()
	{
		chainCounter++;
		//score += baseBubbleValue * chainComboMultipliers [chainCounter];
		setScore (score + baseBubbleValue * chainComboMultipliers [chainCounter]);
		//Debug.Log ("Score: " + score);
	}

	public void setScore(int newScore)
	{
		score = newScore;
		Hashtable data = new Hashtable();
		data ["score"] = score;
		NotificationCenter.DefaultCenter.PostNotification (this, "UpdatedScore", data);
	}

	
	void OnLevelWasLoaded()
	{
		//Debug.Log ("Setting score to ... " + GameManager.getInstance ().Score);
		//setScore (GameManager.getInstance ().Score);
	}
}
