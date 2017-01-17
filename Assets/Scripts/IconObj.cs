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
        //iconPos.y = (64.0f - 32.0f) * -1;
        //iconPos.x = 0.0f;
        iconPos.y = 0.0f;
        objectIcon.GetComponent<Transform>().position = new Vector3(iconPos.x, iconPos.y, 100.0f);
    }

    public static GameObject MakeIconObject(GameObject item, GameObject iconContainer)
    {
        GameObject icon = new GameObject("IconObject"); // create the game object
        icon.transform.SetParent(iconContainer.GetComponentInParent<Transform>());
        icon.AddComponent<IconObj>();
        //icon.AddComponent<SpriteRenderer>();
        icon.AddComponent<Button>(); // make game object into button
        
        icon.AddComponent<Image>();

        icon.GetComponent<IconObj>().io_ObjectForThisIcon = item;

        var ItemIsCharacter = item.GetComponent<CharacterObj>(); //create variable to check if item is a character or an item
        var ItemIsLevel = item.GetComponent<LevelObj>();
        var ItemIsParty = item.GetComponent<PartyObj>();

        int thisItemID = 0;
        if (ItemIsCharacter == null)//meaning the item is NOT a character
        {
            if(ItemIsLevel == null && ItemIsParty == null)//meaning the item is NOT a levelObj or partyObj and is therefore an itemObj
            {
                thisItemID = item.GetComponent<ItemObj>().idInArrays;
                icon.GetComponent<Button>().name = item.GetComponent<ItemObj>().itemName + " Icon";// "Item Name";
                icon.GetComponent<Image>().sprite = ItemCatalogueConstantValues.ICONOFITEM[thisItemID];
                icon.GetComponent<Button>().onClick.AddListener(delegate { icon.GetComponent<IconObj>().TestFunction(icon); });
                Sprite tempSprite = ItemCatalogueConstantValues.ICONOFITEM[thisItemID];
                icon.transform.localScale = item.transform.localScale;
            }
            else if(ItemIsParty != null)//item is a party
            {
                thisItemID = item.GetComponent<PartyObj>().po_PartyID;
                icon.GetComponent<Button>().name = item.GetComponent<PartyObj>().po_PartyName + " Party Icon"; // "Item Name";
                icon.GetComponent<Image>().sprite = ItemCatalogueConstantValues.ICONOFITEM[1]; //TEMP, SUPER TEMP
                icon.GetComponent<Button>().onClick.AddListener(delegate { icon.GetComponent<IconObj>().TestFunction(icon); });
                Sprite tempSprite = ItemCatalogueConstantValues.ICONOFITEM[1];
                icon.transform.localScale = item.transform.localScale;
                item.GetComponent<PartyObj>().po_PartyIconObject = icon;
                icon.transform.SetParent(iconContainer.transform);
                icon.transform.localPosition = new Vector3(icon.transform.localPosition.x, icon.transform.localPosition.y, 0.0f);
                //GlobalPositionIconOnScreen(icon);
            }
            else//this is a Level's icon
            {
                //thisItemID = item.GetComponent<ItemObj>().idInArrays; //Not sure this is needed for levels
                icon.GetComponent<Button>().name = item.GetComponent<LevelObj>().lv_Name + " Icon";// "Item Name";
                icon.GetComponent<Image>().sprite = LevelCatalogueConstantValues.LEVELICONS[thisItemID];
                icon.GetComponent<Button>().onClick.AddListener(delegate { icon.GetComponent<IconObj>().TestFunction(icon); });
                Sprite tempSprite = LevelCatalogueConstantValues.LEVELICONS[thisItemID];
                icon.transform.localScale = item.transform.localScale;
                //icon.transform
            }
            
        }
        else //the item is in fact a Character
        {
            thisItemID = item.GetComponent<CharacterObj>().co_CharacterIDNumber;
            string tempString = item.GetComponent<CharacterObj>().co_Name + " Icon";
            icon.GetComponent<Button>().name = item.GetComponent<CharacterObj>().co_Name + " Icon";// "Item Name";
            icon.GetComponent<Image>().sprite = CharacterCatalogueConstantValues.CHARACTERICONS[thisItemID];
            GameObject characterOfIcon = icon.GetComponent<IconObj>().io_ObjectForThisIcon;
            icon.GetComponent<Button>().onClick.AddListener(delegate { icon.GetComponent<IconObj>().io_ObjectForThisIcon.GetComponent<CharacterObj>().AddRemoveFromParty(characterOfIcon); });
            Sprite tempSprite = CharacterCatalogueConstantValues.CHARACTERICONS[thisItemID];
            icon.transform.localScale = item.transform.localScale;
        }
        
        return icon;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse Enter");
        var checkIfCharacter = io_ObjectForThisIcon.GetComponent<CharacterObj>();
        if(checkIfCharacter != null)
        {
            io_ObjectForThisIcon.GetComponent<CharacterObj>().FillDetailsPanel();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse Exit");
    }

    public void TestFunction(GameObject icon)
    {
        Debug.Log("Icon " + icon.GetComponent<IconObj>().name + " Clicked.");
    }
}
