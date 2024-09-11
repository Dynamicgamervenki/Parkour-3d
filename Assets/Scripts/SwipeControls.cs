using UnityEngine;

public class SwipeControls : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private float swipeThreshold = 50f; // Minimum distance for a swipe to be detected

    void Update()
    {
        DetectSwipe();
    }

    void DetectSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Get the first touch (single touch)

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Capture the initial touch position when the touch begins
                    startTouchPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                    // Capture the final touch position when the touch ends
                    endTouchPosition = touch.position;

                    // Calculate the swipe distance
                    Vector2 swipeVector = endTouchPosition - startTouchPosition;

                    // Detect if the swipe is vertical (more significant vertical movement)
                    if (Mathf.Abs(swipeVector.y) > Mathf.Abs(swipeVector.x) && Mathf.Abs(swipeVector.y) > swipeThreshold)
                    {
                        if (swipeVector.y > 0)
                        {
                            OnSwipeUp();
                        }
                        else
                        {
                            OnSwipeDown();
                        }
                    }
                    break;
            }
        }
    }

    void OnSwipeUp()
    {
        Debug.Log("Swipe Up Detected");
        // Add your logic for swipe up here
    }

    void OnSwipeDown()
    {
        Debug.Log("Swipe Down Detected");
        // Add your logic for swipe down here
    }
}
