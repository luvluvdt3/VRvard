using UnityEngine;

public class NPCController : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float moveSpeed; // Speed at which the NPC moves
    public float stopOffset; // Distance at which the NPC stops moving

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody2D component
    }

    void FixedUpdate()
    {
        if (player == null) return;
        if (Vector3.Distance(transform.position, player.position) < stopOffset)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }
        Vector3 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
        
        var rotation = Quaternion.LookRotation(direction);
        rotation.x = 0;
        rotation.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5);

    }
}