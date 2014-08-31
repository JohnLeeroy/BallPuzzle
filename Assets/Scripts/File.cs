using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class PlayerScore
{
	public string initials;
	public int score;
}

public class File : MonoBehaviour 
{


	public static void SaveScore( string name, int points )
	{
		Debug.Log( "Saving score" );

		List<Dictionary<string,string>> scores = new List<Dictionary<string,string>>();

		Dictionary<string,string> scoreData 	= new Dictionary<string,string>();
		scoreData["Initials"] 					= name;
		scoreData["Score"] 						= points.ToString();

		scores.Add(scoreData);

		StreamWriter writer = new StreamWriter( Application.dataPath + "/SaveData/Highscores.json" );

		string serial = MiniJSON.Json.Serialize(scores);

		print(serial);
		writer.WriteLine(serial);
		writer.Flush();
		writer.Close();
	}
}
