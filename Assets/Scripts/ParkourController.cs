using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourController : MonoBehaviour
{
    private EnvironmentScanner environmentscanner;
    private NinjaController ninjacontroller;
    private Animator anim;
    public List<ParkourActions> actions;
    public bool inAction = false;
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private const float SwipeThreshold = 50f; // Minimum distance for swipe detection

    private void Awake()
    {
        environmentscanner = GetComponent<EnvironmentScanner>();
        ninjacontroller = GetComponent<NinjaController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        var hitData = environmentscanner.ObstacleCheck();

        if (Input.GetKeyDown(KeyCode.Space) && !inAction && !anim.GetBool("isDead"))
        {
            PerformActionBasedOnTag(hitData);
        }

        if (Input.touchCount > 0)
        {
            HandleTouchInput(hitData);
        }
    }

    private void HandleTouchInput(ObstacleData hitData)
    {
        Touch touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                startTouchPosition = touch.position;
                break;

            case TouchPhase.Ended:
                endTouchPosition = touch.position;
                Vector2 swipeVector = endTouchPosition - startTouchPosition;

                if (Mathf.Abs(swipeVector.y) > Mathf.Abs(swipeVector.x) && Mathf.Abs(swipeVector.y) > SwipeThreshold)
                {
                    if (swipeVector.y > 0)
                    {
                        OnSwipeUp(hitData);
                    }
                    else
                    {
                        OnSwipeDown(hitData);
                    }
                }
                break;
        }
    }

    private void PerformActionBasedOnTag(ObstacleData hitData)
    {
        if (hitData.forwordHitFound)
        {
            foreach (ParkourActions action in actions)
            {
                if (hitData.forwordHittag == action.objectTag)
                {
                    StartCoroutine(PerformParkourActions(action));
                    return; // Exit loop once action is performed
                }
            }
            Debug.LogWarning("No matching action found for the hit tag.");
        }
    }

    private void OnSwipeUp(ObstacleData hitData)
    {
        Debug.Log("Swipe Up Detected");
        PerformActionForSwipeDirection(hitData, true);
    }

    private void OnSwipeDown(ObstacleData hitData)
    {
        Debug.Log("Swipe Down Detected");
        PerformActionForSwipeDirection(hitData, false);
    }

    private void PerformActionForSwipeDirection(ObstacleData hitData, bool isSwipeUp)
    {
        foreach (ParkourActions action in actions)
        {
            if (action.isSwipeUp == isSwipeUp && hitData.forwordHittag == action.objectTag)
            {
                StartCoroutine(PerformParkourActions(action));
                return; // Exit loop once action is performed
            }
        }
        Debug.LogWarning("No matching action found for the swipe direction and hit tag.");
    }

    private IEnumerator PerformParkourActions(ParkourActions action)
    {
        if (inAction) yield break;

        Debug.Log("Entering Coroutine");
        string animToPlay = action.animation;
        float length = action.length;
        Debug.Log("Animation Length : " + length);

        inAction = true;
        anim.CrossFade(animToPlay, 0.2f, 0);
        yield return new WaitForSeconds(length);
        inAction = false;
    }
}
