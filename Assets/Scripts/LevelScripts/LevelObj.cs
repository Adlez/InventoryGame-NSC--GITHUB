﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObj : MonoBehaviour
{
    //add "public" as necessary
    public GameObject lv_GameController;
    public GameObject lv_PartyObject;
    public GameObject lv_IconObject;
    public string lv_Name;
    public string lv_ObjectType = "Level";

    public int[] lv_ToolRequiredIndex;
    public int[] lv_LootIndex; //corresponds to treasure array, used to search for the right item
    public List<GameObject> lv_PotentialLootList = new List<GameObject>();

    public float lv_Distance;
    public bool lv_IsActive;

}
