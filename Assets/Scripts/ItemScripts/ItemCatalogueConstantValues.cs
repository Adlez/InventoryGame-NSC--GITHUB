using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml;

public class ItemCatalogueConstantValues : MonoBehaviour
{
    //each value in each array represents the variables of each item
    //values are taken from this class and used to create an item in the ItemCatalogueData.cs file
    public bool[] ARTEFACTYESNO = new bool[16];
    public int[] CASHVALUEOFITEM = new int[16]; //16 is a placeholder value
    public string[] NAMEOFITEM = new string[16];
    public Sprite[] ICONOFITEM = new Sprite[16];
    public Sprite[] PORTRAITOFITEM = new Sprite[16];
    public string[] ITEMDESCRIPTION = new string[16];
    public int[] IDOFITEM = new int[16];
    public float[] ODDSOFFINDING = new float[16];
    public int[] MAXAMOUNTTOFIND = new int[16];
    public int[] ITEMTYPEID = new int[16];

    private void Awake()
    {
        PopulateItemArrays();
        
    }

    void PopulateItemArrays()
    {
        //Temp Code Begins
        for (int i = 0; i < CASHVALUEOFITEM.Length; ++i)
        {
            CASHVALUEOFITEM[i] = 0;
            NAMEOFITEM[i] = "EmptyName";
            ICONOFITEM[i] = null;
            PORTRAITOFITEM[i] = null;
            ITEMDESCRIPTION[i] = "No Description";
            IDOFITEM[i] = i;
            ODDSOFFINDING[i] = 100.0f;
            MAXAMOUNTTOFIND[i] = 2;
            ITEMTYPEID[i] = 0;
        }
        //Temp Code Ends

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
            else
            {
                return;
            }
        }
    }
}
