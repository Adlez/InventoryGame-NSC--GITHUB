using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml;
using UnityEngine.UI;

public class ShopCatalogueConstantValues : MonoBehaviour
{
    public static ShopCatalogueConstantValues shopCataloguePointer;

    public static string[] SHOPNAMES = new string[6];
    public static string[] SHOPKEEPERNAMES = new string[6];
    public static Sprite[] SHOPKEEPERPORTRAITS = new Sprite[6];
    public static float[] SHOPMARKUPPERCENTAGE = new float[6];
    public static int[][] SHOPSTOCK = new int[6][];

    public static void PopulateItemArrays()
    {
        for (int i = 0; i < SHOPKEEPERNAMES.Length; ++i)
        {
            string tempString = "Sprites/TempIconSprite-" + i.ToString() + "-64x64";
            SHOPKEEPERPORTRAITS[i] = Resources.Load<Sprite>(tempString);
        }

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load("./Assets/XML/ShopData.xml");

        XmlNode root = xmlDoc.FirstChild;

        foreach (XmlNode node in root.ChildNodes)
        {
            for(int i = 0; i < SHOPNAMES.Length; ++i)
            {
                if(node.Name == "Shop" + i.ToString() + "Stock")
                {
                    int[] tempArray = new int[16];
                    for(int j = 0; j < tempArray.Length; ++j)
                    {
                        tempArray[j] = int.Parse(node.Attributes[j].Value);
                    }
                    SHOPSTOCK[i] = tempArray;
                }
            }
            if (node.Name == "ShopNames")
            {
                for (int i = 0; i < SHOPNAMES.Length; ++i)
                {
                    SHOPNAMES[i] = node.Attributes[i].Value;
                }
            }
            if (node.Name == "ShopKeeperNames")
            {
                for (int i = 0; i < SHOPKEEPERNAMES.Length; ++i)
                {
                    SHOPKEEPERNAMES[i] = node.Attributes[i].Value;
                }
            }
            if(node.Name == "MarkUp")
            {
                for (int i = 0; i < SHOPMARKUPPERCENTAGE.Length; ++i)
                {
                    SHOPMARKUPPERCENTAGE[i] = float.Parse(node.Attributes[i].Value);
                }
            }
        }
    }
}
