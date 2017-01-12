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

    private void Awake()
    {
        menuNavCataloguePointer = this;
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
        //menuNavCataloguePointer.mn_DetailsPanelObjectDescriptionText.text = objectDesc;
        //menuNavCataloguePointer.mn_DetailsPanelNameText.text = objectName;
    }
}
