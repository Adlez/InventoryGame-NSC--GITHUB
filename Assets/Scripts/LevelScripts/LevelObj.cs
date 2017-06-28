using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class LevelObj : MonoBehaviour
{
    //add "public" as necessary
    public GameObject lv_GameController;
    public GameObject lv_PartyObject;
    public GameObject lv_IconObject;
    public GameObject lv_LootDisplayPanel;
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
                GameObject LootItem = lv_GameController.GetComponent<GameControllerScript>().gc_CatalogueObj.GetComponent<ItemCatalogueData>().icd_ArrayOfItems[i];
                int numCanFindAtOnce = LootItem.GetComponent<ItemObj>().maxFindAtOnce;
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
        lv_LootDisplayPanel = lv_GameController.GetComponent<GameControllerScript>().gc_LootDisplayPanel4ExcavationFuncitons;
        int odds = (int)UnityEngine.Random.Range(0, 100);

        if (odds <= lv_GameController.GetComponent<GameControllerScript>().gc_CatalogueObj.GetComponent<ItemCatalogueData>().icd_ArrayOfItems[index].GetComponent<ItemObj>().oddsOfFinding)
        {
 //           _potentialLootItem = new GameObject(); //Create new object
 //           _potentialLootItem = lv_GameController.GetComponent<GameControllerScript>().gc_CatalogueObj.GetComponent<ItemCatalogueData>().icd_ArrayOfItems[index]; //Assign the new object the attributes of the correct item
            GameObject lootItem = lv_GameController.GetComponent<GameControllerScript>().gc_CatalogueObj.GetComponent<ItemCatalogueData>().CreateAnItemAndIcon(index, -1, lv_LootDisplayPanel, lv_LootDisplayPanel);//shouldn't be the same when done
            lootItem.GetComponent<ItemObj>().io_invIconObject.AddComponent<Button>();
            lootItem.GetComponent<ItemObj>().io_invIconObject.GetComponent<Button>().onClick.AddListener(delegate { lv_GameController.GetComponent<GameControllerScript>().gc_ContainerChangeObject.GetComponent<ChangingContainerScript>().ChangeContainer(lootItem); });

//            Destroy(_potentialLootItem);

            lv_PotentialLootList.Add(lootItem); //Add the new object to the list
        }
    }

    
}
