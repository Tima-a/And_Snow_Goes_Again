using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenu : MonoBehaviour
{
    public GameObject close_scene;
    public GameObject close_scene2;
    public bool is_playing;
    public float StartTime;
    public float EndTime;
    [SerializeField]
    public Slider slider_volume;
    public Slider slider_brightness;
    public TextMeshProUGUI music_txt;
    public TextMeshProUGUI brightness_txt;
    public GameObject MainMenu_;
    public GameObject SettingsMenu;
    public GameObject RulesMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Exit_Settings()
    {
        MainMenu_.SetActive(true);
        SettingsMenu.SetActive(false);
    }
    public void Enter_Settings()
    {
        MainMenu_.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SettingsMenu.SetActive(true);
    }
    public void Exit_Rules()
    {
        MainMenu_.SetActive(true);
        RulesMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Enter_Rules()
    {
        MainMenu_.SetActive(false);
        RulesMenu.SetActive(true);
    }
    public void Play()
    {
        close_scene.SetActive(true);
        close_scene2.SetActive(true);
        is_playing = true;
    }
    public void Quit()
    {
        Application.Quit();
    }
    void StartPlaying()
    {
        if (is_playing)
        {
            StartTime += Time.deltaTime;
            if (StartTime >= EndTime)
            {
                SceneManager.LoadScene("Fortune");
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        StartPlaying();
        music_txt.text = Mathf.Round((slider_volume.value)).ToString() + "%";
        brightness_txt.text = Mathf.Round((slider_brightness.value * 100.0f/3.0f)).ToString() + "%";
    }
}
