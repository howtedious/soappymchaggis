using Microsoft.Xna.Framework.Audio;

namespace SoappMcHaggis
{
    public class Sound
    {
        private static AudioEngine engine;
        private static WaveBank wavebank;
        private static SoundBank soundbank;

        public static Cue Play(string Name)
        {
            Cue returnValue = soundbank.GetCue(Name);
            returnValue.Play();
            return returnValue;
        }

        public static void Stop(Cue cue)
        {
            cue.Stop(AudioStopOptions.Immediate);
        }

        public static void Initialise()
        {
            engine = new AudioEngine("Content/Audio/FabulousAdventures.xgs");
            wavebank = new WaveBank(engine, "Content/Audio/Wave Bank.xwb");
            soundbank = new SoundBank(engine, "Content/Audio/Sound Bank.xsb");
        }

        public static void Update()
        {
            engine.Update();
        }

        public static void Shutdown()
        {
            soundbank.Dispose();
            wavebank.Dispose();
            engine.Dispose();
        }
    }
}
