using Dapper.Contrib.Extensions;

namespace AirCombatAuswertung.Model
{
    public class Pilot
    {
        [Key]
        /// <summary>
        /// AutoGenerierte StartNr des Piloten
        /// </summary>
        public decimal Startnr { get; set; }
        /// <summary>
        /// Vorname des Piloten
        /// </summary>
        public string Firstname { get; set; }
        /// <summary>
        /// Nachname des Piloten
        /// </summary>
        public string Lastname { get; set; }
        /// <summary>
        /// Nation des Piloten (3-Zeichen) z.B. AUT
        /// </summary>
        public string Nation { get; set; }
        /// <summary>
        /// Kanal der Fernsteuerung des Piloten
        /// Wird auch genutzt um Piloten bei der Startlistengenerierung zu separieren
        /// </summary>
        public float Channel { get; set; }
        /// <summary>
        /// Pilot fliegt WW2
        /// </summary>
        public bool FliesWW2 { get; set; }
        /// <summary>
        /// Pilot fliegt WW1
        /// </summary>
        public bool FliesWW1 { get; set; }
        /// <summary>
        /// Pilot fliegt EPA
        /// </summary>
        public bool FliesEPA { get; set; }
        /// <summary>
        /// Wird der Pilot bei der Startlistengenerierung als Judge berücksichtigt
        /// </summary>
        public bool IsJudge { get; set; }
    }
}