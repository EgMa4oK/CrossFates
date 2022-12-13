using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrossFates
{
    public static class Pause
    {
        private static List<IPauseRequster> _requesters = new();

        public static void Request(IPauseRequster requster)
        {           
            Time.timeScale = 0;
            _requesters.Add(requster);
        }

        public static void Stop(IPauseRequster requester)
        {
            _requesters.RemoveAll(elm => elm as Object == null);
            if (_requesters.Contains(requester))
            {
                _requesters.Remove(requester);
            }
            if (_requesters.Count == 0)
            {
                Time.timeScale = 1;
            }
        }
    }
}
