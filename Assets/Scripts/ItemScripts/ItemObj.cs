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
    public bool io_IsLoot;
    public int io_ExcavatedPartyID;
    public int io_CurrentContainer; 
    //-10 Unassigned,-3 Stash -2 Shop, -1 Loot Pile,
    //0 Party 1 Bags, 1 Party 2 Bags, 2 Party 3 Bags, 3 Party 4 Bags, 
    //10 Party 1 Wagon, 11 Party 2 Wagon, 12 Party 3 Wagon, 13 Party 4 Wagon

    public string io_ObjectType = "Item";
    public bool io_InInventory = true; //default to True

    public void BuyItem(GameObject item)
    {
        if(item.GetComponent<ItemObj>().cashValue < GameControllerScript.gc_Munnies)
        {
            //Confirm the purchase
            //Add the item to the "Stash", inventory whatever; add by array
            GameControllerScript.gc_StashOfItems[item.GetComponent<ItemObj>().idInArrays]++;
            item.GetComponent<ItemObj>().io_CurrentContainer = -3;
            GameControllerScript.gc_PlayerStash.Add(item);
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
        GameControllerScript.gc_PlayerStash.Remove(item);
        //give money to player
        GameControllerScript.gc_Munnies += item.GetComponent<ItemObj>().cashValue; //Multiplied by a percentage determined in a constants file
        //update the inventory display
    }
}
