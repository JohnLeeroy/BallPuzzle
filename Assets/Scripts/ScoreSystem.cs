using UnityEngine;
using System.Collections;

public class ScoreSystem : MonoBehaviour {
	
	public static int highestCombo = 0;	//used for analytics

	public int baseBubbleValue = 100;
	public int baseLifeLeftoverBonus = 1000;

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
		if (chainCounter > highestCombo)
			highestCombo = chainCounter;

		setScore (score + baseBubbleValue * chainComboMultipliers [chainCounter]);
	}

	public void setScore(int newScore)
	{
		score = newScore;
		Hashtable data = new Hashtable();
		data ["score"] = score;
		NotificationCenter.DefaultCenter.PostNotification (this, "UpdatedScore", data);
	}

	public void applyLifeLeftoverBonus(int livesLeft)
	{
		if (livesLeft <= 0)
			return;

		setScore (score + livesLeft * baseLifeLeftoverBonus);
	}
	
	void OnLevelWasLoaded()
	{
		//Debug.Log ("Setting score to ... " + GameManager.getInstance ().Score);
		//setScore (GameManager.getInstance ().Score);
	}
}
