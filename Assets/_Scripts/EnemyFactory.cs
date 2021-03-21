/*
 * EnemyFactory.cs
 * ------------------------------
 * Authors: Aloy, Alussius
 *          Ganguli, Jay
 *          Meija Razo, Edgar
 * Last Edited: 2021-03-21
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public GameObject prefab;

    public GameObject NewInstance()
    {
        return Instantiate(prefab) as GameObject;
    }

    public GameObject NewInstance(Vector3 position, Quaternion rotation)
    {
        return Instantiate(prefab, position, rotation, transform);
    }
}
