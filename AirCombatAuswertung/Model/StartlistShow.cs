using System.Collections.Generic;

namespace AirCombatAuswertung.Model
{
    public class StartlistShow
    {
        /// <summary>
        /// Runde
        /// </summary>
        public int Round { get; set; }
        /// <summary>
        /// Heat
        /// </summary>
        public int Heat { get; set; }
        /// <summary>
        /// Frequenz des Piloten
        /// </summary>
        public List<string> Frequency { get; set; }
        /// <summary>
        /// Piloten in der Runde + Heat
        /// </summary>
        public List<string> Pilots { get; set; }
        /// <summary>
        /// Pilotenjudges in der Runde + Heat
        /// </summary>
        public List<string> Judges { get; set; }
    }
}
