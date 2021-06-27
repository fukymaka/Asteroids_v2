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
            // var projectile = Instantiate(projectilePrefab, from, Quaternion.identity);
            var directionProjectile = (to - from).normalized;
            transform.Translate(directionProjectile * (speed * Time.deltaTime));

            // Debug.Log(target2.GetType());
        }

        // public void SetTarget<T>() where T : MonoBehaviour
        // {
        //     var component = target.GetComponent<T>();
        // }

        private void OnTriggerEnter2D(Collider2D target)
        {
            var hit = target.GetComponent<ILoveEnemy>();

            if (targets.HasFlag(hit.Types))
            {
                Destroy(target.gameObject);
                var position = target.transform.position;
                EnemySpawner.SpawnEnemy<AsteroidEnemy>(position);
                EnemySpawner.SpawnEnemy<AsteroidEnemy>(position);
                Destroy(gameObject);
            }
        }
    }
}