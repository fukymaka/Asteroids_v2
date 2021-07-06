using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids.Source
{
    public class UfoEnemy : MonoBehaviour, IMovableObject
    {
        public TypeOfTarget PossibleCollisions { get; set; } = TypeOfTarget.Asteroid | TypeOfTarget.Player;
        public TypeOfTarget Type { get; set; } = TypeOfTarget.Ufo;
        public UfoType UfoType { get; set; }

        public static GameObject ProjectilePrefab { get; set; }
        public static int projectileSpeed { get; set; }

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
            var projectile = Instantiate(ProjectilePrefab, ufoPos, Quaternion.identity);
            var projectileMovement = projectile.GetComponent<ProjectileMovement>();
            
            projectileMovement.from = ufoPos;
            projectileMovement.to = PlayerMovement.CurrentPlayerPosition;
            projectileMovement.speed = projectileSpeed;
            projectileMovement.targets = TypeOfTarget.Player | TypeOfTarget.Asteroid;
            
            SoundsComponent.Sounds.PlayUfoShotSound();
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            this.OnCollision(collision, PossibleCollisions);
        }
    }
}