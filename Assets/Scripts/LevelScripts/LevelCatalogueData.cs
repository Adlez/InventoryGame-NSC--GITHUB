using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCatalogueData : MonoBehaviour
{
    public GameObject lcd_GameController;
    public GameObject[] lcd_ArrayOfLevels = new GameObject[8];//Array of Levels
    public Transform[] lcd_LevelDisplayAreas = new Transform[8];//Array of places on the screen to display level icons

    public void CreateLevels()
    {
        GameObject _LevelContainer = new GameObject("LevelContainer");
        GameObject _LevelIconContainer = new GameObject("LevelIconContainer");

        for(int i = 0; i < lcd_ArrayOfLevels.Length; ++i)
        {
            GameObject newLevel = new GameObject("Level");
            newLevel.transform.SetParent(lcd_LevelDisplayAreas[i]);

            newLevel.AddComponent<LevelScript>();

            newLevel.GetComponent<LevelScript>().lv_GameController = lcd_GameController;
            newLevel.GetComponent<LevelScript>().lv_Name = LevelCatalogueConstantValues.LEVELNAMES[i];
            newLevel.GetComponent<LevelScript>().lv_Distance = LevelCatalogueConstantValues.LEVELDISTANCES[i];
            newLevel.GetComponent<LevelScript>().lv_ToolRequiredIndex = LevelCatalogueConstantValues.LEVELREQUIREDTOOLS[i];
            newLevel.GetComponent<LevelScript>().lv_LootIndex = LevelCatalogueConstantValues.LEVELLOOTINDEX[i];

            newLevel.name = newLevel.GetComponent<LevelScript>().lv_Name;
            lcd_ArrayOfLevels[i] = newLevel;
            /*
            public GameObject lv_GameController;
            public GameObject lv_PartyObject;
            string lv_Name;

            int[] lv_ToolRequiredIndex;
            int[] lv_LootIndex; //corresponds to treasure array, used to search for the right item
            List<GameObject> lv_PotentialLootList = new List<GameObject>();

            public float lv_Distance;
            public bool lv_IsActive;
            */

        }
    }
	
}
