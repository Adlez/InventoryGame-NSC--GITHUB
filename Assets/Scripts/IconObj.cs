using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconObj : MonoBehaviour
{
    //public as necessary
    public Sprite io_IconBox;
    public Sprite io_IconImage;
    public Text io_TextName;
    public string io_StringName;
    public GameObject io_ObjectForThisIcon;
    
    public static GameObject MakeIconObject(GameObject item, GameObject iconContainer)
    {
        GameObject icon = new GameObject("IconObject");
        icon.transform.parent = iconContainer.transform;
        int thisItemID = item.GetComponent<ItemObj>().idInArrays;
        icon.AddComponent<IconObj>();
        icon.AddComponent<SpriteRenderer>();
        icon.GetComponent<IconObj>().io_StringName = item.GetComponent<ItemObj>().itemName;

        Sprite tempSprite = ItemCatalogueConstantValues.ICONOFITEM[thisItemID];
        icon.GetComponent<IconObj>().io_IconImage = tempSprite;
        icon.GetComponent<SpriteRenderer>().sprite = icon.GetComponent<IconObj>().io_IconImage;
        icon.transform.localScale = item.transform.localScale;

        icon.GetComponent<IconObj>().io_IconBox = null;
        return icon;
    }
}
