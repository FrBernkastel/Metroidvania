using UnityEngine;

public class Entity_Health : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected float maxHealth;
    [SerializeField] protected bool isDead;

    public void Awake()
    {
        health = maxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        if(isDead) return;

        ReduceHp(damage);
    }

    private void ReduceHp(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            health = 0;
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Entity died!");
    }
}
