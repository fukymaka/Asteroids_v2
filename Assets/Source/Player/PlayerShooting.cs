using Asteroids.Source;
using Source.ActorSupports;
using Source.Services;
using UnityEngine;

namespace Source.Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private Transform projectilePrefab;
        [SerializeField] private int projectileSpeed = 30;
        
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Shoot();
        }

        private void Shoot()
        {
            var position = transform.position;
            var projectile = Instantiate(projectilePrefab, position, Quaternion.identity);
            var projectileMovement = projectile.GetComponent<ProjectileMovement>();

            projectileMovement.from = position;
            projectileMovement.to =  transform.up + position;
            projectileMovement.speed = projectileSpeed;
            // projectileMovement.targets = PossibleCollisions.Asteroid | PossibleCollisions.Ufo;
            
            SoundsComponent.Sounds.PlayHeroShotSound();
        }
    }
}