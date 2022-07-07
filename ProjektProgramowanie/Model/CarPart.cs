namespace ProjektProgramowanie
{
    /// <summary>
    /// Lista właściwości przedmiotów w tabeli
    /// </summary>
    public class CarPart
    {
        public int Id { get; set; }
        public int MatchingCarId { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }
    }
}
