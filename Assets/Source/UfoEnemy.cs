using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source
{
    public class UfoEnemy : MonoBehaviour, IMovableObject
    {
        public TypesOfTarget Type { get; set; } = TypesOfTarget.Ufo;
        public Generation Generation { get; set; }


        public void Move(float maxSpeed, float minSpeed)
        {    
            StartCoroutine(MoveCoroutine(maxSpeed, minSpeed));
        }

        private IEnumerator MoveCoroutine(float maxSpeed, float minSpeed)
        {
            var nextPoint = GetNextPoint();

            while (Vector3.Distance(transform.position, nextPoint) > 1f)
            {
                transform.position = Vector3.Lerp(transform.position, nextPoint, Time.deltaTime);
                yield return null;
            }
            
            StartCoroutine(MoveCoroutine(maxSpeed, minSpeed));
        }

        private Vector3 GetNextPoint()
        {
            var camHeight = BoundsControl.BoundHeight;
            var camWidth = BoundsControl.BoundWidth;
            
            var startPosX = Random.Range(-camWidth, camWidth);
            var startPosY = Random.Range(-camHeight, camHeight);

            var nextPoint = new Vector3(startPosX, startPosY, 0);
            
            return nextPoint;
        }

        public void DestroyEnemy()
        {
            throw new System.NotImplementedException();
        }
    }
}