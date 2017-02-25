using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class MenuNavigaion : MonoBehaviour
{
    public static MenuNavigaion menuNavCataloguePointer;

    public Text mn_DetailsPanelNameText;
    public Text mn_DetailsPanelObjectStatsText;
    public Text mn_DetailsPanelObjectDescriptionText;
    public Text mn_SelectedPartyText;
    public Text mn_MessageToPlayerText;

    public Text[] mn_PartyAdventureTimerArray = new Text[4];
    public Text mn_PartyAdventureTimer1;
    public Text mn_PartyAdventureTimer2;
    public Text mn_PartyAdventureTimer3;
    public Text mn_PartyAdventureTimer4;

    public GameObject mn_DetailsPanelImage;
    public GameObject mn_MessageToPlayerPanel;
    public GameObject mn_PartiesOnAdventurePanel;
    public GameObject mn_PartyRosterPanel;
    public GameObject mn_Party0DisplayPanel;
    public GameObject mn_Party1DisplayPanel;
    public GameObject mn_Party2DisplayPanel;
    public GameObject mn_Party3DisplayPanel;
    public GameObject[] mn_PartyPanels = new GameObject[4];

    public GameObject mn_LevelViewParty0DisplayPanel;
    public GameObject mn_LevelViewParty1DisplayPanel;
    public GameObject mn_LevelViewParty2DisplayPanel;
    public GameObject mn_LevelViewParty3DisplayPanel;
    public GameObject[] mn_LevelViewPartyPartyPanels = new GameObject[4];

    public GameObject mn_PartyDisplayLoot;
    public GameObject mn_ExcavatedLootPanel;
    public GameObject mn_PartyInvenAndWagoDisplay;
    public GameObject mn_WagoInvenDisplayBtn;
    public Text mn_WagoInvenDisplayText;

    private void Awake()
    {
        menuNavCataloguePointer = this;
        mn_PartyAdventureTimerArray[0] = mn_PartyAdventureTimer1;
        mn_PartyAdventureTimerArray[1] = mn_PartyAdventureTimer2;
        mn_PartyAdventureTimerArray[2] = mn_PartyAdventureTimer3;
        mn_PartyAdventureTimerArray[3] = mn_PartyAdventureTimer4;
    }

    public void ShowHideLevelPartyPanels()
    {
        if (mn_LevelViewPartyPartyPanels[0] == null)
        {
            mn_LevelViewPartyPartyPanels[0] = mn_LevelViewParty0DisplayPanel;
            mn_LevelViewPartyPartyPanels[1] = mn_LevelViewParty1DisplayPanel;
            mn_LevelViewPartyPartyPanels[2] = mn_LevelViewParty2DisplayPanel;
            mn_LevelViewPartyPartyPanels[3] = mn_LevelViewParty3DisplayPanel;
        }
        for (int i = 0; i < GameControllerScript.gc_PartyPanels.Length; ++i)
        {
            if (i == GameControllerScript.gc_SelectedParty)
            {
                mn_LevelViewPartyPartyPanels[i].SetActive(mn_LevelViewPartyPartyPanels[i]);
                for(int j = 0; j < GameControllerScript.gc_Parties[i].GetComponent<PartyObj>().po_PartyMembers.Length; ++j)
                {
                    if(GameControllerScript.gc_Parties[i].GetComponent<PartyObj>().po_PartyMembers[j] != null)
                    {
                        GameObject miniIcon = GameControllerScript.gc_Parties[i].GetComponent<PartyObj>().po_PartyMembers[j].GetComponent<CharacterObj>().coMiniCharacterIconObj;
                        miniIcon.transform.SetParent(mn_LevelViewPartyPartyPanels[i].transform);
                        miniIcon.transform.position = new Vector3(miniIcon.transform.position.x, 10 * j, 100.0f);
                    }
                }
                mn_SelectedPartyText.text = GameControllerScript.gc_SelectedParty.ToString();
            }
            else
            {
                mn_LevelViewPartyPartyPanels[i].SetActive(!mn_LevelViewPartyPartyPanels[i]);
            }
        }
    }

    public void ShowPartyPanelHideOthers()
    {
        if(mn_PartyPanels[0] == null)
        {
            mn_PartyPanels[0] = mn_Party0DisplayPanel;
            mn_PartyPanels[1] = mn_Party1DisplayPanel;
            mn_PartyPanels[2] = mn_Party2DisplayPanel;
            mn_PartyPanels[3] = mn_Party3DisplayPanel;
        }
        for(int i = 0; i < GameControllerScript.gc_PartyPanels.Length; ++i)
        {
            if(i == GameControllerScript.gc_SelectedParty)
            {
                mn_PartyPanels[i].SetActive(mn_PartyPanels[i]);
                for (int j = 0; j < GameControllerScript.gc_Parties[i].GetComponent<PartyObj>().po_PartyMembers.Length; ++j)
                {
                    if (GameControllerScript.gc_Parties[i].GetComponent<PartyObj>().po_PartyMembers[j] != null)
                    {
                        GameObject icon = GameControllerScript.gc_Parties[i].GetComponent<PartyObj>().po_PartyMembers[j].GetComponent<CharacterObj>().co_CharacterIconObject;
                        icon.transform.SetParent(mn_PartyPanels[i].transform);
                        icon.transform.position = new Vector3(icon.transform.position.x, 10 * j, 100.0f);
                    }
                }
            }
            else
            {
                mn_PartyPanels[i].SetActive(!mn_PartyPanels[i]);
            }
        }
    }

    public void ShowHideStashPanel(GameObject stashPanel)
    {
        stashPanel.SetActive(!stashPanel.activeSelf);
    }

    public void ShowNewPanel(GameObject newPanel)
    {
        //newPanel.SetActive(!newPanel.activeSelf);
        newPanel.SetActive(true);
        GameControllerScript.gc_CurActiveCanvasPanel = newPanel;
    }

    public void HideOldPanel(GameObject oldPanel)
    {
        oldPanel.SetActive(!oldPanel.activeSelf);
    }

    public static void UpdateDisplayPanel(string objectName, string objectDesc, Sprite objectPortrait)
    {
        menuNavCataloguePointer.GetComponent<MenuNavigaion>().mn_DetailsPanelNameText.text = objectName;
        menuNavCataloguePointer.GetComponent<MenuNavigaion>().mn_DetailsPanelObjectDescriptionText.text = objectDesc;
        menuNavCataloguePointer.GetComponent<MenuNavigaion>().mn_DetailsPanelImage.GetComponent<Image>().sprite = objectPortrait;
    }

    public void ShowCharactersInParty(GameObject party)
    {
        for(int i = 0; i < party.GetComponent<PartyObj>().po_PartyMembers.Length; ++i)
        {
            GameObject characterInParty = party.GetComponent<PartyObj>().po_PartyMembers[i];
            characterInParty.GetComponent<CharacterObj>().co_CharacterIconObject.transform.SetParent(mn_PartyRosterPanel.transform);
        }
        
    }
}
