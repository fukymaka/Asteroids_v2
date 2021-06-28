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
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 359));
            GetComponent<Rigidbody2D>().AddForce(transform.up * Random.Range(minSpeed, maxSpeed));
        }

        public void DestroyEnemy()
        {
            throw new System.NotImplementedException();
        }
    }
}