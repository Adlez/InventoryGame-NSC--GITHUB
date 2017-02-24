﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

/* Things this script does:
 * a.Determines if the player is looking at the party's wagon or bags
 * b.Fills a list with all the potential loot that the party can get on their expedition
 * c.Displays the items in the party's bags is that's what the player is looking at
 * or the Wagon if they're looking at that 
 * d.Displays the items in the Pile
 * In order to display the items, this script Also:
 *  e.creates new GameObjects that are copies of Items
 *  f.creates new GameObjects that are copies of the Icons of the Items
 */

public class ExcavationFunctions : MonoBehaviour
{
    public GameObject ef_GameController;
    public GameObject ef_CatalogueObject;
    public Text ef_btnText;

    //temp public
    public List<GameObject> listOfItemsInBags = new List<GameObject>();// party.GetComponent<PartyObj>().po_IconsOfItemsInBagsList;
    public List<GameObject> listOfItemsInWagon = new List<GameObject>();// party.GetComponent<PartyObj>().po_IconsOfItemsInWagonList;
    public List<GameObject> listOfItemsInPile = new List<GameObject>();

    public int _LastPartyLookedAt = 1;//Default to 1

    List<GameObject> listOfLimboItems = new List<GameObject>();

    //Create Icon Object to clone later and display on screen with functions
    private GameObject _ExcavationIcon;
    private List<GameObject> listOfIconsForLootPile = new List<GameObject>();

    public void BagsOrWagoView(GameObject thisButton) //a.
    {
        if(ef_btnText.text == "Wagon")//Current view is Bags
        {
            ef_btnText.text = "Bags";
        }
        else
        {
            ef_btnText.text = "Wagon";
        }
        UpdateWagoBagsPileDisplay(-1);
    }

    public void ReadyExcavationPileBagsAndWagon(int partyIndex) //UI Button Calls this function (b.)
    {
        listOfIconsForLootPile.Clear();
        
        MenuNavigaion.menuNavCataloguePointer.mn_PartyInvenAndWagoDisplay.SetActive(true); //Display the panel of loot
        GameObject party = GameControllerScript.gc_Parties[partyIndex];
        GameObject displayPanel = ef_GameController.GetComponent<GameControllerScript>().gc_MenuNavObj.GetComponent<MenuNavigaion>().mn_PartyDisplayLoot;
        displayPanel.SetActive(true);
        
        if (listOfItemsInPile.Count == 0) //If there isn't anything in the pile, nothing has been initialized. Have to do that.
        {
            PopulateBagWagonAndPileLists(party); //fill up the Pile, Bags and the wagon
        }
        for(int i = 0; i < listOfItemsInPile.Count; ++i)
        {
            GameObject excavatedItemIcon = new GameObject("LootIcon");
            //_ExcavationIcon = new GameObject("ExcaLottIcon"); //Create now Icon Object
            excavatedItemIcon = listOfItemsInPile[i]; //Give attributes of Object in the Loot Pile
            excavatedItemIcon.GetComponent<ItemObj>().io_invIconObject.GetComponent<Transform>().SetParent(MenuNavigaion.menuNavCataloguePointer.mn_ExcavatedLootPanel.transform); //Set the parent
            excavatedItemIcon.SetActive(true); //Show the ITem Object
            excavatedItemIcon.GetComponent<ItemObj>().io_invIconObject.SetActive(true); //Show the Icon
            //REMOVE BUTTON FUNCTION
            //_ExcavationIcon.GetComponent<ItemObj>().io_invIconObject.GetComponent<Button>().onClick.RemoveListener(delegate { AddToLootPile(_ExcavationIcon); });
            //ADD BUTTON FUNCTION
            excavatedItemIcon.GetComponent<ItemObj>().io_invIconObject.GetComponent<Button>().onClick.AddListener(delegate { RemoveFromLootPile(excavatedItemIcon); }); //Add Function to add to bags and wagon
            listOfIconsForLootPile.Add(excavatedItemIcon); //Add Item to Loot Pile
        }
        UpdateWagoBagsPileDisplay(partyIndex);
    }

