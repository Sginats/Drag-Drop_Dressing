using UnityEngine;

namespace TelaVeidotajs
{
    /// <summary>
    /// Manages background music and UI sound effects.
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioClip backgroundMusic;
        [SerializeField] private AudioClip clickSound;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            SetupAudioSources();
        }

        /// <summary>
        /// Sets up audio sources for music and SFX.
        /// </summary>
        private void SetupAudioSources()
        {
            if (musicSource == null)
            {
                musicSource = gameObject.AddComponent<AudioSource>();
            }

            if (sfxSource == null)
            {
                sfxSource = gameObject.AddComponent<AudioSource>();
            }

            musicSource.loop = true;
            musicSource.playOnAwake = false;

            sfxSource.loop = false;
            sfxSource.playOnAwake = false;
        }

        /// <summary>
        /// Plays background music if not already playing.
        /// </summary>
        public void PlayBackgroundMusic()
        {
            if (backgroundMusic != null && musicSource != null && !musicSource.isPlaying)
            {
                musicSource.clip = backgroundMusic;
                musicSource.Play();
            }
        }

        /// <summary>
        /// Plays the UI click sound effect.
        /// </summary>
        public void PlayClickSound()
        {
            if (clickSound != null && sfxSource != null)
            {
                sfxSource.PlayOneShot(clickSound);
            }
        }
    }
}
