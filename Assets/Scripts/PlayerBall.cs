using UnityEngine;
using System.Collections;

public class PlayerBall : Ball 
{	
	// Use this for initialization
	public override void Start () 
	{
		fGrowRate 	= 0.01f;
		fMaxSize	= 5.0f;

		bGrowing	= false;

		StartCoroutine( "StartGrowing" );
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
		if( other.gameObject.tag == "Ball" )
		{
			other.gameObject.GetComponent<Ball>().StartCoroutine( "StartGrowing" );
		}
	}
}
