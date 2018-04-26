
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Platformer
{
    public static class SoundEffectManager
    {
        #region Member variables
        static SoundEffect myShootSound;
        static SoundEffectInstance myShootSoundInstance;
        static int myShootSoundDuration;

        #endregion

        #region Public methods
        public static void Update(GameTime aGameTime)
        {
            UpdateShootSound(aGameTime);
        }


        public static void PlayShootSound()
        {
            if (myShootSoundInstance.State == SoundState.Stopped)
            {
                myShootSoundInstance.Play();
                myShootSoundDuration = 50;
            }
        }

        public static void InitalizeVariables()
        {
            InitializeShootSound();
        }
        #endregion

        #region Private methods
        private static void InitializeShootSound()
        {
            myShootSound = Game1.myContentManager.Load<SoundEffect>("ShootSound");
            myShootSoundInstance = myShootSound.CreateInstance();
        }

        private static void UpdateShootSound(GameTime aGameTime)
        {
            myShootSoundDuration -= aGameTime.ElapsedGameTime.Milliseconds;

            if (myShootSoundDuration <= 0)
            {
                myShootSoundInstance.Stop();
            }
        }
        #endregion
    }
}
