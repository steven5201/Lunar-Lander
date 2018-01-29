#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using System.IO;
#endregion

namespace LunarLander
{
    public static class Sound
    {
        #region Variables
        private static List<SoundEffect> soundEffects = new List<SoundEffect>();
        private static SoundEffectInstance thrust;
        private static SoundEffectInstance explosion;
        private static SoundEffectInstance landing;
        private static bool isLoading;
        #endregion

        #region Set Method
        public static void SetSounds(List<SoundEffect> landerSounds)
        {
            foreach (SoundEffect sound in landerSounds)
            {
                soundEffects.Add(sound);
            }

            explosion = soundEffects[0].CreateInstance();
            thrust = soundEffects[1].CreateInstance();
            landing = soundEffects[2].CreateInstance();

            LoadVolume();
        }
        #endregion

        #region Sounds Management Methods [Play, Stop, Volume, Get State]
        public static void PlayExplosion()
        {
            if (explosion.State == SoundState.Stopped)
            {
                explosion.Play();
            }
        }

        public static void StopExplosion()
        {
            if (explosion.State == SoundState.Playing)
            {
                explosion.Stop();
            }
        }

        public static void PlayThrust()
        {
            if (thrust.State == SoundState.Stopped)
            {
                thrust.Play();
            }
        }

        public static void StopThrust()
        {
            if (thrust.State == SoundState.Playing)
            {
                thrust.Stop();
            }
        }

        public static void PlayLanding()
        {
            if (landing.State == SoundState.Stopped)
            {
                landing.Play();
            }
        }

        public static void StopLanding()
        {
            if (landing.State == SoundState.Playing)
            {
                landing.Stop();
            }
        }

        public static void ChangeVolume(float newVolume)
        {
            if (newVolume >= 0 && newVolume <= 1)
            {
                landing.Volume = newVolume;
                thrust.Volume = newVolume;
                explosion.Volume = newVolume;

                if (!isLoading)
                {
                    SaveVolume();
                }
            }
        }

        public static float CurrentVolume()
        {
            return landing.Volume;
        }

        public static bool isExplosionFinished()
        {
            if (explosion.State == SoundState.Playing)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region Load and Save Volume
        private static void LoadVolume()
        {
            isLoading = true;
            using (TextReader reader = File.OpenText(@"./Content/Volume.txt"))
            {
                ChangeVolume(float.Parse(reader.ReadLine()));
                reader.Close();
            }
            isLoading = false;
        }

        private static void SaveVolume()
        {
            using (TextWriter writer = File.CreateText(@"./Content/Volume.txt"))
            {
                writer.WriteLine(CurrentVolume());
                writer.Close();
            }
        }
        #endregion
    }
}
