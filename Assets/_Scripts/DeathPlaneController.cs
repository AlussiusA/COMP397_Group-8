/*
 * ControlPanelController.cs
 * ------------------------------
 * Authors: Aloy, Alussius
 *          Ganguli, Jay
 *          Meija Razo, Edgar
 * Last Edited: 2021-03-07
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlaneController : MonoBehaviour
{
    public Transform spawnPoint;


     void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            var controller = other.gameObject.GetComponent<CharacterController>();
            controller.enabled = false;
            other.gameObject.transform.position = spawnPoint.position;
            controller.enabled = true;

        }
    }
}
