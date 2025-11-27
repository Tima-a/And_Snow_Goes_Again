using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Time_Brishen : MonoBehaviour
{
    public TextMeshProUGUI text_time;
    public int minutes = 12;
    public int hours = 19;
    public float StartTime;
    public float EndTime;
    public AudioSource gunaudio;
    public float Brishen_will_come_time;
    public AudioClip clip;
    public float startfallingtime;
    public float endfallingtime;
    public float volume = 10.0f;
    public bool fall;
    public bool player_fall;
    public GameObject cam;
    public GameObject spine;
    public float Start_player_time;
    public float start_end_time;
    public GameObject hero;
    public string scriptname;
    public bool fps_x;
    public GameObject obj_spin_cam;
    public List<TextMeshProUGUI> list_texts = new List<TextMeshProUGUI>();
    public GameObject feet;
    public static Time_Brishen instance;
    public GameObject audio_dying;
    public float StartTimeDying;
    public float EndTimeDying;
    public bool start_die;
    // Start is called before the first frame update
    void Start()
    {
        if (SettingsMenu.difficulty == 0)
        {
            EndTime = 15.5f;
        }
        if (SettingsMenu.difficulty == 1)
        {
            EndTime = 13.5f;
        }
        if (SettingsMenu.difficulty == 2)
        {
            EndTime = 11.0f;
        }
        instance = this;
        text_time.text = "19:12";
    }
    void Fall_anim()
    {
        if (fall)
        {
            startfallingtime += Time.deltaTime;
            if (startfallingtime >= endfallingtime)
            {
                startfallingtime = 0.0f;
                HeroAnimationSystem.instance.is_falling = true;
                cam.transform.position = obj_spin_cam.transform.position;
                cam.transform.SetParent(spine.transform);
                fall = false;
            }
        }
    }
    void anim_player_cam()
    {
        if (player_fall == true)
        {
            Start_player_time += Time.deltaTime;
            if (Start_player_time >= start_end_time)
            {
                Start_player_time = 0.0f;
                player_fall = false;
            }
        }
    }
    void PlayerStartDying()
    {
        if (start_die == true)
        {
            StartTimeDying += Time.deltaTime;
            if (StartTimeDying >= EndTimeDying)
            {
                StartTimeDying = 0.0f;
                start_die = false;
                audio_dying.SetActive(true);
                DarkEffect.instance.dying_ = true;
            }
        }
    }
    void x_fps()
    {
        if (fps_x == true)
        {
            UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.m_MouseLook.falling_x_max();
        }
    }
    // Update is called once per frame
    void Update()
    {
        PlayerStartDying();
        anim_player_cam();
        Fall_anim();
        x_fps();
        if (minutes >= Brishen_will_come_time + 12)
        {
            //Debug.Log("Brishen is coming");
            gunaudio.PlayOneShot(clip, volume);
            Brishen_will_come_time = 12000;
            fall = true;
            player_fall = true;
            start_die = true;
            fps_x = true;
            HeroAnimationSystem.instance.dont_play_basic_anims = true;
            UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.horizontal_ = "k";
            UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.vertical_ = "k2";
            HeroAnimationSystem.instance.heroanim.SetBool("isStanding", false);
            HeroAnimationSystem.instance.heroanim.SetBool("isWalking", false);
            HeroAnimationSystem.instance.heroanim.SetBool("isRunning", false);
            HeroAnimationSystem.instance.heroanim.SetBool("isWalkingBack", false);
            //cam.transform.position = new Vector3(cam.transform.position.x, 0.4f, cam.transform.position.z);
        }
        if (minutes >= 7+Brishen_will_come_time)
        {
            for (int u = 0; u < list_texts.Count; u++)
            {
                list_texts[u].color = new Color32(255, 0, 0, 255);
            }
            WheelOfFortune.instance.text_processing.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        MinutesChanging();
        text_time.text = "19:" + minutes.ToString();
    }
    void MinutesChanging()
    {
        StartTime += Time.deltaTime;
        if (StartTime >= EndTime)
        {
            StartTime = 0.0f;
            minutes += 1;
        }
    }
}
