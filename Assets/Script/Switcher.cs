using UnityEngine;

public class Switcher : MonoBehaviour
{
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _sniperPanel;

    public void SwitchPanel()
    {
        _mainPanel.SetActive(!_mainPanel.activeSelf);
        _sniperPanel.SetActive(!_sniperPanel.activeSelf);
    }
}
