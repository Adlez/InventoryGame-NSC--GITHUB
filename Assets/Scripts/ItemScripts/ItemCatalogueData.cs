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
            newItem.GetComponent<ItemObj>().itemName = ItemCatalogueConstantValues.NAMEOFITEM[i];
            newItem.GetComponent<ItemObj>().idInArrays = ItemCatalogueConstantValues.IDOFITEM[i];
            newItem.GetComponent<ItemObj>().invIcon = ItemCatalogueConstantValues.ICONOFITEM[i]; //Necessary?
            newItem.GetComponent<ItemObj>().fullImage = ItemCatalogueConstantValues.PORTRAITOFITEM[i];
            newItem.GetComponent<ItemObj>().description = ItemCatalogueConstantValues.ITEMDESCRIPTION[i];
            newItem.GetComponent<ItemObj>().oddsOfFinding = ItemCatalogueConstantValues.ODDSOFFINDING[i];
            newItem.GetComponent<ItemObj>().maxFindAtOnce = ItemCatalogueConstantValues.MAXAMOUNTTOFIND[i];
            newItem.GetComponent<ItemObj>().typeID = ItemCatalogueConstantValues.ITEMTYPEID[i];

            GameObject iconObj = IconObj.MakeIconObject(newItem, icd_InvenScrollViewPanel);
            newItem.GetComponent<ItemObj>().invIconObject = iconObj;

            newItem.name = newItem.GetComponent<ItemObj>().itemName;
            newItem.GetComponent<ItemObj>().invIconObject.GetComponent<IconObj>().GetComponent<Image>().sprite = ItemCatalogueConstantValues.ICONOFITEM[i];


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
        PositionIconsOnScreen();
    }

    public void PositionIconsOnScreen()
    {
        for(int i = 0; i < icd_ArrayOfItems.Length; ++i)
        {
            Vector3 iconPos = icd_ArrayOfItems[i].GetComponent<ItemObj>().invIconObject.GetComponent<Transform>().position;
            iconPos.x = _invenDisplaySlotX * (i % _invenColumns) + _iconWidthOffset;
            iconPos.y = (_invenDisplaySlotY * (i / _invenColumns) - _iconHeightOffset) * -1;
            iconPos.x = 0.0f;
            iconPos.y = 0.0f;
            icd_ArrayOfItems[i].GetComponent<ItemObj>().invIconObject.GetComponent<Transform>().position = new Vector3(iconPos.x, iconPos.y, 100.0f);
        }
    }
}

