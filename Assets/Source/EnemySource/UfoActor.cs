using System.Collections;
using Source.ActorSupports;
using Source.Interfaces;
using Source.Player;
using Source.Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.EnemySource
{
    public class UfoActor : MonoBehaviour, IActor
    {
        public ActorType ActorType { get; } = ActorType.Ufo;
        public PossibleCollisions PossibleCollisions { get; } = PossibleCollisions.Asteroid | PossibleCollisions.Player | PossibleCollisions.PlayerProjectile;
        public Vector3 CurrentPositon { get; private set; }
        public UfoType UfoType { get; set; }
        public DestroyProcessor DestroyProcessor { get; set; }
        private ProjectileActor _projectilePrefab;
        private float _projectileSpeed;
        
        private void Update()
        {
            CurrentPositon = transform.position;
        }

        public void SetProjectilePrefab(ProjectileActor projectile)
        {
            _projectilePrefab = projectile;
        }

        public void SetProjectileSpeed(float projectileSpeed)
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

            var projectileType = ActorType.UfoProjectile;
            var possibleCollisions = PossibleCollisions.Asteroid | PossibleCollisions.Player;
            var from = ufoPos;
            var to = PlayerActor.CurrentPlayerPosition;
            var speed = _projectileSpeed;
            
            projectile.InitSettings(projectileType, possibleCollisions, DestroyProcessor);
            projectile.InitDirection(from, to, speed);
            
            SoundsComponent.Sounds.PlayUfoShotSound();
        }

        public void DestroyThisActor()
        {
            Destroy(gameObject);
        }
        
        public void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.TryGetComponent(out IActor injured)) 
                DestroyProcessor.CheckCollision(this, injured);
        }
    }
}