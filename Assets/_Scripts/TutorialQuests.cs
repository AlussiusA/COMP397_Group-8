/*
 * TutorialQuests.cs
 * ------------------------------
 * Authors: Aloy, Alussius
 *          Ganguli, Jay
 *          Meija Razo, Edgar
 * Last Edited: 2021-04-15
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialQuests : MonoBehaviour
{
    // observable object
    public PlayerStats stats;

    // required UI elements
    public GameObject tutorialMoveUI;
    public GameObject tutorialJumpUI;
    public GameObject tutorialPauseUI;

    // other stuff for tracking changes in stats
    private int previousJumps = 0;
    private int previousPause = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (stats == null)
        {
            stats = FindObjectOfType<PlayerStats>();
        }

        stats.Subscribe(TutorialMove); // add the first objective to the stats delegate

        tutorialMoveUI.SetActive(true);
        tutorialJumpUI.SetActive(false);
        tutorialPauseUI.SetActive(false);
    }

    public void TutorialMove()
    {
        // whenever this gets called, check the player stats and react accordingly
        if (stats.distanceWalked > 1f)
        {
            // objective met, do stuff
            tutorialMoveUI.SetActive(false);

            // unsub this observer (since we're done with this objective) and set up the next one
            stats.Unsubscribe(TutorialMove);

            previousJumps = stats.jumpButtonPressed;
            tutorialJumpUI.SetActive(true);
            stats.Subscribe(TutorialJump);
        }
    }

    public void TutorialJump()
    {
        // we're just checking if the player has pressed jump since this tutorial step started
        if (stats.jumpButtonPressed > previousJumps)
        {
            tutorialJumpUI.SetActive(false);
            stats.Unsubscribe(TutorialJump);

            previousPause = stats.pauseButtonPressed;
            tutorialPauseUI.SetActive(true);
            stats.Subscribe(TutorialPause);
        }
    }

    public void TutorialPause()
    {
        if (stats.pauseButtonPressed > previousPause)
        {
            tutorialPauseUI.SetActive(false);
            stats.Unsubscribe(TutorialPause);
        }
    }
}
