using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source
{
    public class GameInitial : MonoBehaviour
    {
        
        [SerializeField] private EnemySettings asteroidsSettings;
        [SerializeField] private EnemySettings ufoSettings;

        [SerializeField] private GameObject ufoProjectilePrefab;
        [SerializeField] private int ufoProjectileSpeed = 3;
        [SerializeField] private Explosion explosionPrefab;

        
        private void Awake()
        {
            SetEnemySettings();
            SetPrefabs();
        }

        private void SetEnemySettings()
        {
            EnemySpawner.AsteroidsSettings = asteroidsSettings;
            EnemySpawner.UfoSettings = ufoSettings;

            UfoEnemy.ProjectilePrefab = ufoProjectilePrefab;
            UfoEnemy.projectileSpeed = ufoProjectileSpeed;
        }
        
        private void SetPrefabs()
        {
            UfoEnemy.ProjectilePrefab = ufoProjectilePrefab;
            Explosion.Prefab = explosionPrefab;
        }

    }
}
