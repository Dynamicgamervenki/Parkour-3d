using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaController : MonoBehaviour
{
    private CharacterController characterController;
    ParkourController parkourController;
    Animator anim;
    

    [Header("Ninja")]
    [SerializeField] float speed = 0.5f;

    private Vector3 FirstTouch;
    private Vector3 LastTouch;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        parkourController = GetComponent<ParkourController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        characterController.Move(transform.forward * speed * Time.deltaTime);


    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.layer == 6 && !parkourController.inAction)
        {
            Debug.Log("Dead");
            anim.SetBool("isDead", true);
        }
    }

    public void OnControl(bool value)
    {
        characterController.enabled = value;
    }


}
