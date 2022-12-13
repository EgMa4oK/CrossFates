using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CrossFates
{
    public class Level1
    {
        [SerializeField] private string _levelName;
        private bool _isCompleted;

        private void Save()
        {
            SaveManager.Save(_levelName, GetSaveSnapshot());
        }

        private SaveData.Level GetSaveSnapshot()
        {
            var data = new SaveData.Level()
            {
                IsCompleted = _isCompleted,
            };

            return data;
        }
        
        public void Load()
        {
            var data = SaveManager.Load<SaveData.Level>(_levelName);
            _isCompleted = data.IsCompleted;
        }

        public void Complete()
        {
            _isCompleted = true;
            Save();
        }
    }

    namespace SaveData
    {
        [System.Serializable]
        public class Level
        {
            public bool IsCompleted;

            public Level()
            {
                IsCompleted = false;
            }
        }
    }
}
