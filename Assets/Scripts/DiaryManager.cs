using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryManager : MonoBehaviour
{
    public GameObject pressJ;
    public GameObject Diary_obj;
    public int journal_count;
    public static DiaryManager instance;
    public bool jester_chapter1_found;
    public bool jester_chapter2_found;
    public int current_journal = 0;
    public List<GameObject> texts_list = new List<GameObject>();
    public GameObject jester_panel1;
    public GameObject jester_panel2;
    public GameObject hero;
    public GameObject time_;
    public string hero_fps;
    public string time_script;
    public GameObject map;
    public GameObject time;
    public GameObject battery;
    public GameObject money_canvas;
    public GameObject timeline;
    public GameObject jester_panel;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
    void open_jester()
    {
        Debug.Log("Opened jester's diary");
        jester_panel.SetActive(true);
        PagesSystem.instance.init_diary();
        UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.horizontal_ = "k";
        UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.vertical_ = "k2";
    }
    void open_dwight()
    {
        Debug.Log("Opened dwight's diary");

    }
    void DeactivateAll()
    {
        (hero.GetComponent(hero_fps) as MonoBehaviour).enabled = false;
        (time_.GetComponent(time_script) as MonoBehaviour).enabled = false;
    }
    void ActivateAll()
    {
        (hero.GetComponent(hero_fps) as MonoBehaviour).enabled = true;
        (time_.GetComponent(time_script) as MonoBehaviour).enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PagesSystem.instance.jesterpagepanel.SetActive(false);
            map.SetActive(true);
            UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.horizontal_ = "Horizontal";
            UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.vertical_ = "Vertical";
            money_canvas.SetActive(true);
            time.SetActive(true);
            timeline.SetActive(true);
            battery.SetActive(true);
        }
        //if (jester_chapter2_found == true)
        //{
        //    if (current_journal == 0)
        //    {
        //        jester_panel1.SetActive(true);
        //        jester_panel2.SetActive(false);
        //    }
        //    if (current_journal == 1)
        //    {
        //        jester_panel2.SetActive(true);
        //        jester_panel1.SetActive(false);
        //    }
        //    if (Input.GetKeyDown(KeyCode.DownArrow))
        //    {
        //        if (current_journal == 0)
        //        {
        //            current_journal = 1;
        //        }
        //    }
        //    if (Input.GetKeyDown(KeyCode.UpArrow))
        //    {
        //        if (current_journal == 1)
        //        {
        //            current_journal = 0;
        //        }
        //    }
        //}
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (current_journal == 0)
            {
                open_jester();
            }
            Diary_obj.SetActive(false);
            ActivateAll();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (Diary_obj.activeInHierarchy == false)
            {
                map.SetActive(false);
                money_canvas.SetActive(false);
                time.SetActive(false);
                timeline.SetActive(false);
                battery.SetActive(false);
                if (jester_chapter1_found == true)
                {
                    texts_list[0].SetActive(true);
                }
                if (jester_chapter2_found == false && jester_chapter1_found == true)
                {
                    jester_panel1.SetActive(true);
                }
                Diary_obj.SetActive(true);
                
                if (pressJ.activeInHierarchy == true)
                {
                    pressJ.SetActive(false);
                }
                DeactivateAll();
            }
            else
            {
                Diary_obj.SetActive(false);
                ActivateAll();
                map.SetActive(true);
                money_canvas.SetActive(true);
                time.SetActive(true);
                battery.SetActive(true);
                timeline.SetActive(true);
            }
        }
    }
}
