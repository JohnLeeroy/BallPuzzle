using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LeaderboardModal : MonoBehaviour {

	public class ScoreRecord
	{
		public int rank;
		public int score;
		public string name;
	}

	public LeaderboardRow[] rows;

	List<ScoreRecord> records;
	// Use this for initialization
	void Start () {
		IList data = FileScript.GetHighscores();
		int i = 0;
		records = new List<ScoreRecord> ();

		ScoreRecord record;
		LeaderboardRow row;
		foreach(IDictionary scoreData in data)
		{
			record = new ScoreRecord();
			record.rank = i + 1;
			record.score = int.Parse(scoreData["Score"].ToString());
			record.name = scoreData["Initials"].ToString();
			records.Add(record);

			row = rows[i];
			row.rank.text = record.rank.ToString();
			row.name.text = record.name;
			row.score.text = record.score.ToString();

			i++;
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
