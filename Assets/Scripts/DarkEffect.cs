using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using TMPro;

public class DarkEffect : MonoBehaviour
{
    public Vignette vig = null;
    public bool vig_activated;
    public float StartTimevig;
    public float EndTimevig;
    public bool vig_started;
    public bool is_changing_timeline;
    public static DarkEffect instance;
    public float colStartTime;
    [SerializeField]
    public Slider slider_brightness;
    public float colEndTime;
    public bool col_restore;
    ColorGrading col = null;
    public float StartTimeDying_1;
    public float EndTimeDying_1;
    public float StartTimeDying_2;
    public float EndTimeDying_2;
    public float StartTimeDying_3;
    public float EndTimeDying_3;
    public float StartTimeDying_4;
    public float EndTimeDying_4;
    public float StartTimeDying_5;
    public float EndTimeDying_5;
    public float intensity_dying1;
    public float center_x_vig;
    public bool dying_;
    public bool dying_1;
    public bool dying_2;
    public bool dying_3;
    public bool dying_4;
    public bool dying_5;
    public GameObject red_screen_dying;
    public GameObject death_heartbeat;
    public TextMeshProUGUI time_txt;
    public TextMeshProUGUI difficulty_txt;
    public TextMeshProUGUI round_txt;
    public GameObject heartbeat;
    public float StartWaitTime;
    public float EndWaitTime;
    public GameObject stats_panel;
    void Start()
    {
        instance = this;
    }
    void Awake()
    {
        // somewhere during initializing
        PostProcessVolume volume = gameObject.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out vig);
        // somewhere during initializing
        volume.profile.TryGetSettings(out col);
        slider_brightness.value = BrightnessMenu.brightness_level;
        // later in this class during handling and changing
        // later in this class during handling and changing
    }
    void start_dying()
    {
        if (dying_ == true)
        {
            heartbeat.SetActive(true);
            StartTimeDying_1 += Time.deltaTime;
            if (dying_1 == false)
            {
                vig.enabled.value = true;
                center_x_vig -= 0.03f;
                intensity_dying1 -= 0.007f;
                Mathf.Lerp(center_x_vig, 0.5f, 3.0f);
                Mathf.Lerp(intensity_dying1, 0.0f, 1.0f);
                vig.color.value = new Color32(255, 0, 0, 255);
                vig.center.value = new Vector2(center_x_vig, 0.5f);
                vig.intensity.value = intensity_dying1;
            }
            if (StartTimeDying_1 >= EndTimeDying_1)
            {
                dying_1 = true;
                StartTimeDying_2 += Time.deltaTime;
                if (dying_2 == false)
                {
                    intensity_dying1 += 0.007f;
                    Mathf.Lerp(vig.intensity.value, 0.0f, 1.0f);
                    vig.intensity.value = intensity_dying1;

                }
            }
            if (StartTimeDying_2 >= EndTimeDying_2)
            {

                dying_2 = true;
                StartTimeDying_3 += Time.deltaTime;
                if (dying_3 == false)
                {
                    intensity_dying1 -= 0.007f;
                    Mathf.Lerp(vig.intensity.value, 0.0f, 1.0f);
                    vig.intensity.value = intensity_dying1;

                }
            }
            if (StartTimeDying_3 >= EndTimeDying_3)
            {
                dying_3 = true;
                StartTimeDying_4 += Time.deltaTime;
                if (dying_4 == false)
                {
                    intensity_dying1 += 0.007f;
                    Mathf.Lerp(vig.intensity.value, 0.0f, 1.0f);
                    vig.intensity.value = intensity_dying1;

                }
            }
            if (StartTimeDying_4 >= EndTimeDying_4)
            {

                dying_4 = true;
                StartTimeDying_5 += Time.deltaTime;
                if (dying_5 == false)
                {
                    intensity_dying1 -= 0.007f;
                    Mathf.Lerp(vig.intensity.value, 0.0f, 1.0f);
                    vig.intensity.value = intensity_dying1;

                }
            }
            if (StartTimeDying_5 >= EndTimeDying_5+4.0f)
            {
                heartbeat.SetActive(false);
                death_heartbeat.SetActive(true);
                dying_5 = true;
                red_screen_dying.SetActive(true);

            }
        }
    }
    void intensity_increase()
    {
        if (vig_activated == true)
        {
            StartTimevig += Time.deltaTime;
            vig.intensity.value += 0.008f;
            if (StartTimevig >= EndTimevig)
            {
                StartTimevig = 0.0f;
                vig_activated = false;
            }
        }
    }
    void Restore_col()
    {
        if (col_restore == true)
        {
            colStartTime += Time.deltaTime;
            col.temperature.value -= 1.0f;
            if (colStartTime >= colEndTime)
            {
                col.temperature.value = -75.0f;
                colStartTime = 0.0f;
                col_restore = false;
            }
        }
    }
    void Update()
    {
        if (dying_5 == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            WheelOfFortune.instance.can_pause = false;
            TimelineDetecting.instance.sfx_manager.SetActive(false);
            TimelineDetecting.instance.sfx_manager2.SetActive(false);
            TimelineDetecting.instance.sfx_manager3.SetActive(false);
            StartWaitTime += Time.deltaTime;
            if (StartWaitTime >= EndWaitTime)
            {
                stats_panel.SetActive(true);
                string global_time_minutes_str;
                string global_time_seconds_str;
                if (WheelOfFortune.instance.global_time_minutes < 10)
                {
                    global_time_minutes_str = "0" + WheelOfFortune.instance.global_time_minutes.ToString();
                }
                else
                {
                    global_time_minutes_str = WheelOfFortune.instance.global_time_minutes.ToString();
                }
                if (WheelOfFortune.instance.global_time_seconds < 10)
                {
                    global_time_seconds_str = "0" + WheelOfFortune.instance.global_time_seconds.ToString();
                }
                else
                {
                    global_time_seconds_str = WheelOfFortune.instance.global_time_seconds.ToString();
                }
                time_txt.text = "Time: " + global_time_minutes_str + ":" + global_time_seconds_str;
                string difficulty_str = "";
                if (SettingsMenu.difficulty == 0)
                {
                    difficulty_str = "Easy";
                }
                if (SettingsMenu.difficulty == 1)
                {
                    difficulty_str = "Medium";
                }
                if (SettingsMenu.difficulty == 2)
                {
                    difficulty_str = "Hard";
                }
                difficulty_txt.text = "Difficulty: " + difficulty_str;
                round_txt.text = "Round: " + WheelOfFortune.instance.round.ToString();
                StartWaitTime = 0.0f;
            }
        }
        if (center_x_vig <= 0.53f)
        {
            center_x_vig = 0.5f;
        }
        if (intensity_dying1 >= 0.7f)
        {
            intensity_dying1 = 0.7f;
        }
        if (intensity_dying1 <= 0.0f)
        {
            intensity_dying1 = 0.0f;
        }
        Mathf.Lerp(center_x_vig, 0.5f, 3.0f);
        Mathf.Lerp(intensity_dying1, 0.0f, 1.0f);
        start_dying();
        col.postExposure.value = BrightnessMenu.brightness_level-3;
        Restore_col();
        intensity_increase();
        if (Time_Brishen.instance.minutes >= Time_Brishen.instance.Brishen_will_come_time + 7 && dying_ == false && vig_activated == false && vig.center.value.x == 0.5f)
        {
            vig.enabled.value = true;
            vig_activated = true;
            vig.center.value = new Vector2(3.0f, 0.5f);
            vig.color.value = new Color32(255, 0, 0, 255);
            vig.intensity.value = 0.0f;
        }
        if (TimelineDetecting.instance.door.activeInHierarchy == true)
        {
            Time_Brishen.instance.Brishen_will_come_time = 1000.0f;
            vig.color.value = new Color32(0, 0, 0, 255);
            vig.center.value = new Vector2(0.5f, 0.5f);
            vig.enabled.value = true;
            vig.intensity.value = 0.4f;
            //col.temperature.value = 0.0f;

        }
        if (col_restore == true)
        {
            Time_Brishen.instance.Brishen_will_come_time = 10.0F;
        }
    }
}
