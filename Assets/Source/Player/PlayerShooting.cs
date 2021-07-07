using Asteroids.Source;
using Source.ActorSupports;
using Source.Services;
using UnityEngine;

namespace Source.Player
{
    public class PlayerShooting : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Shoot();
        }


        private void Shoot()
        {
            var playerActor = GetComponent<PlayerActor>();
            var projectilePrefab = playerActor.ProjectileActor;
            var position = transform.position;
            var projectile = Instantiate(projectilePrefab, position, Quaternion.identity);
            
            var projectileType = ActorType.PlayerProjectile;
            var possibleCollisions = PossibleCollisions.Asteroid | PossibleCollisions.Ufo;
            var destroyProcessor = playerActor.DestroyProcessor;
            var to =  transform.up + position;
            var speed = playerActor.ProjectileSpeed;
            
            projectile.InitSettings(projectileType, possibleCollisions, destroyProcessor);
            projectile.InitDirection(position, to, speed);

            SoundsComponent.Sounds.PlayHeroShotSound();
        }
    }
}