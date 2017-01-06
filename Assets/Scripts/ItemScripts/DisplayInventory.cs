using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class DisplayInventory : MonoBehaviour
{
    public static DisplayInventory inventoryList;
    public GameObject di_ItemObj;
    public GameObject di_SampleIconButton;

    //public DisplayItemIcon[] di_ItemIconList;

    public bool di_DisplayListIsCreated;

	// Use this for initialization
	void Start ()
    {
		
	}
	
    public void PopulateIconButtonList()
    {

    }
}
