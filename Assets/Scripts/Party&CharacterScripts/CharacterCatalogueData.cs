using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCatalogueData : MonoBehaviour
{
    public static CharacterCatalogueData characterCataloguePointer;
    public GameObject[] ccd_ArrayOfCharacters = new GameObject[16];
    public GameObject ccd_CharacterScrollViewPanel;

    private float _invenDisplaySlotX = 64.0f; //Hardcoded for now
    private float _invenDisplaySlotY = 64.0f;
    private float _iconWidthOffset = 32.0f;
    private float _iconHeightOffset = 32.0f;
    private int _invenColumns = 4;

    public void CreateCharacters()
    {
        GameObject _CharacterContainer = new GameObject("CharacterContainer");
        GameObject _CharacterIconContainer = new GameObject("CharacterIconContainer");
        _CharacterIconContainer.transform.SetParent(ccd_CharacterScrollViewPanel.transform);

        for (int i = 0; i < ccd_ArrayOfCharacters.Length; ++i)
        {
            GameObject newCharacter = new GameObject("Character");//Temp name
            newCharacter.transform.SetParent(_CharacterContainer.transform);

            newCharacter.AddComponent<CharacterObj>();
            newCharacter.transform.localScale = newCharacter.transform.localScale * 64;// * 100;

            string tempString = CharacterCatalogueConstantValues.CHARACTERNAMES[i];
            newCharacter.GetComponent<CharacterObj>().co_Name = CharacterCatalogueConstantValues.CHARACTERNAMES[i];
            newCharacter.GetComponent<CharacterObj>().co_Description = CharacterCatalogueConstantValues.CHARACTERDESCRIPTIONS[i];
            newCharacter.GetComponent<CharacterObj>().co_CharacterIDNumber = CharacterCatalogueConstantValues.CHARACTERIDNUMBERS[i];
            newCharacter.GetComponent<CharacterObj>().co_CarryingCapacityModifier = CharacterCatalogueConstantValues.CHARACTERCARRYINGCAPACITYMODS[i];
            newCharacter.GetComponent<CharacterObj>().co_EnergyModifier = CharacterCatalogueConstantValues.CHARACTERENERGYMODS[i];
            newCharacter.GetComponent<CharacterObj>().co_CharacterReplacesTools = CharacterCatalogueConstantValues.CHARACTERCANREPLACETOOLS[i];
            newCharacter.GetComponent<CharacterObj>().co_IsRepairKit = CharacterCatalogueConstantValues.CHARACTERISREPAIRKIT[i];
            newCharacter.GetComponent<CharacterObj>().co_TravelTimeModifier = CharacterCatalogueConstantValues.CHARACTERTRAVELTIMEMODS[i];
            newCharacter.GetComponent<CharacterObj>().co_PortraitSprite = CharacterCatalogueConstantValues.CHARACTERPORTRAITS[i];
            newCharacter.GetComponent<CharacterObj>().co_LimboScrollPanel = ccd_CharacterScrollViewPanel;

            Sprite tempSprite = newCharacter.GetComponent<CharacterObj>().co_PortraitSprite;

            GameObject iconObj = IconObj.MakeIconObject(newCharacter, ccd_CharacterScrollViewPanel, newCharacter.GetComponent<CharacterObj>().co_ObjectType);
            newCharacter.GetComponent<CharacterObj>().co_CharacterIconObject = iconObj;

            newCharacter.name = newCharacter.GetComponent<CharacterObj>().co_Name;
            newCharacter.GetComponent<CharacterObj>().co_CharacterIconObject.GetComponent<IconObj>().GetComponent<Image>().sprite = CharacterCatalogueConstantValues.CHARACTERICONS[i];

            //add to array
            ccd_ArrayOfCharacters[i] = newCharacter;
            GameControllerScript.PartyLimbo.Add(ccd_ArrayOfCharacters[i]);
        }
    }

    public void DisplayAllCharacters()
    {
        if (ccd_ArrayOfCharacters[0] == null)
        {
            CreateCharacters();
        }
        for(int i = 0; i < ccd_ArrayOfCharacters.Length; ++i)
        {
            if (ccd_ArrayOfCharacters[i].GetComponent<CharacterObj>().co_InRoster)
            {
                PositionIconsOnScreen();
            }
        }
    }

    public void PositionIconsOnScreen()
    {
        for (int i = 0; i < ccd_ArrayOfCharacters.Length; ++i)
        {
            Vector3 iconPos = ccd_ArrayOfCharacters[i].GetComponent<CharacterObj>().co_CharacterIconObject.GetComponent<Transform>().position;
            iconPos.x = _invenDisplaySlotX * (i % _invenColumns) + _iconWidthOffset;
            iconPos.y = (_invenDisplaySlotY * (i / _invenColumns) - _iconHeightOffset) * -1;
            iconPos.x = 0.0f;
            iconPos.y = 0.0f;
            ccd_ArrayOfCharacters[i].GetComponent<CharacterObj>().co_CharacterIconObject.GetComponent<Transform>().position = new Vector3(iconPos.x, iconPos.y, 100.0f);
        }
    }
}
