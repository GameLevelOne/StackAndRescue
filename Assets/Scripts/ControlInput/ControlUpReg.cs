using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControlUpReg : MonoBehaviour {
    UserInput userInput;
    Transform targetControl;

    // Use this for initialization
    void Start () {
        userInput = GetComponentInParent<UserInput>();
	}
	
	// Update is called once per frame
	void Update () {
		if(userInput.testPos != null)
            {
                targetControl = userInput.testPos;
            }
	}

    void OnMouseDown() {
        if(targetControl != null)
            { 
                if(!EventSystem.current.IsPointerOverGameObject())
                    {
                        targetControl.Rotate(0, 0, 90);
                    }
            }
    }
}
