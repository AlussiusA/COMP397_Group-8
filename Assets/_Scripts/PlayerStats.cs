/*
 * PlayerStats.cs
 * ------------------------------
 * Authors: Aloy, Alussius
 *          Ganguli, Jay
 *          Meija Razo, Edgar
 * Last Edited: 2021-04-15
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // stats we may want to track
    public int jumpButtonPressed = 0;
    public int pauseButtonPressed = 0;
    public float distanceWalked = 0f;

    public delegate void UpdateEvent();

    private UpdateEvent updateObservers;

    public void NotifyUpdate()
    {
        if (updateObservers != null)
        {
            updateObservers.Invoke();
        }
    }

    public void Subscribe(UpdateEvent e)
    {
        updateObservers += e;
    }

    public void Unsubscribe(UpdateEvent e)
    {
        updateObservers -= e;
    }
}
