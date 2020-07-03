using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpener : MonoBehaviour
{
    public GameObject panel;

    public void OnMouseEnter()
    {
        panel.SetActive(true);
    }
    public void OnMouseExit()
    {
        panel.SetActive(false);
    }
    public void Click()
    {
        panel.SetActive(!panel.activeSelf);
    }
}
