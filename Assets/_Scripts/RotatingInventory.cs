/*
 * RotatingInventory.cs
 * ------------------------------
 * Authors: Aloy, Alussius
 *          Ganguli, Jay
 *          Meija Razo, Edgar
 * Last Edited: 2021-03-02
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingInventory : MonoBehaviour
{
    public float rotationSpeed = 15f;

    void Start()
    {
    }

    void Update()
    {
        transform.Rotate(new Vector3(0f, Time.unscaledDeltaTime * rotationSpeed, 0f), Space.World);
    }
}
