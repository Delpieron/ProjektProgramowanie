using System;

namespace ProjektProgramowanie.Model
{
    public class CarVo
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Vin { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public string RegistrationNumber { get; set; }
        public string OwnerName { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
