using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnvironmentTrigger : MonoBehaviour
{
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;
    public UnityEvent onTriggerStay;
    public bool destroyOnExit;
    public bool destroyOnEnter;

    private void OnTriggerEnter(Collider other)
    {

        onTriggerEnter.Invoke();

        if (destroyOnEnter)
            Destroy(gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        onTriggerExit.Invoke();

        if (destroyOnExit)
            Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        onTriggerStay.Invoke();
    }
}
