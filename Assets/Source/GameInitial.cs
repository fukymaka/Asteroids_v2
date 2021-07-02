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
            AsteroidEnemy.asteroidsCount = 0;
        }

        private void SetEnemySettings()
        {
            EnemySpawner.asteroidsSettings = asteroidsSettings;
            EnemySpawner.ufoSettings = ufoSettings;

            UfoEnemy.ProjectilePrefab = ufoProjectilePrefab;
            UfoEnemy.projectileSpeed = ufoProjectileSpeed;
        }
        
        private void SetPrefabs()
        {
            UfoEnemy.ProjectilePrefab = ufoProjectilePrefab;
            Explosion.prefab = explosionPrefab;
        }

    }
}
