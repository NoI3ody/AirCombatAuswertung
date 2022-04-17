using Dapper.Contrib.Extensions;

namespace AirCombatAuswertung.Model
{
    public class Judge
    {
        [Key]
        /// <summary>
        /// AutoGenerierte StartNr des externen Judges
        /// </summary>
        public decimal Startnr { get; set; }
        /// <summary>
        /// Vorname des externen Judges
        /// </summary>
        public string Firstname { get; set; }
        /// <summary>
        /// Nachname des externen Judges
        /// </summary>
        public string Lastname { get; set; }
    }
}
