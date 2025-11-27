using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public int inventory_count;
    public GameObject panel_anim;
    public int coins_count;
    public static InventoryManager instance;
    public bool book_picked_up;
    public bool coin_picked_up;
    public int book_index;
    public int coin_index;
    public TextMeshProUGUI coins;
    public GameObject coin_obj;
    public GameObject inventory_panel;
    public GameObject book_obj;
    public TextMeshProUGUI book_txt;
    public TextMeshProUGUI coin_ui_txt;
    public List<Vector2> pos_texts = new List<Vector2>();
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

    }
    void ArrangePosition()
    {
        coin_obj.GetComponent<RectTransform>().anchoredPosition = pos_texts[coin_index];
        book_obj.GetComponent<RectTransform>().anchoredPosition = pos_texts[book_index];
    }
    // Update is called once per frame
    void Update()
    {
        coin_ui_txt.text = coins_count.ToString();
        ArrangePosition();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inventory_panel.activeInHierarchy == true)
            {
                DiaryManager.instance.map.SetActive(true);
                DiaryManager.instance.money_canvas.SetActive(true);
                DiaryManager.instance.time.SetActive(true);
                DiaryManager.instance.timeline.SetActive(true);
                inventory_panel.SetActive(false);

            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventory_panel.activeInHierarchy == true)
            {
                DiaryManager.instance.map.SetActive(true);
                DiaryManager.instance.money_canvas.SetActive(true);
                DiaryManager.instance.time.SetActive(true);
                DiaryManager.instance.timeline.SetActive(true);
                inventory_panel.SetActive(false);
            }
            if (inventory_panel.activeInHierarchy == false)
            {
                DiaryManager.instance.map.SetActive(false);
                DiaryManager.instance.money_canvas.SetActive(false);
                DiaryManager.instance.time.SetActive(false);
                DiaryManager.instance.timeline.SetActive(false);
                inventory_panel.SetActive(true);
            }
        }
        if (inventory_count == 0)
        {
            panel_anim.SetActive(false);
        }
        if (coins_count == 0 && coin_picked_up == true)
        {
            coin_picked_up = false;
            inventory_count--;
            coins.text = "";
        }
        if (coin_picked_up == true)
        {
            //panel_anim.SetActive(true);
            coins.text = "Coin x" + coins_count.ToString();
        }
        if (book_picked_up == true)
        {
            //panel_anim.SetActive(true);
            book_txt.text = "Book";
        }
    }
}
