using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour 
{
	public static InputController instance;
	public Vector3 touchPosition = Vector3.zero;
	public bool    isTouched = false;


	// Use this for initialization
	void Awake () 
	{
		instance = this;
	}
	
	// Update is called once per frame
	void Update () 
	{
		isTouched = false;
		if(Input.GetMouseButtonUp(0))
		{
			isTouched = true;
			touchPosition = Input.mousePosition;
		}
	}
}
