using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfoController : MonoBehaviour
{
    public GameObject GameInfoCanvas;
    private bool isInitialized;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    public void ClosePanel()
    {
        Time.timeScale = 1;
        if (GameInfoCanvas != null) GameInfoCanvas.SetActive(false);
    }
}
