using SamuraiApp.Data;
using SamuraiApp.Domain;

namespace SamuraiApp.UI
{
    public class StartUp
    {
        public static void Main()
        {
            InsertSamurai();
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
