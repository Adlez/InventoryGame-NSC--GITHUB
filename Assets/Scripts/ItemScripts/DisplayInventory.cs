using UnityEngine;
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

    public List<GameObject>[] di_PartyBagsItemsList = new List<GameObject>[4];
    public List<GameObject>[] di_PartyBagsIconsList = new List<GameObject>[4];

    public int[] di_StashArray;

    //public DisplayItemIcon[] di_ItemIconList;

    public bool di_DisplayListIsCreated;
    public bool di_LookingAtTheStash = false;
    public int di_BinaryLookAtStash = 0; //false

    public void DisplayPlayerStash(int di_BinaryLookAtStash)
    {
        DestroyStashIcons();
        if(di_BinaryLookAtStash == 1) // we're about to look at the stash
        {
            CreateStashItems();
        }
    }

    public void DestroyStashIcons()
    {
        int temp = 0;
        while (di_PlayerStashIconList.Count > 0)
        {
            Destroy(di_PlayerStashIconList[0]);
            di_PlayerStashIconList.Remove(di_PlayerStashIconList[0]);
            temp++;
        }
        int temp2 = 0;
        while(di_PlayerStashItemList.Count > 0)
        {
            Destroy(di_PlayerStashItemList[0].GetComponent<ItemObj>().io_invIconObject);
            Destroy(di_PlayerStashItemList[0]);
            di_PlayerStashItemList.Remove(di_PlayerStashItemList[0]);
            temp2++;
        }
    }

    public void CreateStashItems()
    {
        di_StashArray = GameControllerScript.gc_StashOfItems; //array of all items in game
        var gcScript = di_GameController.GetComponent<GameControllerScript>();

        for (int i = 0; i < di_StashArray.Length; ++i)
        {
            if (di_StashArray[i] >= -1) //if there is an item
            {
                int numOfItems = di_StashArray[i]; //get the number of the items

                for (int j = 0; j < numOfItems; ++j)
                {
                    GameObject stashItemObj = new GameObject("ItemInStash");//Temp Name
                    stashItemObj.transform.SetParent(di_TempParentOfStashItems.transform);
                    stashItemObj.AddComponent<ItemObj>();

                    GameObject originalItem = gcScript.gc_CatalogueObj.GetComponent<ItemCatalogueData>().icd_ArrayOfItems[i];

                    stashItemObj.name += originalItem.name;
                    stashItemObj.AddComponent<LayoutElement>().minHeight = 1.0f;
                    stashItemObj.AddComponent<LayoutElement>().minWidth = 1.0f;


                    di_PlayerStashItemList.Add(stashItemObj);

                    stashItemObj.GetComponent<ItemObj>().isArtefact = originalItem.GetComponent<ItemObj>().isArtefact;
                    stashItemObj.GetComponent<ItemObj>().cashValue = originalItem.GetComponent<ItemObj>().cashValue;

                    stashItemObj.GetComponent<ItemObj>().idInArrays = originalItem.GetComponent<ItemObj>().idInArrays;
                    stashItemObj.GetComponent<ItemObj>().invIcon = originalItem.GetComponent<ItemObj>().invIcon;
                    stashItemObj.GetComponent<ItemObj>().fullImage = originalItem.GetComponent<ItemObj>().fullImage;
                    stashItemObj.GetComponent<ItemObj>().description = originalItem.GetComponent<ItemObj>().description;
                    stashItemObj.GetComponent<ItemObj>().oddsOfFinding = originalItem.GetComponent<ItemObj>().oddsOfFinding;
                    stashItemObj.GetComponent<ItemObj>().maxFindAtOnce = originalItem.GetComponent<ItemObj>().maxFindAtOnce;
                    stashItemObj.GetComponent<ItemObj>().typeID = originalItem.GetComponent<ItemObj>().typeID;

                    GameObject newIcon = IconObj.MakeIconObject(stashItemObj, gcScript.gc_PlayerStashPanel, "Item");
                    newIcon.GetComponent<Button>().onClick.AddListener(delegate { RemoveFromStash(stashItemObj, GameControllerScript.GetSelectedPartyIndex()); });
                    stashItemObj.GetComponent<ItemObj>().io_invIconObject = newIcon;
                    di_PlayerStashIconList.Add(newIcon);
                }
            }
        }
    }

    public void RemoveFromStash(GameObject item, int partyIndex)
    {
        var activeParty = GameControllerScript.gc_Parties[partyIndex].GetComponent<PartyObj>();

        //Add to the party's inventory
        activeParty.po_CurInventorySize++;
        activeParty.po_InventoryIndex[item.GetComponent<ItemObj>().idInArrays]++;

        //Remove from the Stash
        for(int i = 0; i < GameControllerScript.gc_PlayerStash.Count; ++i)//List
        {
            if(GameControllerScript.gc_PlayerStash[i].GetComponent<ItemObj>().idInArrays == item.GetComponent<ItemObj>().idInArrays)
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

    public void UpdateItemsOnScreen()
    {
        //Update Party Inventory
        int partyIndex = GameControllerScript.GetSelectedPartyIndex();
        var theParty = GameControllerScript.gc_Parties[partyIndex].GetComponent<PartyObj>();
        var gcScript = di_GameController.GetComponent<GameControllerScript>();

        theParty.po_IconsOfItemsInBagsList.Clear();

        List<GameObject> tempInvenList = new List<GameObject>();

        if (theParty.po_CurInventorySize > 0)
        {
            for (int i = 0; i < theParty.po_TotalCarriedItems.Length; ++i)//every item in the game
            {
                for(int j = 0; j < theParty.po_InventoryIndex[i]; ++j)//number of items at the index
                {
                    GameObject invenItemObj = new GameObject("ItemInBags");//Temp Name
                    invenItemObj.transform.SetParent(di_TempParentOfStashItems.transform);
                    invenItemObj.AddComponent<ItemObj>();

                    GameObject originalItem = gcScript.gc_CatalogueObj.GetComponent<ItemCatalogueData>().icd_ArrayOfItems[i];

                    invenItemObj.name += originalItem.name;
                    invenItemObj.AddComponent<LayoutElement>().minHeight = 1.0f;
                    invenItemObj.AddComponent<LayoutElement>().minWidth = 1.0f;
                    
                    //theParty.po_IconsOfItemsInBagsList.Add(invenItemObj);

                    invenItemObj.GetComponent<ItemObj>().isArtefact = originalItem.GetComponent<ItemObj>().isArtefact;
                    invenItemObj.GetComponent<ItemObj>().cashValue = originalItem.GetComponent<ItemObj>().cashValue;

                    invenItemObj.GetComponent<ItemObj>().idInArrays = originalItem.GetComponent<ItemObj>().idInArrays;
                    invenItemObj.GetComponent<ItemObj>().invIcon = originalItem.GetComponent<ItemObj>().invIcon;
                    invenItemObj.GetComponent<ItemObj>().fullImage = originalItem.GetComponent<ItemObj>().fullImage;
                    invenItemObj.GetComponent<ItemObj>().description = originalItem.GetComponent<ItemObj>().description;
                    invenItemObj.GetComponent<ItemObj>().oddsOfFinding = originalItem.GetComponent<ItemObj>().oddsOfFinding;
                    invenItemObj.GetComponent<ItemObj>().maxFindAtOnce = originalItem.GetComponent<ItemObj>().maxFindAtOnce;
                    invenItemObj.GetComponent<ItemObj>().typeID = originalItem.GetComponent<ItemObj>().typeID;

                    GameObject newIcon = IconObj.MakeIconObject(invenItemObj, gcScript.gc_PlayerStashPanel, "Item");
                    newIcon.transform.SetParent(di_PartyInventoryDisplayPanel.transform);
                    //newIcon.GetComponent<Button>().onClick.AddListener(delegate { RemoveFromStash(invenItemObj, GameControllerScript.GetSelectedPartyIndex()); });
                    theParty.po_IconsOfItemsInBagsList.Add(newIcon);
                }

            }
            /*for (int i = 0; i < listOfItemsInWagon.Count; ++i)
            {
                //make the wagon contents invisible
                listOfItemsInWagon[i].SetActive(false);
                listOfItemsInWagon[i].GetComponent<ItemObj>().io_invIconObject.SetActive(false);
            }*/
        }
        for(int i = 0; i < theParty.po_IconsOfItemsInBagsList.Count; ++i)
        {
            theParty.po_IconsOfItemsInBagsList[i].SetActive(true);
        }
    }


}
