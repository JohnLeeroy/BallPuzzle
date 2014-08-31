using UnityEngine;
using System.Collections;

public class ScoreSystem : MonoBehaviour {

	public int baseBubbleValue = 100;
	public int[] chainComboMultipliers;

	int chainCounter = 0;	//number of chained bubbles per SINGLE touch
	int touchIndex = 0;  //counts the number of spawning taps

	int score = 0;

	void Start () {
		NotificationCenter.DefaultCenter.AddObserver(this, "OnSpawnPlayerBubble");
		NotificationCenter.DefaultCenter.AddObserver(this, "OnBubblePop");
		
	}

	IEnumerator CR_ComboListener()
	{
		int startTouchIndex = touchIndex;
		while (startTouchIndex == touchIndex) {

			yield return 0;
		}
		chainCounter = 0;	//reset chain counter on new touch
	}

	void OnSpawnPlayerBubble()
	{
		touchIndex++;
	}

	void OnBubblePop()
	{
		chainCounter++;
	}

}
