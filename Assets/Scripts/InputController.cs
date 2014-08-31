using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour 
{
	public static InputController instance;
	public Vector3 touchPosition = Vector3.zero;
	public bool    isTouched = false;

	bool enableTouch = true;
	// Use this for initialization
	void Awake () 
	{
		instance = this;
	}

	void Start()
	{
		NotificationCenter.DefaultCenter.AddObserver (this, "Resume");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!enableTouch)
			return;

		isTouched = false;
		if(Input.GetMouseButtonUp(0))
		{
			isTouched = true;
			touchPosition = Input.mousePosition;
		}
	}

	void Resume()
	{
		StartCoroutine (CR_DelayInputHandler ());
	}

	//Wait one frame before enabling touch so it doesnt register the press of the "resume" button
	IEnumerator CR_DelayInputHandler()
	{
		isTouched = false;
		enableTouch = false;
		yield return 0;
		enableTouch = true;
	}


}
