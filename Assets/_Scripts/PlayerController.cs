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
    private CanvasGroup damageAlertFade;
    public GameObject Panel;

    [Header("Game Control Values")]
    public float speed = 600.0f;
    public float turnSpeed = 400.0f;
    public float jumpSpeed = 8f;
    private Vector3 moveDirection = Vector3.zero;
    public float gravity = 20.0f;
    public float jumpBoost = 4f;

    [Header("Audios")]
    public AudioSource jumpSound;
    public AudioSource footstepSound;
    public AudioSource damageSound;
    public AudioSource roarSound;

    [Header("Inventory")]
    public InventoryPanelController inventory;
    public int partsCollected = 0;
    private float inventoryTimer = 0f;

    [Header("Health Related Attributes")]
    public int health = 100;
    public int enemyDamage = 10;
    public int trapDamage = 50;
    public float damageDelay = 60.0f;
    public HealthBarController healthBar;
    public GameObject damageAlertBG;

    [Header("Victory Screen")]
    public GameObject victoryScreen;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = gameObject.GetComponentInChildren<Animator>();
        damageAlertFade = damageAlertBG.GetComponent<CanvasGroup>();

        victoryScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") != 0) // see input manager for the actual key bindings
        {
            anim.SetInteger("AnimationPar", 1);
            //footstepSound.Play();
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
                jumpSound.Play();
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

        // Pause Game Script
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
        
        if (inventoryTimer > 0f)
        {
            inventoryTimer -= Time.deltaTime;
            if (inventoryTimer <= 0f)
            {
                inventory.HideInventory();
            }
        }
        
        if (damageAlertFade.alpha > 0)
        {
            if (Time.frameCount % 2.0f == 0)
            {
                damageAlertFade.alpha -= .05f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ShipPart"))
        {
            other.GetComponent<ShipPartBehaviour>().CollectItem();
            partsCollected += 1;
            inventoryTimer = 5f;
            inventory.ShowInventory();
            return;
        }

        if (other.CompareTag("Enemy"))
        {
            //Debug.Log("Enemy Contact");
            TakeDamage(enemyDamage);
            return;
        }

        if (other.CompareTag("Trap"))
        {
            TakeDamage(trapDamage);
            return;
        }

        if (other.CompareTag("Goal"))
        {
            if (partsCollected >= 4)
            {
                victoryScreen.SetActive(true);
            }
            else
            {
                inventory.ShowInventory();
            }
            return;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (Time.frameCount % damageDelay == 0)
            {
                TakeDamage(enemyDamage);
            }
        }

        //if (other.CompareTag("Trap"))
        //{
        //    if (Time.frameCount % damageDelay == 0)
        //    {
        //        TakeDamage(trapDamage);
        //    }
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            inventory.HideInventory();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.TakeDamage(damage);
        damageSound.Play();
        roarSound.Play();
        damageAlertFade.alpha = 1.0f;
    }
}
