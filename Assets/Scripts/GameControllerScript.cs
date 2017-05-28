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
    public static GameObject[] gc_PartyInventoryPanels = new GameObject[4];
    public static List<GameObject> PartyLimbo = new List<GameObject>(); //list of characters not assigned to a party

    public static List<GameObject> gc_PlayerStash = new List<GameObject>();

    public static Text gc_GlobalMunnieDisplayText;
    public Text _MunnieDisplayText;
    public GameObject gc_PlayerStashPanel;

    public GameObject gc_PartyCatalogue;
    public GameObject gc_CatalogueObj;
    public GameObject gc_MenuNavObj;
    public GameObject gc_ContainerChangeObject;
    public GameObject gc_LevelIconClicked;

    public GameObject gc_ExcavationBtnParty0;
    public GameObject gc_ExcavationBtnParty1;
    public GameObject gc_ExcavationBtnParty2;
    public GameObject gc_ExcavationBtnParty3;

    public GameObject[] gc_ExcavationButtons = new GameObject[4];
    public GameObject gc_LootDisplayPanel4ExcavationFuncitons;

    //add "public" as necessary
    int[] gc_LevelsAvailable = new int[16]; //0 level is unavailable, 1 it is.
    int[] gc_PlayerToolCount = new int[16]; //according to index, count the tools
    public int[] gc_SelectedLevelToolRequired = new int[16]; //corresponds to gc_PlayerToolCount

    public int[] inspectorViewOfPlayerStash = new int[16];
    public int inspectorViewOfSelectedParty;

    public int gc_SelectedLevel;
    string textToDisplay = "";
    bool gc_GoodToGoOnJourney; //probably not necessary
    bool StashIconsCreated;

    private float _invenDisplaySlotX = 64.0f; //Hardcoded for now
    private float _invenDisplaySlotY = 64.0f;
    private float _iconWidthOffset = 32.0f;
    private float _iconHeightOffset = 32.0f;
    private int _invenColumns = 4;

    GameObject[] gc_Levels = new GameObject[8]; //Corresponds to gc_LevelsAvailable

    public static int GetSelectedPartyIndex()
    {
        return gc_SelectedParty;
    }

    public void AdjustSelectedParty(int id)
    {
        if (gc_SelectedParty + id != -1 && gc_SelectedParty + id != 4)
        {
            gc_SelectedParty += id;
            for (int j = 0; j < MenuNavigaion.menuNavCataloguePointer.mn_SelectedPartiesTextFields.Count; ++j)
            {
                //int partyIndexDisplay = gc_SelectedLevel + 1;
                int tempInt = gc_SelectedParty;
                tempInt += 1;
                string tempString = tempInt.ToString();

                MenuNavigaion.menuNavCataloguePointer.mn_SelectedPartiesTextFields[j].text = tempString;
            }
        }
        else
        {
            textToDisplay = "Nothing more that way.";
            gc_MenuNavObj.GetComponent<MenuNavigaion>().mn_MessageToPlayerText.text = textToDisplay;
            gc_MenuNavObj.GetComponent<MenuNavigaion>().mn_MessageToPlayerPanel.SetActive(true);
        }
    }

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
        if(partyID <= 0 || partyID >= 3)
        {
            textToDisplay = "Something went screwy, party is being set to 1.";
            gc_MenuNavObj.GetComponent<MenuNavigaion>().mn_MessageToPlayerText.text = textToDisplay;
            gc_MenuNavObj.GetComponent<MenuNavigaion>().mn_MessageToPlayerPanel.SetActive(true);

            partyID = 0;
        }

        //function called by a UI button
        bool partyIsNotEmpty = false;
        bool partyIsBusy = false;
        bool hasRequiredTool = true;
        bool levelIsUnoccupied = true;

        textToDisplay = "";
        string textNoGo = "Can't Adventure, ";
        string textMoreTools = "More Tools Required. ";
        string textLevelInUse = "Level Is In Use. ";

        var theLevel = gc_CatalogueObj.GetComponent<LevelCatalogueData>().lcd_ArrayOfLevels[levelID].GetComponent<LevelObj>();
        var theParty = gc_Parties[partyID].GetComponent<PartyObj>();

        int LevelTravelCost = (int)Mathf.Floor(theLevel.lv_Distance);

        //reduce Energy
        for (int j = 0; j < theParty.po_PartyMembers.Length; ++j)
        {
            if(theParty.po_PartyMembers[j] != null)
            {
                if (theParty.po_PartyMembers[j].GetComponent<CharacterObj>().co_EnergyModifier >= LevelTravelCost)
                {
                    theParty.po_PartyMembers[j].GetComponent<CharacterObj>().co_EnergyModifier -= LevelTravelCost;
                }
                else
                {
                    textToDisplay = theParty.po_PartyMembers[j].GetComponent<CharacterObj>().co_Name + " is too tired to adventure.";
                }
            }
        }
        for (int p = 0; p < gc_Parties[partyID].GetComponent<PartyObj>().po_PartyMembers.Length; ++p)
        {
            if (gc_Parties[partyID].GetComponent<PartyObj>().po_PartyMembers[p] != null)
            {
                partyIsNotEmpty = true;
            }
        }

        if (theParty.po_PartyIsActive == true)
        {
            partyIsBusy = true;
        }
        else { partyIsBusy = false; }

        if (partyIsNotEmpty && partyIsBusy != true)
        {
            for (int i = 0; i < gc_SelectedLevelToolRequired.Length; ++i) //checking if party has tools needed for journey
            {
                if (theLevel.lv_IsActive == false && gc_SelectedLevelToolRequired[i] <= gc_PlayerToolCount[i])
                {
                    gc_PlayerToolCount[i] -= gc_SelectedLevelToolRequired[i];
                    //reduce energy?
                    gc_GoodToGoOnJourney = true;
                    hasRequiredTool = true;
                }
                else
                {
                    //otherwise the level is occupied or the right tool aren't there
                    gc_GoodToGoOnJourney = false;
                    hasRequiredTool = false;
                    textToDisplay += textNoGo;
                    textToDisplay += textMoreTools;
                    //break out of for loop
                }
            }
            if (theLevel.lv_IsActive == true)//check if level is occupied
            {
                gc_GoodToGoOnJourney = false;
                levelIsUnoccupied = false;
                textToDisplay += textNoGo;
                textToDisplay += textLevelInUse;
            }

            if (gc_GoodToGoOnJourney != false && levelIsUnoccupied != false)
            {
                theParty.po_PartyIsActive = true; //set bool so party doesn't go on multi adventures
                theLevel.GetComponent<LevelObj>().lv_IsActive = true;//set bool so level only has one party at a time
                theLevel.GetComponent<LevelObj>().lv_PartyObject = theParty.po_PartyIconObject.GetComponent<IconObj>().io_ObjectForThisIcon;
                theParty.po_TravelTime = theLevel.lv_Distance;
                theParty.po_LevelExploring = levelID;
                textToDisplay = "Adventure Ho!";
            }
            else
            {
                if(levelIsUnoccupied != false)
                {
                    textToDisplay = "Made it through all checks, but something is wrong.";
                }                
            }
        }
        else
        {
            if(partyIsBusy)
            {
                textToDisplay = "This party is already exploring.";
            }
            else
            {
                textToDisplay = "You must select a party first.";
            }
        }
        gc_MenuNavObj.GetComponent<MenuNavigaion>().mn_MessageToPlayerText.text = textToDisplay;
        gc_MenuNavObj.GetComponent<MenuNavigaion>().mn_MessageToPlayerPanel.SetActive(true);
    }

    void UpdateExplorers()
    {
        textToDisplay = "";
        GameObject[] LevelArray = gc_CatalogueObj.GetComponent<LevelCatalogueData>().lcd_ArrayOfLevels;
        for (int i = 0; i < gc_Parties.Length; ++i)
        {
            var partyObjComp = gc_Parties[i].GetComponent<PartyObj>();
            GameObject party = gc_Parties[i];

            if (partyObjComp.po_PartyIsActive)//is on an adventure
            {
                partyObjComp.po_TimeGoneFor++;
                float timer = partyObjComp.po_TimeGoneFor;

                float microsecondsGoneFor = Mathf.RoundToInt(timer % 60);
                float secondsGoneFor = Mathf.Floor(timer / 60);
                float minutesGoneFor = Mathf.Floor(secondsGoneFor / 60);
                float displaySecondsGoneFor = Mathf.Floor(secondsGoneFor % 60);
                float hoursGoneFor = minutesGoneFor / 60;
                gc_MenuNavObj.GetComponent<MenuNavigaion>().mn_PartyAdventureTimerArray[i].text = minutesGoneFor.ToString("00") + ":" + displaySecondsGoneFor.ToString("00") + ":" + microsecondsGoneFor.ToString("00");

                //Check for Wagon
                if (partyObjComp.po_HasWagon)
                {
                    float d1000 = Mathf.RoundToInt(Random.Range(0.0f, 1000.0f));
                    if (d1000 >= 999)//if wagon check if it breaks
                    {
                        if (!partyObjComp.po_HasRepairKit)//if it breaks check for toolkit
                        {
                            //Wagon Breaks
                            partyObjComp.po_HasWagon = false;
                            textToDisplay = "Your wagon is broken.";
                        }
                        else // fix if there is
                        {
                            partyObjComp.po_HasRepairKit = false;
                            textToDisplay = "Your Wagon has Broken, but also has been repaired.";
                        }
                    }
                }

                //float temp = partyObjComp.po_TravelTime * 2;
                //Debug.Log("The has been gone for: " + secondsGoneFor + " && the party will reach the area in: " + partyObjComp.po_TravelTime.ToString());

                if (secondsGoneFor >= partyObjComp.po_TravelTime && partyObjComp.po_ExcavationComplete == false) //Area reached
                {
                    //ExcavateLevel(party);
                    LevelArray[partyObjComp.po_LevelExploring].GetComponent<LevelObj>().ExcavateLevel(party);
                    //Poorly named function determines the items in the loot pile
                    gc_ExcavationButtons[party.GetComponent<PartyObj>().po_PartyID].SetActive(true);
                    partyObjComp.po_ExcavationComplete = true;
                }
                else if(secondsGoneFor >= partyObjComp.po_TravelTime *2 && partyObjComp.po_ExcavationComplete == true)
                {
                    //reduce extra energy according to loot carried
                    //partyObjComp.po_PartyIsActive = false;
                    //LevelArray[partyObjComp.po_LevelExploring].GetComponent<LevelObj>().lv_IsActive = false;
                }
            }
            //if they're not active they're resting, therefore should be regaining energy
        }
    }

	// Use this for initialization
	void Start ()
    {
        ItemCatalogueConstantValues.PopulateItemArrays();
        LevelCatalogueConstantValues.PopulateItemArrays();
        CharacterCatalogueConstantValues.PopulateItemArrays();
        gc_CatalogueObj.GetComponent<ItemCatalogueData>().CreateItems();
        gc_PartyCatalogue.GetComponent<PartyCatalogueData>().CreateParties();
        ShopCatalogueConstantValues.PopulateItemArrays();

        gc_ExcavationButtons[0] = gc_ExcavationBtnParty0;
        gc_ExcavationButtons[1] = gc_ExcavationBtnParty1;
        gc_ExcavationButtons[2] = gc_ExcavationBtnParty2;
        gc_ExcavationButtons[3] = gc_ExcavationBtnParty3;

        for(int i = 0; i < gc_ExcavationButtons.Length; ++i)
        {
            gc_ExcavationButtons[i].SetActive(false);
        }

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
        gc_GlobalMunnieDisplayText.text = gc_Munnies.ToString();
        inspectorViewOfPlayerStash = gc_StashOfItems;
    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdateExplorers();
        inspectorViewOfSelectedParty = gc_SelectedParty;

    }
}
