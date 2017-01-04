using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconObj : MonoBehaviour
{
    //public as necessary
    public Sprite io_IconBox;
    public Sprite io_IconImage;
    public Text io_Name;
    public GameObject io_ObjectForThisIcon;

    public static GameObject MakeIconObject(GameObject item)
    {
        GameObject icon = new GameObject("IconObject");
        int thisItemID = item.GetComponent<ItemObj>().idInArrays;
        icon.AddComponent<IconObj>();
        //icon.GetComponent<IconObj>().io_Name.text = item.GetComponent<ItemObj>().itemName;
        //icon.GetComponent<IconObj>().io_IconImage = ItemCatalogueConstantValues.itemCataloguePointer.ICONOFITEM[thisItemID];
        icon.GetComponent<IconObj>().io_IconBox = null;
        return icon;
    }
}
