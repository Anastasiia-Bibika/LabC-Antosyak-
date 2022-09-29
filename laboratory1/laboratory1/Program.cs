using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ConsoleApp5
{
    public class Passengers
    {
        protected int id;
        public int Id { get { return id; } set { id = value; } }
        protected string surname;
        public string Surname { get { return surname; } set { surname = value; } }
        protected string name;
        public string Name { get { return name; } set { name = value; } }
        protected string patronymic;
        public string Patronymic { get { return patronymic; } set { patronymic = value; } }
        protected string destination;

        public string Destination { get { return destination; } set { destination = value; } }
        protected int numberseatslugg;
        public int NumberSeatsLugg { get { return numberseatslugg; } set { numberseatslugg = (value >= 0 ? value : 0); } }
        protected int totalweightbaggage;
        public int TotalWeightBaggage { get { return totalweightbaggage; } set { totalweightbaggage = (value >= 0 ? value : 0); } }

        public Passengers(int id,string surname, string name, string patronymic, string destination, int numberseatslugg, int totalweightbaggage)
        {
            this.id = id;
            this.surname = surname;
            this.name = name;
            this.patronymic = patronymic;
            this.destination = destination;
            this.numberseatslugg = numberseatslugg;
            this.totalweightbaggage = totalweightbaggage;
        }
        public override string ToString()
        {
            return $" Surname: {surname} Name: {name}  Patronymic: {patronymic} Destination: {destination}  Numberseatslugg: {numberseatslugg}  TotalWeightbaggage: {totalweightbaggage}";
        }
    }
    public interface IListPassengers
    {
        void Add(Passengers passengers);
        void Delete(int id);
        void EditDestination(int id,string destination);
        void EditNumberseats(int id,int numberseatslugg);
        void Show();
    }
    public class ListPassenger : IListPassengers
    {
        protected List<Passengers> passengers;
        public List<Passengers> Passengers { get { return passengers; } set { passengers = value; } }
        public ListPassenger(List<Passengers> passengers)
        {
            this.passengers = passengers;
        }
        public void Add(Passengers passenger)
        {
            passengers.Add(passenger);
        }
        public void Delete(int id)
        {
            try
            {
                passengers = passengers.Where(item => item.Id != id).ToList();
            }
            catch (Exception exexception)
            {
                Console.WriteLine(exexception.Message);
                Console.WriteLine("Something wrong,please check the id.");
            }
        }
        public void EditDestination(int id, string destination)
        {
            try
            {
                passengers.First(item => item.Id == id).Destination = destination;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.WriteLine("Something wrong,please check the id.");
            }
        }
        public void EditNumberseats(int id, int numberseatslugg)
        {
            try
            {
                passengers.First(item => item.Id == id).NumberSeatsLugg = numberseatslugg;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.WriteLine("Something wrong,please check the id.");
            }
        }
        public void Show()
        {
            foreach (Passengers passenger in passengers)
            {
                Console.WriteLine(passenger);
            }
            Console.WriteLine();
        }
    }
        class Program
    {
        static void Main(string[] args)
        {
            //Список “Пасажир” (прізвище, ім'я, по-батькові; пункт призначення; кількість місць багажу; загальна вага даного багажу).
            //Вивести загальну кількість місць багажу і загальну вагу багажу пасажирів, які слідують у пункту призначення Х, де Х вводиться з клавіатури.
            //Для кожного пункту призначення вивести інформацію про кількість пасажирів, які слідують у відповідний пункт.
            ListPassenger passengers = new ListPassenger(new List<Passengers>
            {
                new Passengers(1,"Kovach", "Ivan", "Myhaylovych","Polish",3,45),
                new Passengers(2,"Halus", "Irina", "Andriivna","Slovak",4,50),
                new Passengers(3,"Babik", "Dmitriy", "Ruslanovich","Polish",2, 40),
                new Passengers(4,"Klumenko", "Igor", "Petrovich","Romanian",4,65),
                new Passengers(5,"Malyar", "Anna", "Ivanivna","Slovak",5,70),
               });
            passengers.Show();
            passengers.Add(new Passengers(5, "Melnyk", "Lubov", "Myhailivna", "Romanian", 4, 65));
            passengers.Show();
            passengers.Delete(3);
            passengers.Delete(9);
            passengers.Show();
            passengers.EditDestination(2, "Polish");
            passengers.Show();
            passengers.EditNumberseats(5,2);
            passengers.Show();
            passengers.EditDestination(7, "Aboba");
            passengers.EditNumberseats(12, 87);
            Console.Write("Пункт призначення:  ");
            var destination = Convert.ToString(Console.ReadLine());
            var task = passengers.Passengers.Where(item => item.Destination == destination);
            var kbaggage = 0;
            var totalmbaggage = 0;
            foreach (var x in task)
            {
                if(x.Destination == destination)
                {
                    kbaggage =+ x.NumberSeatsLugg;
                    totalmbaggage = +x.TotalWeightBaggage;
                }
            }
            Console.WriteLine("Загальна кількість місць багажу: {0}", kbaggage);
            Console.WriteLine("Загальна вага багажу: {0}", totalmbaggage);
            var passegdestination = passengers.Passengers.GroupBy(group => group.Destination).Select(item => new { item.Key, Value = item.Count() });
            foreach ( var num in  passegdestination)
            {
                Console.WriteLine($"Destination: {num.Key}, number of passengers: {num.Value}");
            }
        }
    }
}