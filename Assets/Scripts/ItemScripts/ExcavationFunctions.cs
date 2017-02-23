using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class ExcavationFunctions : MonoBehaviour
{
    public GameObject ef_GameController;
    public GameObject ef_CatalogueObject;
    public Text ef_btnText;

    //temp public
    public List<GameObject> listOfItemsInBags = new List<GameObject>();// party.GetComponent<PartyObj>().po_IconsOfItemsInBagsList;
    public List<GameObject> listOfItemsInWagon = new List<GameObject>();// party.GetComponent<PartyObj>().po_IconsOfItemsInWagonList;
    public List<GameObject> listOfItemsInPile = new List<GameObject>();

    List<GameObject> listOfLimboItems = new List<GameObject>();

    //Create Icon Object to clone later and display on screen with functions
    private GameObject _ExcavationIcon;
    private List<GameObject> listOfIconsForLootPile = new List<GameObject>();

    public void BagsOrWagoView(GameObject thisButton)
    {
        if(ef_btnText.text == "Wagon")//Current view is Bags
        {
            ef_btnText.text = "Bags";
        }
        else
        {
            ef_btnText.text = "Wagon";
        }
        UpdateWagoBagsPileDisplay();
    }

    public void ReadyExcavationPileBagsAndWagon(int partyIndex) //UI Button Calls this function
    {
        listOfIconsForLootPile.Clear();
        GameObject party = GameControllerScript.gc_Parties[partyIndex];// ef_GameController.GetComponent<GameControllerScript>().Party
        MenuNavigaion.menuNavCataloguePointer.mn_PartyInvenAndWagoDisplay.SetActive(true); //Display the panel of loot
        GameObject thisLevel = ef_CatalogueObject.GetComponent<LevelCatalogueData>().lcd_ArrayOfLevels[party.GetComponent<PartyObj>().po_LevelExploring];
        GameObject displayPanel = ef_GameController.GetComponent<GameControllerScript>().gc_MenuNavObj.GetComponent<MenuNavigaion>().mn_PartyDisplayLoot;
        displayPanel.SetActive(true);

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

        if (listOfItemsInPile.Count == 0) //If there isn't anything in the pile, nothing has been initialized. Have to do that.
        {
            PopulateBagWagonAndPileLists(party); //fill up the Pile, Bags and the wagon
        }
        for(int i = 0; i < listOfItemsInPile.Count; ++i)
        {
            _ExcavationIcon = new GameObject("ExcaLottIcon"); //Create now Object
            _ExcavationIcon = listOfItemsInPile[i]; //Give attributes of Object in the Loot Pile
            _ExcavationIcon.GetComponent<ItemObj>().io_invIconObject.GetComponent<Transform>().SetParent(MenuNavigaion.menuNavCataloguePointer.mn_ExcavatedLootPanel.transform); //Set the parent
            _ExcavationIcon.SetActive(true); //Show the ITem Object
            _ExcavationIcon.GetComponent<ItemObj>().io_invIconObject.SetActive(true); //Show the Icon
            //REMOVE BUTTON FUNCTION
            //_ExcavationIcon.GetComponent<ItemObj>().io_invIconObject.GetComponent<Button>().onClick.RemoveListener(delegate { AddToLootPile(_ExcavationIcon); });
            //ADD BUTTON FUNCTION
            _ExcavationIcon.GetComponent<ItemObj>().io_invIconObject.GetComponent<Button>().onClick.AddListener(delegate { RemoveFromLootPile(_ExcavationIcon); }); //Add Function to add to bags and wagon
            listOfIconsForLootPile.Add(_ExcavationIcon); //Add Item to Loot Pile
        }
        UpdateWagoBagsPileDisplay();
    }

    public void UpdateWagoBagsPileDisplay() 
    {
        for (int p = 0; p < listOfIconsForLootPile.Count; ++p)
        {
            //Assign the Parent of the Icon to the Loot Panel and make it visible
            listOfIconsForLootPile[p].GetComponent<ItemObj>().io_invIconObject.GetComponent<Transform>().SetParent(MenuNavigaion.menuNavCataloguePointer.mn_ExcavatedLootPanel.transform);
            listOfIconsForLootPile[p].SetActive(true);

            listOfIconsForLootPile[p].GetComponent<ItemObj>().io_invIconObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            Vector3 curIconPos = listOfIconsForLootPile[p].GetComponent<ItemObj>().io_invIconObject.transform.position;
            listOfIconsForLootPile[p].GetComponent<ItemObj>().io_invIconObject.GetComponent<RectTransform>().transform.position = new Vector3(curIconPos.x, curIconPos.y, 1.0f);
            listOfIconsForLootPile[p].GetComponent<ItemObj>().io_invIconObject.transform.position = new Vector3(curIconPos.x, curIconPos.y, 1.0f);

            listOfIconsForLootPile[p].GetComponent<ItemObj>().io_invIconObject.SetActive(true);
        }
        if (ef_btnText.text == "Wagon") //We're lookin' at bags
        {
            if(listOfItemsInBags.Count > 0)
            {
                for (int i = 0; i < listOfItemsInBags.Count; ++i)
                {
                    //Make the Parent of the icon the Bag/Wagon Display, then make it visible
                    listOfItemsInBags[i].GetComponent<ItemObj>().io_invIconObject.GetComponent<Transform>().SetParent(MenuNavigaion.menuNavCataloguePointer.mn_PartyInvenAndWagoDisplay.transform);

                    listOfItemsInBags[i].GetComponent<ItemObj>().io_invIconObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    Vector3 curIconPos = listOfItemsInBags[i].GetComponent<ItemObj>().io_invIconObject.transform.position;
                    listOfItemsInBags[i].GetComponent<ItemObj>().io_invIconObject.GetComponent<RectTransform>().transform.position = new Vector3(curIconPos.x, curIconPos.y, 1.0f);
                    listOfItemsInBags[i].GetComponent<ItemObj>().io_invIconObject.transform.position = new Vector3(curIconPos.x, curIconPos.y, 1.0f);

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
                    listOfItemsInWagon[i].GetComponent<ItemObj>().io_invIconObject.GetComponent<RectTransform>().transform.position = new Vector3(curIconPos.x, curIconPos.y, 1.0f);
                    listOfItemsInWagon[i].GetComponent<ItemObj>().io_invIconObject.transform.position = new Vector3(curIconPos.x, curIconPos.y, 1.0f);


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
        thisItem.GetComponent<ItemObj>().io_invIconObject.GetComponent<Transform>().SetParent(MenuNavigaion.menuNavCataloguePointer.mn_ExcavatedLootPanel.transform);
        //REMOVE BUTTON FUNCTION
        thisItem.GetComponent<ItemObj>().io_invIconObject.GetComponent<Button>().onClick.RemoveListener(delegate { AddToLootPile(thisItem); });
        //ADD BUTTON FUNCTION
        thisItem.GetComponent<ItemObj>().io_invIconObject.GetComponent<Button>().onClick.AddListener(delegate { RemoveFromLootPile(thisItem); });
        listOfItemsInPile.Add(thisItem);
        UpdateWagoBagsPileDisplay();
    }

    public void RemoveFromLootPile(GameObject thisItem)
    {
        listOfItemsInPile.Remove(thisItem);
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
        thisItem.GetComponent<ItemObj>().io_invIconObject.GetComponent<Button>().onClick.RemoveListener(delegate { RemoveFromLootPile(thisItem); });
        //ADD BUTTON FUNCTION
        thisItem.GetComponent<ItemObj>().io_invIconObject.GetComponent<Button>().onClick.AddListener(delegate { AddToLootPile(thisItem); });
        UpdateWagoBagsPileDisplay();
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
                GameObject tempObj = ef_CatalogueObject.GetComponent<ItemCatalogueData>().icd_ArrayOfItems[party.GetComponent<PartyObj>().po_TotalCarriedItems[i]];
                if(tempObj != null)
                {
                    listOfItemsInBags.Add(tempObj);
                }
            }
        }
        if(party.GetComponent<PartyObj>().po_CurWagonInventorySize > 0)
        {
            for(int i = 0; i < party.GetComponent<PartyObj>().po_TotalWagonItems.Length; ++i)
            {
                GameObject tempObj = ef_CatalogueObject.GetComponent<ItemCatalogueData>().icd_ArrayOfItems[party.GetComponent<PartyObj>().po_TotalWagonItems[i]];
                if (tempObj)
                {
                    listOfItemsInWagon.Add(tempObj);
                }
            }
        }
        GameObject thisLevel = ef_CatalogueObject.GetComponent<LevelCatalogueData>().lcd_ArrayOfLevels[party.GetComponent<PartyObj>().po_LevelExploring];
        for(int i = 0; i < thisLevel.GetComponent<LevelObj>().lv_PotentialLootList.Count; ++i)
        {
            GameObject tmpObj = thisLevel.GetComponent<LevelObj>().lv_PotentialLootList[i];
            listOfItemsInPile.Add(thisLevel.GetComponent<LevelObj>().lv_PotentialLootList[i]);
        }
        //No problems here as afar as I can tell
    }
}
