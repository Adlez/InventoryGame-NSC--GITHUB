using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml;

public class ItemCatalogueConstantValues : MonoBehaviour
{
    public static ItemCatalogueConstantValues itemCataloguePointer;
    //each value in each array represents the variables of each item
    //values are taken from this class and used to create an item in the ItemCatalogueData.cs file
    public static bool[] ARTEFACTYESNO = new bool[16];//Consider seperate arrays for tool, treasure, and artefacts
    public static int[] CASHVALUEOFITEM = new int[16]; //16 is a placeholder value
    public static string[] NAMEOFITEM = new string[16];
    public static Sprite[] ICONOFITEM = new Sprite[16];
    public static Sprite[] PORTRAITOFITEM = new Sprite[16];
    public static string[] ITEMDESCRIPTION = new string[16];
    public static int[] IDOFITEM = new int[16];
    public static float[] ODDSOFFINDING = new float[16];
    public static int[] MAXAMOUNTTOFIND = new int[16];
    public static int[] ITEMTYPEID = new int[16];

    public static void PopulateItemArrays()
    {
        //Load Images
        for (int i = 0; i < CASHVALUEOFITEM.Length; ++i)
        {
            string tempString = "Sprites/TempIconSprite-" + i.ToString() + "-32x32";
            ICONOFITEM[i] = Resources.Load<Sprite>(tempString);
            PORTRAITOFITEM[i] = null;
        }
        
        //Load Items from XML
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load("./Assets/XML/ItemData.xml");

        XmlNode root = xmlDoc.FirstChild;

        foreach (XmlNode node in root.ChildNodes)
        {
            if (node.Name == "ArtefactYesNo")
            {
                for(int i = 0; i < ARTEFACTYESNO.Length; ++i)
                {
                    ARTEFACTYESNO[i] = bool.Parse(node.Attributes[i].Value);
                }
            }
            if (node.Name == "CashValueOfItem")
            {
                for(int i = 0; i < CASHVALUEOFITEM.Length; ++i)
                {
                    CASHVALUEOFITEM[i] = int.Parse(node.Attributes[i].Value);
                }
            }
            if (node.Name == "NameOfItem")
            {
                for (int i = 0; i < NAMEOFITEM.Length; ++i)
                {
                    NAMEOFITEM[i] = node.Attributes[i].Value;
                }
            }
            if (node.Name == "DescriptionOfItem")
            {
                for(int i = 0; i < ITEMDESCRIPTION.Length; ++i)
                {
                    ITEMDESCRIPTION[i] = node.Attributes[i].Value;
                }
            }
            if (node.Name == "OddsOfFinding")
            {
                for(int i = 0; i < ODDSOFFINDING.Length; ++i)
                {
                    ODDSOFFINDING[i] = int.Parse(node.Attributes[i].Value);
                }
            }
            if (node.Name == "MaxFindAtOnce")
            {
                for(int i = 0; i < MAXAMOUNTTOFIND.Length; ++i)
                {
                    MAXAMOUNTTOFIND[i] = int.Parse(node.Attributes[i].Value);
                }
            }
            if (node.Name == "TypeID")
            {
                for(int i = 0; i < ITEMTYPEID.Length; ++i)
                {
                    ITEMTYPEID[i] = int.Parse(node.Attributes[i].Value);
                    //0 == Tool, 1 == Treasure, 2 == Artefact? Necessary?
                }
            }
            if(node.Name == "IDInArrays")
            {
                for(int i = 0; i < IDOFITEM.Length; ++i)
                {
                    IDOFITEM[i] = int.Parse(node.Attributes[i].Value);
                }
            }
        }
    }
}
