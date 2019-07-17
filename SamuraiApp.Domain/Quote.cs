namespace SamuraiApp.Domain
{
    public class Quote
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Samurai Samurai { get; set; } // Navigation property.  Many Quotes have one Samurai
        public int SamuraiId { get; set; } // FK to table Samurai, Id column
    }
}