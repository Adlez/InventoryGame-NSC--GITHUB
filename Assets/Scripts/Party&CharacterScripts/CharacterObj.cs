﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterObj : MonoBehaviour
{
    public Sprite co_IconSprite;
    public Sprite co_PortraitSprite;

    public GameObject co_CharacterIconObject;

    public string co_Name;
    public string co_Description;

    public int co_CharacterIDNumber;
    public int co_CarryingCapacityModifier;
    public int co_EnergyModifier;

    public int co_CurPartyIndex = -1;
    public int[] co_ExpeditionTimeInLevels;
    public int[] co_CharacterReplacesTools;
    
    public bool co_IsRepairKit;
    public float co_TravelTimeModifier;

    public void TestFunction(GameObject character)
    {
        Debug.Log("Icon " + co_CharacterIconObject.GetComponent<IconObj>().name + " Clicked.");
        AddRemoveFromParty(character);
    }

    public void AddRemoveFromParty(GameObject character)
    {
        //pID might need to be created here, or at gotten from a global variable assigned by pressing the "Party" buttons on the party canvas
        int pID = GameControllerScript.gc_SelectedParty;
        int oldPID = pID;

        if(co_CurPartyIndex < 0)//Character is in Limbo
        {
            bool characterHasBeenAdded = false; //sanity bool
            for(int i = 0; i < GameControllerScript.gc_Parties[pID].GetComponent<PartyObj>().po_PartyMembers.Length; ++i)
            {
                if(GameControllerScript.gc_Parties[pID].GetComponent<PartyObj>().po_PartyMembers[i] == null && characterHasBeenAdded == false)
                {
                    GameControllerScript.gc_Parties[pID].GetComponent<PartyObj>().po_PartyMembers[i] = character; //Add character to the the selected party
                    characterHasBeenAdded = true; //change bool for sanity
                }
            }
            
            for(int i = 0; i < GameControllerScript.PartyLimbo.Count; ++i)
            {
                if(GameControllerScript.PartyLimbo[i].GetComponent<CharacterObj>().co_Name == character.GetComponent<CharacterObj>().co_Name)
                {
                    GameControllerScript.PartyLimbo.RemoveAt(i); //remove the character from limbo
                }
            }
            co_CurPartyIndex = pID; //set the party ID the character is now in
            
        }
        else//Character is in a party
        {
            bool characterHasBeenRemoved = false; //sanity bool
            for(int i = 0; i < GameControllerScript.gc_Parties[pID].GetComponent<PartyObj>().po_PartyMembers.Length; ++i)
            {
                if(characterHasBeenRemoved != true && GameControllerScript.gc_Parties[pID].GetComponent<PartyObj>().po_PartyMembers[i].GetComponent<CharacterObj>().co_CharacterIDNumber == 
                    character.GetComponent<CharacterObj>().co_CharacterIDNumber)
                {
                    GameControllerScript.gc_Parties[pID].GetComponent<PartyObj>().po_PartyMembers[i] = null; //remove character from party
                    character.GetComponent<CharacterObj>().co_CurPartyIndex = -1; //set the party ID of the character to negative
                    GameControllerScript.PartyLimbo.Add(character); //return the character to Limbo
                    characterHasBeenRemoved = true;
                    //pID = -1;
                }
            }
            pID = -1;
        }
        UpdateCharacterIconPosition(character, pID, oldPID);
    }

    void UpdateCharacterIconPosition(GameObject character, int pID, int oldPID)
    {
        if(pID < 0)
        {
            Vector3 oldPos = character.GetComponent<CharacterObj>().co_CharacterIconObject.transform.position;

            Transform chTransform = character.GetComponent<CharacterObj>().co_CharacterIconObject.transform;
            GameObject chViewObj = CharacterCatalogueData.characterObj.GetComponent<CharacterCatalogueData>().ccd_CharacterScrollViewPanel;
            chTransform.SetParent(chViewObj.transform);

            character.GetComponent<CharacterObj>().co_CharacterIconObject.transform.SetParent(CharacterCatalogueData.characterObj.GetComponent<CharacterCatalogueData>().ccd_CharacterScrollViewPanel.transform);
            character.GetComponent<CharacterObj>().co_CharacterIconObject.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            // MenuNavigaion.menuNavCataloguePointer.mn_PartyPanels[oldPID].transform);
            Vector3 newPos = character.GetComponent<CharacterObj>().co_CharacterIconObject.transform.position;
        }
        character.GetComponent<CharacterObj>().co_CharacterIconObject.transform.SetParent(MenuNavigaion.menuNavCataloguePointer.mn_PartyPanels[oldPID].transform);
        character.GetComponent<CharacterObj>().co_CharacterIconObject.transform.position = new Vector3(-5.0f, -2.0f, 0.0f);
        character.GetComponent<CharacterObj>().co_CharacterIconObject.GetComponent<RectTransform>().localPosition = new Vector3(-5.0f, -2.0f, 0.0f);
        character.GetComponent<CharacterObj>().co_CharacterIconObject.transform.localScale = new Vector3(2, 2, 1);
    }

    public void FillDetailsPanel()
    {
        //MenuNavigaion
        //MenuNavigaion.menuNavCataloguePointer.mn_DetailsPanelNameString = "The name is being displayed.";// co_Name;
        string tempNameString = co_Name;
        string tempDescripString = co_Description;
        Sprite tempSprite = co_PortraitSprite;
        MenuNavigaion.UpdateDisplayPanel(tempNameString, tempDescripString, tempSprite);
        //MenuNavigaion.mn_DetailsPanelObjectStatsText.text = "The stats are being Displayed";
    }
}
