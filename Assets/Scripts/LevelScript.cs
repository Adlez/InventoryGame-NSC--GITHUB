using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    //add "public" as necessary
    public GameObject lv_GameController;
    public GameObject lv_PartyObject;
    string lv_Name;

    int[] lv_ToolRequiredIndex;
    int[] lv_LootIndex; //corresponds to treasure array, used to search for the right item
    List<GameObject> lv_PotentialLootList = new List<GameObject>();

    public float lv_Distance;
    public bool lv_IsActive;

    
}
