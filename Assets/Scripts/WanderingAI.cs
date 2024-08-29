using UnityEngine;
public enum EnemyStates { alive, dead };

public class WanderingAI : MonoBehaviour
{
    private EnemyStates state;
    [SerializeField] private GameObject laserbeamPrefab;
    private GameObject laserbeam;
    public float fireRate = 2.0f;
    private float nextFire = 0.0f;

    private float enemySpeed = 1.75f;
    private float baseSpeed = 0.25f;
    float difficultySpeedDelta = 0.3f;

    private float obstacleRange = 5.0f;
    private float sphereRadius = 0.75f;

    // Start is called before the first frame update
    void Start()
    {   // enemy is initially alive
        state = EnemyStates.alive;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == EnemyStates.alive)
        {
            Vector3 movement = Vector3.forward * enemySpeed * Time.deltaTime;
            transform.Translate(movement);
            // generate Ray
            Ray ray = new Ray(transform.position, transform.forward);

            RaycastHit hit;
            if (Physics.SphereCast(ray, sphereRadius, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<PlayerCharacter>())
                {
                    if (laserbeam == null && Time.time > nextFire)
                    {
                        nextFire = Time.time + fireRate;
                        laserbeam = Instantiate(laserbeamPrefab) as GameObject;
                        laserbeam.transform.position = transform.TransformPoint(0, 1.5f, 1.5f);
                        laserbeam.transform.rotation = transform.rotation;
                    }
                }
                else if (hit.distance < obstacleRange)
                {
                    float turnAngle = Random.Range(-110, 110);
                    transform.Rotate(0, turnAngle, 0);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        // determine the range vector
        Vector3 rangeTest = transform.position + transform.forward * obstacleRange;
        // draw a line to show the range vector
        Debug.DrawLine(transform.position, rangeTest);
        // Draw a wire sphere at the point on the end of the range vector
        Gizmos.DrawWireSphere(rangeTest, sphereRadius);
    }

    public void ChangeState(EnemyStates state)
    {
        this.state = state;
    }

    public void SetDifficulty(int difficulty)
    {
        Debug.Log("WanderingAI.SetDifficulty(" + difficulty + ")");
        enemySpeed = baseSpeed + (difficulty * difficultySpeedDelta);
    }
}
