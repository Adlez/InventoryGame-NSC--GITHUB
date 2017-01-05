using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class ItemCatalogueData : MonoBehaviour
{
    protected static ItemCatalogueData itemObj;
    public GameObject[] icd_ArrayOfItems = new GameObject[16];
    public GameObject icd_InvenScrollViewPanel;

    private float _invenDisplaySlotX = 64.0f; //Hardcoded for now
    private float _invenDisplaySlotY = 64.0f;
    private float _iconWidthOffset = 32.0f;
    private float _iconHeightOffset = 32.0f;
    private int _invenColumns = 4;

    public void CreateItems()
    {
        GameObject _InventoryContainer = new GameObject("InventoryContainer");
        GameObject _ItemIconContainer = new GameObject("InvenIconContainer");
        _ItemIconContainer.transform.SetParent (icd_InvenScrollViewPanel.transform);
        _ItemIconContainer.GetComponentInParent<Transform>().position = new Vector3(0.0f, 0.0f, 0.0f);
        _ItemIconContainer.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        /*_ItemIconContainer.transform.localPosition = new Vector3(
            _ItemIconContainer.transform.position.x,
            _ItemIconContainer.transform.position.y,
            //2046.0f);
            _ItemIconContainer.transform.position.z);*/

        for (int i = 0; i < icd_ArrayOfItems.Length; ++i)
        {
            //GameObject newItem = Instantiate(itemObjPrefab) as GameObject;
            GameObject newItem = new GameObject("Item");//Temp name
            newItem.transform.SetParent(_InventoryContainer.transform);
            //ItemObj item = newItem.GetComponent<ItemObj>();
            newItem.AddComponent<ItemObj>();
            newItem.transform.localScale = newItem.transform.localScale * 64;// * 100;

            newItem.GetComponent<ItemObj>().isArtefact = ItemCatalogueConstantValues.ARTEFACTYESNO[i];
            newItem.GetComponent<ItemObj>().cashValue = ItemCatalogueConstantValues.CASHVALUEOFITEM[i];
            newItem.GetComponent<ItemObj>().itemName = "TEMP_ITEM";
            newItem.GetComponent<ItemObj>().idInArrays = 0;// ItemCatalogueConstantValues.IDOFITEM[i];
            GameObject iconObj = IconObj.MakeIconObject(newItem, _ItemIconContainer);
            newItem.GetComponent<ItemObj>().invIconObject = iconObj;

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
            go.transform.position = new Vector2
                (
                _invenDisplaySlotX * (i % _invenColumns) + _iconWidthOffset, 
                (_invenDisplaySlotY * (i / _invenColumns) - _iconHeightOffset)*-1
                );
        }
    }
}

