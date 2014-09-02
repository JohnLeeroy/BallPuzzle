using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LeaderboardModal : MonoBehaviour {

	public LeaderboardRow[] rows;
	Leaderboard leaderboard;

	void Start () {
		IList data = FileScript.GetHighscores();
		int i = 0;

		ScoreRecord record;
		LeaderboardRow row;

		leaderboard = Leaderboard.Instance;

		foreach(IDictionary scoreData in data)
		{
			record = leaderboard.records[i];
			row = rows[i];
			row.rank.text = record.rank.ToString();
			row.name.text = record.name;
			row.score.text = record.score.ToString();
			i++;
		}
	}
}
