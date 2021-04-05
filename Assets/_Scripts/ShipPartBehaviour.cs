/* ----------------------
 * ShipPartBehaviour.cs
 * ----------------------
 * Authors: Aloy, Alussius
 *          Ganguli, Jay
 *          Meija Razo, Edgar
 * Last Edited: 2021-03-02
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPartBehaviour : MonoBehaviour
{
    [Header("Floating Movement")]
    public float speed = 1f;
    [SerializeField] private float amplitude = 0.15f;
    [SerializeField] private float rotationSpeed = 15f;

    [Header("Inventory")]
    [SerializeField] private GameObject missingInventoryItem;
    [SerializeField] private GameObject foundInventoryItem;

    [Header("On Pickup")]
    [SerializeField] private float pickupRotationSpeed = 360f;
    [SerializeField] private float pickupSpeed = 2f;
    [SerializeField] private float duration = 1f;
    [SerializeField] private BoxCollider collisionBox;

    private bool isCollected = false;

    Vector3 posOrigin = new Vector3();

    void Start()
    {
        posOrigin = transform.position;

        missingInventoryItem.SetActive(true);
        foundInventoryItem.SetActive(false);

        collisionBox = GetComponent<BoxCollider>();
    }

    void Update()
    {
        if (isCollected)
        {
            transform.Rotate(new Vector3(0f, Time.deltaTime * pickupRotationSpeed, 0f), Space.World);

            transform.position = new Vector3(transform.position.x,
                transform.position.y + pickupSpeed * Time.deltaTime,
                transform.position.z);
        }
        else
        {
            transform.Rotate(new Vector3(0f, Time.deltaTime * rotationSpeed, 0f), Space.World);

            transform.position = new Vector3(transform.position.x,
                posOrigin.y + Mathf.Sin(Time.fixedTime * Mathf.PI * speed) * amplitude,
                transform.position.z);
        }
    }

    public void CollectItem()
    {
        missingInventoryItem.SetActive(false);
        foundInventoryItem.SetActive(true);

        isCollected = true;
        collisionBox.enabled = false;

        // Destroy(gameObject, duration);
        gameObject.SetActive(false);
    }

    public void ResetItem()
    {
        // the opposite of CollectItem()
        missingInventoryItem.SetActive(true);
        foundInventoryItem.SetActive(false);

        isCollected = false;
        collisionBox.enabled = true;

        gameObject.SetActive(true);
    }
}
