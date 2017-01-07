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
        int thisItemID = item.GetComponent<ItemObj>().idInArrays;

        Sprite tempSprite = ItemCatalogueConstantValues.ICONOFITEM[thisItemID];
        icon.transform.localScale = item.transform.localScale;

        //Button Code
         //tempSprite;//icon.GetComponent<IconObj>().io_IconImage;
        icon.GetComponent<Button>().name = item.GetComponent<ItemObj>().itemName + " Icon";// "Item Name";
        icon.GetComponent<Image>().sprite = ItemCatalogueConstantValues.ICONOFITEM[thisItemID];
        icon.GetComponent<Button>().onClick.AddListener(delegate { icon.GetComponent<IconObj>().TestFunction(icon); });

        return icon;
    }

    public void TestFunction(GameObject icon)
    {
        Debug.Log("Icon " + icon.GetComponent<IconObj>().name + " Clicked.");
    }
}
