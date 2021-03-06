﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class DisplayInventory : MonoBehaviour
{
    public static DisplayInventory inventoryList;
    public GameObject di_GameController;
    public GameObject di_ItemObj;
    public GameObject di_SampleIconButton;
    public GameObject di_TempParentOfStashItems;
    public GameObject di_PartyInventoryDisplayPanel;

    public List<GameObject> di_PlayerStashItemList;
    public List<GameObject> di_PlayerStashIconList;

    public GameObject[] tempArry = new GameObject[4];
    
    public int[] di_StashArray;
    public int[] di_PartyBagsArray;

    public List<GameObject> di_TempPileList = new List<GameObject>();

    //public DisplayItemIcon[] di_ItemIconList;

    public bool di_DisplayListIsCreated;
    public bool di_LookingAtTheStash = false;
    public int di_BinaryLookAtStash = 0; //false

    public void DisplayPlayerStash(int di_BinaryLookAtStash)
    {
        di_PlayerStashItemList = GameControllerScript.gc_PlayerStash;
        for (int i = 0; i < di_PlayerStashItemList.Count; ++i)
        {
            di_PlayerStashItemList[i].GetComponent<ItemObj>().io_invIconObject.SetActive(true);
        }
        //DestroyStashIcons();
        if(di_BinaryLookAtStash == 1) // we're about to look at the stash
        {
            //CreateStashItems();
        }
    }

    public void DisplayLootPile(int partyInLevel)
    {
        //int activePartyID = GameControllerScript.GetSelectedPartyIndex();
        int activePartyID = partyInLevel;

        for (int a = 0; a < GameControllerScript.gc_Parties.Length; ++a)
        {
            for (int i = 0; i < GameControllerScript.gc_Parties[a].GetComponent<PartyObj>().po_ItemsInExcavationPile.Count; ++i)
            {
                GameControllerScript.gc_Parties[a].GetComponent<PartyObj>().po_ItemsInExcavationPile[i].GetComponent<ItemObj>().io_invIconObject.SetActive(false);
            }
        }
        for (int i = 0; i < GameControllerScript.gc_Parties[activePartyID].GetComponent<PartyObj>().po_ItemsInExcavationPile.Count; ++i)
        {
            GameControllerScript.gc_Parties[activePartyID].GetComponent<PartyObj>().po_ItemsInExcavationPile[i].GetComponent<ItemObj>().io_invIconObject.SetActive(true);
            // **FIX** UHHHHHHHH, late at night, not thinking much. This is probably an issue with the transform of the icons not being set at the right time, probably somewhere
            // when the loot items are initially created. Fix it after some rest
        }
    }

    public void DisplayPartyBags()
    {
        int activePartyID = GameControllerScript.GetSelectedPartyIndex();
        for (int a = 0; a < GameControllerScript.gc_Parties.Length; ++a)
        {
            for (int i = 0; i < GameControllerScript.gc_Parties[a].GetComponent<PartyObj>().po_ItemsInBagsList.Count; ++i)
            {
                GameControllerScript.gc_Parties[a].GetComponent<PartyObj>().po_ItemsInBagsList[i].GetComponent<ItemObj>().io_invIconObject.SetActive(false);
            }
        }
        for(int i = 0; i < GameControllerScript.gc_Parties[activePartyID].GetComponent<PartyObj>().po_ItemsInBagsList.Count; ++i)
        {
            GameControllerScript.gc_Parties[activePartyID].GetComponent<PartyObj>().po_ItemsInBagsList[i].GetComponent<ItemObj>().io_invIconObject.SetActive(true);
        }
    }

    public void CreateStashItems()//created using the array
    {
        di_StashArray = GameControllerScript.gc_StashOfItems; //array of all items in game
        var gcScript = di_GameController.GetComponent<GameControllerScript>();

        for (int i = 0; i < di_StashArray.Length; ++i)
        {
            if (di_StashArray[i] >= -1) //if there is an item
            {
                int numOfItems = di_StashArray[i]; //get the number of the items

                for (int j = 0; j < numOfItems; ++j)
                {//int itemID, int firstContainer, GameObject parentObj, GameObject iconDisplayObj
                    GameObject stashItemObj = ItemCatalogueData.itemObj.CreateAnItemAndIcon(i, -3, gcScript.gc_PlayerStashPanel, gcScript.gc_PlayerStashPanel);// new GameObject("ItemInStash");//Temp Name
                    //stashItemObj.transform.SetParent(di_TempParentOfStashItems.transform);
                    //stashItemObj.AddComponent<ItemObj>();

                    //GameObject originalItem = gcScript.gc_CatalogueObj.GetComponent<ItemCatalogueData>().icd_ArrayOfItems[i];

                    //stashItemObj.name += originalItem.name;
                    //stashItemObj.AddComponent<LayoutElement>().minHeight = 1.0f;
                    //stashItemObj.AddComponent<LayoutElement>().minWidth = 1.0f;


                    //di_PlayerStashItemList.Add(stashItemObj);

                    //stashItemObj.GetComponent<ItemObj>().isArtefact = originalItem.GetComponent<ItemObj>().isArtefact;
                    //stashItemObj.GetComponent<ItemObj>().cashValue = originalItem.GetComponent<ItemObj>().cashValue;

                    //stashItemObj.GetComponent<ItemObj>().idInArrays = originalItem.GetComponent<ItemObj>().idInArrays;
                    //stashItemObj.GetComponent<ItemObj>().invIcon = originalItem.GetComponent<ItemObj>().invIcon;
                    //stashItemObj.GetComponent<ItemObj>().fullImage = originalItem.GetComponent<ItemObj>().fullImage;
                    //stashItemObj.GetComponent<ItemObj>().description = originalItem.GetComponent<ItemObj>().description;
                    //stashItemObj.GetComponent<ItemObj>().oddsOfFinding = originalItem.GetComponent<ItemObj>().oddsOfFinding;
                    //stashItemObj.GetComponent<ItemObj>().maxFindAtOnce = originalItem.GetComponent<ItemObj>().maxFindAtOnce;
                    //stashItemObj.GetComponent<ItemObj>().typeID = originalItem.GetComponent<ItemObj>().typeID;
                    //stashItemObj.GetComponent<ItemObj>().io_CurrentContainer = -3; //Stash

                    //GameObject newIcon = IconObj.MakeIconObject(stashItemObj, gcScript.gc_PlayerStashPanel, "Item");

                    stashItemObj.GetComponent<ItemObj>().io_invIconObject.AddComponent<Button>();
                    stashItemObj.GetComponent<ItemObj>().io_invIconObject.GetComponent<Button>().onClick.AddListener(delegate { di_GameController.GetComponent<GameControllerScript>().gc_ContainerChangeObject.GetComponent<ChangingContainerScript>().ChangeContainer(stashItemObj); });
;
                    //newIcon.GetComponent<Button>().onClick.AddListener(delegate {di_GameController.GetComponent<GameControllerScript>().gc_ContainerChangeObject.GetComponent<ChangingContainerScript>().ChangeContainer(stashItemObj); });

                    ////newIcon.GetComponent<Button>().onClick.AddListener(delegate { RemoveFromStash(stashItemObj, GameControllerScript.GetSelectedPartyIndex()); });
                    //stashItemObj.GetComponent<ItemObj>().io_invIconObject = newIcon;
                    di_PlayerStashIconList.Add(stashItemObj.GetComponent<ItemObj>().io_invIconObject);
                }
            }
        }
    }

    public void CreatePartyBagIcons(int activePartyID)
    {
        di_PartyBagsArray = GameControllerScript.gc_StashOfItems; //array of all items in game
        var gcScript = di_GameController.GetComponent<GameControllerScript>();

        for (int i = 0; i < di_PartyBagsArray.Length; ++i)
        {
            if (di_PartyBagsArray[i] >= -1) //if there is an item
            {
                int numOfItems = di_PartyBagsArray[i]; //get the number of the items

                for (int j = 0; j < numOfItems; ++j)
                {
                    GameObject bagItemObj = new GameObject("ItemInBags");//Temp Name
                    bagItemObj.transform.SetParent(di_TempParentOfStashItems.transform);
                    bagItemObj.AddComponent<ItemObj>();

                    GameObject originalItem = gcScript.gc_CatalogueObj.GetComponent<ItemCatalogueData>().icd_ArrayOfItems[i];

                    bagItemObj.name += originalItem.name;
                    bagItemObj.AddComponent<LayoutElement>().minHeight = 1.0f;
                    bagItemObj.AddComponent<LayoutElement>().minWidth = 1.0f;


                    //di_PartyBagsItemsList[activePartyID].Add(bagItemObj);

                    bagItemObj.GetComponent<ItemObj>().isArtefact = originalItem.GetComponent<ItemObj>().isArtefact;
                    bagItemObj.GetComponent<ItemObj>().cashValue = originalItem.GetComponent<ItemObj>().cashValue;

                    bagItemObj.GetComponent<ItemObj>().idInArrays = originalItem.GetComponent<ItemObj>().idInArrays;
                    bagItemObj.GetComponent<ItemObj>().invIcon = originalItem.GetComponent<ItemObj>().invIcon;
                    bagItemObj.GetComponent<ItemObj>().fullImage = originalItem.GetComponent<ItemObj>().fullImage;
                    bagItemObj.GetComponent<ItemObj>().description = originalItem.GetComponent<ItemObj>().description;
                    bagItemObj.GetComponent<ItemObj>().oddsOfFinding = originalItem.GetComponent<ItemObj>().oddsOfFinding;
                    bagItemObj.GetComponent<ItemObj>().maxFindAtOnce = originalItem.GetComponent<ItemObj>().maxFindAtOnce;
                    bagItemObj.GetComponent<ItemObj>().typeID = originalItem.GetComponent<ItemObj>().typeID;

                    GameObject newIcon = IconObj.MakeIconObject(bagItemObj, gcScript.gc_PlayerStashPanel, "Item");
                    newIcon.GetComponent<Button>().onClick.AddListener(delegate { RemoveFromStash(bagItemObj, GameControllerScript.GetSelectedPartyIndex()); });
                    bagItemObj.GetComponent<ItemObj>().io_invIconObject = newIcon;
                    //di_PartyBagsIconsList[activePartyID].Add(newIcon);
                }
            }
        }
    }

    public void RemoveFromStash(GameObject item, int partyIndex)
    {
        var activeParty = GameControllerScript.gc_Parties[partyIndex].GetComponent<PartyObj>();
        //if the invenotry isn't full
        if (activeParty.po_CurInventorySize < activeParty.po_MaxInventorySize)
        {
            //Add to the party's inventory
            activeParty.po_CurInventorySize++;
//            activeParty.po_ItemsInBagsList.Add(item);
            //activeParty.po_InventoryIndex[item.GetComponent<ItemObj>().idInArrays]++;

            //Remove from the Stash
            for (int i = 0; i < GameControllerScript.gc_PlayerStash.Count; ++i)//List
            {
                if (GameControllerScript.gc_PlayerStash[i].GetComponent<ItemObj>().idInArrays == item.GetComponent<ItemObj>().idInArrays)
                {
                    GameControllerScript.gc_PlayerStash.Remove(GameControllerScript.gc_PlayerStash[i]);
                    return;//only remove one
                }
            }
            GameControllerScript.gc_StashOfItems[item.GetComponent<ItemObj>().idInArrays]--;
            item.GetComponent<ItemObj>().io_invIconObject.GetComponent<Button>().onClick.RemoveAllListeners();
            Destroy(item.GetComponent<ItemObj>().io_invIconObject);
            Destroy(item);
            //item.GetComponent<ItemObj>().io_invIconObject.transform.SetParent(GameControllerScript.gc_PartyInventoryPanels[partyIndex].transform);

            UpdateItemsOnScreen();
        }
    }

    public void UpdateItemsOnScreen()
    {
        //Update Party Inventory
        int partyIndex = GameControllerScript.GetSelectedPartyIndex();
        var theParty = GameControllerScript.gc_Parties[partyIndex].GetComponent<PartyObj>();
        var gcScript = di_GameController.GetComponent<GameControllerScript>();

        DisplayPartyBags();

        //theParty.po_IconsOfItemsInBagsList.Clear();

        List<GameObject> tempInvenList = new List<GameObject>();
    }


}
