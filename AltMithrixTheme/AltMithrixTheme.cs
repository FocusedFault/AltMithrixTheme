using BepInEx;
using RoR2;
using R2API;
using EntityStates.Missions.BrotherEncounter;
using System.IO;
using System.Reflection;

namespace AltMithrixTheme
{
    [BepInPlugin("com.Nuxlar.AltMithrixTheme", "AltMithrixTheme", "1.0.2")]
    [BepInDependency(SoundAPI.PluginGUID)]

    public class AltMithrixTheme : BaseUnityPlugin
    {
        public void Awake()
        {
            using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("AltMithrixTheme.Resources.AltMithrixTheme.bnk"))
            {
                byte[] buffer = new byte[manifestResourceStream.Length];
                manifestResourceStream.Read(buffer, 0, buffer.Length);
                SoundAPI.SoundBanks.Add(buffer);
            }
            On.RoR2.MusicController.PickCurrentTrack += MusicController_PickCurrentTrack;
            On.EntityStates.Missions.BrotherEncounter.Phase1.OnEnter += Music_START;
            On.EntityStates.Missions.BrotherEncounter.Phase2.OnEnter += Music_START2;
            On.EntityStates.Missions.BrotherEncounter.Phase3.OnEnter += Music_START3;
            On.EntityStates.Missions.BrotherEncounter.Phase4.OnEnter += Music_START4;
            On.EntityStates.Missions.BrotherEncounter.BrotherEncounterPhaseBaseState.OnExit += Music_END;

        }
        private static void MusicController_PickCurrentTrack(On.RoR2.MusicController.orig_PickCurrentTrack orig, MusicController self, ref MusicTrackDef newTrack)
        {
            orig(self, ref newTrack);
            if (newTrack.cachedName == "muSong25")
                newTrack = null;
        }
        private void Music_START(On.EntityStates.Missions.BrotherEncounter.Phase1.orig_OnEnter orig, Phase1 self)
        {
            Util.PlaySound("Play_p1_umbral", self.gameObject);
            orig(self);
        }
        private void Music_START2(On.EntityStates.Missions.BrotherEncounter.Phase2.orig_OnEnter orig, Phase2 self)
        {
            Util.PlaySound("Play_p2_umbral", self.gameObject);
            orig(self);
        }
        private void Music_START3(On.EntityStates.Missions.BrotherEncounter.Phase3.orig_OnEnter orig, Phase3 self)
        {
            Util.PlaySound("Play_p3_umbral", self.gameObject);
            orig(self);
        }
        private void Music_START4(On.EntityStates.Missions.BrotherEncounter.Phase4.orig_OnEnter orig, Phase4 self)
        {
            Util.PlaySound("Play_p4_umbral", self.gameObject);
            orig(self);
        }
        private void Music_END(On.EntityStates.Missions.BrotherEncounter.BrotherEncounterPhaseBaseState.orig_OnExit orig, BrotherEncounterPhaseBaseState self)
        {
            Util.PlaySound("Stop_p3_umbral", self.gameObject);
            if (self is Phase1)
                Util.PlaySound("Stop_p1_umbral", self.gameObject);
            if (self is Phase2)
                Util.PlaySound("Stop_p2_umbral", self.gameObject);
            if (self is Phase4)
                Util.PlaySound("Stop_p4_umbral", self.gameObject);
            orig(self);
        }
    }
}