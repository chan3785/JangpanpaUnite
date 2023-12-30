using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelControl : MonoBehaviour
{
    [SerializeField] private GameObject skillPanel1, skillPanel2, skillPanel3, skillPanel4, skillPanel5, skillPanel6;
    private List<GameObject> skillPanels;

    // Start is called before the first frame update
    private void Awake()
    {
        skillPanels = new List<GameObject>();
    }
    public void RegisterPanels(GameObject panel)
    {
        if (panel == null)
        {
            Debug.LogError("Tried to toggle a null window.");
            return;
        }
        if (!skillPanels.Contains(panel))
        {
            skillPanels.Add(panel);
        }
    }

    public void ToggleWindow(GameObject panel)
    {

        bool isPanelActive = panel.activeSelf;

        // 먼저 모든 창을 닫습니다.
        foreach (var pan in skillPanels)
        {
            if (pan != panel)
            {
                pan.SetActive(false);
            }

        }

        // 현재 창이 닫혀 있다면 엽니다.
        if (!isPanelActive)
        {
            panel.SetActive(true);
        }
    }

}
