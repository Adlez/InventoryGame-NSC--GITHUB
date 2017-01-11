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

    private float _levelDisplaySlotX = 64.0f; //Hardcoded for now
    private float _levelDisplaySlotY = 64.0f;
    private float _iconWidthOffset = 32.0f;
    private float _iconHeightOffset = 32.0f;
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
            newLevel.transform.SetParent(_LevelContainer.transform);
            lcd_ArrayOfLevels[i] = newLevel;
        }
    }

	public void DisplayAllLevels()
    {
        /*if(lcd_ArrayOfLevels[0] == null)
        {
            CreateLevels();
        }*/
        for(int i = 0; i < lcd_LevelDisplayAreas.Length; ++i)
        {
            GameObject levelIcon = IconObj.MakeIconObject(lcd_ArrayOfLevels[i], lcd_LevelsParent);// lcd_LevelDisplayAreas[i]);
            lcd_ArrayOfLevels[i].GetComponent<LevelObj>().lv_IconObject = levelIcon;

            lcd_ArrayOfLevels[i].name = lcd_ArrayOfLevels[i].GetComponent<LevelObj>().lv_Name;
            lcd_ArrayOfLevels[i].GetComponent<LevelObj>().lv_IconObject.GetComponent<IconObj>().GetComponent<Image>().sprite = LevelCatalogueConstantValues.LEVELICONS[i];
        }
        PositionIconsOnScreen();
    }

    public void PositionIconsOnScreen()
    {
        for (int i = 0; i < lcd_ArrayOfLevels.Length; ++i)
        {
            Vector3 iconPos = lcd_ArrayOfLevels[i].GetComponent<LevelObj>().lv_IconObject.GetComponent<Transform>().position;
            iconPos.x = _levelDisplaySlotX * (i % _levelColumns) + _iconWidthOffset;
            iconPos.y = (_levelDisplaySlotY * (i / _levelColumns) - _iconHeightOffset) * -1;
            //iconPos.x = 0.0f;
            //iconPos.y = 0.0f;
            lcd_ArrayOfLevels[i].GetComponent<LevelObj>().lv_IconObject.GetComponent<Transform>().position = new Vector3(iconPos.x, iconPos.y, 100.0f);
        }
    }
}
