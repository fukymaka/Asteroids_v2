using System;
using UnityEngine;

namespace Source
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private Transform projectilePrefab;
        [SerializeField] private float projectileSpeed = 30f;
        
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Fire();
        }

        private void Fire()
        {
            var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            var projectileMovement = projectile.GetComponent<ProjectileMovement>();

            projectileMovement.from = transform.position;
            projectileMovement.to = transform.position - transform.up;
            projectileMovement.speed = projectileSpeed;
            projectileMovement.targets = TypesOfTarget.Asteroid | TypesOfTarget.Ufo;
        }
    }
}