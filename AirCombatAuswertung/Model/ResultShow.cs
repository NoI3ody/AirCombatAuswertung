using Dapper.Contrib.Extensions;

namespace AirCombatAuswertung.Model
{
    public class ResultShow
    {
        [Key]
        /// <summary>
        /// Startnr des Piloten
        /// </summary>
        public decimal Startnr { get; set; }
        [Key]
        /// <summary>
        /// Laufende Nummer für die drei verfügbaren Klassen,
        /// 1...WW2,
        /// 2...WW1.
        /// 3...EPA
        /// </summary>
        public int Classnr { get; set; }
        /// <summary>
        /// Ausgewertete Runde 1
        /// </summary>
        public int Round1 { get; set; }
        /// <summary>
        /// Ausgewertete Runde 2
        /// </summary>
        public int Round2 { get; set; }
        /// <summary>
        /// Ausgewertete Runde 3
        /// </summary>
        public int Round3 { get; set; }
        /// <summary>
        /// Ausgewertete Runde 4
        /// </summary>
        public int Round4 { get; set; }
        /// <summary>
        /// Ausgewertete Runde 5
        /// </summary>
        public int Round5 { get; set; }
        /// <summary>
        /// Ausgewertete Runde 6
        /// </summary>
        public int Round6 { get; set; }
        /// <summary>
        /// Ausgewertete Runde 7
        /// </summary>
        public int Round7 { get; set; }
        /// <summary>
        /// Ausgewertete Runde 8
        /// </summary>
        public int Round8 { get; set; }
        /// <summary>
        /// Ausgewertete Runde 9
        /// </summary>
        public int Round9 { get; set; }
        /// <summary>
        /// Ausgewertete Runde 10
        /// </summary>
        public int Round10 { get; set; }
        /// <summary>
        /// Ausgewertete Runde 11
        /// </summary>
        public int Round11 { get; set; }
        /// <summary>
        /// Ausgewertete Runde 12
        /// </summary>
        public int Round12 { get; set; }
        /// <summary>
        /// Summe der bis Dato erflogenen Punkte
        /// </summary>
        public int Total { get; set; }
    }
}
