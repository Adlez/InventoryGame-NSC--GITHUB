using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class MenuNavigaion : MonoBehaviour
{

	public void ShowNewPanel(GameObject newPanel)
    {
        newPanel.SetActive(!newPanel.activeSelf);
    }

    public void HideOldPanel(GameObject oldPanel)
    {
        oldPanel.SetActive(!oldPanel.activeSelf);
    }
}
