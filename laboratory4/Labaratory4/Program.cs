using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace labka4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Pupiltable table = new Pupiltable();
            Pupil pupil = new Pupil("Yrii", "Lukich", "10-A", 10, 8, 9);

            table.Save(pupil);

            Console.WriteLine(pupil.id);

            foreach (var item in table.GetAll())
            {
                Console.WriteLine(item);
            }

            pupil.name = "Andrii";
            table.Save(pupil);

            foreach (var item in table.GetAll())
            {
                Console.WriteLine(item);
            }

            table.Remove(pupil);

            foreach (var item in table.GetAll())
            {
                Console.WriteLine(item);
            }

            Pupil pupil1 = table.GetById(1);
            Console.WriteLine(pupil1);

            Console.WriteLine(table.GetAvg(2));

            Singleton.CloseConnection();
        }
    }
}
