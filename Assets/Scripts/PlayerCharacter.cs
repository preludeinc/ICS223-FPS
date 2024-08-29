using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    private int health;
    private int maxHealth = 5;
    private float health_remaining = 0.0f;
    [SerializeField] private Image healthBar;

    private void Awake()
    {
        Messenger<int>.AddListener(GameEvent.PICKUP_HEALTH, this.OnPickupHealth);
    }

    private void OnDestroy()
    {
        Messenger<int>.RemoveListener(GameEvent.PICKUP_HEALTH, this.OnPickupHealth);
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void Hit()
    {
        health -= 1;
        health_remaining = ((float) health) / maxHealth;
        Messenger<float>.Broadcast(GameEvent.HEALTH_CHANGED, health_remaining);

        if (health == 0)
        {
            Messenger.Broadcast(GameEvent.PLAYER_DEAD);
        }
    }

    public void OnPickupHealth(int healthAdded)
    {
        health += healthAdded;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        float healthPerecent = ((float)health / maxHealth);
        Messenger<float>.Broadcast(GameEvent.HEALTH_CHANGED, healthPerecent);
    }
}
