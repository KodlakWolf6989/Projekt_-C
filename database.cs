using DevExpress.XtraLayout.Customization;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Test_Licko
{
    public class Database
    {
        private string connectionString = @"Server=CHALLENGER\SQLEXPRESS;Database=Rejstrik;Trusted_Connection=True;User Id=Challanger\licko;Password=''";

        // Vložení nové osoby do databáze
        public bool insertPerson(string name, string surname, DateTime date, string phone, string email)
        {
            string query = "INSERT INTO fyzickeOsoby (name, surname, date, phone, email) VALUES (@name, @surname, @date, @phone, @email)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@surname", surname);
                command.Parameters.AddWithValue("@date", date);
                command.Parameters.AddWithValue("@phone", phone);
                command.Parameters.AddWithValue("@email", email);

                connection.Open();
                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    Console.WriteLine("Záznam byl úspěšně vložen.");
                    MessageBox.Show("Záznam byl úspěšně vložen.");
                    return true;
                } else{
                    Console.WriteLine("Záznam se nepodařilo vložit");
                    MessageBox.Show("Záznam se nepodařilo vložit");
                    return false;
                }
            }
        }

        // Vložení nové firmy do databáze
        public bool insertCompany(string name, string ico, string phone, string email)
        {
            string query = "INSERT INTO firmy (name, ico, phone, email) VALUES (@name, @ico, @phone, @email)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@ico", ico);
                command.Parameters.AddWithValue("@phone", phone);
                command.Parameters.AddWithValue("@email", email);

                connection.Open();
                int result = command.ExecuteNonQuery();

                 if (result > 0)
                {
                    Console.WriteLine("Záznam byl úspěšně vložen.");
                    MessageBox.Show("Záznam byl úspěšně vložen.");
                    return true;
                } else{
                    Console.WriteLine("Záznam se nepodařilo vložit");
                    MessageBox.Show("Záznam se nepodařilo vložit");
                    return false;
                }
            }
        }

        // Načtení všech osob z databáze
        public List<Person> loadDataPersons()
        {
            List<Person> osoby = new List<Person>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT ID, Name, Surname, Date, Phone, Email FROM fyzickeOsoby";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            osoby.Add(new Person
                            {
                                ID = (int)reader["ID"],
                                Name = (string)reader["Name"],
                                Surname = (string)reader["Surname"],
                                Date = (DateTime)reader["Date"],
                                Phone = (string)reader["Phone"],
                                Email = (string)reader["Email"]
                            });
                        }
                    }
                }
                return osoby;
            }
        }

        // Načtení všech firem z databáze
        public List<Company> loadDataCompanies()
        {
            List<Company> firmy = new List<Company>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT ID, Name, Ico, Phone, Email FROM firmy";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            firmy.Add(new Company
                            {
                                ID = (int)reader["ID"],
                                Name = (string)reader["Name"],
                                Ico = (string)reader["Ico"],
                                Phone = (string)reader["Phone"],
                                Email = (string)reader["Email"]
                            });
                        }
                    }
                }
                return firmy;
            }
        }

        // Aktualizace údajů o osobě
        public void editPerson(int id, string name, string surname, DateTime date, string phone, string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE fyzickeOsoby SET name = @Name, surname = @Surname, date = @Date, phone = @Phone, email = @Email WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Surname", surname);
                    command.Parameters.AddWithValue("@Date", date);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@Email", email);

                    int rowsAffected = command.ExecuteNonQuery();
                    MessageBox.Show($"{rowsAffected} záznam(ů) aktualizováno.");
                }
            }
        }

        // Aktualizace údajů o firmě
        public void editCompany(int id, string name, string ico, string phone, string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE firmy SET name = @Name, ico = @Ico, phone = @Phone, email = @Email WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Ico", ico);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@Email", email);

                    int rowsAffected = command.ExecuteNonQuery();
                    MessageBox.Show($"{rowsAffected} záznam(ů) aktualizováno.");
                }
            }
        }

        // Odstranění osoby z databáze
        public void RemovePerson(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM fyzickeOsoby WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"Záznam s ID {id} byl odebrán.");
                            MessageBox.Show($"Záznam s ID {id} byl odebrán.");
                        }
                        else
                        {
                            Console.WriteLine($"Záznam s ID {id} nebyl nalezen.");
                            MessageBox.Show($"Záznam s ID {id} nebyl nalezen.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Nastala chyba: " + ex.Message);
                        MessageBox.Show("Nastala chyba.");
                    }
                }
            }
        }

        // Odstranění firmy z databáze
        public void RemoveCompany(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM firmy WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"Záznam s ID {id} byl odebrán.");
                            MessageBox.Show($"Záznam s ID {id} byl odebrán.");
                        }
                        else
                        {
                            Console.WriteLine($"Záznam s ID {id} nebyl nalezen.");
                            MessageBox.Show($"Záznam s ID {id} nebyl nalezen.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Nastala chyba: " + ex.Message);
                        MessageBox.Show("Nastala chyba.");
                    }
                }
            }
        }
    }

    // Třída pro reprezentaci osoby
    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Date { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    // Třída pro reprezentaci firmy
    public class Company
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Ico { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
