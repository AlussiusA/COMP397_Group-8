/*
 * PlayerController.cs
 * Authors: Aloy, Alussius
 *          Ganguli, Jay
 *          Meija Razo, Edgar
 * Last Edited: 2021-03-08
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    public CharacterController controller;
    private CanvasGroup damageAlertFade;
    public GameObject Panel;

    [Header("Controls")]
    public Joystick joystick;
    public float horizontalSensitivity;
    public float verticalSensitivity;

    [Header("Game Control Values")]
    public float speed = 600.0f;
    public float turnSpeed = 400.0f;
    public float jumpSpeed = 8f;
    private Vector3 moveDirection = Vector3.zero;
    public float gravity = 20.0f;
    public float jumpBoost = 2f;

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

    [Header("Observable")]
    public PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = gameObject.GetComponentInChildren<Animator>();
        damageAlertFade = damageAlertBG.GetComponent<CanvasGroup>();

        if (playerStats == null)
        {
            playerStats = GetComponent<PlayerStats>();
        }

        victoryScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (joystick.Vertical != 0) // see input manager for the actual key bindings
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
            moveDirection = transform.forward * -joystick.Vertical * speed;
            moveDirection.y = -gravity * Time.deltaTime; // resetting gravity to an initial (hoprefully small) value instead of zero to fix a bug with the ground check

            playerStats.distanceWalked += Mathf.Abs(joystick.Vertical) * speed * Time.deltaTime;
            playerStats.NotifyUpdate();
        }
        else
        {
            // directional influence in the air
            moveDirection += transform.forward * -joystick.Vertical * speed * 3f * Time.deltaTime;
        }

        float turn = -joystick.Horizontal;
        transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
        controller.Move(moveDirection * Time.deltaTime);
        moveDirection.y -= gravity * Time.deltaTime;
        
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

    public void OnJumpButtonPress()
    {
        if (controller.isGrounded)
        {
            moveDirection.y = jumpSpeed;
            jumpSound.Play();
            if (joystick.Vertical > 0)
            {
                // when jumping forward, add a little to the player's forward momentum
                // this helps prevent stiff jumps from standing, and makes forward jumps feel better
                moveDirection += transform.forward * jumpBoost;
            }

            controller.Move(moveDirection * Time.deltaTime);

            // update stats/observable
            playerStats.jumpButtonPressed += 1;
            playerStats.NotifyUpdate();
        }
    }

    public void OnPauseButtonPress()
    {
        // Pause Game Script
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            Panel.SetActive(true);
            inventory.ShowInventory();

            // update stats/observable
            playerStats.pauseButtonPressed += 1;
            playerStats.NotifyUpdate();
        }
        else
        {
            Time.timeScale = 1;
            Panel.SetActive(false);
            inventory.HideInventory();
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
            roarSound.Play();
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
        damageAlertFade.alpha = 1.0f;
    }

    public void SetHealth(int healthValue)
    {
        health = healthValue;
        healthBar.SetHealth(health);
    }
}
