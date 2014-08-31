using UnityEngine;
using System.Collections;

public class PlayerBall : Ball 
{	
	public override void Start () 
	{
		bGrowing		= false;
		bCanGrow		= true;

		StartCoroutine( base.StartGrowing() );
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

	void onDestroy()
	{
		NotificationCenter.DefaultCenter.PostNotification (this, "OnPlayerBubblePop");
	}
}
