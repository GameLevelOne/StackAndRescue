using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SwipeDetector : MonoBehaviour {
    public Text testTxt1, testTxt2, testTxt3, testTxt4, testTxt5;

    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;

    private bool started;

    [SerializeField]
    private bool detectSwipeOnlyAfterRelease = false;

    [SerializeField]
    private float minDistanceForSwipe = 15f;

    public static event Action<SwipeData> OnSwipe = delegate { };

    private void Update() {
        #region Obselete Mobile Input
        /*foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                    {
                        fingerUpPosition = touch.position;
                        fingerDownPosition = touch.position;
                        Debug.Log("Touch begin");
                        testTxt1.text = "Touch begin";
                    }
                if (!detectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved)
                    {
                        fingerDownPosition = touch.position;
                        DetectSwipe();
                        Debug.Log("Swipe hold");
                        testTxt1.text = "Swipe hold";
                    }               
                if (touch.phase == TouchPhase.Ended)
                    {
                        fingerDownPosition = touch.position;
                        DetectSwipe();
                        Debug.Log("Swipe after release");
                        testTxt1.text = "Swipe after release";
                    }
            }*/
        #endregion
        #region Mobile Input
        if (Input.touchCount == 1 && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        fingerUpPosition = Input.GetTouch(0).position;
                        fingerDownPosition = Input.GetTouch(0).position;
                        testTxt1.text = "Touch begin";
                    }
                else if (!detectSwipeOnlyAfterRelease && Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        fingerDownPosition = Input.GetTouch(0).position;
                        DetectSwipe();
                        testTxt1.text = "Swipe hold";
                    }               
                else if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled)
                    {
                        fingerDownPosition = Input.GetTouch(0).position;
                        DetectSwipe();
                        testTxt1.text = "Swipe after release";
                    }
            }
        #endregion
        #region PC Control - Debug Purpose
        if(SystemInfo.deviceType==DeviceType.Desktop)
            { 
                if(Input.GetMouseButtonDown(0))
                    {
                        fingerUpPosition = Input.mousePosition;
                        fingerDownPosition = Input.mousePosition;
                        testTxt1.text = "Touch begin";
                    }
                else if (!detectSwipeOnlyAfterRelease && Input.GetMouseButton(0))
                    {
                        fingerDownPosition = Input.mousePosition;
                        DetectSwipe();
                        testTxt1.text = "Swipe hold";
                    }               
                else if (Input.GetMouseButtonUp(0))
                    {
                        fingerDownPosition = Input.mousePosition;
                        DetectSwipe();
                        testTxt1.text = "Swipe after release";
                    }
            }
        #endregion
    }

    private void DetectSwipe() {
        testTxt3.text = SwipeDistanceCheckMet().ToString();
        if (SwipeDistanceCheckMet())
            {
                if (IsVerticalSwipe())
                    {
                        var direction = fingerDownPosition.y - fingerUpPosition.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
                        SendSwipe(direction);
                        testTxt2.text = "Swipe Vertical";
                    } else
                        {
                            var direction = fingerDownPosition.x - fingerUpPosition.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
                            SendSwipe(direction);
                            testTxt2.text = "Swipe Horizontal";
                        }
                fingerUpPosition = fingerDownPosition;
            }
    }

    private bool IsVerticalSwipe() {
        return VerticalMovementDistance() > HorizontalMovementDistance();
    }

    private bool SwipeDistanceCheckMet() {
        testTxt4.text = VerticalMovementDistance().ToString();
        testTxt5.text = HorizontalMovementDistance().ToString();
        return VerticalMovementDistance() > minDistanceForSwipe || HorizontalMovementDistance() > minDistanceForSwipe;
    }

    private float VerticalMovementDistance() {
        return Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);
    }

    private float HorizontalMovementDistance() {
        return Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x);
    }

    private void SendSwipe(SwipeDirection direction) {
        SwipeData swipeData = new SwipeData()
            {
                Direction = direction,
                StartPosition = fingerDownPosition,
                EndPosition = fingerUpPosition
            };
        OnSwipe(swipeData);
    }
}

public struct SwipeData {
    public Vector2 StartPosition;
    public Vector2 EndPosition;
    public SwipeDirection Direction;
}

public enum SwipeDirection {
    Up,
    Down,
    Left,
    Right
}