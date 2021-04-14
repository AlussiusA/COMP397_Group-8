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

public class PanelLogic : MonoBehaviour
{
    public GameObject Panel;
    public InventoryPanelController inventory;

    private void Start()
    {
        inventory = FindObjectOfType<InventoryPanelController>();
    }

    public void OpenPanel()
    {
        if (Panel != null)
        {
            Panel.SetActive(true);
        }
        if (inventory != null)
        {
            inventory.ShowInventory();
        }
    }

    public void ClosePanel()
    {
        if (Panel != null)
        {
            Panel.SetActive(false);
        }
        if (inventory != null)
        {
            inventory.HideInventory();
        }
    }
}
