using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektProgramowanie
{
    /// <summary>
    /// Lista właściwości przedmiotów w tabeli
    /// </summary>
    public class Car
    {
        public int Id { get; set; }
        public string RegistrationNumber { get; set; }
        public string Vin { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }

        [ForeignKey("PartsFK")]
        public int PartsId { get; set; }
    }
}
