using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Splyt;

public class Analytics : MonoBehaviour {

	private static Analytics instance;

	public static Analytics Instance{ get { return instance; } }

	EntityInfo user;
	EntityInfo device;
	static string scene;
	
	void Awake()
	{
		if (instance != null) {
			Destroy(gameObject);
			return;
		}
		instance = this;

		initUserInfo ();
		initDeviceInfo ();
		//personal17153-neonburst-prod
		Splyt.InitParams initParams = Splyt.InitParams.create(
			"personal17153-neonburst-test",
			device,
			user// (required) Customer ID from the Splyt team.  If you don't have one, contact them.
			//,userInfo: Splyt.EntityInfo.createUserInfo("joe")		// (optional) Only necessary if user info is known at startup, otherwise use registerUser later
			//,logEnabled: true										// (optional) Typically only set to true during development
			);

		Core.init(initParams, delegate(Error initError) 
		                {
			if(Error.Success == initError)
				Debug.Log("onSplytInitComplete: " + initError.ToString());
			else
				Debug.LogError("onSplytInitComplete: " + initError.ToString());
		});

		Splyt.Plugins.Session.Transaction().begin();
		DontDestroyOnLoad (gameObject);

		scene = Application.loadedLevelName;
		Splyt.Instrumentation.Transaction(scene).begin();
	}

	void initUserInfo()
	{
		user = EntityInfo.createUserInfo (SystemInfo.deviceUniqueIdentifier);
	}

	void initDeviceInfo()
	{
		Dictionary<string, object> data = new Dictionary<string, object> ();
		data ["Model"] = SystemInfo.deviceModel;
		data ["Type"] = SystemInfo.deviceType;
		data ["OS"] = SystemInfo.operatingSystem;

		data ["Graphics"] = SystemInfo.graphicsDeviceName;
		data ["GraphicsVersion"] = SystemInfo.graphicsDeviceVersion;

		data ["ProcessorType"] = SystemInfo.processorType;
		device = EntityInfo.createDeviceInfo (data);
	}

	void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			Splyt.Core.pause();
			Splyt.Plugins.Session.Transaction ().end ();
		}
		else
		{
			Splyt.Core.resume();
			Splyt.Plugins.Session.Transaction().begin();
		}
	}

	//		
	void OnApplicationQuit()
	{
		Splyt.Plugins.Session.Transaction ().end ();
		/*
		Splyt.Instrumentation.Transaction("game")
			.setProperties(new Dictionary<string, object> {
				{ "numberOfMisses", this.mMisses },
				{ "didWin", won },
				{ "winQuality", _GetScore() }
			}).end();
			*/

		//Splyt.Instrumentation.
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

	void OnLevelWasLoaded(int level) {
		if(scene.Equals(Application.loadedLevelName))
			return;

		Splyt.Instrumentation.Transaction(scene).end();

		scene = Application.loadedLevelName;
		Splyt.Instrumentation.Transaction(scene).begin();
	}
}
