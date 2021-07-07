using UnityEngine;

namespace Source.ActorSupports
{
    public class Explosion : MonoBehaviour
    {
        private ParticleSystem _particle;
        public static Explosion Prefab;
        
        private void Start()
        {
            _particle = GetComponent<ParticleSystem>();        
        }

        private void Update()
        {
            if (_particle.isStopped)
            {
                Destroy(gameObject);
            }
        }
    }
}