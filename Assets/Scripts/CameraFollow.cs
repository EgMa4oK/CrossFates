using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace CrossFates
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        private void Update()
        {

            transform.SetPositionAndRotation(_target.position, transform.rotation);
        }
    }
}
