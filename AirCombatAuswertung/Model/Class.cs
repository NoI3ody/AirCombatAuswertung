using Dapper.Contrib.Extensions;

namespace AirCombatAuswertung.Model
{
    public class Class
    {
        [Key]
        /// <summary>
        /// Laufende Nummer für die drei verfügbaren Klassen,
        /// 1...WW2,
        /// 2...WW1.
        /// 3...EPA
        /// </summary>
        public int Nr { get; set; }
        /// <summary>
        /// kurzer Name der Klasse (3-Zeichen)
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Langer Name der Klasse
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Zur Anwendung kommende Regeln
        /// </summary>
        public string Rules { get; set; }
        public override bool Equals(object obj)
        {
            // If the passed object is null
            if (obj == null)
            {
                return false;
            }
            if (!(obj is Class))
            {
                return false;
            }
            return (this.Nr == ((Class)obj).Nr)
                && (this.Name == ((Class)obj).Name)
                && (this.Description == ((Class)obj).Description)
                && (this.Rules == ((Class)obj).Rules);
        }
        public override int GetHashCode()
        {
            return Nr.GetHashCode() ^ Name.GetHashCode() ^ Description.GetHashCode() ^ Rules.GetHashCode();
        }
    }
}
