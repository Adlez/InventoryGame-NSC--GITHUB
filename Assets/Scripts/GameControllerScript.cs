using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    //Globals
    public static int gc_SelectedParty = 0;
    public static int gc_Munnies;
    public static int[] gc_StashOfItems = new int[16];
    public static GameObject gc_CurActiveCanvasPanel;
    public static GameObject[] gc_Parties = new GameObject[4];
    public static GameObject[] gc_PartyPanels = new GameObject[4];
    public static List<GameObject> PartyLimbo = new List<GameObject>(); //list of characters not assigned to a party

    public static Text gc_GlobalMunnieDisplayText;
    public Text _MunnieDisplayText;

    //add "public" as necessary
    int[] gc_LevelsAvailable = new int[16]; //0 level is unavailable, 1 it is.
    int[] gc_PlayerToolCount = new int[16]; //according to index, count the tools
    public int[] gc_SelectedLevelToolRequired = new int[16]; //corresponds to gc_PlayerToolCount

    public int gc_SelectedLevel;
    public GameObject gc_PartyCatalogue;
    bool gc_GoodToGoOnJourney; //probably not necessary

    GameObject[] gc_Levels = new GameObject[8]; //Corresponds to gc_LevelsAvailable

    public void SetSelectedParty(int id)
    {
        gc_SelectedParty = id;
    }

    void CheckLoot()
    {

    }

    void AddLoot()
    {

    }

    public void AttemptAnAdventure(int partyID, int levelID)
    {
        var theLevel = gc_Levels[levelID].GetComponent<LevelObj>();
        var theParty = gc_Parties[partyID].GetComponent<PartyObj>();
        //function called by a UI button
        for (int i = 0; i < gc_SelectedLevelToolRequired.Length; ++i)
        {
            if (theLevel.lv_IsActive == false && gc_SelectedLevelToolRequired[i] <= gc_PlayerToolCount[i])
            {   //if no one else is exploring the level and player has the required tools
                gc_PlayerToolCount[i] -= gc_SelectedLevelToolRequired[i];
                //reduce energy?
                gc_GoodToGoOnJourney = true;
            }
            else
            {   //otherwise the level is occupied or the right tool aren't there
                if(theLevel.lv_IsActive == false)
                {
                    //change text in the dialogue box to: Level in use
                }
                else
                {
                    //change text in the dialogue box to: Need more tools
                }
                //show dialogue box
                gc_GoodToGoOnJourney = false;
                //break out of for loop
            }
            if (gc_GoodToGoOnJourney != false)
            {
                theParty.po_PartyIsActive = true; //set bool so party doesn't go on multi adventures
                theLevel.GetComponent<LevelObj>().lv_IsActive = true;//set bool so level only has one party at a time
                theParty.po_TravelTime = theLevel.lv_Distance;
                theParty.po_LevelExploring = levelID;
            }
            else
            {
                //show dialogue box
                //change the dialogue text
            }
        }
    }

    void UpdateExplorers()
    {
        for(int i = 0; i < gc_Parties.Length; ++i)
        {
            var party = gc_Parties[i].GetComponent<PartyObj>();
            if (party.po_PartyIsActive)//is on an adventure
            {
                party.po_TimeGoneFor++;
                //reduce Energy
                //Check for Wagon
                //if wagon check if it breaks
                //if it breaks check for toolkit, fix if there is
                if (party.po_TimeGoneFor >= party.po_TravelTime && party.po_ExcavationComplete == false)
                {
                    //ExcavateLevel(party);
                    //function is likely stored in LevelObj.cs
                }
                else if(party.po_TimeGoneFor > party.po_TravelTime *2 && party.po_ExcavationComplete == true)
                {
                    //reduce extra energy according to loot carried
                    party.po_PartyIsActive = false;
                    gc_Levels[party.po_LevelExploring].GetComponent<LevelObj>().lv_IsActive = false;
                }
            }
            //if they're not active they're resting, therefore should be regaining energy
        }
    }

	// Use this for initialization
	void Start ()
    {
        ItemCatalogueConstantValues.PopulateItemArrays();
        //ItemCatalogueData.itemObj.CreateItems();
        LevelCatalogueConstantValues.PopulateItemArrays();
        CharacterCatalogueConstantValues.PopulateItemArrays();
        gc_PartyCatalogue.GetComponent<PartyCatalogueData>().CreateParties();
        ShopCatalogueConstantValues.PopulateItemArrays();
        //ShopDataCatalogue.sdc_ShopCataloguePointer.GetComponent<ShopDataCatalogue>().StockTheShelves();
        //PartyCatalogueData.pcd_Pointer.GetComponent<PartyCatalogueData>().CreateParties();

        //Check if save data exists
            //If it does load it
        //Otherwise set starting junk
        gc_Munnies = 2500; //Temp hardcoded value
        gc_GlobalMunnieDisplayText = _MunnieDisplayText;
        gc_GlobalMunnieDisplayText.text = gc_Munnies.ToString();
    }

    public void UpdateMunnieDisplay()
    {
        _MunnieDisplayText.text = gc_Munnies.ToString();
    }
	
	// Update is called once per frame
	void Update () {

	}
}
