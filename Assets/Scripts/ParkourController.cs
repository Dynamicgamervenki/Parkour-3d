using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class ParkourController : MonoBehaviour
{
    EnvironmentScanner environmentscanner;
    NinjaController ninjacontroller;
    Animator anim;
    public List<ParkourActions> actions;

    public bool inAction = false;  

    private void Awake()
    {
        environmentscanner = GetComponent<EnvironmentScanner>();  
        ninjacontroller = GetComponent<NinjaController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        var hitData = environmentscanner.ObstacleCheck();

        if(hitData.forwordHitFound && !inAction && Input.GetKeyDown(KeyCode.Space) && !anim.GetBool("isDead"))
        {
            foreach (ParkourActions action in actions)
            {

                if (hitData.forwordHittag != action.objectTag)
                {
                    Debug.LogWarning("hitData tag and action tag names are not same !");
                }
                else if (hitData.forwordHittag == action.objectTag)
                {
                    StartCoroutine(performParkourActions(action));
                    break;
                }
                else
                    Debug.Log("smtg else is wrong");

            }
        }
    }

    IEnumerator performParkourActions(ParkourActions action)
    {
        Debug.Log("Entering Couroutine");
        string animToPlay = action.animation;
        float length = action.length;
        Debug.Log("length : " + length);

        inAction = true;
      //  ninjacontroller.OnControl(false);
        anim.CrossFade(animToPlay, 0.2f,0);
        yield return new WaitForSeconds(length);
        inAction = false;
     //   ninjacontroller.OnControl(true);

    }
}
