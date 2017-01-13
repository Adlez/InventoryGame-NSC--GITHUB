using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyCatalogueData : MonoBehaviour
{
    public GameObject pcd_GameController;
    //public static GameObject[] pcd_ArrayOfParties = new GameObject[4];
    public GameObject pcd_PartyScrollViewPanel;
    public GameObject pcd_PartyIcon;

    public void CreateParties()
    {
        GameObject _PartyContainer = new GameObject("PartyContainer");
        GameObject _PartyIconContainer = new GameObject("PartyIconContainer");

        for (int i = 0; i < GameControllerScript.gc_Parties.Length; ++i)
        {
            GameObject newParty = new GameObject("Party"+i.ToString());
            newParty.AddComponent<PartyObj>();
            newParty.transform.SetParent(_PartyContainer.transform);
            newParty.GetComponent<PartyObj>().po_PartyID = i;

           
            IconObj.MakeIconObject(newParty, _PartyIconContainer);
            GameControllerScript.gc_Parties[i] = newParty;
        }
    }

    public void DisplayParties()
    {
        if(GameControllerScript.gc_Parties[0] == null)
        {
            CreateParties();
        }
        for(int i = 0; i < GameControllerScript.gc_Parties.Length; ++i)
        {
            GameControllerScript.gc_Parties[i].GetComponent<PartyObj>().po_PartyIconObject.transform.SetParent(pcd_PartyScrollViewPanel.transform);
        }
    }
}
