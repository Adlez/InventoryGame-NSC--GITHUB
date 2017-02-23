using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class LevelCatalogueData : MonoBehaviour
{
    public GameObject lcd_GameController;
    public GameObject lcd_LevelsParent;
    public GameObject[] lcd_ArrayOfLevels = new GameObject[8];//Array of Levels
    public GameObject[] lcd_LevelDisplayAreas = new GameObject[8];//Array of places on the screen to display level icons

    private float _levelDisplaySlotX = 16.0f; //Hardcoded for now
    private float _levelDisplaySlotY = 16.0f;
    private float _iconWidthOffset = 16.0f;
    private float _iconHeightOffset = 16.0f;
    private int _levelColumns = 4;

    public void CreateLevels()
    {
        GameObject _LevelContainer = new GameObject("LevelContainer");
        GameObject _LevelIconContainer = new GameObject("LevelIconContainer");

        for(int i = 0; i < lcd_ArrayOfLevels.Length; ++i)
        {
            GameObject newLevel = new GameObject("Level");
            newLevel.transform.SetParent(_LevelContainer.transform);

            newLevel.AddComponent<LevelObj>();

            newLevel.GetComponent<LevelObj>().lv_GameController = lcd_GameController;
            newLevel.GetComponent<LevelObj>().lv_Name = LevelCatalogueConstantValues.LEVELNAMES[i];
            newLevel.GetComponent<LevelObj>().lv_Distance = LevelCatalogueConstantValues.LEVELDISTANCES[i];
            newLevel.name = newLevel.GetComponent<LevelObj>().lv_Name;
            newLevel.GetComponent<LevelObj>().lv_ToolRequiredIndex = LevelCatalogueConstantValues.LEVELREQUIREDTOOLS[i];
            newLevel.GetComponent<LevelObj>().lv_LootIndex = LevelCatalogueConstantValues.LEVELLOOTINDEX[i];
            newLevel.GetComponent<LevelObj>().lv_ID = i;
            newLevel.transform.SetParent(_LevelContainer.transform);
            lcd_ArrayOfLevels[i] = newLevel;
        }
    }

    public void Bilbo(GameObject LevelIconClicked)
    {
        lcd_GameController.GetComponent<GameControllerScript>().AttemptAnAdventure(GameControllerScript.gc_SelectedParty, LevelIconClicked.GetComponent<IconObj>().io_ObjectForThisIcon.GetComponent<LevelObj>().lv_ID);
    }

	public void DisplayAllLevels()
    {
        if(lcd_ArrayOfLevels[0] == null)
        {
            CreateLevels();
        }
        for(int i = 0; i < lcd_LevelDisplayAreas.Length; ++i)
        {
            GameObject levelIcon = IconObj.MakeIconObject(lcd_ArrayOfLevels[i], lcd_LevelsParent, "Level");
            lcd_ArrayOfLevels[i].GetComponent<LevelObj>().lv_IconObject = levelIcon;

            lcd_ArrayOfLevels[i].name = lcd_ArrayOfLevels[i].GetComponent<LevelObj>().lv_Name;
            lcd_ArrayOfLevels[i].GetComponent<LevelObj>().lv_IconObject.GetComponent<IconObj>().GetComponent<Image>().sprite = LevelCatalogueConstantValues.LEVELICONS[i];
            lcd_ArrayOfLevels[i].GetComponent<LevelObj>().lv_IconObject.AddComponent<Button>();
            //GameObject Level = lcd_ArrayOfLevels[i];
            lcd_ArrayOfLevels[i].GetComponent<LevelObj>().lv_IconObject.GetComponent<Button>().onClick.AddListener(delegate { Bilbo(levelIcon); });
            //Delegate
        }
        PositionIconsOnScreen(lcd_ArrayOfLevels);
    }

    public void PositionIconsOnScreen(GameObject[] arrayOfLevels)
    {
        for (int i = 0; i < arrayOfLevels.Length; ++i)
        {
            arrayOfLevels[i].GetComponent<LevelObj>().lv_IconObject.transform.SetParent(lcd_LevelDisplayAreas[i].transform);
            arrayOfLevels[i].GetComponent<LevelObj>().lv_IconObject.transform.localPosition = new Vector3( 0.0f, 0.0f, 0.0f);
        }
    }
}