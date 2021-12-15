using UnityEngine;

public class Switcher : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Camera _weaponCamera;
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _sniperPanel;

    public void SwitchAll()
    {
        //SwitchCamera();
        SwitchPanel();
    }

    private void SwitchCamera()
    {
        _mainCamera.enabled = !_mainCamera.enabled;
        _weaponCamera.enabled = !_weaponCamera.enabled;
    }
    private void SwitchPanel()
    {
        _mainPanel.SetActive(!_mainPanel.activeSelf);
        _sniperPanel.SetActive(!_sniperPanel.activeSelf);
    }
}
