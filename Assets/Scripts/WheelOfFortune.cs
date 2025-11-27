using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class WheelOfFortune : MonoBehaviour
{
    public GameObject obj;
    public string scriptname;
    public string scriptname2;
    public string scriptname3;
    public GameObject spinaudio;
    public GameObject fortunedetector;
    public GameObject pausepanel;
    public float canspinagain;
    public int pressed;
    public int stopped;
    public float playernumber;
    public float stop;
    public float StartTime;
    public GameObject Moneyprefab;
    public float EndTime;
    public bool moneybool;
    public float stopit;
    public bool spinbool;
    public bool instantiatedmoney;
    public float stopset;
    public float numstopdash;
    public List<GameObject> TimelineTexts = new List<GameObject>();
    public List<Vector3> moneypos = new List<Vector3>();
    public float StartTimeMoney;
    public int currentmoney;
    public List<Vector3> wheel_spawn_positions = new List<Vector3>();
    public bool timelinechanged = false;
    public Transform MyCamera;
    public bool initial_timeline = true;
    public static WheelOfFortune instance;
    public int money_count_to_spin;
    public int moneyquantity;
    public GameObject need_to_spin_text;
    public float StartNeedTime;
    public float EndNeedTime;
    public GameObject anim_need_obj;
    public bool activate_need_text;
    [Header("Environment Objects")]
    public List<GameObject> objects_environment_list = new List<GameObject>();
    public GameObject pressE;
    public GameObject map;
    public float EndTimePressE;
    public float StartTimePressE;
    public GameObject pressJ;
    public GameObject player;
    public string fps;
    public bool opened_door;
    public float StartDoorTime;
    public bool can_pause;
    public float EndDoorTime;
    public float TransformStartTime;
    public float TransformEndTime;
    public float x;
    public float y;
    public float z;
    public bool transform_player;
    public List<Vector3> coin_converter_poses = new List<Vector3>();
    public List<Vector3> player_spawn_positions = new List<Vector3>();
    public GameObject pressF;
    public float StartTimePressF;
    public TextMeshProUGUI money_text;
    public float EndTimePressF;
    public bool money_transfer;
    public float MoneyTransferStartTime;
    public GameObject text_processing;
    public float MoneyTransferEndTime;
    public int current_balance;
    public GameObject atm;
    public GameObject canvas_game_group;
    public int pfs;
    public int round;
    public int global_time_minutes;
    public int global_time_seconds;
    public float StartCountTime;
    public float EndCountTime;
    public TextMeshProUGUI text_required;
    public int mmn;
    public GameObject wof;
    public AudioSource audio_coin;
    public AudioClip audio_clip_coin;
    public List<AudioSource> audio_atm = new List<AudioSource>();
    public List<AudioClip> audio_clip_atm = new List<AudioClip>();
    public GameObject settings_menu;
    public GameObject hero;
    public float StartTimeAtmAudio;
    public float EndTimeAtmAudio;
    public bool you_won;
    // Start is called before the first frame update
    void Start()
    {
        int b = UnityEngine.Random.Range(0, 6);
        if (b != 2 && b != 5)
        {
            wof.transform.position = wheel_spawn_positions[b];
            wof.transform.rotation = Quaternion.EulerAngles(0.0f, 0.0f, 0.0f);
        }
        else
        {
            wof.transform.position = wheel_spawn_positions[b];
            wof.transform.rotation = Quaternion.EulerAngles(0.0f, 90.0f, 0.0f);
        }
        pfs = 60000;
        atm.transform.position = coin_converter_poses[UnityEngine.Random.Range(0,6)];
        text_processing.GetComponent<MeshRenderer>().material.color = Color.green;
        current_balance = 80000;
        mmn = 65001;
        money_text.text = "CB: " + current_balance.ToString() + "$ / MMN: " + mmn.ToString() + "$ / PFS: " + pfs.ToString() + "$";
        for (int j = 0; j < 4; j++)
        {
            int a = 0;
            if (j == 3)
            {
                a = 1;
            }
            GameObject obj = Instantiate(Moneyprefab, moneypos[UnityEngine.Random.Range(9 * j, ((j + 1) * 9)-a)], Quaternion.Euler(new Vector3(-90.0f, 0.0f, 90.0f)));
            obj.name = "Coin " + j.ToString();
            coin_map.instance.coin_objs[j] = obj;
        }
        pressed = 0;
        stop = 1;
        stopset = 1;
        //if (PlayerPrefs.HasKey("PlayerX") == true)
        //{
        //    stopit = 1;
        //}
        EndTime = 0.4f;
        playernumber = 0;
        instance = this;
        //(obj.GetComponent(scriptname) as MonoBehaviour).enabled = false;
    }

    public void UnPause()
    {
        WheelOfFortune.instance.can_pause = true;
        Time.timeScale = 1.0f;
        pausepanel.SetActive(false);
        canvas_game_group.SetActive(true);
        (player.GetComponent("FirstPersonController") as MonoBehaviour).enabled = true;

    }
    public void Restart()
    {
        SceneManager.LoadScene("Fortune");
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    void CheckMoneyQuantity()
    {
        int b = 0;
        for (int i = 0; i < 16; i++)
        {
            if (GameObject.Find("10000dol(Clone)"+i.ToString()))
            {
                b += 1;
            }
        }
        moneyquantity = b;
    }
    void NeedtextActivate()
    {
        if (activate_need_text == true)
        {
            StartNeedTime += Time.deltaTime;
            if (StartNeedTime >= EndNeedTime)
            {
                need_to_spin_text.SetActive(false);
                StartNeedTime = 0.0f;
                activate_need_text = false;
            }
        }
    }
    void TransformPlayer(float x, float y, float z)
    {
        if (transform_player == true)
        {
            TransformStartTime += Time.deltaTime;
            UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.horizontal_ = "k";
            UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.vertical_ = "k2";
            //(player.GetComponent("FirstPersonController") as MonoBehaviour).enabled = false;
            player.transform.position = new Vector3(x, y, z);
            if (TransformStartTime >= TransformEndTime)
            {
                //(player.GetComponent("FirstPersonController") as MonoBehaviour).enabled = true;
                UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.horizontal_ = "Horizontal";
                UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.vertical_ = "Vertical";
                transform_player = false;
                TransformStartTime = 0.0f;
            }
        }
    }
    void TransferMoney()
    {
        if (money_transfer == true)
        {
            MoneyTransferStartTime += Time.deltaTime;
            StartTimeAtmAudio += Time.deltaTime;
            if (MoneyTransferStartTime >= MoneyTransferEndTime)
            {
                MoneyTransferStartTime = 0.0f;
                InventoryManager.instance.coins_count--;
                current_balance += 30000;
                money_text.text = "CB: " + current_balance.ToString() + "$ / MMN: " + mmn.ToString() + "$ / PFS: " + pfs.ToString() + "$";
                text_processing.SetActive(false);
                money_transfer = false;
            }
        }
    }
    void Update()
    {
        if (!Time_Brishen.instance.fall)
        {
            StartCountTime += Time.deltaTime;
            global_time_seconds = (int)StartCountTime;
            if (StartCountTime >= EndCountTime)
            {
                StartCountTime = 0.0f;
                global_time_minutes++;
            }
        }
        if (settings_menu.activeInHierarchy == true)
        {
            Cursor.lockState = CursorLockMode.None;
            (player.GetComponent("FirstPersonController") as MonoBehaviour).enabled = false;
            Cursor.visible = true;
            Time.timeScale = 0.0000000000000000000000000001f;
        }
        if (TimelineDetecting.instance.is_changing_timeline_now == true)
        {
            canvas_game_group.SetActive(false);
        }
        TransferMoney();
        TransformPlayer(x, y, z);
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (map.activeInHierarchy == false)
            {
                map.SetActive(true);
            }
            else if (map.activeInHierarchy == true)
            {
                map.SetActive(false);
            }
        }
        if (pressE.activeInHierarchy == true)
        {
            StartTimePressE += Time.deltaTime;
            if (StartTimePressE >= EndTimePressE)
            {
                StartTime = 0.0f;
                pressE.SetActive(false);
            }
        }
        if (pressF.activeInHierarchy == true)
        {
            StartTimePressF += Time.deltaTime;
            if (StartTimePressF >= EndTimePressF)
            {
                StartTime = 0.0f;
                pressF.SetActive(false);
            }
        }
        CheckMoneyQuantity();
        NeedtextActivate();
        if (timelinechanged == true && moneyquantity != 16)
        {
            //int randomizingmethod = 0;
            //randomizingmethod = UnityEngine.Random.Range(0, 4);
            //for (int j = 0; j < 16 - moneyquantity; j++)
            //{
            //    if (randomizingmethod == 1)
            //    {
            //        Instantiate(Moneyprefab, moneypos[j], Quaternion.Euler(new Vector3(90.0f, 0.0f, 0.0f)));
            //    }
            //    if (randomizingmethod == 2)
            //    {
            //        Instantiate(Moneyprefab, moneypos[16-j], Quaternion.Euler(new Vector3(90.0f, 0.0f, 0.0f)));
            //    }
            //    if (randomizingmethod == 3)
            //    {
            //        if (j+10 < 16 && j+10 >= 10)
            //        {
            //            Instantiate(Moneyprefab, moneypos[j + 10], Quaternion.Euler(new Vector3(90.0f, 0.0f, 0.0f)));
            //        }
            //        else if (j+10 >= 16 && j < 16)
            //        {
            //            Instantiate(Moneyprefab, moneypos[Mathf.Abs(6-j)], Quaternion.Euler(new Vector3(90.0f, 0.0f, 0.0f)));
            //        }
            //    }
            //}
        }
        //if (instantiatedmoney == true)
        //{
        //    for (int k = 0; k < 12; k++)
        //    {
        //        finishedmoneypos[k] = moneypos[UnityEngine.Random.Range(0, 24)];
        //    }
        //    for (int x = 0; x < 12; x++)
        //    {
        //        for (int i = 0; i < 12; i++)
        //        {
        //            if (finishedmoneypos[x] == finishedmoneypos[i] && x != i)
        //            {
        //                finishedmoneypos[i] = moneypos[UnityEngine.Random.Range(0, 24)];
        //            }
        //        }
        //    }
        //    for (int j = 0; j < 12; j++)
        //    {
        //        Instantiate(Moneyprefab, finishedmoneypos[j], Quaternion.Euler(new Vector3(90.0f, 0.0f, 0.0f)));
        //    }
        //    instantiatedmoney = true;
        //}
        if (pausepanel.activeInHierarchy == false && !Time_Brishen.instance.fall && can_pause)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                settings_menu.SetActive(false);
                pausepanel.SetActive(true);
                WheelOfFortune.instance.can_pause = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                canvas_game_group.SetActive(false);
                (player.GetComponent("FirstPersonController") as MonoBehaviour).enabled = false;
            }
        }
        if (pausepanel.activeInHierarchy == true)
        {
            Time.timeScale = 0.0000000000000000000000000001f;
        }
        Ray ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo1;
        if (Physics.Raycast(ray1, out hitInfo1))
        {
            if (hitInfo1.distance <= 5.774f)
            {
                if (hitInfo1.collider.gameObject.tag == "atm")
                {
                    pressF.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        if (InventoryManager.instance.coins_count > 0 && InventoryManager.instance.coin_picked_up == true)
                        {
                            int i = UnityEngine.Random.Range(0, 3);
                            audio_atm[i].PlayOneShot(audio_clip_atm[i]);
                            audio_coin.PlayOneShot(audio_clip_coin);
                            money_transfer = true;
                            text_processing.SetActive(true);
                        }
                    }
                }
            }
        }

        Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo2;
        if (Physics.Raycast(ray2, out hitInfo2))
        {
            if (hitInfo2.distance <= 10.774f)
            {
                if (hitInfo2.collider.gameObject.tag == "Battery")
                {
                    pressE.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        hitInfo2.transform.gameObject.SetActive(false);
                        coin_map.instance.battery_rt.SetActive(false);
                        if (SettingsMenu.difficulty == 0)
                        {
                            Flash_light.instance.battery_level += 3;
                        }
                        if (SettingsMenu.difficulty == 1)
                        {
                            Flash_light.instance.battery_level += 2;
                        }
                        if (SettingsMenu.difficulty == 2)
                        {
                            Flash_light.instance.battery_level++;
                        }
                    }
                }
                if (hitInfo2.collider.gameObject.tag == "Teddybear")
                {
                    pressE.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        current_balance += 50000;
                        money_text.text = "CB: " + current_balance.ToString() + "$ / MMN: " + mmn.ToString() + "$ / PFS: " + pfs.ToString() + "$";
                        hitInfo2.transform.gameObject.SetActive(false);
                        coin_map.instance.teddy_map.SetActive(false);
                    }
                }
                if (hitInfo2.collider.gameObject.tag == "Coins")
                {
                    pressE.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        hitInfo2.transform.gameObject.SetActive(false);
                        for (int i = 0; i < 4; i++)
                        {
                            if (coin_map.instance.rt_coin_objs[i].GetComponent<RectTransform>().anchoredPosition.x == hitInfo2.transform.gameObject.transform.position.x && coin_map.instance.rt_coin_objs[i].GetComponent<RectTransform>().anchoredPosition.y == hitInfo2.transform.gameObject.transform.position.z)
                            {
                                coin_map.instance.rt_coin_objs[i].SetActive(false);
                            }
                        }
                        InventoryManager.instance.coin_picked_up = true;
                        if (InventoryManager.instance.coins_count == 0)
                        {
                            InventoryManager.instance.inventory_count++;
                            InventoryManager.instance.coins_count = 1;
                            if (InventoryManager.instance.book_picked_up)
                            {
                                InventoryManager.instance.coin_index = 1;
                            }
                            else
                            {
                                InventoryManager.instance.coin_index = 0;
                            }
                        }
                        else
                        {
                            InventoryManager.instance.coins_count++;
                        }
                    }
                }
                if (hitInfo2.collider.gameObject.tag == "Book")
                {
                    pressE.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        InventoryManager.instance.book_picked_up = true;
                        Destroy(hitInfo2.transform.gameObject);
                        pressJ.SetActive(true);
                        DiaryManager.instance.journal_count += 1;
                        InventoryManager.instance.inventory_count++;
                        DiaryManager.instance.jester_chapter1_found = true;
                        if (InventoryManager.instance.coin_picked_up)
                        {
                            InventoryManager.instance.book_index = 1;
                        }
                        else
                        {
                            InventoryManager.instance.book_index = 0;
                        }
                    }
                }
            }
        }
        if (Spin.instance.X <= 0.02f)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    if (hitInfo.distance <= 10.774f)
                    {
                        if (hitInfo.collider.gameObject.tag == "Button")
                        {
                            if (currentmoney >= money_count_to_spin && current_balance >= pfs && current_balance - pfs >= mmn)
                            {
                                bool stopbool = false;
                                if (stopbool == false)
                                {
                                    if (InventoryManager.instance.coins_count == 8 && InventoryManager.instance.coin_picked_up == true)
                                    {
                                        you_won = true;
                                    }
                                    can_pause = false;
                                    current_balance -= 60000 + TimelineDetecting.instance.timeline_changed_count * 10000;
                                    money_text.text = "CB: " + current_balance.ToString() + "$ / MMN: " + mmn.ToString() + "$ / PFS: " + pfs.ToString() + "$";
                                    TimelineDetecting.instance.colliders.SetActive(true);
                                    UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.flash_back.SetActive(false);
                                    pressed = 1;
                                    stopbool = true;
                                    stopped = 1;
                                    Time_Brishen.instance.Brishen_will_come_time = 10000;
                                    round++;
                                    Spin.instance.X = 0.01f;
                                    Spin.instance.minusSpeed = 0.0f;
                                    spinbool = true;
                                    (obj.GetComponent(scriptname) as MonoBehaviour).enabled = true;
                                    moneybool = true;
                                    spinaudio.SetActive(true);
                                    instantiatedmoney = false;
                                }

                            }
                            else if (current_balance <= pfs)
                            {
                                text_required.text = "Required " + pfs.ToString() + "$ to spin the wheel of fortune.";
                                activate_need_text = true;
                                if (activate_need_text == true)
                                {
                                    NeedtextActivate();
                                }
                                need_to_spin_text.SetActive(true);

                            }
                            else if (current_balance - pfs <= mmn)
                            {
                                text_required.text = "You cannot spin the wheel of fortune because in the next round current balance will be less than MMN(" + mmn.ToString() + "$)";
                                activate_need_text = true;
                                if (activate_need_text == true)
                                {
                                    NeedtextActivate();
                                }
                                need_to_spin_text.SetActive(true);

                            }
                        }
                    }
                }
            }
        }
    }
}
