using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace labka4
{
    internal class DaiTable
    {
        private string tableName;
        public DaiTable()
        {
            tableName = "Pupil";
        }
        public Pupil GetById(int id)
        {
            Pupil pupil = null;
            SQLiteConnection conn = Singleton.GetInstance();

            using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM " + tableName + " WHERE id = @Id", conn))
            {
                SQLiteParameter param = new SQLiteParameter();
                param.ParameterName = "@Id";
                param.Value = id;

                command.Parameters.Add(param);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    pupil = new Pupil
                    {
                        id = Convert.ToInt32(reader[0].ToString()),
                        name = reader[1].ToString(),
                        surname = reader[2].ToString(),
                        name_class = reader[3].ToString(),
                        mark_Ukrainian = reader[4].ToString(),
                        mark_Math = reader[5].ToString(),
                        mark_History = reader[6].ToString(),
                    };
                }
                reader.Close();
                return pupil;
            }
        }

        public IEnumerable<Pupil> GetAll()
        {
            SQLiteConnection conn = Singleton.GetInstance();

            using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM " + tableName, conn))
            {
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Pupil pupil = new Pupil
                    {
                        id = Convert.ToInt32(reader[0].ToString()),
                        name = reader[1].ToString(),
                        surname = reader[2].ToString(),
                        name_class = reader[3].ToString(),
                        mark_Ukrainian = reader[4].ToString(),
                        mark_Math = reader[5].ToString(),
                        mark_History = reader[6].ToString(),
                    };
                    yield return pupil;
                }
                reader.Close();
            }
        }

          public int GetAvg(int x)
        {
            SQLiteConnection conn = Singleton.GetInstance();

            using (SQLiteCommand command = new SQLiteCommand("SELECT mark_Ukrainian,mark_Math,mark_History FROM " + tableName, conn))
            {
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    float mark1 = Convert.ToInt32(reader["mark_Ukrainian"]);
                    float mark2 = Convert.ToInt32(reader["mark_Math"]);
                    float mark3 = Convert.ToInt32(reader["mark_History"]);
                    float min = (mark1+mark2+mark3)/3;
                    if((mark1+mark2+mark3)/3 < min)
                    {
                        min = mark1+mark2+mark3)/3
                    }
                    return min;
                }
                return 0;
            }
        }

        public void Save(Pupil pupil)
        {
            SQLiteConnection conn = Singleton.GetInstance();

            SQLiteCommand command = null;

            if (pupil.id < 1)
            {
                using (command = new SQLiteCommand("INSERT INTO " + tableName + "(name, surname,name_class, mark_Ukrainian,  mark_Math, mark_History) " +
                    "VALUES (@name, @surname, @name_class, @mark_Ukrainian,@mark_Math,@mark_History)", conn))
                {
                    command.Parameters.AddWithValue("@name", pupil.name);
                    command.Parameters.AddWithValue("@surname", pupil.surname);
                    command.Parameters.AddWithValue("@name_class", pupil.name_class);
                    command.Parameters.AddWithValue("@mark_Ukrainian", pupil.mark_Ukrainian);
                    command.Parameters.AddWithValue("@mark_Math", pupil.mark_Math);
                    command.Parameters.AddWithValue("@mark_History", pupil.mark_History);
                    command.ExecuteNonQuery();
                    command.CommandText = "Select seq from sqlite_sequence where name = '" + tableName + "'";
                    pupil.id = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            else
            {
                using (command = new SQLiteCommand("UPDATE " + tableName +
                    " SET name = @name, surname = @surname, name_class = @name_class, mark_Ukrainian = @mark_Ukrainian,mark_Math = @mark_Math, mark_History = @mark_History" +
                    " WHERE id = @id", conn))
                {
                    command.Parameters.Add(new SQLiteParameter("@name", pupil.name));
                    command.Parameters.Add(new SQLiteParameter("@surname", pupil.surname));
                    command.Parameters.Add(new SQLiteParameter("@number_car", pupil.number_car));
                    command.Parameters.Add(new SQLiteParameter("@name_class", pupil.name_class));
                    command.Parameters.Add(new SQLiteParameter("@mark_Ukrainian", pupil.mark_Ukrainian));
                    command.Parameters.Add(new SQLiteParameter("@mark_Math", pupil.mark_Math));
                    command.Parameters.Add(new SQLiteParameter("@mark_History", pupil.mark_History));
                    command.Parameters.Add(new SQLiteParameter("@id", pupil.id));
                    command.ExecuteNonQuery();
                }
            }

        }

        public void Remove(Pupil pupil)
        {
            SQLiteConnection conn = Singleton.GetInstance();

            using (SQLiteCommand command = new SQLiteCommand("DELETE FROM " + tableName + " WHERE id = @id", conn))
            {
                command.Parameters.Add(new SQLiteParameter("@id", pupil.id));
                command.ExecuteNonQuery();
                pupil.id = 0;
            }
        }
    }
}
