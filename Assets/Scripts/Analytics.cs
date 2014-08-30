using UnityEngine;
using System.Collections;

public class Analytics : MonoBehaviour {

	void Awake()
	{
		Splyt.InitParams initParams = Splyt.InitParams.create(
			"personal17153-test"								// (required) Customer ID from the Splyt team.  If you don't have one, contact them.
			//,userInfo: Splyt.EntityInfo.createUserInfo("joe")		// (optional) Only necessary if user info is known at startup, otherwise use registerUser later
			//,logEnabled: true										// (optional) Typically only set to true during development
			);

		Splyt.Core.init(initParams, delegate(Splyt.Error initError) 
		                {
			if(Splyt.Error.Success == initError)
				Debug.Log("onSplytInitComplete: " + initError.ToString());
			else
				Debug.LogError("onSplytInitComplete: " + initError.ToString());
			

			/*
			// in this contrived case, we learn about the user just after init - generally this would be triggered by a user action (login?)
			Splyt.EntityInfo user = Splyt.EntityInfo.createUserInfo(
				"joe",
				properties: new Dictionary<string, object> { { "funguy", true }, { "favorite team", "Sweepers" } }
			);
			
			Splyt.Core.registerUser(user, delegate(Splyt.Error registerError) {
				if(Splyt.Error.Success == registerError)
					Debug.Log("onSplytRegisterUserComplete: " + registerError.ToString());
				else
					Debug.LogError("onSplytRegisterUserComplete: " + registerError.ToString());
				
				OnGameReady();
			});
			*/
		});

		Splyt.Instrumentation.Transaction("game").begin();
		Splyt.Plugins.Session.Transaction().begin();
		
	}

	void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			Splyt.Core.pause();
		}
		else
		{
			Splyt.Core.resume();
		}
	}

	//		
	void OnApplicationQuit()
	{
		/*
		Splyt.Instrumentation.Transaction("game")
			.setProperties(new Dictionary<string, object> {
				{ "numberOfMisses", this.mMisses },
				{ "didWin", won },
				{ "winQuality", _GetScore() }
			}).end();
			*/
	}

	void PurchaseExample()
	{
		//Purchase Example
		//Splyt.Plugins.Purchase.Transaction(mPurchaseId).end();
		//Splyt.Plugins.Purchase.Transaction(mPurchaseId).end(Splyt.Constants.TXN_ERROR);

		/*
		// you don't NEED a purchase id unless multiple purchases may be occuring simulatenously, but it's included here to show the API
		mPurchaseId = System.Guid.NewGuid().ToString();
		
		Splyt.Plugins.Purchase.Transaction(mPurchaseId)
			.setPointOfSale("Main Menu")
				.begin();
		
		// let's assume we don't learn more about the item until after this purchase process has begun, for this case...  we can add
		//  more properties through an update
		Splyt.Plugins.Purchase.Transaction(mPurchaseId)
			.setPrice(1.99, "usd")
				.setOfferId("200gold_199")
				.setItemName("200 Gold")
				.update(1);
		
		mPurchasing = true;
		*/
	}

	private void GetVarExample()
	{
		//mGameCost = Splyt.Tuning.getVar("newGameCost", 100);
	}
}
