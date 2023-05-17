using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HostDataManager :MonoBehaviour
{
    public InputField inputHost;
    public Button inputBtn;
    public Button resetBtn;

    public void Start()
    {
        if (PlayerPrefs.HasKey("host"))
        {
            inputBtn.gameObject.SetActive(false);
            inputHost.gameObject.SetActive(false);
            inputHost.text = PlayerPrefs.GetString("host");
            StudentAdmin.Instance._Host = inputHost.text;
        }
        inputBtn.onClick.AddListener(() =>
        {
            inputBtn.gameObject.SetActive(false);
            inputHost.gameObject.SetActive(false);
            transform.GetChild(0).gameObject.SetActive(false);
            SetString("host", inputHost.text);
            StudentAdmin.Instance._Host = inputHost.text;
            StudentAdmin.Instance.Init();
        });
        resetBtn.onClick.AddListener(() =>
        {
            inputBtn.gameObject.SetActive(true);
            inputHost.gameObject.SetActive(true);
            PlayerPrefs.DeleteKey("host");
        });
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.R)&&Input.GetKeyDown(KeyCode.E))
        {
            transform.GetChild(0).gameObject.SetActive(!transform.GetChild(0).gameObject.activeSelf);
        }
    }

    public static void SetString(string key,string value)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }
    public static string GetString(string key)
    {
        return PlayerPrefs.GetString(key);
    }
}
