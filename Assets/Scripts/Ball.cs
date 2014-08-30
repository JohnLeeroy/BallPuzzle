using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour 
{
	public float fGrowRate;
	public float fMaxSize;

	// Use this for initialization
	void Start () 
	{
		fGrowRate 	= 0.01f;
		fMaxSize	= 5.0f;

		NotificationCenter.DefaultCenter.AddObserver( this, "StartGrowing" );
	}

	void OnDisable()
	{
		NotificationCenter.DefaultCenter.RemoveObserver( this, "StartGrowing" );
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public IEnumerator StartGrowing()
	{
		while( transform.localScale.x < fMaxSize )
		{
			transform.localScale += new Vector3( fGrowRate, fGrowRate, fGrowRate );
			yield return null;
		}
	}
}
