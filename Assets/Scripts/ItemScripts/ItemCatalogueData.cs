using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class ItemCatalogueData : MonoBehaviour
{
    protected static ItemCatalogueData itemObj;
    public GameObject[] icd_ArrayOfItems = new GameObject[16];
    public GameObject itemObjPrefab;

    private float _invenDisplaySlotX = 25.0f; //Hardcoded for now
    private float _invenDisplaySlotY = 25.0f;
    private float _iconWidthOffset = 32.0f;
    private float _iconHeightOffset = 32.0f;
    private int _invenColumns = 5;

    public void CreateItems()
    {
        for(int i = 0; i < icd_ArrayOfItems.Length; ++i)
        {
            //GameObject newItem = Instantiate(itemObjPrefab) as GameObject;
            GameObject newItem = new GameObject("Item");//Temp name
            //ItemObj item = newItem.GetComponent<ItemObj>();
            newItem.AddComponent<ItemObj>();

            newItem.GetComponent<ItemObj>().isArtefact = ItemCatalogueConstantValues.ARTEFACTYESNO[i];
            newItem.GetComponent<ItemObj>().cashValue = ItemCatalogueConstantValues.CASHVALUEOFITEM[i];
            newItem.GetComponent<ItemObj>().invIconObject = IconObj.MakeIconObject(newItem);
            newItem.GetComponent<ItemObj>().itemName = "TEMP_ITEM";
            //remaining values assigned here
            //add to array
            icd_ArrayOfItems[i] = newItem;

        }
    }

    public void DisplayAllItems()
    {
        if(icd_ArrayOfItems[0] == null)
        {
            CreateItems();
        }
        for(int i = 0; i < icd_ArrayOfItems.Length; ++i)
        {
            GameObject go = icd_ArrayOfItems[i].GetComponent<ItemObj>().invIconObject;
            go.transform.position = new Vector2(_invenDisplaySlotX * i + _iconWidthOffset, 
                _invenDisplaySlotY * (i / _invenColumns) + _iconHeightOffset);
        }
    }
}

