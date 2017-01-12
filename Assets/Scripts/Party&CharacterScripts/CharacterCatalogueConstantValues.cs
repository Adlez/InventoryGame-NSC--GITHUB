using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml;

public class CharacterCatalogueConstantValues : MonoBehaviour
{
    public static CharacterCatalogueConstantValues characterCataloguePointer;

    public static string[] CHARACTERNAMES = new string[24];
    public static string[] CHARACTERDESCRIPTIONS = new string[24];
    public static int[] CHARACTERIDNUMBERS = new int[24];
    public static int[] CHARACTERCARRYINGCAPACITYMODS = new int[24];
    public static int[] CHARACTERENERGYMODS = new int[24];
    public static int[][] CHARACTERCANREPLACETOOLS = new int[24][];
    public static bool[] CHARACTERISREPAIRKIT = new bool[24];
    public static float[] CHARACTERTRAVELTIMEMODS = new float[24];
    public static Sprite[] CHARACTERICONS = new Sprite[24];
    public static Sprite[] CHARACTERPORTRAITS = new Sprite[24];

    public static void PopulateItemArrays()
    {
        //Load Images
        for (int i = 0; i < CHARACTERNAMES.Length; ++i)
        {
            string tempString = "Sprites/TempIconSprite-" + i.ToString() + "-32x32";
            string tempString2 = "Sprites/TempIconSprite-" + i.ToString() + "-128x128"; //"TempIconSprite-1-128x128"
            CHARACTERICONS[i] = Resources.Load<Sprite>(tempString);
            CHARACTERPORTRAITS[i] = Resources.Load<Sprite>(tempString2);
        }

        //Load Characters from XML
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load("./Assets/XML/CharacterData.xml");

        XmlNode root = xmlDoc.FirstChild;

        foreach (XmlNode node in root.ChildNodes)
        {
            if (node.Name == "CharacterNames")
            {
                for (int i = 0; i < CHARACTERNAMES.Length; ++i)
                {
                    CHARACTERNAMES[i] = node.Attributes[i].Value;
                }
            }
            if (node.Name == "CharacterDescriptions")
            {
                for (int i = 0; i < CHARACTERDESCRIPTIONS.Length; ++i)
                {
                    CHARACTERDESCRIPTIONS[i] = node.Attributes[i].Value;
                }
            }
            if(node.Name == "CharacterIDs")
            {
                for (int i = 0; i < CHARACTERIDNUMBERS.Length; ++i)
                {
                    CHARACTERIDNUMBERS[i] = int.Parse(node.Attributes[i].Value);
                }
            }
            if (node.Name == "CharacterCarryingCapacity")
            {
                for (int i = 0; i < CHARACTERCARRYINGCAPACITYMODS.Length; ++i)
                {
                    CHARACTERCARRYINGCAPACITYMODS[i] = int.Parse(node.Attributes[i].Value);
                }
            }
            if (node.Name == "CharacterEnergy")
            {
                for (int i = 0; i < CHARACTERENERGYMODS.Length; ++i)
                {
                    CHARACTERENERGYMODS[i] = int.Parse(node.Attributes[i].Value);
                }
            }
            for (int i = 0; i < CHARACTERCANREPLACETOOLS.Length; ++i)
            {
                if (node.Name == "Character" + i + "ToolBeltContents")
                {
                    int[] tempArray = new int[16];
                    for (int j = 0; j < tempArray.Length; ++j)
                    {
                        tempArray[j] = int.Parse(node.Attributes[j].Value);
                    }
                    CHARACTERCANREPLACETOOLS[i] = tempArray;
                }
            }
            if (node.Name == "CharacterHasARepairKit")
            {
                for (int i = 0; i < CHARACTERISREPAIRKIT.Length; ++i)
                {
                    CHARACTERISREPAIRKIT[i] = bool.Parse(node.Attributes[i].Value);
                }
            }
            if (node.Name == "CharacterTravelTimeMods")
            {
                for (int i = 0; i < CHARACTERTRAVELTIMEMODS.Length; ++i)
                {
                    CHARACTERTRAVELTIMEMODS[i] = float.Parse(node.Attributes[i].Value);
                }
            }
        }
    }
}
