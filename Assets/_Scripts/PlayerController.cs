/*
 * PlayerController.cs
 * Authors: Aloy, Alussius
 *          Ganguli, Jay
 *          Meija Razo, Edgar
 * Last Edited: 2021-02-14
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private CharacterController controller;
    public GameObject Panel;

    public float speed = 600.0f;
    public float turnSpeed = 400.0f;
    public float jumpSpeed = 8f;
    private Vector3 moveDirection = Vector3.zero;
    public float gravity = 20.0f;
    public float jumpBoost = 4f;
    AudioSource audio;

    public int partsCollected = 0;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = gameObject.GetComponentInChildren<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") != 0) // see input manager for the actual key bindings
        {
            anim.SetInteger("AnimationPar", 1);
            
        }
        else
        {
            anim.SetInteger("AnimationPar", 0);
            
        }
        
        if (controller.isGrounded)
        {
            moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;
            moveDirection.y = -gravity * Time.deltaTime; // resetting gravity to an initial (hoprefully small) value instead of zero to fix a bug with the ground check

            if (Input.GetAxis("Jump") > 0)
            {
                moveDirection.y = jumpSpeed;
                if (Input.GetAxis("Vertical") > 0)
                {
                    // when jumping forward, add a little to the player's forward momentum
                    // this helps prevent stiff jumps from standing, and makes forward jumps feel better
                    moveDirection += transform.forward * jumpBoost;
                }
            }
        }

        float turn = Input.GetAxis("Horizontal");
        transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
        controller.Move(moveDirection * Time.deltaTime);
        moveDirection.y -= gravity * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                Panel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                Panel.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ShipPart"))
        {
            other.GetComponent<ShipPartBehaviour>().CollectItem();
            partsCollected += 1;
        }
    }
}
