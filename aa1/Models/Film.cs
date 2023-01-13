namespace aa1.Models
{
    public class Film
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsOverEighteen { get; set; }
        public decimal Rating { get; set; }
        public int VotesGiven { get; set; }
    }
}