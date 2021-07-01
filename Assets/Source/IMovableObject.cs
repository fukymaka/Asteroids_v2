using UnityEngine;

namespace Source
{
    public interface IMovableObject
    {
        TypeOfTarget PossibleCollisions { get; set; }
        TypeOfTarget Type { get; set; }
        Generation Generation { get; set; }

        // void Init() todo
        void Move(float maxSpeed, float minSpeed);
    }
}