using Dapper.Contrib.Extensions;

namespace AirCombatAuswertung.Model
{
    public class Rule
    {
        [Key]
        /// <summary>
        /// Regelname
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Wert der Punktekategorie
        /// </summary>
        public double Value { get; set; }
        /// <summary>
        /// Regelbeschreibung
        /// </summary>
        public string Description { get; set; }
    }
}
