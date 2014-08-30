using UnityEngine;
using System.Collections;

public class PlayerBall : Ball 
{	
	#region public override void Start()
	public override void Start () 
	{
		fGrowRate 		= 1.0f;
		fMaxSize		= 5.0f;
		fDestroyDelay	= 0.2f;
		
		bGrowing		= false;

		StartCoroutine( base.StartGrowing() );
	}
	#endregion

	#region public override IEnumerator StartGrowing()
	public override IEnumerator StartGrowing()
	{
		base.StartGrowing();
		yield return null;
	}
	#endregion

	#region public override void OnTriggerEnter( Collider other )
	public override void OnTriggerEnter( Collider other )
	{
		base.OnTriggerEnter( other );
	}
	#endregion

	#region public override void DestroyBall()
	public override void DestroyBall()
	{
		base.DestroyBall();
	}
	#endregion
}
