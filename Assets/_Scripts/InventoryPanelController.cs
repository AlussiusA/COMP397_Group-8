/*
 * ControlPanelController.cs
 * ------------------------------
 * Authors: Aloy, Alussius
 *          Ganguli, Jay
 *          Meija Razo, Edgar
 * Last Edited: 2021-04-14
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanelController : MonoBehaviour
{
    public GameObject pausePanel;
    public Vector2 offScreenPosition;
    public Vector2 onScreenPosition;

    [Range(1f, 10f)]public float speed = 1.0f;

    private RectTransform rectTransform;
    private float timer = 0f;
    private bool isOnScreen = false;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = offScreenPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //isOnScreen = false;
        //if (pausePanel.activeSelf)
        //{
        //    isOnScreen = true;
        //}

        if (isOnScreen)
        {
            if (timer < 0f)
            {
                timer = 0f;
            }
            MovePanelUp();
        }
        else
        {
            if (timer > 1f)
            {
                timer = 1f;
            }
            MovePanelDown();
        }
    }

    private void MovePanelUp()
    {
        rectTransform.anchoredPosition = Vector2.Lerp(offScreenPosition, onScreenPosition, timer);
        if (timer < 1f)
        {
            timer += Time.unscaledDeltaTime * speed;
        }
    }

    private void MovePanelDown()
    {
        rectTransform.anchoredPosition = Vector2.Lerp(offScreenPosition, onScreenPosition, timer);
        if (timer > 0f)
        {
            timer -= Time.unscaledDeltaTime * speed;
        }
    }

    public void ShowInventory()
    {
        isOnScreen = true;
    }

    public void HideInventory()
    {
        isOnScreen = false;
    }
}
