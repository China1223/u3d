using UnityEngine;
using Unity.Netcode; 

public class SnailController : NetworkBehaviour
{
    public float moveSpeed = 2f;
    public Transform player;
    
    public override void OnNetworkSpawn()
    {

        base.OnNetworkSpawn();
    }
    public override void OnNetworkDespawn()
    {
        // When non-authority instances finally despawn,
        // the explosion FX will begin playing.
 
        base.OnNetworkDespawn();
    }
    private void OnExplosionFxChanged(NetworkBehaviourReference previous, NetworkBehaviourReference current)
    {
        // If possible, get the ExplosionFx component
    }
    private void Start()
    {

    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        AdjustHeightToGround();

        if (distanceToPlayer <= 5f)
        {
            MoveTowardsPlayer();
        }

    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position);
        direction.y = 0;
        direction.Normalize();

        Vector3 targetDirection = new Vector3(direction.x, 0, direction.z);
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y+180, 0);

        transform.position += direction * moveSpeed * Time.deltaTime;
        AdjustHeightToGround();
    }

    private void AdjustHeightToGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position+5*Vector3.up+Vector3.right, Vector3.down, out hit, 10f))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }
    }
}