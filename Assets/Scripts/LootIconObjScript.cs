using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootIconObjScript : IconObj
{
    bool io_InLootList;
    //override GameObject io_ObjectForThisIcon;
    GameObject io_Party;

    public void Click(GameObject openContainer)//could likely pass in a bool value and have the icon button itself say whether it's going in the wagon or inventory
    {
        var curParty = io_Party.GetComponent<PartyObj>();
        int idOfItem = 0;//io_ObjectForThisIcon.GetComponent<scriptWithAllTheItems>().idOfItem;
        //Make sure to name Click and newContainer better
        if (io_InLootList)
        {
            #region Place Item in Inventory/Wagon
            if (openContainer == openContainer)//if the newContainer == Inventory
            {
                if (curParty.po_MaxInventorySize > curParty.po_CurInventorySize)
                {
                    curParty.po_CurInventorySize++;
                    curParty.po_InventoryIndex[idOfItem]++;
                }
                else
                {
                    //display Dialogue box
                    //change text in the dialogue box to: Inventory is full
                }
            }
            else //new container is the wagon
            {
                if (curParty.po_MaxWagonInventorySize > curParty.po_CurWagonInventorySize)
                {
                    curParty.po_CurWagonInventorySize++;
                    curParty.po_WagonIndex[idOfItem]++;
                }
                else
                {
                    //display Dialogue box
                    //change text in the dialogue box to: Wagon is full
                }
            }
            io_InLootList = false;
            #endregion
        }
        else //item is located in either the inventory or the wagon and must be moved to loot pile
        {
            #region Move Into Loot Pile
            if(openContainer == openContainer)//if container is inventory
            {
                curParty.po_InventoryIndex[idOfItem]--;
                curParty.po_CurInventorySize--;
            }
            else //container is wagon
            {
                curParty.po_WagonIndex[idOfItem]--;
                curParty.po_CurWagonInventorySize--;
            }
            //PotentialLootList.AddItem(idOfItem); //potential loot list does not yet exist, but it will
            io_InLootList = true;
            #endregion
        }
    }
}
