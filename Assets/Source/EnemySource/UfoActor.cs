using System.Collections;
using Asteroids.Source;
using Source.ActorSupports;
using Source.Interfaces;
using Source.Player;
using Source.Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.EnemySource
{
    public class UfoActor : MonoBehaviour, IActor, ICollision
    {
        public ActorType ActorType { get; } = ActorType.Ufo;
        public PossibleCollisions PossibleCollisions { get; } = PossibleCollisions.Asteroid | PossibleCollisions.Player;
        public UfoType UfoType { get; set; }

        private ProjectileMovement _projectilePrefab;
        private int _projectileSpeed;

        public void SetProjectilePrefab(ProjectileMovement projectile)
        {
            _projectilePrefab = projectile;
        }

        public void SetProjectileSpeed(int projectileSpeed)
        {
            _projectileSpeed = projectileSpeed;
        }

        public void Move(float maxSpeed, float minSpeed)
        {
            StartCoroutine(MoveCoroutine(maxSpeed, minSpeed));
            SoundsComponent.Sounds.PlayUfoComingSound();
        }

        private IEnumerator MoveCoroutine(float maxSpeed, float minSpeed)
        {
            var nextPoint = GetNextPoint();

            while (Vector3.Distance(transform.position, nextPoint) > 1f)
            {
                var speed = Random.Range(minSpeed, maxSpeed);
                var recalculedSpeed = speed / 100;

                transform.position = Vector3.Lerp(transform.position, nextPoint, Time.deltaTime * recalculedSpeed);
                yield return null;
            }
            
            Shoot();
            StartCoroutine(MoveCoroutine(maxSpeed, minSpeed));
        }

        private Vector3 GetNextPoint()
        {
            var camHeight = BoundsControl.BoundHeight;
            var camWidth = BoundsControl.BoundWidth;
            
            var startPosX = Random.Range(-camWidth, camWidth);
            var startPosY = Random.Range(-camHeight, camHeight);

            var nextPoint = new Vector3(startPosX, startPosY, 0);
            
            return nextPoint;
        }

        private void Shoot()
        {
            var ufoPos = transform.position;
            var projectile = Instantiate(_projectilePrefab, ufoPos, Quaternion.identity);
            
            projectile.from = ufoPos;
            projectile.to = PlayerMovement.CurrentPlayerPosition;
            projectile.speed = _projectileSpeed;
            // projectile.targets = PossibleCollisions.Player | PossibleCollisions.Asteroid;
            
            SoundsComponent.Sounds.PlayUfoShotSound();
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            // this.OnCollision(collision, PossibleCollisions);
        }

        
    }
}