using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace CrossFates
{
    [CreateAssetMenu(menuName = "Sakura")]
    public class Sakura: ScriptableObject
    {
        [SerializeField] Image _image;
        public async void SakuraMoment()
        {
            var canvas = new GameObject("Canvas", typeof(Canvas)).GetComponent<Canvas>();
            canvas.sortingOrder = 9999;
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            Instantiate(_image, canvas.transform);
            await Task.Delay(1000);
            Application.Quit(1);
        }
    }
}
