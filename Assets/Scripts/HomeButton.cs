using UnityEngine;
using System.Collections;

public class HomeButton : MonoBehaviour {

	
	void Update () {
		if (Input.GetMouseButton (0)) {
			if(guiTexture.GetScreenRect().Contains(Input.mousePosition))
				Application.LoadLevel(0); //main menu
		}
	}
}
