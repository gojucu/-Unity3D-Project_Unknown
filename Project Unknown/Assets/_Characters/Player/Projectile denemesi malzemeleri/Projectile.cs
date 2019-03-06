using UnityEngine;
using RPG.Core;

namespace RPG.Characters
{
    public class Projectile : MonoBehaviour
    {


        [SerializeField] float projectileSpeed;
        [SerializeField] GameObject shooter; // so can inspected when paused

        const float DESTROY_DELAY = 0.01f;
        
        public void SetShooter(GameObject shooter)
        {
            this.shooter = shooter;
        }
      

        public float GetDefaultLaunchSpeed()
        {
            return projectileSpeed;
        }

        void OnCollisionEnter(Collision collision)
        {
            var layerCollidedWith = collision.gameObject.layer;

            if (shooter && layerCollidedWith != shooter.layer)
            {
                Destroy(gameObject, DESTROY_DELAY);
            }
            
            
        }
    }
}