using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyCompanyGroupScript : MonoBehaviour
{
    //add "public" as necessary
    int pcg_MaxEnergy;
    int pcg_CurEnergy;
    int pcg_NumOfExpeditions;

    int pcg_CurBagSize; //possibly not neccessary

    public int pcg_MaxInventorySize; //compared to curInventorySize, this is the total number of items the party can carry
    public int pcg_CurInventorySize; //actual number of items being carrie by the party.

    public int pcg_MaxWagonInventorySize; //compared to curWagonInventorySize, this is the total number of items the wagon can hold.
    public int pcg_CurWagonInventorySize; //actual number of items being stored in the wagon.

    public int[] pcg_TotalCarriedItems = new int[16]; //This is the number of each item in the Inventory according to the correct ID's
    public int[] pcg_TotalWagonItems = new int[16]; //This is the number of each item in the Wagon according to the correct ID's
    public int[] pcg_InventoryIndex; //Actual items in inventory according to ID
    public int[] pcg_WagonIndex; //Actual items in wagon according to ID

    bool pcg_HasWagon;
    bool pcg_HasRepairKit;
    public bool pcg_PartyIsActive;
    public bool pcg_ExcavationComplete;

    public float pcg_TravelTime;
    public float pcg_TimeGoneFor;

    int[] pcg_PartyMembersIndex;
    string pcg_PartyName;

    public int pcg_LevelExploring;
}
