using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class NinjaController : MonoBehaviour
{
    private CharacterController characterController;
    ParkourController parkourController;
    Animator anim;
    

    [Header("Ninja")]
    [SerializeField] float speed = 0.5f;
    public GameObject deadUi;

    private Vector3 FirstTouch;
    private Vector3 LastTouch;

    public AudioSource RunAudioSource;
    public AudioSource DeathAudioSource;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        parkourController = GetComponent<ParkourController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(anim.GetBool("isDead") == true)
            return;

        characterController.Move(transform.forward * speed * Time.deltaTime);
        RunAudioSource.loop = true;
        RunAudioSource.Play();

    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.layer == 6 && !parkourController.inAction)
        {
            Debug.Log("Dead");
            anim.SetBool("isDead", true);
            DeathAudioSource.Play();
            deadUi.SetActive(true);
        }
    }

    public GameObject road01;
    public GameObject road02;
    int count = 3;

   public List<GameObject> newroads = new List<GameObject>();
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Spawn"))
        {
            GameObject instantiateRoad;
            if (count % 2 == 0)
            {
                instantiateRoad = Instantiate(road02, new Vector3(-113f * count, 10.1000004f, -493.577606f), Quaternion.identity);
            }
            else
                instantiateRoad = Instantiate(road01, new Vector3(-113f * count, 10.1000004f, -493.577606f), Quaternion.identity);

            count = count + 1;
            newroads.Add(instantiateRoad);
        }

        if(other.gameObject.CompareTag("Delete"))
        {
            newroads.RemoveAt(0);
        }
    }

    public void OnControl(bool value)
    {
        characterController.enabled = value;
    }


}
