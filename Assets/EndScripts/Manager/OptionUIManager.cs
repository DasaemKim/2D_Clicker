using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionUIManager : MonoBehaviour
{
    public GameObject optionPanel;

    public bool isOpen = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleOption();
        }
    }

    public void ToggleOption()
    {
        isOpen = !isOpen;
        optionPanel.SetActive(isOpen);
        Time.timeScale = isOpen ? 0 : 1;
    }

    public void OpenOption()
    {
        isOpen = true;
        optionPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseOption()
    {
        isOpen = false;
        optionPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
