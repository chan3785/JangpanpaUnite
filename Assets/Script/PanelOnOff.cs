using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOnOff : MonoBehaviour
{
    private PanelControl manager;

    private void Start()
    {
        manager = FindObjectOfType<PanelControl>();
        manager.RegisterPanels(this.gameObject);
    }

    public void OnOpenButtonClicked()
    {
        manager.ToggleWindow(this.gameObject);
    }
}
