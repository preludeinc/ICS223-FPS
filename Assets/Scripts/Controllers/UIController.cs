using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreValue;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image crossHair;
    [SerializeField] private OptionsPopup optionsPopup;
    [SerializeField] private SettingsPopup settingsPopup;
    [SerializeField] private Slider difficultySlider;
    [SerializeField] private GameOverPopup gameOverPopup;

    public int popupsActive = 0;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.POPUP_OPENED, OnPopupOpened);
        Messenger.AddListener(GameEvent.POPUP_CLOSED, OnPopupClosed);
        Messenger<float>.AddListener(GameEvent.HEALTH_CHANGED, OnHealthChanged);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.POPUP_OPENED, OnPopupOpened);
        Messenger.RemoveListener(GameEvent.POPUP_CLOSED, OnPopupClosed);
        Messenger<float>.RemoveListener(GameEvent.HEALTH_CHANGED, OnHealthChanged);
    }

    // Start is called before the first frame update
    void Start()
    {
        healthBar.fillAmount = 1;
        healthBar.color = Color.green;
        UpdateHealth(1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !optionsPopup.IsActive()
            && popupsActive == 0)
        {
            optionsPopup.Open();
        }
    }

    // update score display
    public void UpdateScore(int newScore)
    {
        scoreValue.text = newScore.ToString();
    }

    public void SetGameActive(bool active)
    {
        if (active)
        {
            Messenger.Broadcast(GameEvent.GAME_ACTIVE);
            Time.timeScale = 1;                       // unpause the game
            Cursor.lockState = CursorLockMode.Locked; // lock the cursor at center
            Cursor.visible = false;                   // hide cursor
            crossHair.gameObject.SetActive(true);     // show the crosshair
        }
        else
        {
            Messenger.Broadcast(GameEvent.GAME_INACTIVE);
            Time.timeScale = 0;                       // pause the game
            Cursor.lockState = CursorLockMode.None;   // let cursor move freely
            Cursor.visible = true;                    // show the cursor
            crossHair.gameObject.SetActive(false);    // turn off the crosshair
        }
    }

    private void OnPopupOpened()
    {
        if (popupsActive == 0)
        {
            SetGameActive(false);
        }
        popupsActive++;

    }

    private void OnPopupClosed()
    {
        popupsActive--;
        if (popupsActive == 0)
        {
            SetGameActive(true);
        }
    }

    public void OnHealthChanged(float health_remaining)
    {
        Debug.Log("Health remaining " + health_remaining * 100 + " %");
        UpdateHealth(health_remaining);
    }

    private void UpdateHealth(float healthPercentage)
    {
        healthBar.fillAmount = healthPercentage;
        healthBar.color = Color.Lerp(Color.red, Color.green, healthPercentage);
    }

    public void ShowGameOverPopup()
    {
        gameOverPopup.Open();
    }
}
