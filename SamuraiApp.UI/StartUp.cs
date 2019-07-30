using Microsoft.EntityFrameworkCore;
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
            //MultipleDatabaseOperations();
            //InsertBattle();
            //QueryAndUpdateBattle_Disconected();
            //DeleteWhileTracked();
            //AddMoreSamurais();
            //DeleteMany();
            //DeleteWhileNotTracked();
            DeleteUsingId(3);

        }

        private static void DeleteUsingId(int samuraiId) // if object doesnt exist 
        {
            // Two round trip to DB 
            // First retrieve record in memory
            var samurai = _context.Samurais.Find(samuraiId);
            _context.Remove(samurai);
            // Delete form DB
            _context.SaveChanges();
            // alternate best practice : call a store procedure!
            // _context.Database.ExecuteSqlCommand($"exec DeleteById {samuraiId}");
        }

        private static void DeleteMany()
        {
            var samurais = _context.Samurais.Where(s => s.Name.Contains("o"));
            _context.Samurais.RemoveRange(samurais);
            // alternate: _context.RemoveRange(samurais);
            _context.SaveChanges();
        }

        private static void AddMoreSamurais()
        {
            _context.AddRange(
                new Samurai { Name = "Kambei Shamda" },
                new Samurai { Name = "Shijsaoasa" },
                new Samurai { Name = "Kambej Shama" },
                new Samurai { Name = "Kuzio" },
                new Samurai { Name = "Goredio Flores" },
                new Samurai { Name = "Zshij sdioq" });
            _context.SaveChanges();
        }

        private static void DeleteWhileTracked() // connected scenario
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name == "Kambei Shamda");
            _context.Samurais.Remove(samurai);
            // some alternates:
            //_context.Remove(samurai);
            //_context.Samurais.Remove(_context.Samurais.Find(1));
            _context.SaveChanges();
        }

        private static void DeleteWhileNotTracked() // disconected scenario
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name == "Kambei Shamda");
            using (var contextNewAppInstance = new SamuraiContext())
            {
                contextNewAppInstance.Samurais.Remove(samurai);
                //contextNewAppInstance.Entry(samurai).State = EntityState.Deleted;
                contextNewAppInstance.SaveChanges();
            }
        }

        private static void InsertBattle()
        {
            _context.Battles.Add(new Battle { Name = "Battle of Azserf", StartDate = new DateTime(1580, 05, 01), EndDate = new DateTime(1560, 06, 15) });
            _context.SaveChanges();
        }

        private static void QueryAndUpdateBattle_Disconected()
        {
            var battle = _context.Battles.FirstOrDefault();
            battle.EndDate = new DateTime(1560, 06, 30);
            using (var newContextInstance = new SamuraiContext())
            {
                newContextInstance.Battles.Update(battle);
                newContextInstance.SaveChanges();
            }
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
