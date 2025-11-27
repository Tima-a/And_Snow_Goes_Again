using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin_map : MonoBehaviour
{
    public List<GameObject> coin_objs = new List<GameObject>();
    public List<GameObject> rt_coin_objs = new List<GameObject>();
    public GameObject atm;
    public GameObject atm_rt;
    public GameObject wheel;
    public GameObject battery;
    public GameObject battery_rt;
    public GameObject teddy_map;
    public GameObject teddy;
    public GameObject wheel_point;
    public static coin_map instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        for (int i = 0; i < 4; i++)
        {
            if (i == 0)
            {
                rt_coin_objs[0] = GameObject.Find("coin");
            }
            else
            {
                rt_coin_objs[i] = GameObject.Find("coin (" + i.ToString() + ")");
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            rt_coin_objs[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(coin_objs[i].transform.position.x, coin_objs[i].transform.position.z);
        }
        atm_rt.GetComponent<RectTransform>().anchoredPosition = new Vector2(atm.transform.position.x, atm.transform.position.z);
        wheel_point.GetComponent<RectTransform>().anchoredPosition = new Vector2(wheel.transform.position.x, wheel.transform.position.z);
        battery_rt.GetComponent<RectTransform>().anchoredPosition = new Vector2(battery.transform.position.x, battery.transform.position.z);
        teddy_map.GetComponent<RectTransform>().anchoredPosition = new Vector2(teddy.transform.position.x, teddy.transform.position.z);
    }
}
