using UnityEngine;

namespace Source
{
    public interface IMovableObject
    {
        TypesOfTarget Type { get; set; }
        Generation Generation { get; set; }
        void Move(float maxSpeed, float minSpeed);
        void DestroyEnemy();
    }
}