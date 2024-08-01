using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
namespace SorryLab.Audio {
    public class AudioPlayer : MonoBehaviour {
        static GameObject m_instance;
        static public float mainVolume = 1f;
        static Transform m_listener;
        static List<AudioPlayerClip> m_playingList = new List<AudioPlayerClip>();
        static Dictionary<string, AudioClip> m_audioClips = new Dictionary<string, AudioClip>();
        static Pool<AudioPlayerClip> m_pool;
        static Transform GetListener() {
            if (m_listener == null) {
                AudioListener listener = FindObjectOfType<AudioListener>(true);
                if (listener != null) {
                    m_listener = listener.transform;
                } else {
                    Debug.LogError("[AudioPlayer]No audioListener here.");
                }
            }
            return m_listener;
        }

        static public AudioSource Play(AudioClip audioClip, float volume, Transform parent = null) {
            if (parent == null) { parent = m_instance.transform.parent; }
            if (audioClip == null) { return null; }
            AudioSource audioSource = CreateAndPlay(audioClip, volume);
            return audioSource;
        }
        static public void Play(string path, float volume, System.Action<AudioSource> callback = null, Transform parent = null) {
            AudioSource source = null;
            if (!m_audioClips.ContainsKey(path)) {
                GameObject go = new GameObject("[AudioPlayerLoader]");
                AudioClipLoader audioClipLoader = go.AddComponent<AudioClipLoader>();
                audioClipLoader.Load(path, (audioClip) => {
                    m_audioClips[path] = audioClip;
                    source = Play(audioClip, volume, parent);
                    callback?.Invoke(source);
                });
            } else {
                source = Play(m_audioClips[path], volume, parent);
                callback?.Invoke(source);
            }
        }

        static public void StopAll() {
            AudioPlayerClip[] playingLs = m_playingList.ToArray();
            foreach (AudioPlayerClip i in playingLs) { i.Stop(); }
        }

        static public void Stop(AudioSource audioSource) {
            AudioPlayerClip audioPlayerClip = audioSource.GetComponent<AudioPlayerClip>();
            audioPlayerClip?.Stop();
        }

        static AudioPlayer() {
            m_instance = new GameObject("[AudioPlayerInstance]");
            m_instance.transform.localPosition = Vector3.zero;
            AudioPlayerClip apc = m_instance.AddComponent<AudioPlayerClip>();
            apc.audioSource = m_instance.AddComponent<AudioSource>();
            m_pool = new Pool<AudioPlayerClip>(apc);
            m_instance.SetActive(false);
            DontDestroyOnLoad(m_instance);
        }

        static AudioSource CreateAndPlay(AudioClip audioClip, float volume) {
            AudioPlayerClip player = m_pool.Create();
            m_playingList.Add(player);
            player.Play(audioClip, volume);
            return player.audioSource;
        }

        internal class AudioPlayerClip : MonoBehaviour {
            public AudioSource audioSource;
            public void Play(AudioClip audioClip, float volume) {
                audioSource.clip = audioClip;
                audioSource.volume = volume * mainVolume;
                audioSource.Play();
                StartCoroutine(RecoverTime(audioClip.length));
            }
            IEnumerator RecoverTime(float time) {
                yield return new WaitForSeconds(time);
                Return();
            }
            public void Stop() {
                StopAllCoroutines();
                Return();
            }
            void Return() {
                m_playingList.Remove(this);
                m_pool.Return(this);
            }
        }
        internal class AudioClipLoader : MonoBehaviour {
            public void Load(string path, System.Action<AudioClip> callback) {
                StartCoroutine(LoadAsync(path, callback));
            }
            IEnumerator LoadAsync(string path, System.Action<AudioClip> callback) {
                using (UnityWebRequest uwr = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.UNKNOWN)) {
                    yield return uwr.SendWebRequest();
                    if (uwr.result != UnityWebRequest.Result.Success) {
                        Debug.LogWarning(string.Format("Audio無法載入，錯誤路徑:\"{0}\"", path));
                        Debug.LogWarning(uwr.error);
                        callback?.Invoke(null);
                    } else {
                        AudioClip audioClip = DownloadHandlerAudioClip.GetContent(uwr);
                        callback?.Invoke(audioClip);
                    }
                    uwr.Dispose();
                }
                DestroyImmediate(gameObject);
            }
        }
    }

}