using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyCatalogueData : MonoBehaviour
{
    public GameObject pcd_GameController;
    public GameObject[] pcd_ArrayOfParties = new GameObject[4];
    public GameObject pcd_PartyScrollViewPanel;

    public void CreateParties()
    {
        GameObject _PartyContainer = new GameObject("PartyContainer");
        GameObject _PartyIconContainer = new GameObject("PartyIconContainer");

        for (int i = 0; i < pcd_ArrayOfParties.Length; ++i)
        {
            GameObject newParty = new GameObject("Party");
            newParty.transform.SetParent(_PartyContainer.transform);

            newParty.AddComponent<PartyObj>();
            pcd_ArrayOfParties[i] = newParty;
        }
    }

    public void DisplayParties()
    {
        if(pcd_ArrayOfParties[0] == null)
        {
            CreateParties();
        }
    }
}
