using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace Source.Services
{
    public class SoundsComponent : MonoBehaviour
    {
        [SerializeField] private AudioSource mainThemeMusic;
        [SerializeField] private AudioSource heroShotSound;
        [SerializeField] private AudioSource heroDeathSound;
        [SerializeField] private AudioSource thrustSound;
        [SerializeField] private AudioSource asteroidExplosionSound;
        [SerializeField] private AudioSource ufoDeathSound;
        [SerializeField] private AudioSource ufoShotSound;
        [SerializeField] private AudioSource ufoComingSound;
        [SerializeField] private AudioSource clickSound;
        [SerializeField] private AudioMixerGroup mixer;

        public static SoundsComponent Sounds;


        private void Awake()
        {
            Sounds = this;
        }

        public void PlayMainThemeMusic(bool isPlay)
        {
            if (isPlay)
                mainThemeMusic.Play();
            else
                mainThemeMusic.Stop();
        }

        public void PlayEffectsSound(bool isEnable)
        {
            if (isEnable)
                mixer.audioMixer.SetFloat("EffectsVolume", -80);
            else
                mixer.audioMixer.SetFloat("EffectsVolume", 0);
        }

        public void PlayHeroShotSound()
        {
            heroShotSound.pitch = Random.Range(0.8f, 1f);
            heroShotSound.Play();
        }

        public void PlayHeroDeathSound()
        {
            heroDeathSound.Play();
        }

        public void PlayAsteroidExplosionSound()
        {
            asteroidExplosionSound.volume = Random.Range(0.2f, 0.6f);
            asteroidExplosionSound.pitch = Random.Range(0.7f, 0.8f);
            asteroidExplosionSound.Play();
        }


        public void PlayUfoDeathSound()
        {
            ufoDeathSound.Play();
            ufoComingSound.Stop();
        }


        public void PlayUfoShotSound()
        {
            ufoShotSound.Play();
        }


        public void PlayUfoComingSound()
        {
            ufoComingSound.Play();
        }


        public void PlayClickSound()
        {
            clickSound.Play();
        }
    }
}
