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

    public float speed = 600.0f;
    public float turnSpeed = 400.0f;
    public float jumpSpeed = 8f;
    private Vector3 moveDirection = Vector3.zero;
    public float gravity = 20.0f;

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

            if (Input.GetAxis("Jump") > 0)
            {
                moveDirection.y = jumpSpeed;
            }
        }

        float turn = Input.GetAxis("Horizontal");
        transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
        controller.Move(moveDirection * Time.deltaTime);
        moveDirection.y -= gravity * Time.deltaTime;
    }
}
