using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyCatalogueData : MonoBehaviour
{
    public GameObject pcd_GameController;
    //public static GameObject[] pcd_ArrayOfParties = new GameObject[4];
    public GameObject pcd_PartyScrollViewPanel;
    public GameObject pcd_PartyIcon;

    public GameObject[] pcd_PartyWagonDisplayIcons;
    public GameObject[] pcd_PartyBagsDisplayIcons;

    public void CreateParties()
    {
        GameObject _PartyContainer = new GameObject("PartyContainer");
        GameObject _PartyIconContainer = new GameObject("PartyIconContainer");
        _PartyContainer.name = "PartyContainer";
        _PartyIconContainer.name = "Party ICON Container";
        _PartyIconContainer.transform.SetParent(pcd_PartyScrollViewPanel.transform);
        _PartyIconContainer.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        _PartyIconContainer.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        for (int i = 0; i < GameControllerScript.gc_Parties.Length; ++i)
        {
            GameObject newParty = new GameObject("Party"+i.ToString());
            newParty.AddComponent<PartyObj>();
            newParty.transform.SetParent(_PartyContainer.transform);
            newParty.GetComponent<PartyObj>().po_PartyID = i;

            IconObj.MakeIconObject(newParty, _PartyIconContainer, newParty.GetComponent<PartyObj>().po_ObjectType);
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
            
            //PositionIconOnScreen();
        }
    }
}
