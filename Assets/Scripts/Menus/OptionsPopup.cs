using UnityEngine;

public class OptionsPopup : BasePopup
{
    [SerializeField] private UIController uiController;
    [SerializeField] private SettingsPopup settingsPopup;
    override public void Open()
    {
        base.Open();
    }

    public void OnSettingsButton()
    {
        Debug.Log("settings clicked");
        base.Close();
        settingsPopup.Open();
    }

    public void OnExitGameButton()
    {
        Debug.Log("exit game");
        Application.Quit();
    }

    public void OnReturnToGameButton()
    {
        Debug.Log("return to game");
        base.Close();
    }
}
