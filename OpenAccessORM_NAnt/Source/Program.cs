using System;
using System.Linq;

namespace MyNantTestApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (EntitiesModel context = new EntitiesModel())
            {
                Console.WriteLine(context.People.Count());
            }
        }
    }
}
