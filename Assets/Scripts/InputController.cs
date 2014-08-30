using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour 
{
	public Vector3 touchPosition = Vector3.zero;
	public bool    isTouched = false;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.touchCount > 0)
		{
			isTouched = true;
			touchPosition = Input.touches[0].position;
		}
		else
		{
			isTouched = false;
		}
	}
}
