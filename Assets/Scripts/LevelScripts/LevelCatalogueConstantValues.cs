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
    
    public static string[] LEVELNAMES;
    public static int[][] LEVELREQUIREDTOOLS;
    public static int[][] LEVELLOOTINDEX; //corresponds to treasure array, used to search for the right item
    public static float[] LEVELDISTANCES;
}
