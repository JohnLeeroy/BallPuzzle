using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour 
{
	public float 	fPercentComplete = 0.0f;
	public int		iTotalBalls;
	public int 	iBallsDestroyed = 0;

	// Use this for initialization
	void Start () 
	{
		iTotalBalls = GameObject.Find( "LevelManager" ).GetComponent<LevelManager>().ballsCount;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdatePercentage()
	{
		iBallsDestroyed++;
		fPercentComplete = iBallsDestroyed / (float)iTotalBalls;
	}
}
