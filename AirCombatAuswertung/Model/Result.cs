using Dapper.Contrib.Extensions;

namespace AirCombatAuswertung.Model
{
    public class Result
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
        [Key]
        /// <summary>
        /// Ausgewertete Runde
        /// </summary>
        public int Round { get; set; }
        /// <summary>
        /// Summe der Modellpunkte
        /// </summary>
        public int SumModP { get; set; }
        /// <summary>
        /// Summe der Runde
        /// </summary>
        public int Sum { get; set; }
        /// <summary>
        /// Summe der bis Dato erflogenen Punkte
        /// </summary>
        public int Total { get; set; }
    }
}
