using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CrossFates
{
    public class SaveReset : MonoBehaviour
    {
        public static event Action SaveRemoved;
        public static void RemoveSave()
        {
            PlayerPrefs.DeleteAll();
            if (SaveRemoved != null)
            {
                SaveRemoved.Invoke();
            }
        }
    }
}