    public void UpdateWagoBagsPileDisplay(int partyIndex) //c. d.
    {
        if(partyIndex < 0) //Checking for when the WagoBag button is pressed
        {
            partyIndex = _LastPartyLookedAt;
        }
        _LastPartyLookedAt = partyIndex;

        GameObject party = GameControllerScript.gc_Parties[partyIndex];
        GameObject thisLevel = ef_CatalogueObject.GetComponent<LevelCatalogueData>().lcd_ArrayOfLevels[party.GetComponent<PartyObj>().po_LevelExploring];
        GameObject partyInLevel = thisLevel.GetComponent<LevelObj>().lv_PartyObject;
        bool partyHasWagon = partyInLevel.GetComponent<PartyObj>().po_HasWagon;

        if (partyHasWagon)//Check for wagon
        {
            MenuNavigaion.menuNavCataloguePointer.mn_WagoInvenDisplayBtn.SetActive(true); //set wagonviewbtn to active if the wagon exists
        }
        else //No wagon, no button. Set Active to False
        {
            MenuNavigaion.menuNavCataloguePointer.mn_WagoInvenDisplayBtn.SetActive(false);
        }

        for (int p = 0; p < listOfIconsForLootPile.Count; ++p)//c.
        {
            //Assign the Parent of the Icon to the Loot Panel and make it visible
            listOfIconsForLootPile[p].GetComponent<ItemObj>().io_invIconObject.GetComponent<Transform>().SetParent(MenuNavigaion.menuNavCataloguePointer.mn_ExcavatedLootPanel.transform);
            listOfIconsForLootPile[p].SetActive(true);

            listOfIconsForLootPile[p].GetComponent<ItemObj>().io_invIconObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            Vector3 curIconPos = listOfIconsForLootPile[p].GetComponent<ItemObj>().io_invIconObject.transform.localPosition;
            listOfIconsForLootPile[p].GetComponent<ItemObj>().io_invIconObject.GetComponent<RectTransform>().transform.localPosition = new Vector3(curIconPos.x, curIconPos.y, 1.0f);
            listOfIconsForLootPile[p].GetComponent<ItemObj>().io_invIconObject.transform.localPosition = new Vector3(curIconPos.x, curIconPos.y, 1.0f);

            listOfIconsForLootPile[p].GetComponent<ItemObj>().io_invIconObject.SetActive(true);
        }
        if (ef_btnText.text == "Wagon") //We're lookin' at bags (d.)
        {
            if(listOfItemsInBags.Count > 0)
            {
                for (int i = 0; i < listOfItemsInBags.Count; ++i)
                {
                    //Make the Parent of the icon the Bag/Wagon Display, then make it visible
                    listOfItemsInBags[i].GetComponent<ItemObj>().io_invIconObject.GetComponent<Transform>().SetParent(MenuNavigaion.menuNavCataloguePointer.mn_PartyInvenAndWagoDisplay.transform);

                    listOfItemsInBags[i].GetComponent<ItemObj>().io_invIconObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    Vector3 curIconPos = listOfItemsInBags[i].GetComponent<ItemObj>().io_invIconObject.transform.position;
                    listOfItemsInBags[i].GetComponent<ItemObj>().io_invIconObject.GetComponent<RectTransform>().transform.localPosition = new Vector3(curIconPos.x, curIconPos.y, 1.0f);
                    listOfItemsInBags[i].GetComponent<ItemObj>().io_invIconObject.transform.localPosition = new Vector3(curIconPos.x, curIconPos.y, 1.0f);

                    listOfItemsInBags[i].SetActive(true);
                    listOfItemsInBags[i].GetComponent<ItemObj>().io_invIconObject.SetActive(true);
                }
                for (int i = 0; i < listOfItemsInWagon.Count; ++i)
                {
                    //make the wagon contents invisible
                    listOfItemsInWagon[i].SetActive(false);
                    listOfItemsInWagon[i].GetComponent<ItemObj>().io_invIconObject.SetActive(false);
                }
            }
        }
        else
        {
            if(listOfItemsInWagon.Count > 0)
            {
                for (int i = 0; i < listOfItemsInWagon.Count; ++i)
                {
                    //Make the Parent of the Icon the Bag/Wagon Display, then make it visible
                    listOfItemsInWagon[i].GetComponent<ItemObj>().io_invIconObject.GetComponent<Transform>().SetParent(MenuNavigaion.menuNavCataloguePointer.mn_PartyInvenAndWagoDisplay.transform);

                    listOfItemsInWagon[i].GetComponent<ItemObj>().io_invIconObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    Vector3 curIconPos = listOfItemsInWagon[i].GetComponent<ItemObj>().io_invIconObject.transform.position;
                    listOfItemsInWagon[i].GetComponent<ItemObj>().io_invIconObject.GetComponent<RectTransform>().transform.localPosition = new Vector3(curIconPos.x, curIconPos.y, 1.0f);
                    listOfItemsInWagon[i].GetComponent<ItemObj>().io_invIconObject.transform.localPosition = new Vector3(curIconPos.x, curIconPos.y, 1.0f);


                    listOfItemsInWagon[i].SetActive(true);
                    listOfItemsInWagon[i].GetComponent<ItemObj>().io_invIconObject.SetActive(true);
                }
                for (int i = 0; i < listOfItemsInBags.Count; ++i)
                {
                    //make the bag contents invisible
                    listOfItemsInBags[i].SetActive(false);
                    listOfItemsInBags[i].GetComponent<ItemObj>().io_invIconObject.SetActive(false);
                }
            }
        }
    }

