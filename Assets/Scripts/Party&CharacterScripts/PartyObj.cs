using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PartyObj : MonoBehaviour
{
    //add "public" as necessary
    public int po_MaxEnergy;
    public int po_CurEnergy;
    public int po_NumOfExpeditions;
    public int po_PartyID;
    public Sprite po_PartyIconSprite;
    public GameObject po_PartyIconObject;

    int po_CurBagSize; //possibly not neccessary

    public int po_MaxInventorySize; //compared to curInventorySize, this is the total number of items the party can carry
    public int po_CurInventorySize; //actual number of items being carrie by the party.

    public int po_MaxWagonInventorySize; //compared to curWagonInventorySize, this is the total number of items the wagon can hold.
    public int po_CurWagonInventorySize; //actual number of items being stored in the wagon.

    public List<GameObject> po_IconsOfItemsInBagsList = new List<GameObject>();
    public List<GameObject> po_IconsOfItemsInWagonList = new List<GameObject>();
    public List<GameObject> po_IconsOfItemsInExcavatedPile = new List<GameObject>();
    public List<GameObject> po_ItemsInExcavationPile = new List<GameObject>();

    public int[] po_TotalCarriedItems = new int[16]; //This is the number of each item in the Inventory according to the correct ID's
    public int[] po_TotalWagonItems = new int[16]; //This is the number of each item in the Wagon according to the correct ID's
    //Not actually sure what exactly the above to variable are for when two variables below seem to be the things too... Are the
    //Ones below supposed to be gameobjects?
    public int[] po_InventoryIndex = new int[16]; //Actual items in inventory according to ID
    public int[] po_WagonIndex; //Actual items in wagon according to ID

    public bool po_HasWagon;
    public bool po_HasRepairKit;
    public bool po_PartyIsActive;
    public bool po_ExcavationComplete;

    public float po_TravelTime;
    public float po_TimeGoneFor;

    public GameObject[] po_PartyMembers = new GameObject[4];
    public string po_PartyName;
    public string po_ObjectType = "Party";

    public int po_LevelExploring;

    private float _invenDisplaySlotX = 64.0f; //Hardcoded for now
    private float _invenDisplaySlotY = 64.0f;
    private float _iconWidthOffset = 32.0f;
    private float _iconHeightOffset = 32.0f;
    private int _invenColumns = 4;

    public void DisplayPartyInventory()
    {
        int thisPartyID = po_PartyID;
        HideOtherPartyBags(thisPartyID);

        for(int i = 0; i < po_IconsOfItemsInBagsList.Count; ++i)
        {
            po_IconsOfItemsInBagsList[i].SetActive(true);
        }
    }

    public void HideOtherPartyBags(int partyID)
    {
        for(int i = 0; i < GameControllerScript.gc_Parties.Length; ++i)
        {
            if(GameControllerScript.gc_Parties[i].GetComponent<PartyObj>().po_PartyID != partyID)
            {
                for(int j = 0; j < GameControllerScript.gc_Parties[i].GetComponent<PartyObj>().po_IconsOfItemsInBagsList.Count; ++j)
                {
                    GameControllerScript.gc_Parties[i].GetComponent<PartyObj>().po_IconsOfItemsInBagsList[i].SetActive(false);
                }
            }
        }
    }

    public void UpdatePartyToolsAccordingToCharacter(GameObject character, int newpID, int oldpID)
    {
        var tempCharStuff = character.GetComponent<CharacterObj>();
        if (newpID >= 0)//Character has been added to a party, increase party attributes
        {
            GameControllerScript.gc_Parties[newpID].GetComponent<PartyObj>().po_MaxEnergy += tempCharStuff.co_EnergyModifier;
            GameControllerScript.gc_Parties[newpID].GetComponent<PartyObj>().po_MaxInventorySize += tempCharStuff.co_CarryingCapacityModifier;
            if (tempCharStuff.co_IsRepairKit)
            {
                GameControllerScript.gc_Parties[newpID].GetComponent<PartyObj>().po_HasRepairKit = true;
            }
            for (int i = 0; i < tempCharStuff.co_CharacterReplacesTools.Length; ++i)
            {
                if (tempCharStuff.co_CharacterReplacesTools[i] != 0)
                {
                    GameControllerScript.gc_Parties[newpID].GetComponent<PartyObj>().po_InventoryIndex[i] += tempCharStuff.co_CharacterReplacesTools[i];
                    GameControllerScript.gc_Parties[newpID].GetComponent<PartyObj>().po_TotalCarriedItems[i] += tempCharStuff.co_CharacterReplacesTools[i];
                    GameControllerScript.gc_Parties[newpID].GetComponent<PartyObj>().po_CurInventorySize += tempCharStuff.co_CharacterReplacesTools[i];
                }
            }
        }
        else //Character has been removed from a party, decrease party attributes
        {
            GameControllerScript.gc_Parties[oldpID].GetComponent<PartyObj>().po_MaxEnergy -= tempCharStuff.co_EnergyModifier;
            GameControllerScript.gc_Parties[oldpID].GetComponent<PartyObj>().po_MaxInventorySize -= tempCharStuff.co_CarryingCapacityModifier;

            for (int i = 0; i < GameControllerScript.gc_Parties[oldpID].GetComponent<PartyObj>().po_PartyMembers.Length; ++i)
            {//Don't change any bools right away though, have to check the entire party to make sure changing the bool is what needs to be done 
                //more than one character might have a toolkit
                if(GameControllerScript.gc_Parties[oldpID].GetComponent<PartyObj>().po_PartyMembers[i] != null)
                {
                    var newTempChar = GameControllerScript.gc_Parties[oldpID].GetComponent<PartyObj>().po_PartyMembers[i];
                    if (newTempChar.GetComponent<CharacterObj>().co_IsRepairKit)
                    {
                        GameControllerScript.gc_Parties[oldpID].GetComponent<PartyObj>().po_HasRepairKit = true;
                    }
                    else { GameControllerScript.gc_Parties[oldpID].GetComponent<PartyObj>().po_HasRepairKit = false; }
                }
            }
            for (int i = 0; i < tempCharStuff.co_CharacterReplacesTools.Length; ++i)
            {
                if (tempCharStuff.co_CharacterReplacesTools[i] != 0)
                {
                    GameControllerScript.gc_Parties[oldpID].GetComponent<PartyObj>().po_InventoryIndex[i] -= tempCharStuff.co_CharacterReplacesTools[i];
                    GameControllerScript.gc_Parties[oldpID].GetComponent<PartyObj>().po_TotalCarriedItems[i] -= tempCharStuff.co_CharacterReplacesTools[i];
                    GameControllerScript.gc_Parties[oldpID].GetComponent<PartyObj>().po_CurInventorySize -= tempCharStuff.co_CharacterReplacesTools[i];
                }
            }
        }
        
    }
}
