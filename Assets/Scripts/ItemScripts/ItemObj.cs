using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObj : MonoBehaviour
{
    public bool isArtefact; //
    public int cashValue; //
    public string itemName; //
    public GameObject invIconObject; //
    public Sprite invIcon; //Necessary?
    public Sprite fullImage; //
    public string description; //
    public int idInArrays; //
    public float oddsOfFinding;//
    public int maxFindAtOnce;
    public int typeID;

    public string io_ObjectType = "Item";
    public bool io_InInventory = true; //default to True

    public void BuyItem(GameObject item)
    {
        if(!item.GetComponent<ItemObj>().io_InInventory)
        {
            //If this has been clicked and it's not in the inventory, the player is in a shop so no need to check active panel
            //Confirm the purchase
            //Add the item to the "Stash", inventory whatever; add by array
            //take the player's money
            //update the inventory display
            //set io_InInventory to True
        }
    }

    public void SellItem(GameObject item)
    {
        if(item.GetComponent<ItemObj>().io_InInventory)
        {
            //Check if a Shop is the current active panel
            //Confirm player wants to sell the item
            //remove item from stash, or inventory or whatever; subrtact from array
            //give money to player
            //update the inventory display
        }
    }
}
