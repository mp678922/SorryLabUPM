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
            if (Application.isPlaying) {
                GameObject gameObj = new GameObject("[StaticCoroutine]");
                m_instance = gameObj.AddComponent<StaticCoroutineMono>();
                Object.DontDestroyOnLoad(gameObj);
            }
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
        /* instance */

        public bool isPlaying { get { return m_isPlaying; } }
        Coroutine m_coroutine;
#if UNITY_EDITOR
        Unity.EditorCoroutines.Editor.EditorCoroutine m_editorCoroutine;
#endif
        bool m_isPlaying = false;
        public System.Action onComplete;
        public System.Action onKill;
        public StaticCoroutine(IEnumerator coroutine, System.Action onComplete = null) {
            m_instance.StartCoroutine(Async(coroutine));
            this.onComplete = onComplete;
        }

        IEnumerator Async(IEnumerator coroutine) {
            m_isPlaying = true;
            if (Application.isPlaying) {
                m_coroutine = m_instance.StartCoroutine(coroutine);
                yield return m_coroutine;
            } else {
#if UNITY_EDITOR
                m_editorCoroutine = Unity.EditorCoroutines.Editor.EditorCoroutineUtility.StartCoroutine(coroutine, this);
                yield return m_editorCoroutine;
#endif
            }
            m_isPlaying = false;
            onComplete?.Invoke();
            if (Application.isPlaying) {
                m_coroutine = null;
            } else {
#if UNITY_EDITOR
                m_editorCoroutine = null;
#endif
            }
        }

        public void Kill() {
            onKill?.Invoke();
            if (Application.isPlaying) {
                if (m_coroutine != null) {
                    m_instance?.StopCoroutine(m_coroutine);
                    m_coroutine = null;
                }
            } else {
#if UNITY_EDITOR
                Unity.EditorCoroutines.Editor.EditorCoroutineUtility.StopCoroutine(m_editorCoroutine);
                m_editorCoroutine = null;
#endif
            }
        }

        internal class StaticCoroutineMono : MonoBehaviour { void Awake() { } }
    }

}

