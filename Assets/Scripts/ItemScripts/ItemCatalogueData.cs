using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class ItemCatalogueData : MonoBehaviour
{
    protected static ItemCatalogueData itemObj;
    GameObject[] icd_ArrayOfItems = new GameObject[16];
    public GameObject itemObjPrefab;
    //public GameObject It
    // Use this for initialization

    public void CreateItem()
    {
        for(int i = 0; i < icd_ArrayOfItems.Length; ++i)
        {
            GameObject newItem = Instantiate(itemObjPrefab) as GameObject;
            ItemObj item = newItem.GetComponent<ItemObj>();

            item.cashValue = icd_ArrayOfItems[i].GetComponent<ItemObj>().cashValue;
        }
    }
}

[System.Serializable]
public class ItemObj
{
    public bool isArtefact;
    public int cashValue;
    public string name;
    public Sprite invIcon;
    public Sprite fullImage;
    public string description;
    public int idInArrays;
    public float oddsOfFinding;
    public int maxFindAtOnce;
    public int typeID;
}
