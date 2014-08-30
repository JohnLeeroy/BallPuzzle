using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour 
{
	public float fGrowRate;												// Speed at which the ball grows
	public float fMaxSize;												// Maximum size the ball can be
	public float fDestroyDelay;											// Time between the ball reaching max growth and being destroyed

	public bool bGrowing;												// Is the ball currently growing
	public bool bCanGrow;												// Is the ball allowed to grow

	public float fSpeed = 10;
	
	Vector3 dir;

	public virtual void Start () 
	{
		fGrowRate 		= 1.0f;
		fMaxSize		= 5.0f;
		fDestroyDelay	= 0.2f;

		bGrowing		= false;
		bCanGrow		= false;
		startMoving ();
	}

	public virtual IEnumerator StartGrowing()
	{
		// If the ball hasn't yet started growing
		if( bGrowing == false )
		{
			bGrowing = true;

			// Increase the scale of the ball
			while( transform.localScale.x < fMaxSize )
			{
				transform.localScale += new Vector3( fGrowRate, fGrowRate, fGrowRate ) * Time.deltaTime;
				yield return null;
			}

			// Clamp the ball size to the max size
			if( transform.localScale.x > fMaxSize || transform.localScale.y > fMaxSize || transform.localScale.z > fMaxSize )
				transform.localScale = new Vector3( fMaxSize, fMaxSize, fMaxSize );

			// Broadcast that the ball is being destroyed
			NotificationCenter.DefaultCenter.PostNotification(this, "OnDestroyBall");

			// Destroy the ball
			DestroyBall();
		}
	}

	public void StartGrowth()
	{
		if( bCanGrow )
			StartCoroutine( StartGrowing() );
	}

	public void startMoving()
	{
		StartCoroutine (Moving ());
	}

	IEnumerator Moving()
	{
		dir = new Vector3 (Random.Range (0f, 1f), Random.Range (0f, 1f), 0);

		while (!bGrowing) {
			transform.position += fSpeed * Time.deltaTime * dir;
			yield return 0;
		}
	}

	public virtual void OnTriggerEnter( Collider other )
	{
		// Make other ball start growing
		if (other.gameObject.tag == "Ball" && gameObject.tag == "PlayerBall" ) 
		{
			other.gameObject.GetComponent<Ball>().SetCanGrow( true );
			Debug.Log (gameObject.name + " HIT BALL " + other.gameObject.name);
			other.gameObject.GetComponent<Ball>().StartGrowth();
		} 
		else if (other.gameObject.tag == "VWall") 
		{
			dir.x *= -1;
		} 
		else if (other.gameObject.tag == "HWall") 
		{
			dir.y *= -1;
		}
	}

	public virtual void DestroyBall()
	{		
		// Destroy the ball after destroy seconds
		Destroy( gameObject, fDestroyDelay );
	}

	public void SetCanGrow( bool b )
	{
		bCanGrow = b;
		gameObject.tag = "PlayerBall";
	}

	public void OnDestroy()
	{
		if( gameObject.name != "PlayerBall(Clone)" )
		{
			GameObject.Find( "ProgressBar" ).GetComponent<ProgressBar>().UpdatePercentage();
		}
	}
}
