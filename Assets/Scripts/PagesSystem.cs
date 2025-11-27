using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PagesSystem : MonoBehaviour
{
    public int current_page;
    public List<GameObject> pages_text = new List<GameObject>();
    public static PagesSystem instance;
    public TextMeshProUGUI pages_count_text;
    public GameObject jesterpagepanel;
    public bool inMenu;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
    public void init_diary()
    {
        current_page = 0;
    }
    public void NextPage()
    {
        if (current_page < pages_text.Count-1)
        {
            current_page++;
        }
    }
    public void PreviousPage()
    {
        if (current_page >= 1)
        {
            current_page--;
        }
    }
    void deactivate_all_other_texts()
    {
        for (int i = 0; i < pages_text.Count; i++)
        {
            if (i != current_page)
            {
                pages_text[i].SetActive(false);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        deactivate_all_other_texts();
        pages_count_text.text = (current_page + 1).ToString() + " out of 5";
        if (!inMenu)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                jesterpagepanel.SetActive(false);
                DiaryManager.instance.map.SetActive(true);
                DiaryManager.instance.battery.SetActive(true);
                UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.horizontal_ = "Horizontal";
                UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.vertical_ = "Vertical";
                DiaryManager.instance.money_canvas.SetActive(true);
                DiaryManager.instance.time.SetActive(true);
                DiaryManager.instance.timeline.SetActive(true);
            }
        }
        pages_text[current_page].SetActive(true);
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreviousPage();
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextPage();
        }
    }
}
