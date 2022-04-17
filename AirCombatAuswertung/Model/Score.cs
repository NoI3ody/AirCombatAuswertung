using Dapper.Contrib.Extensions;

namespace AirCombatAuswertung.Model
{
    public class Score
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
        [Key]
        /// <summary>
        /// Auswertungs Kategorie
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// Wert der Option/Information
        /// </summary>
        public int Value { get; set; }        
    }
}
