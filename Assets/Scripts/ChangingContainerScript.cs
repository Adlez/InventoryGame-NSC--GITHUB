using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ChangingContainerScript : MonoBehaviour
{
    //Need Party Object, get using ID from GameController
    //Need Party LootPile
    //Need Party Bags
    //Need Party Wagon
    //Need the 3 Panels
    //Stash Items will go into Bags
    //Pile Items will go into Bags or Wagon
    //Bag Items will go into Stash or Pile

    public GameObject ccs_CurParty;
    public List<GameObject> ccs_CurPartyPileOfLoot = new List<GameObject>();
    public List<GameObject> ccs_CurPartyBagsContents = new List<GameObject>();
    public List<GameObject> ccs_CurPartyWagonContents = new List<GameObject>();
    public List<GameObject> ccs_StashOfItems = new List<GameObject>();

    public GameObject ccs_SelfCanvas;
    public GameObject ccs_StashPanel;
    public GameObject ccs_StashParentPanel;
    public GameObject ccs_PartyInvenParentPanel;
    public GameObject ccs_PartyInvenPanel;
    public GameObject ccs_PartyPilePanel;
    public GameObject ccs_LootParentPanel;

    public string css_Location = "Null";
    public bool ccs_WagonOpen = false;


    public void UIButtonClicked(int curPartyID)
    {
        if(curPartyID < 0)
        {
            curPartyID = GameControllerScript.gc_SelectedParty;
        }
        ccs_SelfCanvas.SetActive(true);

        ccs_StashPanel.SetActive(true);
        ccs_StashParentPanel.SetActive(true);
        ccs_PartyInvenParentPanel.SetActive(true);
        ccs_PartyInvenPanel.SetActive(true);
        ccs_PartyPilePanel.SetActive(true);

        ccs_CurParty = GameControllerScript.gc_Parties[curPartyID];
        ccs_CurPartyBagsContents = ccs_CurParty.GetComponent<PartyObj>().po_ItemsInBagsList;
        ccs_CurPartyPileOfLoot = ccs_CurParty.GetComponent<PartyObj>().po_ItemsInExcavationPile;
        ccs_CurPartyWagonContents = ccs_CurParty.GetComponent<PartyObj>().po_ItemsInWagonList;
        ccs_StashOfItems = GameControllerScript.gc_PlayerStash;

        DisplayItems();
    }

    public void UIExcavationButtonClicked(int curPartyID)
    {
        ccs_SelfCanvas.SetActive(true);
        ccs_PartyInvenParentPanel.SetActive(true);
        ccs_PartyInvenPanel.SetActive(true);
        ccs_LootParentPanel.SetActive(true);
        ccs_PartyPilePanel.SetActive(true);

        ccs_CurParty = GameControllerScript.gc_Parties[curPartyID];
        ccs_CurPartyBagsContents = ccs_CurParty.GetComponent<PartyObj>().po_ItemsInBagsList;
        ccs_CurPartyPileOfLoot = ccs_CurParty.GetComponent<PartyObj>().po_ItemsInExcavationPile;
        ccs_CurPartyWagonContents = ccs_CurParty.GetComponent<PartyObj>().po_ItemsInWagonList;
        ccs_StashOfItems = GameControllerScript.gc_PlayerStash;

        DisplayItems();
    }

    public void UIStashButtonClicked(int curPartyID)
    {
        if (curPartyID < 0)
        {
            curPartyID = GameControllerScript.gc_SelectedParty;
        }
        ccs_SelfCanvas.SetActive(true);

        ccs_StashPanel.SetActive(true);
        ccs_StashParentPanel.SetActive(true);
        ccs_PartyInvenParentPanel.SetActive(true);
        ccs_PartyInvenPanel.SetActive(true);

        ccs_CurParty = GameControllerScript.gc_Parties[curPartyID];
        ccs_CurPartyBagsContents = ccs_CurParty.GetComponent<PartyObj>().po_ItemsInBagsList;
        ccs_CurPartyPileOfLoot = ccs_CurParty.GetComponent<PartyObj>().po_ItemsInExcavationPile;
        ccs_CurPartyWagonContents = ccs_CurParty.GetComponent<PartyObj>().po_ItemsInWagonList;
        ccs_StashOfItems = GameControllerScript.gc_PlayerStash;

        DisplayItems();
    }

    void DisplayItems()
    {
        for(int i = 0; i < ccs_CurPartyBagsContents.Count; ++i)
        {
            ccs_CurPartyBagsContents[i].GetComponent<ItemObj>().io_invIconObject.transform.SetParent(ccs_PartyInvenPanel.transform);
            ccs_CurPartyBagsContents[i].GetComponent<ItemObj>().io_invIconObject.SetActive(true);
            ccs_CurPartyBagsContents[i].GetComponent<ItemObj>().io_invIconObject.transform.localScale = new Vector2(1.0f, 1.0f);
        }
        for (int i = 0; i < ccs_CurPartyWagonContents.Count; ++i)
        {
            ccs_CurPartyWagonContents[i].GetComponent<ItemObj>().io_invIconObject.transform.SetParent(ccs_PartyInvenPanel.transform);
            ccs_CurPartyWagonContents[i].GetComponent<ItemObj>().io_invIconObject.SetActive(true);
            ccs_CurPartyWagonContents[i].GetComponent<ItemObj>().io_invIconObject.transform.localScale = new Vector2(1.0f, 1.0f);
        }
        for (int i = 0; i < ccs_CurPartyPileOfLoot.Count; ++i)
        {
            ccs_CurPartyPileOfLoot[i].GetComponent<ItemObj>().io_invIconObject.transform.SetParent(ccs_PartyPilePanel.transform);
            ccs_CurPartyPileOfLoot[i].GetComponent<ItemObj>().io_invIconObject.SetActive(true);
            ccs_CurPartyPileOfLoot[i].GetComponent<ItemObj>().io_invIconObject.transform.localScale = new Vector2(1.0f, 1.0f);
        }
        for (int i = 0; i < ccs_CurPartyBagsContents.Count; ++i)
        {
            ccs_CurPartyBagsContents[i].GetComponent<ItemObj>().io_invIconObject.transform.SetParent(ccs_PartyInvenPanel.transform);
            ccs_CurPartyBagsContents[i].GetComponent<ItemObj>().io_invIconObject.SetActive(true);
            ccs_CurPartyBagsContents[i].GetComponent<ItemObj>().io_invIconObject.transform.localScale = new Vector2(1.0f, 1.0f);
        }
        for (int i = 0; i < ccs_CurPartyWagonContents.Count; ++i)
        {
            ccs_CurPartyWagonContents[i].GetComponent<ItemObj>().io_invIconObject.transform.SetParent(ccs_PartyInvenPanel.transform);
            ccs_CurPartyWagonContents[i].GetComponent<ItemObj>().io_invIconObject.SetActive(true);
            ccs_CurPartyWagonContents[i].GetComponent<ItemObj>().io_invIconObject.transform.localScale = new Vector2(1.0f, 1.0f);
        }
        for (int i = 0; i < ccs_StashOfItems.Count; ++i)
        {
            ccs_StashOfItems[i].GetComponent<ItemObj>().io_invIconObject.transform.SetParent(ccs_StashPanel.transform);
            ccs_StashOfItems[i].GetComponent<ItemObj>().io_invIconObject.SetActive(true);
            ccs_StashOfItems[i].GetComponent<ItemObj>().io_invIconObject.transform.localScale = new Vector2(1.0f, 1.0f);
        }
    }

    public void SetLocation(string location)
    {
        css_Location = location;
    }
    public void OpenWagon()
    {
        ccs_WagonOpen = !ccs_WagonOpen;
    }

    public void ChangeContainer(GameObject item)
    {
        //Select Wagon or Bags
        //Check item's current container
        //-10 Unassigned,-3 Stash -2 Shop, -1 Loot Pile,
        //0 Party 1 Bags, 1 Party 2 Bags, 2 Party 3 Bags, 3 Party 4 Bags, 
        //10 Party 1 Wagon, 11 Party 2 Wagon, 12 Party 3 Wagon, 13 Party 4 Wagon
        int curContainer = item.GetComponent<ItemObj>().io_CurrentContainer;
        bool hasWagon = ccs_CurParty.GetComponent<PartyObj>().po_HasWagon;
        if(item.GetComponent<ItemObj>().io_invIconObject.GetComponent<Button>() == null)
        {
            item.GetComponent<ItemObj>().io_invIconObject.AddComponent<Button>();
        }
        //In case it doesn't have the listener:
        item.GetComponent<ItemObj>().io_invIconObject.GetComponent<Button>().onClick.RemoveAllListeners();
        //Add Function to button
        item.GetComponent<ItemObj>().io_invIconObject.GetComponent<Button>().onClick.AddListener(delegate { ChangeContainer(item); });

        //css_Location = "Null"; // default until assigned

        if (curContainer == 10 || curContainer == 11 || curContainer == 12 || curContainer == 13 && css_Location == "Stash")//In a wagon put it in the stash
        {
            item.GetComponent<ItemObj>().io_CurrentContainer = -3;
            ccs_CurPartyWagonContents.Remove(item);
            ccs_StashOfItems.Add(item);
            item.GetComponent<ItemObj>().io_invIconObject.transform.SetParent(ccs_StashPanel.transform);
        }
        else if (curContainer == 10 || curContainer == 11 || curContainer == 12 || curContainer == 13 && css_Location == "Pile")//In a wagon put it in the pile
        {
            item.GetComponent<ItemObj>().io_CurrentContainer = -1;
            ccs_CurPartyWagonContents.Remove(item);
            ccs_CurPartyPileOfLoot.Add(item);
            item.GetComponent<ItemObj>().io_invIconObject.transform.SetParent(ccs_PartyPilePanel.transform);
        }
        else if (curContainer == 0 && css_Location == "Stash" || curContainer == 1 && css_Location == "Stash" || curContainer == 2 && css_Location == "Stash" || curContainer == 3 && css_Location == "Stash")//In bags put it in the stash
        {
            item.GetComponent<ItemObj>().io_CurrentContainer = -3;
            ccs_CurPartyBagsContents.Remove(item);
            ccs_StashOfItems.Add(item);
            item.GetComponent<ItemObj>().io_invIconObject.transform.SetParent(ccs_StashPanel.transform);
        }
        else if (curContainer == 0 && css_Location == "Pile" || curContainer == 1 && css_Location == "Pile" || curContainer == 2 && css_Location == "Pile" || curContainer == 3 && css_Location == "Pile")//In a wagon put it in the pile
        {
            item.GetComponent<ItemObj>().io_CurrentContainer = -1;
            ccs_CurPartyBagsContents.Remove(item);
            ccs_CurPartyPileOfLoot.Add(item);
            item.GetComponent<ItemObj>().io_invIconObject.transform.SetParent(ccs_PartyPilePanel.transform);
        }
        else if (curContainer == -3)//In stash, add to bags
        {
            item.GetComponent<ItemObj>().io_CurrentContainer = ccs_CurParty.GetComponent<PartyObj>().po_PartyID;
            ccs_StashOfItems.Remove(item);
            ccs_CurPartyBagsContents.Add(item);
            item.GetComponent<ItemObj>().io_invIconObject.transform.SetParent(ccs_PartyInvenPanel.transform);
        }
        else if (curContainer == -1)//In Loot Pile
        {
            ccs_CurPartyPileOfLoot.Remove(item);//remove from pile
            if (hasWagon == true && ccs_WagonOpen == true)//Add to Wagon
            {
                item.GetComponent<ItemObj>().io_CurrentContainer = ccs_CurParty.GetComponent<PartyObj>().po_PartyID + 10;
                ccs_CurPartyWagonContents.Add(item);
                item.GetComponent<ItemObj>().io_invIconObject.transform.SetParent(ccs_PartyInvenPanel.transform);
            }
            else //add to bags
            {
                item.GetComponent<ItemObj>().io_CurrentContainer = ccs_CurParty.GetComponent<PartyObj>().po_PartyID;
                ccs_CurPartyBagsContents.Add(item);
                item.GetComponent<ItemObj>().io_invIconObject.transform.SetParent(ccs_PartyInvenPanel.transform);
            }

        }
        else
        {
            Debug.Log("The Item is not currently in a container, find where it came from and remedy this issue. " + curContainer.ToString());
        }
        //Now we put our modified lists back into the Party's list
        ccs_CurParty.GetComponent<PartyObj>().po_ItemsInBagsList = ccs_CurPartyBagsContents;
        ccs_CurParty.GetComponent<PartyObj>().po_ItemsInExcavationPile = ccs_CurPartyPileOfLoot;
        ccs_CurParty.GetComponent<PartyObj>().po_ItemsInWagonList = ccs_CurPartyWagonContents;
        GameControllerScript.gc_PlayerStash = ccs_StashOfItems;
    }
}
