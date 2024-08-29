using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject iguanaPrefab;
    [SerializeField] private Transform iguanaSpawnPt;
    [SerializeField] private UIController ui;

    private Vector3 spawnPoint = new Vector3(0, 0, 5);
    private int numEnemies = 5;
    private int numIguanas = 10;
    private GameObject[] enemies;
    private GameObject[] iguanas;
    private int score = 0;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.ENEMY_DEAD, OnEnemyDead);
        Messenger<int>.AddListener(GameEvent.DIFFICULTY_CHANGED, OnDifficultyChanged);
        Messenger.AddListener(GameEvent.PLAYER_DEAD, OnPlayerDead);
        Messenger.AddListener(GameEvent.RESTART_GAME, OnRestartGame);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_DEAD, OnEnemyDead);
        Messenger<int>.RemoveListener(GameEvent.DIFFICULTY_CHANGED, OnDifficultyChanged);
        Messenger.RemoveListener(GameEvent.PLAYER_DEAD, OnPlayerDead);
        Messenger.RemoveListener(GameEvent.RESTART_GAME, OnRestartGame);
    }

    private void Start()
    {
        ui.UpdateScore(score);
        // allocate - instantiate array of enemies here
        enemies = new GameObject[numEnemies];
        iguanas = new GameObject[numIguanas];
    }

    // Update is called once per frame
    void Update()
    {
        // use a loop to iterate through enemy array
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] == null)
            {
                enemies[i] = Instantiate(enemyPrefab);
                enemies[i].transform.position = spawnPoint;
                float angle = Random.Range(0, 360);
                enemies[i].transform.Rotate(0, angle, 0);
                WanderingAI ai = enemies[i].GetComponent<WanderingAI>();
                ai.SetDifficulty(GetDifficulty());
            }
        }

        for (int i = 0; i < iguanas.Length; i++)
        {
            if (iguanas[i] == null)
            {
                iguanas[i] = Instantiate(iguanaPrefab);
                iguanas[i].transform.position = new Vector3(iguanaSpawnPt.position.x,
                    iguanaSpawnPt.position.y,
                    iguanaSpawnPt.position.z);
                float angle = Random.Range(0, 360);
                iguanas[i].transform.Rotate(0, angle, 0);

            }
        }
    }


    private void OnDifficultyChanged(int newDifficulty)
    {

        Debug.Log("Scene.OnDifficultyChanged(" + newDifficulty + ")");
        for (int i = 0; i < enemies.Length; i++)
        {
            WanderingAI ai = enemies[i].GetComponent<WanderingAI>();
            ai.SetDifficulty(newDifficulty);
        }
    }

    public int GetDifficulty()
    {
        return PlayerPrefs.GetInt("difficulty", 1);
    }

    private void OnEnemyDead()
    {
        score++;
        ui.UpdateScore(score);
    }

    private void OnPlayerDead()
    {
        ui.ShowGameOverPopup();
    }

    public void OnRestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
