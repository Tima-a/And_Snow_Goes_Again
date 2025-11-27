using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float X;
    public float minusSpeed;
    public int right_or_left_dash;
    public int dash_count;
    public static Spin instance;
    public float StartTime;
    public float EndTime;
    public GameObject FortuneDetect;
    public float stop;
    // Start is called before the first frame update
    void Start()
    {
        stop = 1;
        X = 0;
        instance = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (X <= 5.0f)
        {
            X += X * 0.35f;
        }
        if (X >= 5.0f && X <= 20.0f)
        {
            X += X * 0.025f;
        }
        if (X >= 20.0f)
        {
            X += X * 0.0048f;
        }
        transform.Rotate(0, 0, X);
    }
}
