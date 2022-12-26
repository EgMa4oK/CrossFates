using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrossFates
{
    [CreateAssetMenu(fileName = "new level", menuName = "level", order = 50)]
    public class Level : ScriptableObject
    {
        private bool _isCompleted = false;
        private const string saveKey = "mainSave";
        [SerializeField] private Vector2 _spawnPoint = new Vector2(0, 0);

        public Vector2 SpawnPoint => _spawnPoint;
        public bool IsCompleted => _isCompleted;
        private void OnEnable()
        {
            SaveReset.SaveRemoved += Load;
            Load();
        }

        private void Load()
        {
            var data = SaveManager.Load<SaveData.LevelProfile>(saveKey);

            _isCompleted = data.isCompleted;
        }

        private void Save()
        {
            SaveManager.Save(saveKey, GetSaveSnapshot());
        }
        private SaveData.LevelProfile GetSaveSnapshot()
        {
            var data = new SaveData.LevelProfile()
            {
                isCompleted = _isCompleted,
            };

            return data;
        }

        public void Complete()
        {
            _isCompleted = true;
            Save();
        }
    }
}

