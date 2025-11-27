using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Threading;

public class CamMenu : MonoBehaviour
{
    public float x, y, z;
    public GameObject obj1, obj2, obj3, obj4, obj5, rulestext;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        transform.eulerAngles += new Vector3(x, y, z) * Time.deltaTime * 30.0f;
    }
    public void Fortune()
    {
        SceneManager.LoadScene("Fortune");
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Rules()
    {
        obj1.SetActive(false);
        obj2.SetActive(false);
        obj3.SetActive(false);
        rulestext.SetActive(true);
        obj5.SetActive(false);
        obj4.SetActive(true);
    }
    public void ExitRules()
    {
        obj1.SetActive(true);
        obj5.SetActive(true);
        obj2.SetActive(true);
        obj3.SetActive(true);
        rulestext.SetActive(false);
        obj4.SetActive(false);
    }
}
