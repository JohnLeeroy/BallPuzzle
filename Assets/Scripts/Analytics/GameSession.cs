using Splyt;
using System.Collections.Generic;

public class GameSession {

	Transaction gameSession;

	// Use this for initialization
	public void start (string gameMode = "Arcade") {
		gameSession = Splyt.Instrumentation.Transaction("Game");
		gameSession.begin ();
		gameSession.setProperty ("Mode", gameMode);
	}

	public void end(ref Dictionary<string, object> data)
	{
		if (gameSession == null)	//is null if starting game in arcade scene
			return;

		gameSession.setProperties (data);
		gameSession.end ();
	}

	public void end(bool isWin, int round, int score, int tries){
		if (gameSession == null)	//is null if starting game in arcade scene
			return;
		gameSession.setProperty ("Result", (isWin) ? 1 : 0);
		gameSession.setProperty ("Round", round);
		gameSession.setProperty ("Score", score);
		gameSession.setProperty ("Tries", tries);
		gameSession.end ();
	}
}
