using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrossFates
{
    public class Hhahaha : MonoBehaviour
    {
        [SerializeField] private Level level;
        private TMPro.TMP_Text text;
        void Start()
        {
            text = GetComponent<TMPro.TMP_Text>();
        }

        void Update()
        {
            text.text = level.IsCompleted.ToString();
        }
    }
}
