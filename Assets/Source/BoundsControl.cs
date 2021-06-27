using System;
using UnityEngine;

namespace Source
{
    public class BoundsControl : MonoBehaviour
    {
        private bool _isBoundsOut;
        [SerializeField] private bool keepOnScreen;
        
        private float _camHeight;
        private float _camWidth;
        
        private void Start()
        {
            CalculateCameraSize();
        }
        
        private void Update()
        {
            BoundsCheck();
        }

        private void CalculateCameraSize()
        {
            var mainCamera = Camera.main;
            _camHeight = mainCamera.orthographicSize;
            _camWidth = _camHeight * mainCamera.aspect;
        }

        private void BoundsCheck()
        {
            var pos = transform.position;
            
            if (Math.Abs(transform.position.y) > _camHeight)
            {
                if (pos.y > 0)
                    pos.y = -_camHeight;
                else
                    pos.y = _camHeight;

                _isBoundsOut = true;
            }
            
            if (Math.Abs(transform.position.x) > Math.Abs(_camWidth))
            {
                if (pos.x > 0)
                    pos.x = -_camWidth;
                else
                    pos.x = _camWidth;

                _isBoundsOut = true;
            }

            if (keepOnScreen)
                transform.position = pos;
            
            if (!keepOnScreen && _isBoundsOut)
                Destroy(gameObject);
        }
    }
}