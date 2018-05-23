using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour
{
    public string text;
    TextDrawer td;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            td.ShowText(text);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            td.ShowText("");
            //this.gameObject.SetActive(false);
        }
    }

    private void Awake()
    {
        td = GetComponent<TextDrawer>();
    }
}