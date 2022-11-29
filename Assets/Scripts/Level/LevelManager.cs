using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CrossFates
{
    public static class LevelManager
    {
        private static Level _level;
        public static Level Level => _level;


        public static void LoadLevel(Level level)
        {
            _level = level;
            SceneManager.LoadScene(1);
        }
        
    }
   
}
