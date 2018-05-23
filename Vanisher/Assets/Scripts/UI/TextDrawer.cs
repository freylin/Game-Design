using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDrawer : MonoBehaviour {
    private GameObject textGO;
    private Text text;

    private void Awake()
    {
        GameObject ui = GameObject.Find("UI");
        textGO = ui.transform.Find("MainCanvas").Find("TextGO").gameObject;
        if(textGO == null)
        {
            Debug.LogError("No text game object in main canvas!");
        }

        text = textGO.GetComponent<Text>();
        if(text == null)
        {
            Debug.LogError("no text component in textGO!");
        }
    }

    public void ShowText(string str)
    {
        text.enabled = true;
        text.text = str;
    }

    public void DisableText()
    {
        text.enabled = false;
    }
}
