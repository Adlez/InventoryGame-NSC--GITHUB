using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyObj : MonoBehaviour
{
    //add "public" as necessary
    int po_MaxEnergy;
    int po_CurEnergy;
    int po_NumOfExpeditions;

    int po_CurBagSize; //possibly not neccessary

    public int po_MaxInventorySize; //compared to curInventorySize, this is the total number of items the party can carry
    public int po_CurInventorySize; //actual number of items being carrie by the party.

    public int po_MaxWagonInventorySize; //compared to curWagonInventorySize, this is the total number of items the wagon can hold.
    public int po_CurWagonInventorySize; //actual number of items being stored in the wagon.

    public int[] po_TotalCarriedItems = new int[16]; //This is the number of each item in the Inventory according to the correct ID's
    public int[] po_TotalWagonItems = new int[16]; //This is the number of each item in the Wagon according to the correct ID's
    public int[] po_InventoryIndex; //Actual items in inventory according to ID
    public int[] po_WagonIndex; //Actual items in wagon according to ID

    bool po_HasWagon;
    bool po_HasRepairKit;
    public bool po_PartyIsActive;
    public bool po_ExcavationComplete;

    public float po_TravelTime;
    public float po_TimeGoneFor;

    public GameObject[] po_PartyMembers = new GameObject[4];
    public string po_PartyName;

    public int po_LevelExploring;
}
