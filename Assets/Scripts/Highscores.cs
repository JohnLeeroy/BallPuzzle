using UnityEngine;
using System.Collections;

public class Highscores : MonoBehaviour
{
	private float fFontSizeRate = 0.05f;

	void Start()
	{
		int fontSize = (int)( fFontSizeRate * Screen.height );

		IList data = FileScript.GetHighscores();

		string rank = "Rank\n";
		string names = "Initials\n";
		string scores = "Score\n";
	
		int i = 0;
		
		foreach(IDictionary scoreData in data)
		{
			if( i < 9 )
			{
				rank += ( i + 1 ).ToString() + "\n";
				names += scoreData["Initials"] + "\n";
				scores += scoreData["Score"] + "\n";
			}
			else
			{
				rank += ( i + 1 ).ToString();
				names += scoreData["Initials"];
				scores += scoreData["Score"];
			}
			i++;
		}

		GameObject go = GameObject.Find( "Rank" );
		go.guiText.text = rank;
		go.guiText.fontSize = fontSize;

		go = GameObject.Find( "Names" );
		go.guiText.text = names;
		go.guiText.fontSize = fontSize;

		go = GameObject.Find( "Scores" );
		go.guiText.text = scores;
		go.guiText.fontSize = fontSize;
	}
	
	void Update()
	{
		if( Input.GetKeyDown( KeyCode.Return ) )
			FileScript.SaveScore( "---", 0 );
	}
}
