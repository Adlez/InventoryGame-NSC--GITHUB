using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconObj : MonoBehaviour
{
    public GameObject io_ObjectForThisIcon;
    
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
        int thisItemID = 0;
        if (ItemIsCharacter == null)
        {
            thisItemID = item.GetComponent<ItemObj>().idInArrays;
            icon.GetComponent<Button>().name = item.GetComponent<ItemObj>().itemName + " Icon";// "Item Name";
            icon.GetComponent<Image>().sprite = ItemCatalogueConstantValues.ICONOFITEM[thisItemID];
            icon.GetComponent<Button>().onClick.AddListener(delegate { icon.GetComponent<IconObj>().TestFunction(icon); });
            Sprite tempSprite = ItemCatalogueConstantValues.ICONOFITEM[thisItemID];
            icon.transform.localScale = item.transform.localScale;
        }
        else
        {
            thisItemID = item.GetComponent<CharacterObj>().co_CharacterIDNumber;
            string tempString = item.GetComponent<CharacterObj>().co_Name + " Icon";
            icon.GetComponent<Button>().name = item.GetComponent<CharacterObj>().co_Name + " Icon";// "Item Name";
            icon.GetComponent<Image>().sprite = CharacterCatalogueConstantValues.CHARACTERICONS[thisItemID];
            icon.GetComponent<Button>().onClick.AddListener(delegate { icon.GetComponent<IconObj>().TestFunction(icon); });
            Sprite tempSprite = CharacterCatalogueConstantValues.CHARACTERICONS[thisItemID];
            icon.transform.localScale = item.transform.localScale;
        }
        
        return icon;
    }

    public void TestFunction(GameObject icon)
    {
        Debug.Log("Icon " + icon.GetComponent<IconObj>().name + " Clicked.");
    }
}
