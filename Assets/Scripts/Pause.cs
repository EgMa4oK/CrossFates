using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrossFates
{
    public static class Pause
    {
        private static int _requestCount = 0;

        public static void Start()
        {
            Time.timeScale = 0;
            _requestCount += 1;
        }

        public static void Stop()
        {
            _requestCount -= 1;
            if (_requestCount == 0)
            {
                Time.timeScale = 1;
            }
        }
    }
}
