using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanelController : MonoBehaviour
{
    [Header("Data")]
    public SceneDataSO sceneData;

    [Header("Player")]
    public PlayerController player;

    [Header("Ship Parts")]
    public ShipPartBehaviour[] shipParts;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        GameObject scene = GameObject.Find("SceneController");
        if (scene)
        {
            if (scene.GetComponent<SceneSwitcher>().loadSave)
            {
                OnLoadButtonPressed();
            }
        }
        gameObject.SetActive(false);
    }

    public void OnLoadButtonPressed()
    {
        player.controller.enabled = false;
        player.transform.position = sceneData.playerPosition;
        player.transform.localRotation = sceneData.playerRotation;
        player.controller.enabled = true;

        // this section looks complicated because it makes no assumption about the length of
        // either shipParts array. This allows us to use as many parts as we want
        int i = 0;
        while (i < sceneData.collectedParts.Length && i < shipParts.Length)
        {
            if (!(sceneData.collectedParts[i]))
            {
                shipParts[i].ResetItem();
            }
            else
            {
                shipParts[i].CollectItem();
            }
            i++;
        }
        while (i < shipParts.Length)
        {
            shipParts[i].ResetItem();
            i++;
        }

        // Load health and collected parts here
        player.SetHealth(sceneData.health);
    }

    public void OnSaveButtonPressed()
    {
        sceneData.playerPosition = player.transform.position;
        sceneData.playerRotation = player.transform.localRotation;

        // Save Health and Collected Parts here
        bool[] collectedParts = new bool[shipParts.Length];
        for (int i = 0; i < shipParts.Length; i++)
        {
            collectedParts[i] = !(shipParts[i].gameObject.activeSelf);
        }
        sceneData.collectedParts = collectedParts;
        sceneData.health = player.health;
    }
}
