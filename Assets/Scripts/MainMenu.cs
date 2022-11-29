using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CrossFates
{

    public class MainMenu : MonoBehaviour
    {
    
        public void LoadLevel(Level level)
        {
            LevelManager.LoadLevel(level);
        }

    }
}
