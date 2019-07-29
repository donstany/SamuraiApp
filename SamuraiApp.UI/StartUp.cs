using SamuraiApp.Data;
using SamuraiApp.Domain;
using System;
using System.Linq;

namespace SamuraiApp.UI
{
    public class StartUp
    {
        private static SamuraiContext _context = new SamuraiContext(); // use only if you are absolutely sure that one method were executed at one moment
        public static void Main()
        {
            // InsertSamurai();
            // https://www.brentozar.com/archive/2017/12/can-prevent-deletes-inserts-without-clause-running/ - SQL guru 
            // InsertMultipleSamurai();
            // InsertMultipleDifferntObjects();
            //SimpleSamuraiQuery();
            //MoreQueries();
            //RetrieveAndUpdateSamurai();
            //RetrieveAndUpdateMultipleSamurai();
            MultipleDatabaseOperations();
        }

        private static void MultipleDatabaseOperations()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Name += "Hiro";
            _context.Samurais.Add(new Samurai { Name = "Kaykycho" });
            _context.SaveChanges();
        }

        private static void RetrieveAndUpdateMultipleSamurai()
        {
            var samurais = _context.Samurais.ToList();
            samurais.ForEach(s => s.Name += "San");
            _context.SaveChanges();
        }

        private static void RetrieveAndUpdateSamurai()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Name += "San";
            _context.SaveChanges();
        }

        private static void MoreQueries()
        {
            //var nameSt = "Stanislav"; // paramterized query and escape Sql parameter
            //var samurais = _context.Samurais.Where(s => s.Name == nameSt).ToList();
            //var samurais = _context.Samurais.FirstOrDefault(s => s.Name == nameSt);

            //var samurais = _context.Samurais.FirstOrDefault(s => s.Id == 2);
            //var samurais = _context.Samurais.Find(2);

            //var samuraisJ = _context.Samurais.Where(s => EF.Functions.Like(s.Name, "s%")).ToList();
            //var search = "S%";
            //var samuraisJParameter = _context.Samurais.Where(s => EF.Functions.Like(s.Name, search)).ToList();

            var name = "Stanislav";
            var lastSampson = _context.Samurais.LastOrDefault(s => s.Name == name);
        }

        private static void SimpleSamuraiQuery()
        {
            using (var context = new SamuraiContext())
            {
                var samurais = context.Samurais.ToList();
                //var query = context.Samurais;
                //var samuraisAgain = query.ToList();
                //foreach (var samurai in context.Samurais) // enumeration invoke execution but is danger when retrieve too many data, isted of that use ToList(0 first()
                //{
                //    Console.WriteLine(samurai.Name);
                //}
            }
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
