using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    //add "public" as necessary
    int[] gc_LevelsAvailable = new int[16]; //0 level is unavailable, 1 it is.
    int[] gc_PlayerToolCount = new int[16]; //according to index, count the tools
    int gc_SelectedLevel;
    public int[] gc_SelectedLevelToolRequired = new int[16]; //corresponds to gc_PlayerToolCount
    bool gc_GoodToGoOnJourney; //probably not necessary

    GameObject[] gc_Parties = new GameObject[16];
    GameObject[] gc_Levels = new GameObject[16]; //Corresponds to gc_LevelsAvailable
    List<GameObject> PartyLimbo = new List<GameObject>(); //list of characters not assigned to a party

    void CheckLoot()
    {

    }

    void AddLoot()
    {

    }

    public void AttemptAnAdventure(int partyID, int levelID)
    {
        var theLevel = gc_Levels[levelID].GetComponent<LevelScript>();
        var theParty = gc_Parties[partyID].GetComponent<PartyCompanyGroupScript>();
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
                theParty.pcg_PartyIsActive = true; //set bool so party doesn't go on multi adventures
                theLevel.GetComponent<LevelScript>().lv_IsActive = true;//set bool so level only has one party at a time
                theParty.pcg_TravelTime = theLevel.lv_Distance;
                theParty.pcg_LevelExploring = levelID;
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
            var party = gc_Parties[i].GetComponent<PartyCompanyGroupScript>();
            if (party.pcg_PartyIsActive)//is on an adventure
            {
                party.pcg_TimeGoneFor++;
                //reduce Energy
                //Check for Wagon
                //if wagon check if it breaks
                //if it breaks check for toolkit, fix if there is
                if (party.pcg_TimeGoneFor >= party.pcg_TravelTime && party.pcg_ExcavationComplete == false)
                {
                    //ExcavateLevel(party);
                    //function is likely stored in LevelScript.cs
                }
                else if(party.pcg_TimeGoneFor > party.pcg_TravelTime *2 && party.pcg_ExcavationComplete == true)
                {
                    //reduce extra energy according to loot carried
                    party.pcg_PartyIsActive = false;
                    gc_Levels[party.pcg_LevelExploring].GetComponent<LevelScript>().lv_IsActive = false;
                }


            }
            //if they're not active they're resting, therefore should be regaining energy
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
