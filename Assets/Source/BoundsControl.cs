using System;
using UnityEngine;

namespace Source
{
    public class BoundsControl : MonoBehaviour
    {
        [SerializeField] private bool keepOnScreen;

        public bool isBoundsOut;
        public static float BoundHeight { get; private set; }
        public static float BoundWidth { get; private set; }

        private void Awake()
        {
            CalculateBounds();
        }
        
        private void Update()
        {
            BoundsCheck();
        }

        private void CalculateBounds()
        {
            var mainCamera = Camera.main;
            BoundHeight = mainCamera.orthographicSize;
            BoundWidth = BoundHeight * mainCamera.aspect;
        }

        private void BoundsCheck()
        {
            var pos = transform.position;
            
            if (Math.Abs(transform.position.y) > BoundHeight)
            {
                if (pos.y > 0)
                    pos.y = -BoundHeight;
                else
                    pos.y = BoundHeight;

                isBoundsOut = true;
            }
            
            if (Math.Abs(transform.position.x) > Math.Abs(BoundWidth))
            {
                if (pos.x > 0)
                    pos.x = -BoundWidth;
                else
                    pos.x = BoundWidth;

                isBoundsOut = true;
            }

            if (keepOnScreen)
                transform.position = pos;
            
            // if (!keepOnScreen && _isBoundsOut)
            //     Destroy(gameObject);
        }
    }
}