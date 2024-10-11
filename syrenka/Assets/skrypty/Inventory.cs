using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool hasRed = false;
    public bool hasBlue = false;
    public bool hasGreen = false;

    // This method logs the current keys in the inventory to the console
    public void DisplayInventory()
    {
        Debug.Log("Inventory:");
        Debug.Log("Red Key: " + (hasRed ? "Yes" : "No"));
        Debug.Log("Blue Key: " + (hasBlue ? "Yes" : "No"));
        Debug.Log("Green Key: " + (hasGreen ? "Yes" : "No"));
    }
}
