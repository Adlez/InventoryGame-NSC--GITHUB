using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopDataCatalogue : MonoBehaviour
{
    public static ShopDataCatalogue sdc_ShopCataloguePointer;

    public GameObject sdc_GameControllerObj;
    public GameObject sdc_ItemCatalogue;

    public int[][] sds_AllShopsStock = new int[6][];

    //public GameObject[][] sds_ArrayOfStock = new GameObject[6][16];
    public GameObject[] sds_Shop1ArrayOfStock = new GameObject[16];
    public GameObject[] sds_Shop2ArrayOfStock = new GameObject[16];
    public GameObject[] sds_Shop3ArrayOfStock = new GameObject[16];
    public GameObject[] sds_Shop4ArrayOfStock = new GameObject[16];
    public GameObject[] sds_Shop5ArrayOfStock = new GameObject[16];
    public GameObject[] sds_Shop6ArrayOfStock = new GameObject[16];

    public GameObject sds_ShopScrollViewPanel;

    private float _invenDisplaySlotX = 64.0f; //Hardcoded for now
    private float _invenDisplaySlotY = 64.0f;
    private float _iconWidthOffset = 32.0f;
    private float _iconHeightOffset = 32.0f;
    private int _invenColumns = 4;
    private bool _StockItemsCreated = false;

    public void StockTheShelves()
    {
        sds_AllShopsStock = ShopCatalogueConstantValues.SHOPSTOCK;

        for(int i = 0; i < sds_AllShopsStock.Length; ++i)
        {
            for(int j = 0; j < sds_Shop1ArrayOfStock.Length; ++j)
            {
                //sds_AllShopsStock[i][j] = ItemCatalogueData.itemObj.icd_ArrayOfItems[j].GetComponent<ItemObj>().idInArrays;
            }
        }
        for(int i = 0; i < sds_Shop1ArrayOfStock.Length; ++i)
        {
            GameObject tempItem = ItemCatalogueData.itemObj.icd_ArrayOfItems[i]; //Don't code at 03:00
            sds_Shop1ArrayOfStock[i] = tempItem; //Fix this garbage after you wake up
        }
        for (int i = 0; i < sds_Shop2ArrayOfStock.Length; ++i)
        {
            GameObject tempItem = ItemCatalogueData.itemObj.icd_ArrayOfItems[i];
            sds_Shop2ArrayOfStock[i] = tempItem;
        }
        for (int i = 0; i < sds_Shop3ArrayOfStock.Length; ++i)
        {
            GameObject tempItem = ItemCatalogueData.itemObj.icd_ArrayOfItems[i];
            sds_Shop3ArrayOfStock[i] = tempItem;
        }
        for (int i = 0; i < sds_Shop4ArrayOfStock.Length; ++i)
        {
            GameObject tempItem = ItemCatalogueData.itemObj.icd_ArrayOfItems[i];
            sds_Shop4ArrayOfStock[i] = tempItem;
        }
        for (int i = 0; i < sds_Shop5ArrayOfStock.Length; ++i)
        {
            GameObject tempItem = ItemCatalogueData.itemObj.icd_ArrayOfItems[i];
            sds_Shop5ArrayOfStock[i] = tempItem;
        }
        for (int i = 0; i < sds_Shop6ArrayOfStock.Length; ++i)
        {
            GameObject tempItem = ItemCatalogueData.itemObj.icd_ArrayOfItems[i];
            sds_Shop6ArrayOfStock[i] = tempItem;
        }
        CreateShopStockIcons();
    }

    public void BuyShopItem(GameObject item)
    {
        if (item.GetComponent<ItemObj>().cashValue < GameControllerScript.gc_Munnies)
        {
            //Confirm the purchase
            //Add the item to the "Stash", inventory whatever; add by array
            GameControllerScript.gc_StashOfItems[item.GetComponent<ItemObj>().idInArrays]++;
            //take the player's money
            GameControllerScript.gc_Munnies -= item.GetComponent<ItemObj>().cashValue;
            item.GetComponent<ItemObj>().io_InInventory = true;
            //update the inventory display
            sdc_GameControllerObj.GetComponent<GameControllerScript>().UpdateMunnieDisplay();
        }
    }

    public void CreateShopStockIcons()
    {
        if (!_StockItemsCreated)
        {
            for (int i = 0; i < sds_Shop1ArrayOfStock.Length; ++i)
            {
                sds_Shop1ArrayOfStock[i] = sdc_ItemCatalogue.GetComponent<ItemCatalogueData>().icd_ArrayOfItems[i];//.itemObj.icd_ArrayOfItems[i];
                GameObject stockItem = sds_Shop1ArrayOfStock[i];
                if (stockItem == null)
                {
                    GameObject stockIconObj = IconObj.MakeIconObject(stockItem, sds_ShopScrollViewPanel, stockItem.GetComponent<ItemObj>().io_ObjectType);
                    stockIconObj.GetComponent<Button>().onClick.AddListener(delegate { BuyShopItem(stockIconObj); });

                    stockItem.GetComponent<ItemObj>().invIconObject = stockIconObj;

                    stockItem.name = stockItem.GetComponent<ItemObj>().itemName + "Shop 1 Item " + i.ToString() + " Icon";
                    stockItem.GetComponent<ItemObj>().invIconObject.GetComponent<IconObj>().GetComponent<Image>().sprite = ItemCatalogueConstantValues.ICONOFITEM[i];

                    //stockItem.GetComponent<ItemObj>().invIconObject.SetActive(false);
                }
                stockItem.GetComponent<ItemObj>().invIconObject.GetComponent<Button>().onClick.AddListener(delegate { BuyShopItem(stockItem); }); 
            }
            for (int i = 0; i < sds_Shop2ArrayOfStock.Length; ++i)
            {
                sds_Shop2ArrayOfStock[i] = sdc_ItemCatalogue.GetComponent<ItemCatalogueData>().icd_ArrayOfItems[i];//.itemObj.icd_ArrayOfItems[i];
                GameObject stockItem = sds_Shop2ArrayOfStock[i];
                if (stockItem == null)
                {
                    GameObject stockIconObj = IconObj.MakeIconObject(stockItem, sds_ShopScrollViewPanel, stockItem.GetComponent<ItemObj>().io_ObjectType);
                    stockIconObj.GetComponent<Button>().onClick.AddListener(delegate { BuyShopItem(stockIconObj); });

                    stockItem.GetComponent<ItemObj>().invIconObject = stockIconObj;

                    stockItem.name = stockItem.GetComponent<ItemObj>().itemName + "Shop 1 Item " + i.ToString() + " Icon";
                    stockItem.GetComponent<ItemObj>().invIconObject.GetComponent<IconObj>().GetComponent<Image>().sprite = ItemCatalogueConstantValues.ICONOFITEM[i];

                    //stockItem.GetComponent<ItemObj>().invIconObject.SetActive(false);
                }
                stockItem.GetComponent<ItemObj>().invIconObject.GetComponent<Button>().onClick.AddListener(delegate { BuyShopItem(stockItem); });
            }
            for (int i = 0; i < sds_Shop3ArrayOfStock.Length; ++i)
            {
                sds_Shop3ArrayOfStock[i] = sdc_ItemCatalogue.GetComponent<ItemCatalogueData>().icd_ArrayOfItems[i];//.itemObj.icd_ArrayOfItems[i];
                GameObject stockItem = sds_Shop3ArrayOfStock[i];
                if (stockItem == null)
                {
                    GameObject stockIconObj = IconObj.MakeIconObject(stockItem, sds_ShopScrollViewPanel, stockItem.GetComponent<ItemObj>().io_ObjectType);
                    stockIconObj.GetComponent<Button>().onClick.AddListener(delegate { BuyShopItem(stockIconObj); });

                    stockItem.GetComponent<ItemObj>().invIconObject = stockIconObj;

                    stockItem.name = stockItem.GetComponent<ItemObj>().itemName + "Shop 1 Item " + i.ToString() + " Icon";
                    stockItem.GetComponent<ItemObj>().invIconObject.GetComponent<IconObj>().GetComponent<Image>().sprite = ItemCatalogueConstantValues.ICONOFITEM[i];

                    //stockItem.GetComponent<ItemObj>().invIconObject.SetActive(false);
                }
                stockItem.GetComponent<ItemObj>().invIconObject.GetComponent<Button>().onClick.AddListener(delegate { BuyShopItem(stockItem); });
            }
            for (int i = 0; i < sds_Shop4ArrayOfStock.Length; ++i)
            {
                sds_Shop4ArrayOfStock[i] = sdc_ItemCatalogue.GetComponent<ItemCatalogueData>().icd_ArrayOfItems[i];//.itemObj.icd_ArrayOfItems[i];
                GameObject stockItem = sds_Shop4ArrayOfStock[i];
                if (stockItem == null)
                {
                    GameObject stockIconObj = IconObj.MakeIconObject(stockItem, sds_ShopScrollViewPanel, stockItem.GetComponent<ItemObj>().io_ObjectType);
                    stockIconObj.GetComponent<Button>().onClick.AddListener(delegate { BuyShopItem(stockIconObj); });

                    stockItem.GetComponent<ItemObj>().invIconObject = stockIconObj;

                    stockItem.name = stockItem.GetComponent<ItemObj>().itemName + "Shop 1 Item " + i.ToString() + " Icon";
                    stockItem.GetComponent<ItemObj>().invIconObject.GetComponent<IconObj>().GetComponent<Image>().sprite = ItemCatalogueConstantValues.ICONOFITEM[i];

                    //stockItem.GetComponent<ItemObj>().invIconObject.SetActive(false);
                }
                stockItem.GetComponent<ItemObj>().invIconObject.GetComponent<Button>().onClick.AddListener(delegate { BuyShopItem(stockItem); });
            }
            for (int i = 0; i < sds_Shop5ArrayOfStock.Length; ++i)
            {
                sds_Shop5ArrayOfStock[i] = sdc_ItemCatalogue.GetComponent<ItemCatalogueData>().icd_ArrayOfItems[i];//.itemObj.icd_ArrayOfItems[i];
                GameObject stockItem = sds_Shop5ArrayOfStock[i];
                if (stockItem == null)
                {
                    GameObject stockIconObj = IconObj.MakeIconObject(stockItem, sds_ShopScrollViewPanel, stockItem.GetComponent<ItemObj>().io_ObjectType);
                    stockIconObj.GetComponent<Button>().onClick.AddListener(delegate { BuyShopItem(stockIconObj); });

                    stockItem.GetComponent<ItemObj>().invIconObject = stockIconObj;

                    stockItem.name = stockItem.GetComponent<ItemObj>().itemName + "Shop 1 Item " + i.ToString() + " Icon";
                    stockItem.GetComponent<ItemObj>().invIconObject.GetComponent<IconObj>().GetComponent<Image>().sprite = ItemCatalogueConstantValues.ICONOFITEM[i];

                    //stockItem.GetComponent<ItemObj>().invIconObject.SetActive(false);
                }
                stockItem.GetComponent<ItemObj>().invIconObject.GetComponent<Button>().onClick.AddListener(delegate { BuyShopItem(stockItem); });
            }
            for (int i = 0; i < sds_Shop6ArrayOfStock.Length; ++i)
            {
                sds_Shop6ArrayOfStock[i] = sdc_ItemCatalogue.GetComponent<ItemCatalogueData>().icd_ArrayOfItems[i];//.itemObj.icd_ArrayOfItems[i];
                GameObject stockItem = sds_Shop6ArrayOfStock[i];
                if (stockItem == null)
                {
                    GameObject stockIconObj = IconObj.MakeIconObject(stockItem, sds_ShopScrollViewPanel, stockItem.GetComponent<ItemObj>().io_ObjectType);
                    stockIconObj.GetComponent<Button>().onClick.AddListener(delegate { BuyShopItem(stockIconObj); });

                    stockItem.GetComponent<ItemObj>().invIconObject = stockIconObj;

                    stockItem.name = stockItem.GetComponent<ItemObj>().itemName + "Shop 1 Item " + i.ToString() + " Icon";
                    stockItem.GetComponent<ItemObj>().invIconObject.GetComponent<IconObj>().GetComponent<Image>().sprite = ItemCatalogueConstantValues.ICONOFITEM[i];

                    //stockItem.GetComponent<ItemObj>().invIconObject.SetActive(false);
                }
                stockItem.GetComponent<ItemObj>().invIconObject.GetComponent<Button>().onClick.AddListener(delegate { BuyShopItem(stockItem); });
            }
        }
        
        _StockItemsCreated = true;
    }

    public void DisplayTestShop()
    {
        /*if (!_StockItemsCreated)
        {
            CreateShopStockIcons();
        }
        for (int i = 0; i < sds_ArrayOfStock.Length; ++i)
        {
            sds_ArrayOfStock[i].GetComponent<ItemObj>().invIconObject.SetActive(true);
            sds_ArrayOfStock[i].GetComponent<ItemObj>().invIconObject.GetComponent<Transform>().SetParent(sds_ShopScrollViewPanel.transform);
            sds_ArrayOfStock[i].GetComponent<ItemObj>().invIconObject.GetComponent<Transform>().localScale = new Vector3(64.0f, 64.0f, 1.0f);
        }
        PositionIconsOnScreen();*/
    }

    public void DisplayShop1()
    {
        if (!_StockItemsCreated)
        {
            StockTheShelves();
        }
        for(int i = 0; i < sds_Shop1ArrayOfStock.Length; ++i)
        {
            sds_Shop1ArrayOfStock[i].GetComponent<ItemObj>().invIconObject.SetActive(true);
            sds_Shop1ArrayOfStock[i].GetComponent<ItemObj>().invIconObject.GetComponent<Transform>().SetParent(sds_ShopScrollViewPanel.transform);
            sds_Shop1ArrayOfStock[i].GetComponent<ItemObj>().invIconObject.GetComponent<Transform>().localScale = new Vector3(64.0f, 64.0f, 1.0f);

        }
    }

    public void PositionIconsOnScreen(GameObject[] iconsToPosition)
    {
        for (int i = 0; i < iconsToPosition.Length; ++i)
        {
            Vector3 iconPos = iconsToPosition[i].GetComponent<ItemObj>().invIconObject.GetComponent<Transform>().position;
            iconPos.x = _invenDisplaySlotX * (i % _invenColumns) + _iconWidthOffset;
            iconPos.y = (_invenDisplaySlotY * (i / _invenColumns) - _iconHeightOffset) * -1;
            iconsToPosition[i].GetComponent<ItemObj>().invIconObject.GetComponent<Transform>().position = new Vector3(iconPos.x, iconPos.y, 100.0f);
        }
    }
}
