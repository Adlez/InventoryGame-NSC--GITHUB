using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class ItemCatalogueData : MonoBehaviour
{
    public static ItemCatalogueData itemObj;
    public GameObject[] icd_ArrayOfItems = new GameObject[16];
    public GameObject icd_InvenScrollViewPanel;
    public GameObject icd_TempItemDisplay;
    public GameObject icd_TestShopDisplayPanel;

    private float _invenDisplaySlotX = 64.0f; //Hardcoded for now
    private float _invenDisplaySlotY = 64.0f;
    private float _iconWidthOffset = 32.0f;
    private float _iconHeightOffset = 32.0f;
    private int _invenColumns = 4;
    public bool icd_ItemsCreated = false;

    public void CreateItems()
    {
        if (!icd_ItemsCreated)
        {
            GameObject _InventoryContainer = new GameObject("InventoryContainer");
            _InventoryContainer.name = "InventoryContainer";
            //GameObject _ItemIconContainer = new GameObject("InvenIconContainer");

            for (int i = 0; i < icd_ArrayOfItems.Length; ++i)
            {
                GameObject newItem = new GameObject("Item");//Temp name
                newItem.transform.SetParent(_InventoryContainer.transform);
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

                GameObject iconObj = IconObj.MakeIconObject(newItem, icd_TempItemDisplay, newItem.GetComponent<ItemObj>().io_ObjectType);
                GameObject iconObjS = IconObj.MakeIconObject(newItem, icd_TempItemDisplay, newItem.GetComponent<ItemObj>().io_ObjectType);
                iconObj.transform.SetParent(icd_TempItemDisplay.transform);
                iconObjS.transform.SetParent(icd_TempItemDisplay.transform);
                newItem.GetComponent<ItemObj>().io_invIconObject = iconObj;
                newItem.GetComponent<ItemObj>().io_storeIconObject = iconObjS;

                newItem.name = newItem.GetComponent<ItemObj>().itemName;
                newItem.GetComponent<ItemObj>().io_invIconObject.GetComponent<IconObj>().GetComponent<Image>().sprite = ItemCatalogueConstantValues.ICONOFITEM[i];
                newItem.GetComponent<ItemObj>().io_storeIconObject.GetComponent<IconObj>().GetComponent<Image>().sprite = ItemCatalogueConstantValues.ICONOFITEM[i];

                newItem.GetComponent<ItemObj>().io_invIconObject.SetActive(false);
                newItem.GetComponent<ItemObj>().io_storeIconObject.SetActive(false);

                newItem.GetComponent<ItemObj>().io_invIconObject.name += " Inventory";
                newItem.GetComponent<ItemObj>().io_storeIconObject.name = newItem.name + " Icon InStore";

                //add to array
                icd_ArrayOfItems[i] = newItem;
            }
            icd_ItemsCreated = true;
        }
    }

    public void DisplayTestShop()
    {
        if (!icd_ItemsCreated)
        {
            CreateItems();
        }
        for (int i = 0; i < icd_ArrayOfItems.Length; ++i)
        {
            icd_ArrayOfItems[i].GetComponent<ItemObj>().io_invIconObject.SetActive(true);
            icd_ArrayOfItems[i].GetComponent<ItemObj>().io_invIconObject.GetComponent<Transform>().SetParent(icd_TestShopDisplayPanel.transform);
            icd_ArrayOfItems[i].GetComponent<ItemObj>().io_invIconObject.GetComponent<Transform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        PositionIconsOnScreen();
    }

    public void DisplayAllItems()
    {
        if(!icd_ItemsCreated)
        {
            CreateItems();
        }
        for (int i = 0; i < icd_ArrayOfItems.Length; ++i)
        {
            icd_ArrayOfItems[i].GetComponent<ItemObj>().io_invIconObject.SetActive(true);
            icd_ArrayOfItems[i].GetComponent<ItemObj>().io_invIconObject.GetComponent<Transform>().SetParent(icd_TempItemDisplay.transform);
            icd_ArrayOfItems[i].GetComponent<ItemObj>().io_invIconObject.GetComponent<Transform>().localScale = new Vector3(64.0f, 64.0f, 1.0f);
        }
        PositionIconsOnScreen();
    }

    public void DisplayPartyBags(GameObject party)
    {
        for(int i = 0; i < party.GetComponent<PartyObj>().po_InventoryIndex.Length; ++i)
        {
            //GameObject NewItemIcon = 
            //party.GetComponent<PartyObj>().po_InventoryIndex
        }
    }


    public void PositionIconsOnScreen()
    {
        for(int i = 0; i < icd_ArrayOfItems.Length; ++i)
        {
            Vector3 iconPos = icd_ArrayOfItems[i].GetComponent<ItemObj>().io_invIconObject.GetComponent<Transform>().position;
            iconPos.x = _invenDisplaySlotX * (i % _invenColumns) + _iconWidthOffset;
            iconPos.y = (_invenDisplaySlotY * (i / _invenColumns) - _iconHeightOffset) * -1;
            icd_ArrayOfItems[i].GetComponent<ItemObj>().io_invIconObject.GetComponent<Transform>().position = new Vector3(iconPos.x, iconPos.y, 1.0f);
        }
    }
}

