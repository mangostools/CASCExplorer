using CASCLib;
using System;
using System.Windows.Forms;

namespace CASCExplorer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            WDC3ReaderGeneric<SpellEffectRec> spellEffect = new WDC3ReaderGeneric<SpellEffectRec>(@"f:\Dev\WoW\DBFilesClient_30495\SpellEffect.db2");
            var effect1 = spellEffect.GetRow(1);
            var effect2 = spellEffect.GetRow(2);
            ;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }

    public class SpellEffectRec : ClientDBRow // sizeof(0x82)
    {
        public override int GetId() => Id;

        public int Id; // size 1, type 0, flags 0
        public int DifficultyID; // size 1, type 0, flags 2
        public int EffectIndex; // size 1, type 0, flags 2
        public uint Effect; // size 1, type 0, flags 6
        public float EffectAmplitude; // size 1, type 3, flags 2
        public int EffectAttributes; // size 1, type 0, flags 2
        public short EffectAura; // size 1, type 5, flags 2
        public int EffectAuraPeriod; // size 1, type 0, flags 2
        public float EffectBonusCoefficient; // size 1, type 3, flags 2
        public float EffectChainAmplitude; // size 1, type 3, flags 2
        public int EffectChainTargets; // size 1, type 0, flags 2
        public int EffectItemType; // size 1, type 0, flags 2
        public int EffectMechanic; // size 1, type 0, flags 2
        public float EffectPointsPerResource; // size 1, type 3, flags 2
        public float EffectPos_facing; // size 1, type 3, flags 2
        public float EffectRealPointsPerLevel; // size 1, type 3, flags 2
        public int EffectTriggerSpell; // size 1, type 0, flags 2
        public float BonusCoefficientFromAP; // size 1, type 3, flags 2
        public float PvpMultiplier; // size 1, type 3, flags 2
        public float Coefficient; // size 1, type 3, flags 2
        public float Variance; // size 1, type 3, flags 2
        public float ResourceCoefficient; // size 1, type 3, flags 2
        public float GroupSizeBasePointsCoefficient; // size 1, type 3, flags 2
        public float EffectBasePointsF; // size 1, type 3, flags 2
        [ArraySize(2)]
        public int[] EffectMiscValue; // size 2, type 0, flags 2
        [ArraySize(2)]
        public uint[] EffectRadiusIndex; // size 2, type 0, flags 6
        [ArraySize(4)]
        public int[] EffectSpellClassMask; // size 4, type 0, flags 2
        [ArraySize(2)]
        public short[] ImplicitTarget; // size 2, type 5, flags 2
        public int SpellID; // size 1, type 0, flags 2
    }
}
