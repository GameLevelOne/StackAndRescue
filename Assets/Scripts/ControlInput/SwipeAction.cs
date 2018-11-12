using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SwipeAction : MonoBehaviour {
    public float gravDef, gravPress;
    public Text testTxt1, testTxt2;
    public GameObject potBar;

    UserInput userInput;
    Transform targetControl;

    float timer=2f;

	private void Start(){
		SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
        userInput = GetComponent<UserInput>();
        gravDef = .25f;
        gravPress = 2f;
    }

	void Update () {
        timer-=Time.deltaTime;
		if(userInput.testPos != null)
            {
                targetControl = userInput.testPos;
                testTxt1.text = targetControl.name;
            }
	}

	private void SwipeDetector_OnSwipe(SwipeData data){
        if(data.Direction == SwipeDirection.Left)
            {
                if(targetControl!=null /*&& !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)*/)
                    {
                        targetControl.position = new Vector2(targetControl.position.x - .4f, targetControl.position.y);
                        testTxt2.text = "Left";
                    }
            }
        if(data.Direction == SwipeDirection.Right)
            {
                if(targetControl!=null /*&& !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)*/)
                    {
                        targetControl.position = new Vector2(targetControl.position.x + .4f, targetControl.position.y);
                        testTxt2.text = "Right";
                    }
            }
        if(data.Direction == SwipeDirection.Up)
            {
                /*if(!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                    {*/
                        if(timer<=0)
                            { 
                                potBar.GetComponent<PotionBtn>().OpenPotBar();
                                timer = 1f;
                            }
                    //}
            }
        if(data.Direction == SwipeDirection.Down)
            {
                if(targetControl!=null /*&& !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)*/)
                    {
                        targetControl.GetComponent<Rigidbody2D>().AddForce(Vector2.down*120000*Time.deltaTime); //when mass 10 = 20000
                        //targetControl.GetComponent<Rigidbody2D>().gravityScale = gravPress;
                        testTxt2.text = "Down Pressed";
                    }
            } else
                {
                    if(targetControl!=null /*&& !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)*/)
                        {
                            targetControl.GetComponent<Rigidbody2D>().gravityScale = gravDef;
                            //testTxt2.text = "Down UnPressed";
                        }
                }
	}
}
