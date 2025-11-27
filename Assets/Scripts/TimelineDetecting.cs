using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class TimelineDetecting : MonoBehaviour
{
    public TextMeshProUGUI text_timer;
    public float timer_timeline;
    public GameObject currtimelineobj;
    public List<string> TimelineTexts = new List<string>();
    public List<GameObject> texts_obj_timeline = new List<GameObject>();
    public Material mat_black;
    public GameObject floor_black;
    public Vector3 player_spawn_position;
    public Vector3 hero_rotation;
    public GameObject hero;
    public GameObject audio_wicked_fascinations;
    public GameObject cam;
    public string glitch_script;
    public Vector3 timeline_pos;
    public GameObject door;
    public Material red_mat;
    public int timeline_changed_count;
    public GameObject door_light;
    public GameObject spawned_obj;
    public bool text_bug;
    public GameObject temp_obj;
    public float StartTimeline;
    public float EndTimeline;
    public float StartTimeline2;
    public float EndTimeline2;
    public bool timeline_text_change;
    public int h1;
    public GameObject snow_a;
    public static TimelineDetecting instance;
    public GameObject colliders;
    public bool is_changing_timeline_now;
    public string fps;
    public float flashStartTime;
    public float flashEndTime;
    public Color _emissionColorValue;
    public float _intensity;
    public Material mat_red;
    public GameObject dark_wall;
    public GameObject flash;
    public GameObject snow_floor;
    public GameObject snow_floor_timeline;
    public GameObject sfx_manager;
    public GameObject sfx_manager2;
    public GameObject flash_victory;
    public GameObject flash_continue;
    public GameObject sfx_manager3;
    public GameObject flash_image;
    public GameObject collider_place;
    public GameObject stats_panel_won;
    public float StartTimeWon;
    public TextMeshProUGUI time_txt;
    public TextMeshProUGUI difficulty_txt;
    public TextMeshProUGUI round_txt;
    public float EndTimeWon;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
    void buggy_text()
    {
        if (text_bug == true)
        {
            UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.m_WalkSpeed = 3.5f;
            UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.m_RunSpeed = 3.5f;
            if (timer_timeline > 0.0f)
            {
                timer_timeline -= Time.deltaTime;
                text_timer.text = timer_timeline.ToString();
            }
            if (timer_timeline <= 19.8f)
            {
                (hero.GetComponent(fps) as MonoBehaviour).enabled = true;
            }
            StartTimeline += Time.deltaTime;
            if (StartTimeline >= EndTimeline)
            {

                StartTimeline = 0.0f;
                if (h1 == 31)
                {
                    h1 = 0;
                }
                if (h1 == 0)
                {
                    texts_obj_timeline[31].SetActive(false);

                }
                texts_obj_timeline[h1+1].SetActive(true);
                texts_obj_timeline[h1].SetActive(false);
                h1++;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Spin.instance.X >= 100.0f && !WheelOfFortune.instance.you_won)
        {
            flash.SetActive(true);
            WheelOfFortune.instance.can_pause = false;
            flashStartTime += Time.deltaTime;
            (hero.GetComponent(fps) as MonoBehaviour).enabled = false;
            if (flashStartTime >= flashEndTime)
            {
                flashStartTime = 0.0f;
                (hero.GetComponent(fps) as MonoBehaviour).enabled = true;
                Spin.instance.X = 0.0f;
                WheelOfFortune.instance.spinaudio.SetActive(false);
                flash.SetActive(false);
                StartDispatching();
                WheelOfFortune.instance.can_pause = true;
            }
            WheelOfFortune.instance.stopped = 0;
        }
        if (WheelOfFortune.instance.you_won)
        {
            flash_victory.SetActive(true);
            WheelOfFortune.instance.can_pause = false;
            flash_image.GetComponent<Image>().color = new Color32(0, 255, 0, 0);
            Time_Brishen.instance.Brishen_will_come_time = 10000.0f;
            sfx_manager.SetActive(false);
            sfx_manager2.SetActive(false);
            sfx_manager3.SetActive(false);
            StartTimeWon += Time.deltaTime;
            if (StartTimeWon >= EndTimeWon)
            {
                flash_continue.SetActive(true);
                stats_panel_won.SetActive(true);
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
                StartTimeWon = 0.0f;
            }
        }
        buggy_text();
    }
    void StartDispatching()
    {
        DarkEffect.instance.col_restore = true;
        UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.door_open.SetActive(false);
        UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.door_closed.SetActive(true);
        UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.WickedFascinatinsaudio.volume = 1.0f;
        is_changing_timeline_now = true;
        timeline_changed_count++;
        _intensity = 0.0f;
        sfx_manager.SetActive(false);
        sfx_manager2.SetActive(false);
        sfx_manager3.SetActive(false);
        mat_red.SetVector("_EmissionColor", _emissionColorValue * _intensity);
        RenderSettings.skybox = mat_black;
        for (int z = 0; z < 32; z++)
        {
            currtimelineobj = WheelOfFortune.instance.TimelineTexts[z];
            GameObject spawned_obj = Instantiate(currtimelineobj, timeline_pos, Quaternion.Euler(-180.0f, 270.0f, -180.0f));
            spawned_obj.transform.localScale = new Vector3(33.6485f, 33.6485f, 33.6485f);
            spawned_obj.layer = LayerMask.NameToLayer("NoPost");
            spawned_obj.GetComponent<MeshRenderer>().material = red_mat;
            texts_obj_timeline[z] = spawned_obj;
            WheelOfFortune.instance.transform_player = true;
            WheelOfFortune.instance.x = player_spawn_position.x;
            WheelOfFortune.instance.y = player_spawn_position.y;
            WheelOfFortune.instance.z = player_spawn_position.z;
        }
        text_bug = true;
        RenderSettings.fogDensity = 0.0f;
        DispatchToAnotherTimeline();
    }
    void DispatchToAnotherTimeline()
    {
        collider_place.SetActive(false);
        snow_floor.SetActive(false);
        snow_floor_timeline.SetActive(true);
        dark_wall.SetActive(true);
        for (int k = 0; k < 32; k++)
        {
            texts_obj_timeline[k].SetActive(false);
        }
        door_light.SetActive(true);
        audio_wicked_fascinations.SetActive(true);
        DarkEffect.instance.is_changing_timeline = true;
        //(cam.GetComponent(glitch_script) as MonoBehaviour).enabled = true;
        for (int i = 0; i < 9; i++)
        {
            if (i != 8)
            {
                WheelOfFortune.instance.objects_environment_list[i].SetActive(false);
            }
        }
        
        //UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.LockMouse();
    }
}