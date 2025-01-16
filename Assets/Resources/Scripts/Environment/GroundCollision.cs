using UnityEngine;

namespace Environment
{
    public class GroundCollision : MonoBehaviour
    {
        [SerializeField] private ParticleSystem dustParticles;
    
        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Player"))
            {
                return;
            }
        
            dustParticles.transform.position = other.GetContact(0).point;
            dustParticles.Play();
        }
    }
}