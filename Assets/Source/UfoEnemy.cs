using UnityEngine;

namespace Source
{
    public class UfoEnemy : MonoBehaviour, ILoveEnemy
    {
        public TypesOfTarget Type { get; set; }
        
        private void Start()
        {
            Type = TypesOfTarget.Ufo;
        }
        
        public void Move(float maxSpeed, float minSpeed)
        {    
            GetComponent<Rigidbody2D>().AddForce(transform.up * Random.Range(minSpeed, maxSpeed));
        }

        public void DestroyEnemy()
        {
            throw new System.NotImplementedException();
        }
    }
}