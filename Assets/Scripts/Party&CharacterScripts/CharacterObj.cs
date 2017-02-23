using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterObj : MonoBehaviour
{
    public Sprite co_IconSprite;
    public Sprite co_PortraitSprite;

    public GameObject co_CharacterIconObject;
    public GameObject coMiniCharacterIconObj;
    public GameObject co_LimboScrollPanel;

    public string co_Name;
    public string co_Description;
    public string co_ObjectType = "Character";

    public int co_CharacterIDNumber;
    public int co_CarryingCapacityModifier;
    public int co_Energy;
    public int co_EnergyModifier;

    public int co_CurPartyIndex = -1;
    public bool co_InRoster = true;
    public bool co_IsHired = true; //Default to True *CHANGE later

    public int[] co_ExpeditionTimeInLevels = new int[8]; //the index is the level, the intetger is the number of times
    public int[] co_CharacterReplacesTools = new int[16];

    public bool co_IsRepairKit;
    public float co_TravelTimeModifier;

    public void TestFunction(GameObject character)
    {
        Debug.Log("Icon " + co_CharacterIconObject.GetComponent<IconObj>().name + " Clicked.");
        AddRemoveFromParty(character);
    }
    public void HireCharacter(GameObject character)
    {
        if (!character.AddComponent<CharacterObj>().co_IsHired)
        {
            //Code to check if player can afford character
            //Code to add character to Roster
            //Probably UpdateCharacterIconPosition passing in character, -1, -1
            //Set bool co_IsHired to true
        }
    }

    public bool CheckIfPartyBeingAddedToIsFull(int pID)
    {
        bool PartyIsFull = false;
        if (GameControllerScript.gc_Parties[pID].GetComponent<PartyObj>().po_PartyMembers[0] != null
            && GameControllerScript.gc_Parties[pID].GetComponent<PartyObj>().po_PartyMembers[1] != null
            && GameControllerScript.gc_Parties[pID].GetComponent<PartyObj>().po_PartyMembers[2] != null
            && GameControllerScript.gc_Parties[pID].GetComponent<PartyObj>().po_PartyMembers[3] != null)
        {
            PartyIsFull = false;
        }
        else { PartyIsFull = true; }
        return PartyIsFull;
    }

    public void AddRemoveFromParty(GameObject character)
    {
        //if (!character.AddComponent<CharacterObj>().co_IsHired)
        //{
        int pID = GameControllerScript.gc_SelectedParty;
        int oldPID = pID;
        

        //Check if the party has an open slot
        
        if (co_CurPartyIndex < 0)//Character is in Limbo
        {
            if (CheckIfPartyBeingAddedToIsFull(pID))//Can't put a character into a party if there's no room
            {
                bool characterHasBeenAdded = false; //sanity bool
                for (int i = 0; i < GameControllerScript.gc_Parties[pID].GetComponent<PartyObj>().po_PartyMembers.Length; ++i)
                {
                    if (GameControllerScript.gc_Parties[pID].GetComponent<PartyObj>().po_PartyMembers[i] == null && characterHasBeenAdded == false)//find the first open slot
                    {
                        GameControllerScript.gc_Parties[pID].GetComponent<PartyObj>().po_PartyMembers[i] = character; //Add character to the the selected party
                        characterHasBeenAdded = true; //change bool for sanity
                    }
                }

                for (int i = 0; i < GameControllerScript.PartyLimbo.Count; ++i)
                {
                    if (GameControllerScript.PartyLimbo[i].GetComponent<CharacterObj>().co_Name == character.GetComponent<CharacterObj>().co_Name)//Find the character in limbo
                    {
                        GameControllerScript.PartyLimbo.RemoveAt(i); //remove the character from limbo
                    }
                }
                co_CurPartyIndex = pID; //set the party ID the character is now in
                                        //And next, we change the Party's stats using the new character that's been added

                GameControllerScript.gc_Parties[pID].GetComponent<PartyObj>().UpdatePartyToolsAccordingToCharacter(character, pID, oldPID);
                UpdateCharacterIconPosition(character, pID, oldPID);
            }
            else
            {
                Debug.Log("The Party you're trying to put this character in is full.");
            }
        }
        else//Character is in a party
        {
            bool characterHasBeenRemoved = false; //sanity bool
            for (int i = 0; i < GameControllerScript.gc_Parties[pID].GetComponent<PartyObj>().po_PartyMembers.Length; ++i)
            {
                if (GameControllerScript.gc_Parties[pID].GetComponent<PartyObj>().po_PartyMembers[i] != null) //Make sure this is doing what it's supposed to do, which is prevent "Object is null" error
                {//When going through the array looking to remove an object. Also make sure it isn't up to any funny business.
                    if (characterHasBeenRemoved != true && GameControllerScript.gc_Parties[pID].GetComponent<PartyObj>().po_PartyMembers[i].GetComponent<CharacterObj>().co_CharacterIDNumber ==
                    character.GetComponent<CharacterObj>().co_CharacterIDNumber) //Find the character in the party
                    {
                        GameControllerScript.gc_Parties[pID].GetComponent<PartyObj>().po_PartyMembers[i] = null; //remove character from party
                        character.GetComponent<CharacterObj>().co_CurPartyIndex = -1; //set the party ID of the character to negative
                        GameControllerScript.PartyLimbo.Add(character); //return the character to Limbo
                        characterHasBeenRemoved = true;
                    }
                }
            }
            
            pID = -1;
            GameControllerScript.gc_Parties[oldPID].GetComponent<PartyObj>().UpdatePartyToolsAccordingToCharacter(character, pID, oldPID);
            UpdateCharacterIconPosition(character, pID, oldPID);
        }
        
        /*}
        else//Character hasn't been hired
        {
            Debug.Log("This character has not yet been hired.");
        }*/
    }

    void UpdateCharacterIconPosition(GameObject character, int pID, int oldPID)
    {
        if (pID < 0)
        {
            character.GetComponent<CharacterObj>().co_CharacterIconObject.transform.SetParent(character.GetComponent<CharacterObj>().co_LimboScrollPanel.GetComponentInParent<Transform>());
            character.GetComponent<CharacterObj>().co_CharacterIconObject.GetComponent<IconObj>().ResetIconSize(character.GetComponent<CharacterObj>().co_CharacterIconObject);
        }
        else
        {
            character.GetComponent<CharacterObj>().co_CharacterIconObject.transform.SetParent(MenuNavigaion.menuNavCataloguePointer.mn_PartyPanels[oldPID].transform);
            character.GetComponent<CharacterObj>().co_CharacterIconObject.transform.position = new Vector3(-5.0f, -2.0f, 0.0f);
            character.GetComponent<CharacterObj>().co_CharacterIconObject.GetComponent<RectTransform>().localPosition = new Vector3(-5.0f, -2.0f, 0.0f);
            character.GetComponent<CharacterObj>().co_CharacterIconObject.transform.localScale = new Vector3(2, 2, 1);
        }
    }

    public void FillDetailsPanel()
    {
        string tempNameString = co_Name;
        string tempDescripString = co_Description;
        Sprite tempSprite = co_PortraitSprite;
        MenuNavigaion.UpdateDisplayPanel(tempNameString, tempDescripString, tempSprite);
    }
}