    public void AddToLootPile(GameObject thisItem)
    {
        //Remove from Bags/Wago
        if (ef_btnText.text == "Wagon") //We're lookin' at bags (d.)
        {
            listOfItemsInBags.Remove(thisItem);
        }
        else
        {
            listOfItemsInWagon.Remove(thisItem);
        }

        thisItem.GetComponent<ItemObj>().io_invIconObject.GetComponent<Transform>().SetParent(MenuNavigaion.menuNavCataloguePointer.mn_ExcavatedLootPanel.transform);

        //REMOVE BUTTON FUNCTION
        thisItem.GetComponent<ItemObj>().io_invIconObject.GetComponent<Button>().onClick.RemoveAllListeners();//.RemoveListener(delegate { AddToLootPile(thisItem); });
        //ADD BUTTON FUNCTION
        thisItem.GetComponent<ItemObj>().io_invIconObject.GetComponent<Button>().onClick.AddListener(delegate { RemoveFromLootPile(thisItem); });

        listOfItemsInPile.Add(thisItem);
        UpdateWagoBagsPileDisplay(-1);
    }

    public void RemoveFromLootPile(GameObject thisItem)
    {
        //Add to Bag/Wago
        if(ef_btnText.text == "Wagon") //We're lookin' at bags
        {
            listOfItemsInBags.Add(thisItem);
        }
        else //Else we're lookin' at the wagon
        {
            listOfItemsInWagon.Add(thisItem);
        }
        thisItem.GetComponent<ItemObj>().io_invIconObject.GetComponent<Transform>().SetParent(MenuNavigaion.menuNavCataloguePointer.mn_PartyInvenAndWagoDisplay.transform);

        //REMOVE BUTTON FUNCTION
        thisItem.GetComponent<ItemObj>().io_invIconObject.GetComponent<Button>().onClick.RemoveAllListeners();//.RemoveListener(delegate { RemoveFromLootPile(thisItem); });
        //ADD BUTTON FUNCTION
        thisItem.GetComponent<ItemObj>().io_invIconObject.GetComponent<Button>().onClick.AddListener(delegate { AddToLootPile(thisItem); });

        listOfItemsInPile.Remove(thisItem);
        UpdateWagoBagsPileDisplay(-1);
    }

    public void PopulateBagWagonAndPileLists(GameObject party)
    {
        //Clear lists before populating them
        listOfItemsInBags.Clear();
        listOfItemsInWagon.Clear();
        listOfItemsInPile.Clear();

        if(party.GetComponent<PartyObj>().po_CurInventorySize > 0)
        {
            for(int i = 0; i < party.GetComponent<PartyObj>().po_TotalCarriedItems.Length; ++i)
            {
                int numOfItems = party.GetComponent<PartyObj>().po_TotalCarriedItems[i];
                for(int j = 0; j < numOfItems; ++j)
                {
                    GameObject ItemClone = new GameObject();
                    ItemClone = ef_CatalogueObject.GetComponent<ItemCatalogueData>().icd_ArrayOfItems[party.GetComponent<PartyObj>().po_TotalCarriedItems[i]];
                    if (ItemClone.GetComponent<ItemObj>().io_invIconObject != null) //gonna need to keep an eye on this, this kind of check probably won't work here
                    {
                        listOfItemsInBags.Add(ItemClone);
                    }
                }
            }
        }
        if(party.GetComponent<PartyObj>().po_CurWagonInventorySize > 0)
        {
            for(int i = 0; i < party.GetComponent<PartyObj>().po_TotalWagonItems.Length; ++i)
            {
                int numOfItems = party.GetComponent<PartyObj>().po_TotalWagonItems[i];
                for (int j = 0; j < numOfItems; ++j)
                {
                    GameObject ItemClone = new GameObject();
                    ItemClone = ef_CatalogueObject.GetComponent<ItemCatalogueData>().icd_ArrayOfItems[party.GetComponent<PartyObj>().po_TotalWagonItems[i]];
                    if (ItemClone.GetComponent<ItemObj>().io_invIconObject != null) //gonna need to keep an eye on this, this kind of check probably won't work here
                    {
                        listOfItemsInWagon.Add(ItemClone);
                    }
                }
            }
        }

        //When Item is rolled for (in ItemObj) each Item is added in multiples already if needed
        GameObject thisLevel = ef_CatalogueObject.GetComponent<LevelCatalogueData>().lcd_ArrayOfLevels[party.GetComponent<PartyObj>().po_LevelExploring];
        for(int i = 0; i < thisLevel.GetComponent<LevelObj>().lv_PotentialLootList.Count; ++i)
        {
            GameObject tmpObj = thisLevel.GetComponent<LevelObj>().lv_PotentialLootList[i];
            listOfItemsInPile.Add(thisLevel.GetComponent<LevelObj>().lv_PotentialLootList[i]);
        }
    }
}