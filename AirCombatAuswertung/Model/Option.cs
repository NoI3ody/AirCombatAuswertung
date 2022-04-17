using Dapper.Contrib.Extensions;

namespace AirCombatAuswertung.Model
{
    public class Option
    {
        [Key]
        /// <summary>
        /// Name der Option/Information
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Minimumwert der Option/Information
        /// </summary>
        public int Min { get; set; }
        /// <summary>
        /// Maximumwert der Option/Information
        /// </summary>
        public int Max { get; set; }
        /// <summary>
        /// Wert der Option/Information
        /// </summary>
        public string Value { get; set; }
    }
}