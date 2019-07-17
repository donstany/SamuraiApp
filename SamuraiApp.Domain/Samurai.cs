using System.Collections.Generic;

namespace SamuraiApp.Domain
{
    public class Samurai
    {
        public Samurai()
        {

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Quote> Quotes { get; set; } // navigation property One to many relation. One Samurai have many quotes
        public int BattleId { get; set; } // FK to table Battle, Id column
    }
}
