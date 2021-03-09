/*
 * ControlPanelController.cs
 * ------------------------------
 * Authors: Aloy, Alussius
 *          Ganguli, Jay
 *          Meija Razo, Edgar
 * Last Edited: 2021-03-02
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepsAudio : MonoBehaviour
{
    AudioSource animationSoundPlayer;

    // Start is called before the first frame update
    void Start()
    {
        animationSoundPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayerFoottStepSound ()
    {
        animationSoundPlayer.Play();
    }
}
