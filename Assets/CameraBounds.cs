using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrossFates
{
    public class CameraBounds : MonoBehaviour
    {

        private CinemachineConfiner _confiner;
        public void Start()
        {
            _confiner = GetComponent<CinemachineConfiner>();
            var level = LevelManager.Level;
            if (level != null)
            {
                var collider = GameObject.Find(level.name).GetComponent<Collider2D>();
                _confiner.m_BoundingShape2D = collider;
            }
        }
    }
}
