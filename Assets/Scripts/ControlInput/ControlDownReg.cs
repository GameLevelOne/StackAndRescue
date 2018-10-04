using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControlDownReg : MonoBehaviour {
    public float gravDef, gravPress;

    UserInput userInput;
    Transform targetControl;

    // Use this for initialization
    void Start () {
        userInput = GetComponentInParent<UserInput>();
        gravDef = .25f;
        gravPress = 2f;
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
                        targetControl.GetComponent<Rigidbody2D>().gravityScale = gravPress;
                    }
            }
    }

    void OnMouseUp() {
        if(targetControl != null)
            { 
                targetControl.GetComponent<Rigidbody2D>().gravityScale = gravDef;
            }
    }
}
