using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    public Transform player; // Assign the player's transform in the inspector or via script.
    public Vector3 axisCorrection = Vector3.zero; // Adjust this if needed to fix initial orientation issues.

    void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("Player Transform is not assigned!");
            return;
        }

        // Calculate the direction to the player
        Vector3 directionToPlayer = player.position - transform.position;

        // Normalize the direction vector
        directionToPlayer.Normalize();

        // Create the rotation to face the player
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);

        // Apply the rotation with an optional correction for initial orientation
        transform.rotation = lookRotation * Quaternion.Euler(axisCorrection);
    }
}