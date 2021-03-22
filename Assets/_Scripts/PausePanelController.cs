using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanelController : MonoBehaviour
{
    [Header("Data")]
    public SceneDataSO sceneData;

    [Header("Player")]
    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnLoadButtonPressed()
    {
        player.controller.enabled = false;
        player.transform.position = sceneData.playerPosition;
        player.transform.localRotation = sceneData.playerRotation;
        player.controller.enabled = true;

        // Load health and collected parts here
    }

    public void OnSaveButtonPressed()
    {
        sceneData.playerPosition = player.transform.position;
        sceneData.playerRotation = player.transform.localRotation;

        // Save Health and Collected Parts here
    }
}
