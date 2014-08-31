using UnityEngine;
using System.Collections;

public class MainMenuButton : MonoBehaviour 
{	
	void Update () 
	{
		if( Input.GetMouseButtonUp( 0 ) )
		{
			switch( gameObject.name )
			{
			case "StartButton":
				Application.LoadLevel( "Arcade" );
				break;
			case "LeaderboardButton":
				Application.LoadLevel( "Highscores" );
				break;
			case "CreditsButton":
				Application.LoadLevel( "Credits" );
				break;
			case "RetryButton":
				Debug.Log( "Retrying" );
				break;
			}
		}
	}
}
