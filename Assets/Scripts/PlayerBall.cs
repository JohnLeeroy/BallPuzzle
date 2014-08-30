using UnityEngine;
using System.Collections;

public class PlayerBall : Ball 
{	
	// Use this for initialization
	public override void Start () 
	{
		fGrowRate 	= 1.0f;												// Speed at which the ball grows	
		fMaxSize	= 5.0f;												// Maximum size the ball can be
											
		bGrowing	= false;											// Is the ball currently growing	

		StartCoroutine( base.StartGrowing() );
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	public override IEnumerator StartGrowing()
	{
		base.StartGrowing();
		yield return null;
	}

	public override void OnTriggerEnter( Collider other )
	{
		base.OnTriggerEnter( other );
	}
}
