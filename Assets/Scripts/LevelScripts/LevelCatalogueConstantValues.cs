using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml;

public class LevelCatalogueConstantValues : MonoBehaviour
{
    public static LevelCatalogueConstantValues levelCataloguePointer;
    
    public static string[] LEVELNAMES = new string[8];
    public static int[][] LEVELREQUIREDTOOLS = new int[8][];
    public static int[][] LEVELLOOTINDEX = new int[8][]; //corresponds to tool,treasure,artefact array, used to search for the right item
    public static float[] LEVELDISTANCES = new float[8];
    public static Sprite[] LEVELICONS = new Sprite[8];
    
    public static void PopulateItemArrays()
    {
        //Load Images
        for (int i = 0; i < LEVELNAMES.Length; ++i)
        {
            string tempString = "Sprites/TempIconSprite-" + i.ToString() + "-32x32";
            LEVELICONS[i] = Resources.Load<Sprite>(tempString);
        }

        //Load Items from XML

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load("./Assets/XML/LevelData.xml");

        XmlNode root = xmlDoc.FirstChild;

        foreach (XmlNode node in root.ChildNodes)
        {
            if (node.Name == "LevelName")
            {
                for (int i = 0; i < LEVELNAMES.Length; ++i)
                {
                    LEVELNAMES[i] = node.Attributes[i].Value;
                }
            }
            //Works fine
            //INDECIES, 0-4 = Tools, 5-14 = Treasure, 15 = Artefact
            for(int i = 0; i < LEVELREQUIREDTOOLS.Length; ++i)
            {
                if (node.Name == "Level" + i + "ToolRequirements")
                {
                    int[] tempArray = new int[16];
                    for (int j = 0; j < tempArray.Length; ++j)
                    {
                        tempArray[j] = int.Parse(node.Attributes[j].Value);
                    }
                    LEVELREQUIREDTOOLS[i] = tempArray;
                }
            }
            for (int i = 0; i < LEVELLOOTINDEX.Length; ++i)
            {
                if (node.Name == "Level" + i + "PossibleLoot")
                {
                    int[] tempArray = new int[16];
                    for (int j = 0; j < tempArray.Length; ++j)
                    {
                        tempArray[j] = int.Parse(node.Attributes[j].Value);
                    }
                    LEVELLOOTINDEX[i] = tempArray;
                }
            }
            if (node.Name == "LevelDistances")
            {
                for (int i = 0; i < LEVELDISTANCES.Length; ++i)
                {
                    LEVELDISTANCES[i] = int.Parse(node.Attributes[i].Value);
                }
            }
        }
    }
}
