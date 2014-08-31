using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour 
{
	public float fGrowRate = 1.0f;										// Speed at which the ball grows
	public float fMaxSize = 5.0f;										// Maximum size the ball can be
	public float fDestroyDelay = .2f;									// Time between the ball reaching max growth and being destroyed

	public bool bGrowing;												// Is the ball currently growing
	public bool bCanGrow;												// Is the ball allowed to grow

	public float fSpeed = 10;
	
	Vector3 dir;

	private static int counter = 0;
	public virtual void Start () 
	{
		name = "Ball " + counter;
		counter++;
		bGrowing		= false;
		bCanGrow		= false;
		startMoving ();
		startRotating ();
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
		dir = new Vector3 (Random.Range (-1f, 1f), Random.Range (-1f, 1f), 0);

		while (!bGrowing) {
			transform.position += fSpeed * Time.deltaTime * dir;
			yield return 0;
		}
	}
	public void  startRotating()
	{
		StartCoroutine("Rotating");
	}
	IEnumerator Rotating()
	{
		while(!bGrowing)
		{
			transform.Rotate (fSpeed,fSpeed,fSpeed);
			yield return 0;
		}
	}
	public virtual void OnTriggerEnter( Collider other )
	{
		// Make other ball start growing
		if (other.gameObject.tag == "Ball" && gameObject.tag == "PlayerBall" ) 
		{
			other.gameObject.GetComponent<Ball>().SetCanGrow( true );
			//Debug.Log (gameObject.name + " HIT BALL " + other.gameObject.name);
			other.gameObject.GetComponent<Ball>().StartGrowth();
		} 
		else if (other.gameObject.tag == "VWall") 
		{
			dir.x *= -1;
			AudioManager.getInstance().Play(2);
		} 
		else if (other.gameObject.tag == "HWall") 
		{
			dir.y *= -1;
			AudioManager.getInstance().Play(2);
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
		if (GameManager.isGameOver)
			return;
		NotificationCenter.DefaultCenter.PostNotification(this, "OnBubblePop");
	}
}
