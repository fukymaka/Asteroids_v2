using Source.ActorSupports;
using Source.EnemySource;
using UnityEngine;

namespace Source.Services
{
    public class GameInitial : MonoBehaviour
    {
        [SerializeField] private Explosion explosionPrefab;

        
        private void Awake()
        {
            SetPrefabs();
            AsteroidActor.AsteroidsCount = 0;
        }

        private void SetPrefabs()
        {
            Explosion.Prefab = explosionPrefab;
        }

    }
}
