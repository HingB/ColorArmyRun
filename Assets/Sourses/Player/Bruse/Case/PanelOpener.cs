using UnityEngine;

internal class PanelOpener
{
    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
        Time.timeScale = 1;
    }
}