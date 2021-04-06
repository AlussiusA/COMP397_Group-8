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
        LoadFromPlayerPrefs();

        player.controller.enabled = false;
        player.transform.position = sceneData.playerPosition;
        player.transform.localRotation = sceneData.playerRotation;
        player.controller.enabled = true;

        // this section looks complicated because it makes no assumption about the length of
        // either shipParts array. This allows us to use as many parts as we want
        int i = 0;
        while (i < sceneData.collectedParts.Length && i < shipParts.Length)
        {
            if (sceneData.collectedParts[i])
            {
                shipParts[i].CollectItem();
            }
            else
            {
                shipParts[i].ResetItem();
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

        SaveToPlayerPrefs();
    }

    public void SaveToPlayerPrefs()
    {
        PlayerPrefs.SetFloat("playerTransformX", sceneData.playerPosition.x);
        PlayerPrefs.SetFloat("playerTransformY", sceneData.playerPosition.y);
        PlayerPrefs.SetFloat("playerTransformZ", sceneData.playerPosition.z);

        PlayerPrefs.SetFloat("playerRotationX", sceneData.playerRotation.x);
        PlayerPrefs.SetFloat("playerRotationY", sceneData.playerRotation.y);
        PlayerPrefs.SetFloat("playerRotationZ", sceneData.playerRotation.z);
        PlayerPrefs.SetFloat("playerRotationW", sceneData.playerRotation.w);

        PlayerPrefs.SetInt("playerHealth", sceneData.health);

        for (int i = 0; i < shipParts.Length; i++)
        {
            if (shipParts[i].gameObject.activeSelf)
            {
                PlayerPrefs.SetInt("collectedPart" + i, 0);
            }
            else
            {
                PlayerPrefs.SetInt("collectedPart" + i, 1);
            }
        }
    }

    public void LoadFromPlayerPrefs()
    {
        if (!(PlayerPrefs.HasKey("playerHealth")))
        {
            return;
        }

        sceneData.playerPosition.x = PlayerPrefs.GetFloat("playerTransformX");
        sceneData.playerPosition.y = PlayerPrefs.GetFloat("playerTransformY");
        sceneData.playerPosition.z = PlayerPrefs.GetFloat("playerTransformZ");

        sceneData.playerRotation.x = PlayerPrefs.GetFloat("playerRotationX");
        sceneData.playerRotation.y = PlayerPrefs.GetFloat("playerRotationY");
        sceneData.playerRotation.z = PlayerPrefs.GetFloat("playerRotationZ");
        sceneData.playerRotation.w = PlayerPrefs.GetFloat("playerRotationW");

        sceneData.health = PlayerPrefs.GetInt("playerHealth");

        bool[] collectedParts = new bool[shipParts.Length];
        for(int i = 0; i < shipParts.Length; i++)
        {
            if (PlayerPrefs.HasKey("collectedPart"+i))
            {
                collectedParts[i] = PlayerPrefs.GetInt("collectedPart" + i, 0) == 1;
            }
            else
            {
                collectedParts[i] = false;
            }
        }
        sceneData.collectedParts = collectedParts;
    }
}
