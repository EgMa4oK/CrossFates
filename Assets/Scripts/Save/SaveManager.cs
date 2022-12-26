using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CrossFates
{
    public static class SaveManager
    {
        public static void Save<T>(string Key, T SaveData)
        {
            string JsonDataString = JsonUtility.ToJson(SaveData, true);
            PlayerPrefs.SetString(Key, JsonDataString);
        }

        public static T Load<T>(string Key) where T : new()
        {
            if (PlayerPrefs.HasKey(Key))
            {
                string LoadedString = PlayerPrefs.GetString(Key);
                return JsonUtility.FromJson<T>(LoadedString);
            }
            else
            {
                return new T();
            }
        }
    }
}
