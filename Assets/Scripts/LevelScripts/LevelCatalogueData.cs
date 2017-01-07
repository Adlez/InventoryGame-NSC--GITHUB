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

            newLevel.AddComponent<LevelObj>();

            newLevel.GetComponent<LevelObj>().lv_GameController = lcd_GameController;
            newLevel.GetComponent<LevelObj>().lv_Name = LevelCatalogueConstantValues.LEVELNAMES[i];
            newLevel.GetComponent<LevelObj>().lv_Distance = LevelCatalogueConstantValues.LEVELDISTANCES[i];
            //int[] tempArray = new int[16];
            //tempArray = LevelCatalogueConstantValues.LEVELREQUIREDTOOLS[i];
            //int tempInt = tempArray[5];
            newLevel.GetComponent<LevelObj>().lv_ToolRequiredIndex = LevelCatalogueConstantValues.LEVELREQUIREDTOOLS[i]; //STAR - not doing the thing
            newLevel.GetComponent<LevelObj>().lv_LootIndex = LevelCatalogueConstantValues.LEVELLOOTINDEX[i];

            newLevel.name = newLevel.GetComponent<LevelObj>().lv_Name;
            newLevel.transform.SetParent(_LevelContainer.transform);
            lcd_ArrayOfLevels[i] = newLevel;

        }
    }
	public void DisplayAllLevels()
    {
        if(lcd_ArrayOfLevels[0] == null)
        {
            CreateLevels();
        }
    }
}
