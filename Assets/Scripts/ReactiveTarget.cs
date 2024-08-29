using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    private bool isAlive = true;

    public void ReactToHit()
    {
        if (isAlive)
        {
            WanderingAI enemyAI = GetComponent<WanderingAI>();
            if (enemyAI != null)
            {
                enemyAI.ChangeState(EnemyStates.dead);
            }
            Animator enemyAnimator = GetComponent<Animator>();
            if (enemyAnimator != null)
            {
                enemyAnimator.SetTrigger("Die");
            }
            isAlive = false;
            Messenger.Broadcast(GameEvent.ENEMY_DEAD);
        }
    }
}
