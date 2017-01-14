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
    public GameObject mn_DetailsPanelImage;
    public GameObject mn_PartyRosterPanel;
    public GameObject mn_Party0DisplayPanel;
    public GameObject mn_Party1DisplayPanel;
    public GameObject mn_Party2DisplayPanel;
    public GameObject mn_Party3DisplayPanel;
    public GameObject[] mn_PartyPanels = new GameObject[4];

    private void Awake()
    {
        menuNavCataloguePointer = this;
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
            }
            else
            {
                mn_PartyPanels[i].SetActive(!mn_PartyPanels[i]);
            }
        }
    }

    public void ShowNewPanel(GameObject newPanel)
    {
        newPanel.SetActive(!newPanel.activeSelf);
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
