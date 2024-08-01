using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SorryLab {
    public class StaticCoroutine {

        /* static */

        static StaticCoroutineMono m_instance;

        static IEnumerator WaitTime(float time, bool isRealTime = false) {
            if (isRealTime) {
                yield return new WaitForSecondsRealtime(time);
            } else {
                yield return new WaitForSeconds(time);
            }
        }

        static StaticCoroutine() {
            GameObject gameObj = new GameObject("[OutsourceCoroutine]");
            m_instance = gameObj.AddComponent<StaticCoroutineMono>();
            Object.DontDestroyOnLoad(gameObj);
        }

        static public Coroutine StartCoroutine(IEnumerator method, System.Action onComplete = null) {
            return new StaticCoroutine(method, onComplete).m_coroutine;
        }

        static public StaticCoroutine DelayInvoke(float delayTime, System.Action method, bool usingTimeScale = true) {
            return new StaticCoroutine(DelayCallAsync(delayTime, method, usingTimeScale));
        }

        static public StaticCoroutine DelayInvoke<T>(float delayTime, System.Func<T> method, bool usingTimeScale = true) {
            return new StaticCoroutine(DelayCallAsync(delayTime, method, usingTimeScale));
        }

        static public StaticCoroutine UpdateInvoke(System.Action method) {
            return new StaticCoroutine(UpdateAsync(method));
        }

        static public StaticCoroutine UpdateInvoke<T>(System.Func<T> method) {
            return new StaticCoroutine(UpdateAsync(method));
        }

        static IEnumerator DelayCallAsync(float delayTime, System.Action method, bool usingTimeScale) {
            yield return WaitTime(delayTime, usingTimeScale);
            method();
        }

        static IEnumerator DelayCallAsync<T>(float delayTime, System.Func<T> method, bool usingTimeScale) {
            yield return WaitTime(delayTime, usingTimeScale);
            method();
        }

        static IEnumerator UpdateAsync(System.Action method) {
            while (true) {
                method();
                yield return null;
            }
        }

        static IEnumerator UpdateAsync<T>(System.Func<T> method) {
            while (true) {
                method();
                yield return null;
            }
        }
        public StaticCoroutine(IEnumerator coroutine, System.Action onComplete = null) {
            m_instance.StartCoroutine(Async(coroutine));
            this.onComplete = onComplete;
        }

        /* instance */

        public bool isPlaying { get { return m_isPlaying; } }
        Coroutine m_coroutine;
        bool m_isPlaying = false;
        public System.Action onComplete;
        public System.Action onKill;

        IEnumerator Async(IEnumerator coroutine) {
            m_isPlaying = true;
            m_coroutine = m_instance.StartCoroutine(coroutine);
            yield return m_coroutine;
            m_isPlaying = false;
            onComplete?.Invoke();
        }

        public void Kill() {
            onKill?.Invoke();
            m_instance.StopCoroutine(m_coroutine);
        }

        internal class StaticCoroutineMono : MonoBehaviour { void Awake() { } }
    }

}

