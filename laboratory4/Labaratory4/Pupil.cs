using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace labaratory4
{
    internal class Pupil
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string name_class { get; set; }
        public int mark_Ukrainian { get; set; }
        public int mark_Math { get; set; }
        public int mark_History { get; set; }

        public Pupil(int id, string name, string surname, string name_class, int mark_Ukrainian, int mark_Math, int mark_History)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.name_class = name_class;
            this.mark_Ukrainian = mark_Ukrainian;
            this.mark_Math = mark_Math;
            this.mark_History = mark_History;
        }
        public Pupil(int id, string name, string surname, string name_class, int mark_Ukrainian, int mark_Math, int mark_History)
        {
            this.id = 0;
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
    
       
    }
}