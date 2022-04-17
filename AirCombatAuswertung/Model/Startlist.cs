namespace AirCombatAuswertung.Model
{
    public class Startlist
    {
        /// <summary>
        /// Klasse der Startliste
        /// </summary>
        public int Classnr { get; set; }
        /// <summary>
        /// Runde
        /// </summary>
        public int Round { get; set; }
        /// <summary>
        /// Heat
        /// </summary>
        public int Heat { get; set; }
        /// <summary>
        /// Piloten in der Runde + Heat
        /// </summary>
        public int Pilotnr { get; set; }
        /// <summary>
        /// Pilotenjudges in der Runde + Heat
        /// </summary>
        public int Judgenr { get; set; }
    }
}
