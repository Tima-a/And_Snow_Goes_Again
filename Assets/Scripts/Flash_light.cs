using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash_light : MonoBehaviour
{
    public float StartFlashlightTime;
    public float EndFlashlightTime;
    public int minimum_battery_level;
    public int battery_level;
    public int maximum_battery_level;
    public static Flash_light instance;
    public List<GameObject> flash_light_levels = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        if (SettingsMenu.difficulty == 0)
        {
            EndFlashlightTime = 55.0f;
        }
        if (SettingsMenu.difficulty == 1)
        {
            EndFlashlightTime = 40.0f;
        }
        if (SettingsMenu.difficulty == 2)
        {
            EndFlashlightTime = 30.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        battery_level = Mathf.Clamp(battery_level, 0, 4);
        if (battery_level > minimum_battery_level)
        {
            StartFlashlightTime += Time.deltaTime;
            if (StartFlashlightTime >= EndFlashlightTime)
            {
                StartFlashlightTime = 0.0f;
                battery_level--;
            }
        }
        if (battery_level == 3)
        {
            flash_light_levels[1].SetActive(true);
            flash_light_levels[2].SetActive(true);
            flash_light_levels[3].SetActive(true);
            flash_light_levels[0].SetActive(true);
        }
        if (battery_level == 2)
        {
            flash_light_levels[1].SetActive(true);
            flash_light_levels[2].SetActive(true);
            flash_light_levels[3].SetActive(false);
            flash_light_levels[0].SetActive(true);
        }
        if (battery_level == 1)
        {
            flash_light_levels[1].SetActive(true);
            flash_light_levels[2].SetActive(false);
            flash_light_levels[3].SetActive(false);
            flash_light_levels[0].SetActive(true);
        }
        if (battery_level == 0)
        {
            flash_light_levels[1].SetActive(false);
            flash_light_levels[2].SetActive(false);
            flash_light_levels[3].SetActive(false);
            flash_light_levels[0].SetActive(true);
        }
        if (battery_level == -1 && TimelineDetecting.instance.is_changing_timeline_now == false)
        {
            RenderSettings.fogDensity = 0.7f;
            flash_light_levels[1].SetActive(false);
            flash_light_levels[2].SetActive(false);
            flash_light_levels[3].SetActive(false);
            flash_light_levels[0].SetActive(false);
        }
        if (battery_level > -1 && TimelineDetecting.instance.is_changing_timeline_now == false)
        {
            RenderSettings.fogDensity = 0.2f;
        }
    }
}
