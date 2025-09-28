using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    public Collider2D[] targetColliders;

    [Header("Target detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius;
    [SerializeField] private LayerMask whatIsTarget;

    public void PerformAttack()
    {
        Collider2D[] targetColliders = GetDetectedColliders();

        foreach(var collider in targetColliders)
        {
            Debug.Log("Attacking:" + collider.name);
        }
    }

    private Collider2D[] GetDetectedColliders()
    {
        return Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, whatIsTarget);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }

}
