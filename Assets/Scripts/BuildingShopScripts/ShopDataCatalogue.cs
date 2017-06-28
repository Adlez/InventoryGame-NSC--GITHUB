using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopDataCatalogue : MonoBehaviour
{
    public static ShopDataCatalogue sdc_ShopCataloguePointer;

    public GameObject sdc_GameControllerObj;
    public GameObject sdc_ItemCatalogue;
    public GameObject sdc_StashContainer;

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

        //Shop1Shelves, Toolshop
        sds_Shop1ArrayOfStock[0] = sdc_ItemCatalogue.GetComponent<ItemCatalogueData>().icd_ArrayOfItems[0];
        sds_Shop1ArrayOfStock[1] = sdc_ItemCatalogue.GetComponent<ItemCatalogueData>().icd_ArrayOfItems[1];
        sds_Shop1ArrayOfStock[2] = sdc_ItemCatalogue.GetComponent<ItemCatalogueData>().icd_ArrayOfItems[2];
        sds_Shop1ArrayOfStock[3] = sdc_ItemCatalogue.GetComponent<ItemCatalogueData>().icd_ArrayOfItems[3];
        sds_Shop1ArrayOfStock[4] = null;
        sds_Shop1ArrayOfStock[5] = null;
        sds_Shop1ArrayOfStock[6] = null;
        sds_Shop1ArrayOfStock[7] = null;
        sds_Shop1ArrayOfStock[8] = null;
        sds_Shop1ArrayOfStock[9] = null;
        sds_Shop1ArrayOfStock[10] = null;
        sds_Shop1ArrayOfStock[11] = null;
        sds_Shop1ArrayOfStock[12] = null;
        sds_Shop1ArrayOfStock[13] = null;
        sds_Shop1ArrayOfStock[14] = null;
        sds_Shop1ArrayOfStock[15] = null;


        CreateShopStockIcons();
        _StockItemsCreated = true;
    }

    public void BuyShopItem(GameObject item)
    {
        if (item.GetComponent<ItemObj>().cashValue < GameControllerScript.gc_Munnies)
        {
            //Confirm the purchase
            
            //Add the item to the "Stash", inventory whatever; add by array
            GameControllerScript.gc_StashOfItems[item.GetComponent<ItemObj>().idInArrays] += 1;//just an int, not the object itself.
            GameObject boughtItem = sdc_GameControllerObj.GetComponent<GameControllerScript>().gc_CatalogueObj.GetComponent<ItemCatalogueData>().CreateAnItemAndIcon(item.GetComponent<ItemObj>().idInArrays, -3, sdc_StashContainer, sdc_StashContainer);

            //GameControllerScript.gc_PlayerStash.Add(boughtItem);//For the sake of having the objects created
            //boughtItem.GetComponent<ItemObj>().io_CurrentContainer = -3; //Set container to Stash
            //Remove any button Functionality that may be present
            boughtItem.GetComponent<ItemObj>().io_invIconObject.GetComponent<Button>().onClick.RemoveAllListeners();
            //Add Function to button
            boughtItem.GetComponent<ItemObj>().io_invIconObject.GetComponent<Button>().onClick.AddListener(delegate 
                { sdc_GameControllerObj.GetComponent<GameControllerScript>().gc_ContainerChangeObject.GetComponent<ChangingContainerScript>().ChangeContainer(boughtItem); });
            GameControllerScript.gc_PlayerStash.Add(boughtItem);
            //take the player's money
            GameControllerScript.gc_Munnies -= boughtItem.GetComponent<ItemObj>().cashValue;
            boughtItem.GetComponent<ItemObj>().io_InInventory = true;

            //update the inventory display
            sdc_GameControllerObj.GetComponent<GameControllerScript>().UpdateMunnieDisplay();

            //Note on some functionality:
            //Originally icons were meant to be created and destroyed dynamically when panels were being displayed in order to save processor power and memory
            //This has proven to make the code too complex across all the scripts since I didn't keep good track of when and where things are created and destroyed
            //Now the code looks like a complete mess, but is functional across the board; At least functional where it counts so far.
        }
    }

    public void CreateShopStockIcons()
    {
        if (!_StockItemsCreated)
        {
            for (int i = 0; i < sds_Shop1ArrayOfStock.Length; ++i)
            {
                GameObject stockItem = sds_Shop1ArrayOfStock[i];
                if (stockItem != null)
                {
                    stockItem.GetComponent<ItemObj>().io_CurrentContainer = -2;//Set Container to Shop
                    stockItem.GetComponent<ItemObj>().io_storeIconObject.GetComponent<Button>().onClick.AddListener(delegate { BuyShopItem(stockItem); });
                }
                #region Old maybe useful
                //sds_Shop1ArrayOfStock[i] = sdc_ItemCatalogue.GetComponent<ItemCatalogueData>().icd_ArrayOfItems[i];//.itemObj.icd_ArrayOfItems[i];
                /*
                if (stockItem == null)
                {
                    GameObject stockIconObj = IconObj.MakeIconObject(stockItem, sds_ShopScrollViewPanel, stockItem.GetComponent<ItemObj>().io_ObjectType);
                    stockIconObj.GetComponent<Button>().onClick.AddListener(delegate { BuyShopItem(stockIconObj); });
                    stockIconObj.name = "Shop 1 Item " + i.ToString() + " Icon";

                    stockItem.GetComponent<ItemObj>().invIconObject = stockIconObj;

                    stockItem.name = stockItem.GetComponent<ItemObj>().itemName + "Shop 1 Item " + i.ToString() + " Icon";
                    stockItem.GetComponent<ItemObj>().invIconObject.GetComponent<IconObj>().GetComponent<Image>().sprite = ItemCatalogueConstantValues.ICONOFITEM[i];

                    //stockItem.GetComponent<ItemObj>().invIconObject.SetActive(false);
                }
                else
                {
                    stockItem.GetComponent<ItemObj>().invIconObject.GetComponent<Button>().onClick.AddListener(delegate { BuyShopItem(stockItem); });
                }*/
#endregion
            }
            for (int i = 0; i < sds_Shop2ArrayOfStock.Length; ++i)
            {
                GameObject stockItem = sds_Shop2ArrayOfStock[i];
                if (stockItem != null)
                {
                    stockItem.GetComponent<ItemObj>().io_CurrentContainer = -2;//Set Container to Shop
                    stockItem.GetComponent<ItemObj>().io_storeIconObject.GetComponent<Button>().onClick.AddListener(delegate { BuyShopItem(stockItem); });
                }
            }
            for (int i = 0; i < sds_Shop3ArrayOfStock.Length; ++i)
            {
                GameObject stockItem = sds_Shop3ArrayOfStock[i];
                if (stockItem != null)
                {
                    stockItem.GetComponent<ItemObj>().io_CurrentContainer = -2;//Set Container to Shop
                    stockItem.GetComponent<ItemObj>().io_storeIconObject.GetComponent<Button>().onClick.AddListener(delegate { BuyShopItem(stockItem); });
                }
            }
            for (int i = 0; i < sds_Shop4ArrayOfStock.Length; ++i)
            {
                GameObject stockItem = sds_Shop4ArrayOfStock[i];
                if (stockItem != null)
                {
                    stockItem.GetComponent<ItemObj>().io_CurrentContainer = -2;//Set Container to Shop
                    stockItem.GetComponent<ItemObj>().io_storeIconObject.GetComponent<Button>().onClick.AddListener(delegate { BuyShopItem(stockItem); });
                }
                
            }
            for (int i = 0; i < sds_Shop5ArrayOfStock.Length; ++i)
            {
                GameObject stockItem = sds_Shop5ArrayOfStock[i];
                if (stockItem != null)
                {
                    stockItem.GetComponent<ItemObj>().io_CurrentContainer = -2;//Set Container to Shop
                    stockItem.GetComponent<ItemObj>().io_storeIconObject.GetComponent<Button>().onClick.AddListener(delegate { BuyShopItem(stockItem); });
                }
            }
            for (int i = 0; i < sds_Shop6ArrayOfStock.Length; ++i)
            {
                GameObject stockItem = sds_Shop6ArrayOfStock[i];
                if (stockItem != null)
                {
                    stockItem.GetComponent<ItemObj>().io_CurrentContainer = -2;//Set Container to Shop
                    stockItem.GetComponent<ItemObj>().io_storeIconObject.GetComponent<Button>().onClick.AddListener(delegate { BuyShopItem(stockItem); });
                }
            }
        }
    }

    public void DisplayTestShop(int shopID)
    {
        if(shopID == 0)
        {
            DisplayShop1(sds_Shop1ArrayOfStock);
        }
    }

    public void DisplayShop1(GameObject[] shopStock)
    {
        if (!_StockItemsCreated)
        {
            StockTheShelves();
        }
        for (int i = 0; i < sds_Shop1ArrayOfStock.Length; ++i)
        {
            if (sds_Shop1ArrayOfStock[i] != null)
            {
                shopStock[i].GetComponent<ItemObj>().io_storeIconObject.SetActive(true);
                shopStock[i].GetComponent<ItemObj>().io_storeIconObject.GetComponent<Transform>().SetParent(sds_ShopScrollViewPanel.transform);
                shopStock[i].GetComponent<ItemObj>().io_storeIconObject.GetComponent<Transform>().localScale = new Vector3(64.0f, 64.0f, 1.0f);
            }
        }
        PositionIconsOnScreen(sds_Shop1ArrayOfStock);
    }

    public void PositionIconsOnScreen(GameObject[] iconsToPosition)
    {
        for (int i = 0; i < iconsToPosition.Length; ++i)
        {
            if (iconsToPosition[i] != null)
            { 
                Vector3 iconPos = iconsToPosition[i].GetComponent<ItemObj>().io_storeIconObject.GetComponent<Transform>().position;
                iconPos.x = _invenDisplaySlotX * (i % _invenColumns) + _iconWidthOffset;
                iconPos.y = (_invenDisplaySlotY * (i / _invenColumns) - _iconHeightOffset) * -1;
                iconsToPosition[i].GetComponent<ItemObj>().io_storeIconObject.GetComponent<Transform>().position = new Vector3(iconPos.x, iconPos.y, 100.0f);
            }
        }
    }
}
