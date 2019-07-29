using System;
using SamuraiApp.Data;
using SamuraiApp.Domain;

namespace SamuraiApp.UI
{
    public class StartUp
    {
        public static void Main()
        {
            // InsertSamurai();
            // https://www.brentozar.com/archive/2017/12/can-prevent-deletes-inserts-without-clause-running/ - SQL guru 
            InsertMultipleSamurai();
            InsertMultipleDifferntObjects();
        }

        private static void InsertMultipleDifferntObjects()
        {
            var samurai = new Samurai { Name = "Super Samurai" };
            var battle = new Battle
            {
                Name = "Battle of Nagashino",
                StartDate = new DateTime(1575, 10, 10),
                EndDate = new DateTime(1575, 10, 15)
            };

            using (var context = new SamuraiContext())
            {
                context.AddRange(samurai, battle); // automatically recognize entities
                context.SaveChanges();
            }
        }

        private static void InsertMultipleSamurai()
        {
            var samurai = new Samurai { Name = "Stanislav" };
            var samuraiSamy = new Samurai { Name = "Sami" };
            using (var context = new SamuraiContext())
            {
                context.Samurais.AddRange(samurai, samuraiSamy);
                // context.Add(samurai);
                context.SaveChanges();
            }
        }

        private static void InsertSamurai()
        {
            var samurai = new Samurai { Name = "Stanislav" };
            using (var context = new SamuraiContext())
            {
                context.Samurais.Add(samurai);
                // context.Add(samurai);
                context.SaveChanges();
            }
        }
    }
}
