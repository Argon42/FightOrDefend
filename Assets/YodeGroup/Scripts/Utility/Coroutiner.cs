using System.Collections;
using UnityEngine;

namespace YodeGroup.Utility
{
    public class Coroutiner : MonoBehaviour
    {
        private static Coroutiner _instance;
        public static Coroutiner Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("Coroutine").AddComponent<Coroutiner>();
                    GameObject.DontDestroyOnLoad(_instance);
                }
                return _instance;
            }
        }

        public static Coroutine Start(IEnumerator corutine) =>
            Instance.StartCoroutine(corutine);

        public static void Stop(IEnumerator corutine) =>
            Instance.StopCoroutine(corutine);
    }
}