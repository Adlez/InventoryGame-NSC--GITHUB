using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class IconObj : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject io_ObjectForThisIcon;

    private float _invenDisplaySlotX = 64.0f; //Hardcoded for now
    private float _invenDisplaySlotY = 64.0f;
    private float _iconWidthOffset = 32.0f;
    private float _iconHeightOffset = 32.0f;
    private int _invenColumns = 1;

    public static void GlobalPositionIconOnScreen(GameObject objectIcon, int i)
    {
        Vector3 iconPos = objectIcon.GetComponent<Transform>().position;
        iconPos.x = 64.0f + i * 32.0f;
        iconPos.y = 0.0f;
        objectIcon.GetComponent<Transform>().position = new Vector3(iconPos.x, iconPos.y, 1.0f);
    }

    public void ResetIconSize(GameObject iconObject)
    {
        iconObject.GetComponent<IconObj>().transform.localScale = new Vector3(32.0f, 32.0f, 1.0f);

    }

    public static GameObject MakeIconObject(GameObject item, GameObject iconContainer, string itemType)
    {
        GameObject icon = new GameObject("IconObject"); // create the game object
        icon.transform.SetParent(iconContainer.GetComponentInParent<Transform>());
        icon.AddComponent<IconObj>();
        icon.AddComponent<Button>(); // make game object into button

        icon.AddComponent<Image>();

        icon.GetComponent<IconObj>().io_ObjectForThisIcon = item;

        bool isCharacter = false;
        bool isItem = false;
        bool isLevel = false;
        bool isParty = false;

        if (itemType == "Character")
        {
            isCharacter = true;
            isItem = false;
            isLevel = false;
            isParty = false;
        }
        else if (itemType == "Item")
        {
            isItem = true;
            isCharacter = false;
            isLevel = false;
            isParty = false;
        }
        else if (itemType == "Level")
        {
            isLevel = true;
            isCharacter = false;
            isItem = false;
            isParty = false;
        }
        else if (itemType == "Party")
        {
            isParty = true;
            isCharacter = false;
            isItem = false;
            isLevel = false;
        }
        
        int thisItemID = 0;

        if (isItem)//item is itemObj
        {
            thisItemID = item.GetComponent<ItemObj>().idInArrays;
            icon.GetComponent<Button>().name = item.GetComponent<ItemObj>().itemName + " Icon";// "Item Name";
            icon.GetComponent<Image>().sprite = ItemCatalogueConstantValues.ICONOFITEM[thisItemID];
            icon.GetComponent<Button>().onClick.AddListener(delegate { icon.GetComponent<IconObj>().TestFunction(icon); });
//            icon.GetComponent<Button>().onClick.AddListener(delegate { icon.GetComponent<IconObj>().IconClicked(icon); });
            //icon.GetComponent<Button>().onClick.AddListener(delegate { icon.GetComponent<ItemObj>().BuyItem(icon, true); });
            //icon.GetComponent<Button>().onClick.AddListener(delegate { icon.GetComponent<ItemObj>().SellItem(icon); });
            Sprite tempSprite = ItemCatalogueConstantValues.ICONOFITEM[thisItemID];
            icon.transform.localScale = item.transform.localScale;
            icon.transform.localPosition = new Vector3(icon.transform.localPosition.x, icon.transform.localPosition.y, 1.0f);
        }
        else if (isParty)//item is a party
        {
            thisItemID = item.GetComponent<PartyObj>().po_PartyID;
            icon.GetComponent<Button>().name = item.GetComponent<PartyObj>().po_PartyName + " Party Icon"; // "Item Name";
            icon.GetComponent<Image>().sprite = ItemCatalogueConstantValues.ICONOFITEM[1]; //TEMP, SUPER TEMP, hardcoded value is bad
            icon.GetComponent<Button>().onClick.AddListener(delegate { icon.GetComponent<IconObj>().TestFunction(icon); });
            Sprite tempSprite = ItemCatalogueConstantValues.ICONOFITEM[1];
            icon.transform.localScale = item.transform.localScale;
            item.GetComponent<PartyObj>().po_PartyIconObject = icon;
            icon.transform.SetParent(iconContainer.transform);
            icon.transform.localPosition = new Vector3(icon.transform.localPosition.x, icon.transform.localPosition.y, 1.0f);
        }
        else if (isLevel)//this is a Level's icon
        {
            icon.GetComponent<Button>().name = item.GetComponent<LevelObj>().lv_Name + " Icon";// "Item Name";
            icon.GetComponent<Image>().sprite = LevelCatalogueConstantValues.LEVELICONS[thisItemID];
            icon.GetComponent<Button>().onClick.AddListener(delegate { icon.GetComponent<IconObj>().TestFunction(icon); });
            Sprite tempSprite = LevelCatalogueConstantValues.LEVELICONS[thisItemID];
            icon.transform.localScale = item.transform.localScale;
        }
        else if (isCharacter)//the item is in fact a Character
        {
            thisItemID = item.GetComponent<CharacterObj>().co_CharacterIDNumber;
            string tempString = item.GetComponent<CharacterObj>().co_Name + " Icon";
            icon.GetComponent<Button>().name = item.GetComponent<CharacterObj>().co_Name + " Icon";// "Item Name";
            icon.GetComponent<Image>().sprite = CharacterCatalogueConstantValues.CHARACTERICONS[thisItemID];
            GameObject characterOfIcon = icon.GetComponent<IconObj>().io_ObjectForThisIcon;
            icon.GetComponent<Button>().onClick.AddListener(delegate { icon.GetComponent<IconObj>().io_ObjectForThisIcon.GetComponent<CharacterObj>().AddRemoveFromParty(characterOfIcon); });
            icon.GetComponent<Button>().onClick.AddListener(delegate { icon.GetComponent<IconObj>().io_ObjectForThisIcon.GetComponent<CharacterObj>().HireCharacter(characterOfIcon); });
            Sprite tempSprite = CharacterCatalogueConstantValues.CHARACTERICONS[thisItemID];
            icon.transform.localScale = item.transform.localScale;
        }
        return icon;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Mouse Enter");
        var checkIfCharacter = io_ObjectForThisIcon.GetComponent<CharacterObj>();
        if (checkIfCharacter != null)
        {
            io_ObjectForThisIcon.GetComponent<CharacterObj>().FillDetailsPanel();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Mouse Exit");
    }

    public void TestFunction(GameObject icon)
    {
        Debug.Log("Icon " + icon.GetComponent<IconObj>().name + " Clicked.");
    }

    public void IconClicked(GameObject itemIcon)
    {
        GameObject item = itemIcon.GetComponent<IconObj>().io_ObjectForThisIcon;
        string itemParentName = item.transform.parent.name;

        if (GameControllerScript.gc_CurActiveCanvasPanel.name == "UltraTestShopPanel")
        { 
            if (itemParentName == "ShopContentPanel")//Icon is in the shop, probably
            {
                item.GetComponent<ItemObj>().BuyItem(item);
            }
            else//Icon is in the stash or a party inventory, probably
            {
                item.GetComponent<ItemObj>().SellItem(item);
            }
           
        }
    }
}
