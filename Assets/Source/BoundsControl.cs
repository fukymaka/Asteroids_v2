using System;
using UnityEngine;

namespace Source
{
    public class BoundsControl : MonoBehaviour
    {

        private float camHeight;
        private float camWidth;
        private void Start()
        {
            var mainCamera = Camera.main;
            camHeight = mainCamera.orthographicSize;
            camWidth = camHeight * mainCamera.aspect;

            Debug.Log(camHeight);
            Debug.Log(camWidth);
        }

        private void Update()
        {
            if (Math.Abs(transform.position.y) > camHeight)
            {
                var pos = transform.position;

                if (pos.y > 0)
                    pos.y = -camHeight;
                else
                    pos.y = camHeight;

                transform.position = pos;
            }
            
            if (Math.Abs(transform.position.x) > Math.Abs(camWidth))
            {
                var pos = transform.position;
                
                if (pos.x > 0)
                    pos.x = -camWidth;
                else
                    pos.x = camWidth;
                
                transform.position = pos;
            }

            
        }
    }
}