using UnityEngine;

public class UIManagerMenu : MonoBehaviour

{
    public GameObject panelStage;
    public GameObject panelVehicle;
    public GameObject panelSetting;
    private void Start()
    {
        panelVehicle.SetActive(true);
    }

    public void OpenStage()
    {
        SoundManager.PlaySound("Button");
        panelVehicle.SetActive(false);
        panelStage.SetActive(true);
        panelSetting.SetActive(false);
    }

    public void OpenVehicle()
    {
        SoundManager.PlaySound("Button");
        panelVehicle.SetActive(true);
        panelStage.SetActive(false);
        panelSetting.SetActive(false);
    }
    public void OpenSetting()
    {
        SoundManager.PlaySound("Button");
        panelVehicle.SetActive(false);
        panelStage.SetActive(false);
        panelSetting.SetActive(true);
    }
}
