using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Clicker : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            Ray toMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rhInfo;
            bool didHit = Physics.Raycast(toMouse, out rhInfo);
            if (didHit)
            {
                Debug.Log(rhInfo.transform.name + " " + rhInfo.point);
            }
            else
            {
                Debug.Log("Clicked on empty space");
            }
        }
            
    }
}
