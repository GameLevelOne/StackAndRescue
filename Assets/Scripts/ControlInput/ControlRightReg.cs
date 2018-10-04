using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControlRightReg : MonoBehaviour {
    UserInput userInput;
    Transform targetControl;
    bool pressed;
    float timer = .1f;

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

    void FixedUpdate() {
        if (pressed)
            { 
                if (timer<=0)
                    { 
                        targetControl.position = new Vector2(targetControl.position.x + .4f, targetControl.position.y);
                        timer = .15f;
                    }
                timer -= Time.deltaTime;
            }
    }

    void OnMouseDown() {
        if(targetControl != null)
            { 
                if(!EventSystem.current.IsPointerOverGameObject())
                    {
                        pressed = true;
                    }
            }
    }

    void OnMouseUp() {
        if(targetControl != null)
            { 
                pressed = false;
            }
    }
}
