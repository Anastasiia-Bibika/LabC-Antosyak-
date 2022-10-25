using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;


//Інформація про учня складається із його імені та прізвища, назви класу, оцінок по трьом предметам.
//a. Сформувати xml-файл, який би містив інформацію щонайменше про 10 учнів.
//b. Видалити інформацію про учнів із найменшим середнім балом.
//c. Вивести статистичну інформацію про всіх студентів однофамільців. 

namespace ConsoleApp6
{
    public class Pupil
    {
        public string name { get; set; }
        public string surname { get; set; }
        public string name_class { get; set; }
        public int mark_Ukrainian { get; set; }
        public int mark_Math { get; set; }
        public int mark_History { get; set; }

        public Pupil(string name, string surname, string name_class, int mark_Ukrainian, int mark_Math, int mark_History)
        {
            this.name = name;
            this.surname = surname;
            this.name_class = name_class;
            this.mark_Ukrainian = mark_Ukrainian;
            this.mark_Math = mark_Math;
            this.mark_History = mark_History;
        }

        public Pupil()
        {

        }

        public override string ToString()
        {
            return $"Pupil: ім'я:'{name}',прізвище:{surname},клас:{name_class},оцінка з укрїнської мови:{mark_Ukrainian},оцінка з математики:{mark_Math},оцінка з історії:{mark_History}";
        }

    }
    public class Pupils
    {
        public List<Pupil> pupils = new List<Pupil>();
        public Pupils()
        {

        }
        public void Add(string name, string surname, string name_class, int mark_Ukrainian, int mark_Math, int mark_History)
        {
            pupils.Add(new Pupil(name, surname, name_class, mark_Ukrainian, mark_Math, mark_History));
        }
        public void CreatePO(string filename)
        {
            string json = JsonSerializer.Serialize(pupils);
            File.WriteAllText(filename, json);
        }
        public void ReadPO(string filename)
        {
            string json = File.ReadAllText(filename);
            this.pupils = JsonSerializer.Deserialize<List<Pupil>>(json);
        }
        public void Delete()
        {
            int minAverage = this.pupils.Min(x => (x.mark_Ukrainian + x.mark_Math + x.mark_History) / 3);
            this.pupils = this.pupils.Where(elem => (elem.mark_History + elem.mark_Math + elem.mark_Ukrainian) / 3 != minAverage).ToList();
        }
        class Program
        {
            static void Main(string[] args)
            {
                string fileName = @"D:\Проекти з Антосяком\Nastya\laboratory3\laboratory3\json1.json";

                Pupils pupils = new Pupils();
                pupils.Add("Yrii", "Lukich", "10-A", 10, 8, 9);
                pupils.Add("Anna", "Kostenko", "10-B", 12, 10, 9);
                pupils.Add("Katya", "Halus", "10-A", 11, 7, 8);
                pupils.Add("Igor", "Garbuzov", "10-B", 10, 10, 9);
                pupils.Add("Angelina", "Dmutrushn", "10-V", 9, 8, 7);
                pupils.Add("Oleksandr", "Emchuk", "10-V", 8, 10, 7);
                pupils.Add("Yuliia", "Zanko", "10-B", 12, 10, 10);
                pupils.Add("Viktor", "Kostenko", "10-V", 8, 7, 6);

                pupils.CreatePO(fileName);

                Pupils pupils2 = new Pupils();
                pupils2.ReadPO(fileName);

                pupils2.Delete();

                foreach (Pupil pupil in pupils2.pupils)
                {
                    Console.WriteLine(pupil.ToString());
                }
                Console.WriteLine();

                var task = pupils.pupils
                .GroupBy(group => $"{group.surname}")
                .Select(item => new { item.Key, Value = item.Count() });

                foreach (var item in task)
                {
                    Console.WriteLine($"Surname: {item.Key}, number of pupils: {item.Value}");
                }
                Console.WriteLine();
                Console.ReadLine();
            }
        }
    }
}
