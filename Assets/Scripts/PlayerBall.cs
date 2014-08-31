using UnityEngine;
using System.Collections;

public class PlayerBall : Ball 
{	
	#region public override void Start()
	public override void Start () 
	{
		bGrowing		= false;
		bCanGrow		= true;

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
