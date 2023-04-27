using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Camera
{
    // Values or methods that other can use
    public partial class CameraMovement
    {
        
    }
    
    // Values or methods that other cannot use
    public partial class CameraMovement
    {
        private Transform _camPos;
        private Transform _playerPos;
        [SerializeField] private Vector3 offset;
    }
    
    // body of MonoBehaviour
    public partial class CameraMovement : MonoBehaviour
    {
        private void Awake()
        {
            _camPos = GetComponent<Transform>();
            _playerPos = GameObject.Find("Player").GetComponent<Transform>();
        }

        private void LateUpdate()
        {
            _camPos.position = _playerPos.position + offset;
        }
    }
    
    // body of others
    public partial class CameraMovement
    {
        
    }
}


