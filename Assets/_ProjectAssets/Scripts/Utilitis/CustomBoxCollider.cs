using UnityEngine;
using UnityEngine.UIElements;

public class CustomBoxCollider : MonoBehaviour
{

    public LayerMask targetMask;

    public void NotticeEenemies(Transform transform)
    {
        Collider[] rangeChecks =
            Physics.OverlapBox(this.transform.position, this.transform.localScale/2,Quaternion.identity , targetMask, QueryTriggerInteraction.Collide);
        foreach (var enemy in rangeChecks)
        {
            Transform newTransform = new GameObject().transform;
            newTransform.position = new Vector3(transform.position.x + Random.Range(-2,2f), transform.position.y , transform.position.z+Random.Range(-2,2f));
            EnemyMovement enemyMovementComponent = enemy.GetComponent<EnemyMovement>();
            enemyMovementComponent._travelPoints.Add(newTransform);
            enemyMovementComponent.Travel();
        }
    }
    
    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent red cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
    
    public void KillAllEnemies()
    {
        Collider[] rangeChecks =
            Physics.OverlapBox(transform.position, transform.localScale/2,Quaternion.identity, targetMask, QueryTriggerInteraction.Collide);
        foreach (var enemy in rangeChecks)
        {
            enemy.GetComponent<EnemyHealth>().TakeDmg(1000);
        }
    }    
}
