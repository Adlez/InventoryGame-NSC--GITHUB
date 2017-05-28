using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObj : MonoBehaviour
{
    //add "public" as necessary
    public GameObject lv_GameController;
    public GameObject lv_PartyObject;
    public GameObject lv_IconObject;
    public string lv_Name;
    public string lv_ObjectType = "Level";
    public int lv_ID;

    public int[] lv_ToolRequiredIndex;
    public int[] lv_LootIndex; //corresponds to treasure array, used to search for the right item
    public List<GameObject> lv_PotentialLootList = new List<GameObject>();

    public float lv_Distance;
    public bool lv_IsActive;

    private GameObject _potentialLootItem;

    public void ExcavateLevel(GameObject theParty) //poor name, should be: FillLevelLootList
    {
        while (lv_PotentialLootList.Count < 4) // lv_PotentialLootList.Count <= 4)//if there's less than 5 items, keep going
        { 
            FillLevelLootList();
        }
        theParty.GetComponent<PartyObj>().po_ItemsInExcavationPile = lv_PotentialLootList;
    }

    public void FillLevelLootList()
    {
        //decide which loot can be gotten
        for (int i = 0; i < lv_LootIndex.Length; ++i)
        {
            if (lv_LootIndex[i] > 0)
            {
                int numFound = 0;
                //var referenceToItemCatalogue = ItemCatalogueData.itemObj;
                GameObject Item = lv_GameController.GetComponent<GameControllerScript>().gc_CatalogueObj.GetComponent<ItemCatalogueData>().icd_ArrayOfItems[i];
                int numCanFindAtOnce = Item.GetComponent<ItemObj>().maxFindAtOnce;
                //ItemCatalogueData.itemObj.icd_ArrayOfItems[i].GetComponent<ItemObj>().maxFindAtOnce;

                if (numCanFindAtOnce >= numFound)
                {
                    RollForItem(i);
                    numFound++;
                }
            }
        }
    }

    private void RollForItem(int index)
    {
        int odds = (int)Random.Range(0, 100);

        if (odds <= lv_GameController.GetComponent<GameControllerScript>().gc_CatalogueObj.GetComponent<ItemCatalogueData>().icd_ArrayOfItems[index].GetComponent<ItemObj>().oddsOfFinding)
        {
            _potentialLootItem = new GameObject(); //Create new object
            _potentialLootItem = lv_GameController.GetComponent<GameControllerScript>().gc_CatalogueObj.GetComponent<ItemCatalogueData>().icd_ArrayOfItems[index]; //Assign the new object the attributes of the correct item
            lv_PotentialLootList.Add(_potentialLootItem); //Add the new object to the list
        }
    }

    
}
