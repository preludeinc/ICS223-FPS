using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SettingsPopup : BasePopup
{
    [SerializeField] private UIController uiController;
    [SerializeField] private OptionsPopup optionsPopup;
    [SerializeField] private TextMeshProUGUI difficultyLabel;
    [SerializeField] private Slider difficultySlider;

    override public void Open()
    {
        base.Open();
        difficultySlider.value = PlayerPrefs.GetInt("difficulty", 1);
        UpdatedDifficulty(difficultySlider.value);
    }

    public void OnOKButton()
    {
        base.Close();
        PlayerPrefs.SetInt("difficulty", (int)difficultySlider.value);
        Messenger<int>.Broadcast(GameEvent.DIFFICULTY_CHANGED, (int)difficultySlider.value);
        optionsPopup.Open();
    }

    public void OnCancelButton()
    {
        base.Close();
        optionsPopup.Open();
    }

    public void UpdatedDifficulty(float difficulty)
    {
        difficultyLabel.text = "Difficulty: " + ((int)difficulty).ToString();
    }

    public void OnDifficultyValueChanged(float difficulty)
    {
        UpdatedDifficulty(difficulty);
    }
}
