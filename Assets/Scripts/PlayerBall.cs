using UnityEngine;
using System.Collections;

public class PlayerBall : Ball 
{	
	// Use this for initialization
	public override void Start () 
	{
		fGrowRate 		= 1.0f;
		fMaxSize		= 5.0f;
		fDestroyDelay	= 2.0f;
		
		bGrowing		= false;

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

	public override void DestroyBall()
	{
		base.DestroyBall();
	}
}
