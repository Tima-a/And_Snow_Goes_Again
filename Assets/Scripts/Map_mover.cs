using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_mover : MonoBehaviour
{
    public RectTransform rt;
    public GameObject hero;
    // Start is called before the first frame update
    void Start()
    {
        rt.eulerAngles = new Vector3(0.0f, 0.0f, 360.0f - hero.transform.eulerAngles.y);
        rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rt.anchoredPosition = new Vector2(hero.transform.position.x, hero.transform.position.z);
        rt.eulerAngles = new Vector3(0.0f, 0.0f, 270.0f-hero.transform.eulerAngles.y);
    }
}
