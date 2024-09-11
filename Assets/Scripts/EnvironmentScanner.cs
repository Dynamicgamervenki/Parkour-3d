using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnvironmentScanner : MonoBehaviour
{
    public float yOffset;
    public float forwordRayLength = 0.5f;
    public LayerMask parkourMask;

    public ObstacleData ObstacleCheck()
    {
        ObstacleData obstacleData = new ObstacleData();
        if (Physics.Raycast(transform.position + Vector3.up * yOffset, transform.forward, out obstacleData.forwordHit,forwordRayLength, parkourMask,QueryTriggerInteraction.Collide))
        {
            obstacleData.forwordHitFound = true;
            obstacleData.forwordHittag = obstacleData.forwordHit.transform.gameObject.tag;
        }
        else
        {
            obstacleData.forwordHitFound = false;
        }
        return obstacleData;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + Vector3.up *  yOffset , transform.forward * forwordRayLength);
    }
}


public struct ObstacleData
{
    public RaycastHit forwordHit;
    public bool forwordHitFound;
    public string forwordHittag;
}
