using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    private int value = 1;

    // Update is called once per frame
    void Update()
    {
        Vector3 rotation = Vector3.up * 180 * Time.deltaTime;
        transform.Rotate(rotation, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Messenger<int>.Broadcast(GameEvent.PICKUP_HEALTH, value);
            Destroy(this.gameObject);
        }
    }
}
