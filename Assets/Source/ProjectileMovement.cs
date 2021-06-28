using System;
using TMPro;
using UnityEngine;

namespace Source
{
    public class ProjectileMovement : MonoBehaviour 
    {
        public Vector2 from;
        public Vector2 to;
        public float speed;
        public TypesOfTarget targets;

        private void Update()
        {
            Fire();
        }

        private void Fire()
        {
            var directionProjectile = (from - to).normalized;
            transform.Translate(directionProjectile * (speed * Time.deltaTime));
        }

        private void OnTriggerEnter2D(Collider2D target)
        {
            var hit = target.GetComponent<ILoveEnemy>();

            if (targets.HasFlag(hit.Type))
            {
                switch (hit.Type)
                {
                    case TypesOfTarget.Asteroid:
                        var genAsteroid = (int) target.GetComponent<AsteroidEnemy>().Generation;
                        var position = target.transform.position;
                        EnemySpawner.SpawnAsteroids(position, genAsteroid + 1);
                        EnemySpawner.SpawnAsteroids(position, genAsteroid + 1);
                        DestroyTarget(target.gameObject);
                        break;
                    
                    case TypesOfTarget.Ufo:
                    case TypesOfTarget.Player:
                        DestroyTarget(target.gameObject);
                        break;
                }
            }
        }

        private void DestroyTarget(GameObject target)
        {
            Destroy(target);
            Destroy(gameObject);
        }
    }
}