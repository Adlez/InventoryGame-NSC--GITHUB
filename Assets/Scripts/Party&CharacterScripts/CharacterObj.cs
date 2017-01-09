using System.Collections;
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

    public int[] co_CurPartyIndex;
    public int[] co_ExpeditionTimeInLevels;
    public int[] co_CharacterReplacesTools;
    
    public bool co_IsRepairKit;
    public float co_TravelTimeModifier;

    public void TestFunction(GameObject character)
    {
        Debug.Log("Icon " + co_CharacterIconObject.GetComponent<IconObj>().name + " Clicked.");
    }
}
