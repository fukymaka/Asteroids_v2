using UnityEngine;

namespace Source
{
    internal class Explosion : MonoBehaviour
    {
        private ParticleSystem _particle;
        public static Explosion prefab;
        
        private void Start()
        {
            _particle = GetComponent<ParticleSystem>();        
        }

        private void Update()
        {
            if (_particle.isStopped) 
                Destroy(gameObject);
        }
    }
}