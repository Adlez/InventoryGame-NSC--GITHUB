using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconObj : MonoBehaviour
{
    //public as necessary
    public Sprite io_IconBox;
    public Sprite io_IconImage;
    public Image io_ImageIconImage;
    public Text io_TextName;
    public string io_StringName;
    public GameObject io_ObjectForThisIcon;
    
    public static GameObject MakeIconObject(GameObject item, GameObject iconContainer)
    {
        GameObject icon = new GameObject("IconObject"); // create the game object
        icon.transform.SetParent (iconContainer.transform);
        icon.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        int thisItemID = item.GetComponent<ItemObj>().idInArrays;
        icon.AddComponent<IconObj>();
        icon.AddComponent<SpriteRenderer>();
        icon.GetComponent<IconObj>().io_StringName = item.GetComponent<ItemObj>().itemName;
        Sprite tempSprite = ItemCatalogueConstantValues.ICONOFITEM[thisItemID];
        icon.GetComponent<IconObj>().io_IconImage = tempSprite;
        //icon.GetComponent<SpriteRenderer>().sprite = icon.GetComponent<IconObj>().io_IconImage;
        icon.transform.localScale = item.transform.localScale;

        icon.GetComponent<IconObj>().io_IconBox = null;

        //Button Code
        icon.AddComponent<Button>(); // make game object into button
        icon.AddComponent<Image>();
        icon.GetComponent<Image>().sprite = icon.GetComponent<IconObj>().io_IconImage;
        icon.GetComponent<Button>().name = "Item Name";
        icon.GetComponent<Button>().onClick.AddListener(delegate { icon.GetComponent<IconObj>().TestFunction(icon); });

        return icon;
    }

    public void TestFunction(GameObject icon)
    {
        Debug.Log("Icon " + icon.GetComponent<IconObj>().io_StringName + " Clicked.");
    }
}
