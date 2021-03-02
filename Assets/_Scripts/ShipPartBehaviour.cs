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
    public float speed = 1f;
    [SerializeField] private float amplitude = 0.15f;
    [SerializeField] private float rotationSpeed = 15f;

    Vector3 posOrigin = new Vector3();

    void Start()
    {
        posOrigin = transform.position;
    }

    void Update()
    {
        transform.Rotate(new Vector3(0f, Time.deltaTime * rotationSpeed, 0f), Space.World);

        transform.position = new Vector3(transform.position.x, 
            posOrigin.y + Mathf.Sin(Time.fixedTime * Mathf.PI * speed) * amplitude,
            transform.position.z);
    }
}
