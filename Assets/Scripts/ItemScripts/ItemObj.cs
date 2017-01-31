using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObj : MonoBehaviour
{
    public bool isArtefact; //
    public int cashValue; //
    public string itemName; //
    public GameObject io_invIconObject; //
    public GameObject io_storeIconObject; //
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
        if(item.GetComponent<ItemObj>().cashValue < GameControllerScript.gc_Munnies)
        {
            //Confirm the purchase
            //Add the item to the "Stash", inventory whatever; add by array
            GameControllerScript.gc_StashOfItems[item.GetComponent<ItemObj>().idInArrays]++;
            //take the player's money
            GameControllerScript.gc_Munnies -= item.GetComponent<ItemObj>().cashValue;
            //item.GetComponent<ItemObj>().io_InInventory = true;
            //update the inventory display
        }
    }

    public void SellItem(GameObject item)
    {
        //Check if a Shop is the current active panel
        //Confirm player wants to sell the item
        //remove item from stash, or inventory or whatever; subrtact from array
        GameControllerScript.gc_StashOfItems[item.GetComponent<ItemObj>().idInArrays]--;
        //give money to player
        GameControllerScript.gc_Munnies += item.GetComponent<ItemObj>().cashValue; //Multiplied by a percentage determined in a constants file
        //update the inventory display
    }
}
