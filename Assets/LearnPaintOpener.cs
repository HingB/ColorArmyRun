using UnityEngine;

public class LearnPaintOpener : MonoBehaviour
{
    [SerializeField] private GameObject _learnPanel;
    [SerializeField] private GameObject _closePanel;
    public void OpenPanel() => new PanelOpener().OpenPanel(_learnPanel);
    public void ClosePanelText() => _closePanel.SetActive(false);
    public void ClosePanelAll() => _learnPanel.SetActive(false);
}
