using Generator.ViewModel;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.SqlMethods
{
    public class SqlHelper
    {
        public static int GetRowCount(string columnName, string tableName)
        {
            int rowCount = 0;
            using (OracleConnection connection = new OracleConnection(SqlConnection.connectionString))
            {
                OracleCommand command = connection.CreateCommand();
                command.CommandText = "SELECT " + columnName + " FROM " + tableName;
                connection.Open();
                OracleDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    rowCount++;
                }
                connection.Close();
            }

            return rowCount;
        }

        public static List<int> GetIds(string columnName, string tableName)
        {
            List<int> ids = new List<int>();
            using (OracleConnection connection = new OracleConnection(SqlConnection.connectionString))
            {
                OracleCommand command = connection.CreateCommand();
                command.CommandText = "SELECT " + columnName +" FROM " + tableName;
                connection.Open();
                OracleDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    ids.Add(Convert.ToInt32(reader[columnName]));
                }
                connection.Close();
            }

            return ids;
        }
        public static async void InsertClubsToDatabaseAsync(int rows)
        {
            List<string> commandsToTextFile = new List<string>();
            for(int i = 0; i < rows; i++)
            {
                var commandText = "insert into Kluby (nazwa_klubu, punkty, zwyciestwa, wspolczynnik_setow, miejsce_w_tabeli) values(:nazwa_klubu, :punkty, :zwyciestwa, :wspolczynnik_setow, :miejsce_w_tabeli)";
                

                using (OracleConnection connection = new OracleConnection(SqlConnection.connectionString))
                {
                    using (OracleCommand command = new OracleCommand(commandText, connection))
                    {
                        command.Parameters.Add(new OracleParameter("nazwa_klubu", SD.clubs[RandomElements.GetRandomNumber(0, SD.clubs.Count-1)]));
                        command.Parameters.Add(new OracleParameter("punkty", RandomElements.GetRandomNumber(0, 99)));
                        command.Parameters.Add(new OracleParameter("zwyciestwa", RandomElements.GetRandomNumber(0, 27)));
                        command.Parameters.Add(new OracleParameter("wspolczynnik_setow", RandomElements.GetRandomDouble(0.25, 5.0, 2)));
                        command.Parameters.Add(new OracleParameter("miejsce_w_tabeli", RandomElements.GetRandomNumber(1, 99)));
                        commandsToTextFile.Add("insert into Kluby (nazwa_klubu, punkty, zwyciestwa, wspolczynnik_setow, miejsce_w_tabeli) " +
                            "values(" + command.Parameters[0].Value.ToString() + ", " + command.Parameters[1].Value.ToString() + ", " + command.Parameters[2].Value.ToString() +
                            ", " + command.Parameters[3].Value.ToString() + ", " + command.Parameters[4].Value.ToString() + ")");
                        command.Connection.Open();
                        await command.ExecuteNonQueryAsync();
                        command.Connection.Close();
                    }
                }
            }
            WriteCommandsToTextFile("Kluby", commandsToTextFile);
        }

        public static async void InsertGymsToDatabaseAsync(int rows)
        {
            List<string> commandsToTextFile = new List<string>();
            List<int> clubIds = new List<int>();
            List<int> foreignKeys = new List<int>();
            foreignKeys.Clear();
            clubIds.Clear();
            clubIds = GetIds("id_klubu", "Kluby");
            foreignKeys = GetIds("Kluby_id_klubu", "Hale_sportowe");
            clubIds.RemoveAll(i => foreignKeys.Contains(i));

            for (int i = 0; i < rows; i++)
            {
                var commandText = "insert into Hale_sportowe (Kluby_id_klubu, nazwa_hali, liczba_miejsc) values(:Kluby_id_klubu, :nazwa_hali, :liczba_miejsc)";

                using (OracleConnection connection = new OracleConnection(SqlConnection.connectionString))
                {
                    using (OracleCommand command = new OracleCommand(commandText, connection))
                    {
                        command.Parameters.Add(new OracleParameter("Kluby_id_klubu", clubIds[RandomElements.GetRandomNumber(0, clubIds.Count-1)]));
                        command.Parameters.Add(new OracleParameter("nazwa_hali", SD.gyms[RandomElements.GetRandomNumber(0, SD.gyms.Count-1)]));
                        command.Parameters.Add(new OracleParameter("liczba_miejsc", Convert.ToString(RandomElements.GetRandomNumber(300, 50000))));
                        commandsToTextFile.Add("insert into Hale_sportowe (Kluby_id_klubu, nazwa_hali, liczba_miejsc) " +
                            "values(" + command.Parameters[0].Value.ToString() + ", " + command.Parameters[1].Value.ToString() + ", " + command.Parameters[2].Value.ToString() + ")");

                        command.Connection.Open();
                        await command.ExecuteNonQueryAsync();
                        command.Connection.Close();
                        clubIds.Remove((int)command.Parameters[0].Value);
                    }
                }
            }
            foreignKeys.Clear();
            clubIds.Clear();
            WriteCommandsToTextFile("Hale_sportowe", commandsToTextFile);
        }

        public static async void InsertTeamStuffsToDatabaseAsync(int rows)
        {
            List<string> commandsToTextFile = new List<string>();
            List<int> clubIds = new List<int>();
            List<int> foreignKeys = new List<int>();
            foreignKeys.Clear();
            clubIds.Clear();
            clubIds = GetIds("id_klubu", "Kluby");
            foreignKeys = GetIds("Kluby_id_klubu", "Sztaby_druzyn");
            clubIds.RemoveAll(i => foreignKeys.Contains(i));
            for (int i = 0; i < rows; i++)
            {
                var commandText = "insert into Sztaby_druzyn (Kluby_id_klubu, trener, drugi_trener, prezes, menadzer, dyrektor_sportowy) values(:Kluby_id_klubu, :trener, :drugi_trener, :prezes, :menadzer, :dyrektor_sportowy)";


                using (OracleConnection connection = new OracleConnection(SqlConnection.connectionString))
                {
                    using (OracleCommand command = new OracleCommand(commandText, connection))
                    {
                        command.Parameters.Add(new OracleParameter("Kluby_id_klubu", clubIds[RandomElements.GetRandomNumber(0, clubIds.Count-1)]));
                        command.Parameters.Add(new OracleParameter("trener", SD.people[RandomElements.GetRandomNumber(0, SD.people.Count-1)]));
                        command.Parameters.Add(new OracleParameter("drugi_trener", SD.people[RandomElements.GetRandomNumber(0, SD.people.Count-1)]));
                        command.Parameters.Add(new OracleParameter("prezes", SD.people[RandomElements.GetRandomNumber(0, SD.people.Count-1)]));
                        command.Parameters.Add(new OracleParameter("menadzer", SD.people[RandomElements.GetRandomNumber(0, SD.people.Count-1)]));
                        command.Parameters.Add(new OracleParameter("dyrektor_sportowy", SD.people[RandomElements.GetRandomNumber(0, SD.people.Count-1)]));                    
                        commandsToTextFile.Add("insert into Sztaby_druzyn (Kluby_id_klubu, trener, drugi_trener, prezes, menadzer, dyrektor_sportowy) " +
                            "values(" + command.Parameters[0].Value.ToString() + ", " + command.Parameters[1].Value.ToString() + ", " + command.Parameters[2].Value.ToString() +
                             ", " + command.Parameters[3].Value.ToString() + ", " + command.Parameters[4].Value.ToString() + ", " + command.Parameters[5].Value.ToString() + ")");

                        command.Connection.Open();
                        await command.ExecuteNonQueryAsync();
                        command.Connection.Close();
                        clubIds.Remove((int)command.Parameters[0].Value);
                    }
                }
            }
            WriteCommandsToTextFile("Sztaby_druzyn", commandsToTextFile);
            clubIds.Clear();
            foreignKeys.Clear();
        }

        public static async void InsertAddressesToDatabaseAsync(int rows)
        {
            List<string> commandsToTextFile = new List<string>();
            List<int> clubIds = new List<int>();
            List<int> foreignKeys = new List<int>();
            clubIds.Clear();
            foreignKeys.Clear();
            clubIds = GetIds("id_klubu", "Kluby");
            foreignKeys = GetIds("Kluby_id_klubu", "Adresy");
            clubIds.RemoveAll(i => foreignKeys.Contains(i));
            for (int i = 0; i < rows; i++)
            {
                var commandText = "insert into Adresy (Kluby_id_klubu, kod_pocztowy, ulica, miejscowosc,nr_budynku, nr_lokalu, strona_internetowa, email, telefon) values(:Kluby_id_klubu, :kod_pocztowy, :ulica, :miejscowosc, :nr_budynku, :nr_lokalu, :strona_internetowa, :email, :telefon)";


                using (OracleConnection connection = new OracleConnection(SqlConnection.connectionString))
                {
                    using (OracleCommand command = new OracleCommand(commandText, connection))
                    {
                        command.Parameters.Add(new OracleParameter("Kluby_id_klubu", clubIds[RandomElements.GetRandomNumber(0, clubIds.Count-1)]));
                        command.Parameters.Add(new OracleParameter("kod_pocztowy", Convert.ToString(RandomElements.GetRandomNumber(10000, 99999))));
                        command.Parameters.Add(new OracleParameter("ulica", SD.streets[RandomElements.GetRandomNumber(0, SD.streets.Count-1)]));
                        command.Parameters.Add(new OracleParameter("miejscowosc", SD.cities[RandomElements.GetRandomNumber(0, SD.cities.Count-1)]));
                        command.Parameters.Add(new OracleParameter("nr_budynku", RandomElements.GetRandomNumber(1, 999)));
                        command.Parameters.Add(new OracleParameter("nr_lokalu", RandomElements.GetRandomNumber(1, 99)));
                        command.Parameters.Add(new OracleParameter("strona_internetowa", SD.websites[RandomElements.GetRandomNumber(0, SD.websites.Count-1)]));
                        command.Parameters.Add(new OracleParameter("email", SD.emails[RandomElements.GetRandomNumber(0, SD.emails.Count-1)]));
                        command.Parameters.Add(new OracleParameter("telefon", Convert.ToString(RandomElements.GetRandomNumber(100000000, 999999999))));
                        commandsToTextFile.Add("insert into Adresy (Kluby_id_klubu, kod_pocztowy, ulica, miejscowosc,nr_budynku, nr_lokalu, strona_internetowa, email, telefon) " +
                            "values(" + command.Parameters[0].Value.ToString() + ", " + command.Parameters[1].Value.ToString() + ", " + command.Parameters[2].Value.ToString() +
                             ", " + command.Parameters[3].Value.ToString() + ", " + command.Parameters[4].Value.ToString() + ", " + command.Parameters[5].Value.ToString()
                             + ", " + command.Parameters[6].Value.ToString() + ", " + command.Parameters[7].Value.ToString() + ", " + command.Parameters[8].Value.ToString() + ")");

                        command.Connection.Open();
                        await command.ExecuteNonQueryAsync();
                        command.Connection.Close();
                        clubIds.Remove((int)command.Parameters[0].Value);
                    }
                }
            }
            WriteCommandsToTextFile("Adresy", commandsToTextFile);
            clubIds.Clear();
            foreignKeys.Clear();
        }

        public static async void InsertPlayersToDatabaseAsync(int rows)
        {
            List<string> commandsToTextFile = new List<string>();
            List<int> clubIds = new List<int>();
            clubIds.Clear();
            clubIds = GetIds("id_klubu", "Kluby");
            for (int i = 0; i < rows; i++)
            {
                var commandText = "insert into Zawodnicy (imie, nazwisko, data_urodzenia, Kluby_id_klubu, wzrost, waga, pozycja, numer, zasieg) values(:imie, :nazwisko, TO_DATE(:data_urodzenia,'DD/MM/YYYY'), :Kluby_id_klubu, :wzrost, :waga, :pozycja, :numer, :zasieg)";


                using (OracleConnection connection = new OracleConnection(SqlConnection.connectionString))
                {
                    using (OracleCommand command = new OracleCommand(commandText, connection))
                    {
                        command.Parameters.Add(new OracleParameter("imie", SD.people[RandomElements.GetRandomNumber(0, SD.people.Count-1)].Split(' ').First()));
                        command.Parameters.Add(new OracleParameter("nazwisko", SD.people[RandomElements.GetRandomNumber(0, SD.people.Count-1)].Split(' ')[1]));
                        command.Parameters.Add(new OracleParameter("data_urodzenia", RandomElements.GetRandomDate(1970, 2000, "d")));
                        command.Parameters.Add(new OracleParameter("Kluby_id_klubu", clubIds[RandomElements.GetRandomNumber(0, clubIds.Count-1)]));
                        command.Parameters.Add(new OracleParameter("wzrost", RandomElements.GetRandomNumber(170, 215)));
                        command.Parameters.Add(new OracleParameter("waga", RandomElements.GetRandomNumber(70, 115)));
                        command.Parameters.Add(new OracleParameter("pozycja", SD.positions[RandomElements.GetRandomNumber(0, SD.positions.Count-1)]));
                        command.Parameters.Add(new OracleParameter("numer", RandomElements.GetRandomNumber(1, 99)));
                        command.Parameters.Add(new OracleParameter("zasieg", RandomElements.GetRandomNumber(300, 385)));
                        commandsToTextFile.Add("insert into Zawodnicy (imie, nazwisko, data_urodzenia, Kluby_id_klubu, wzrost, waga, pozycja, numer, zasieg) " +
                            "values(" + command.Parameters[0].Value.ToString() + ", " + command.Parameters[1].Value.ToString() + ", TO_DATE(" + command.Parameters[2].Value.ToString() +
                             ", DD/MM/YYYY), " + command.Parameters[3].Value.ToString() + ", " + command.Parameters[4].Value.ToString() + ", " + command.Parameters[5].Value.ToString()
                             + ", " + command.Parameters[6].Value.ToString() + ", " + command.Parameters[7].Value.ToString() + ", " + command.Parameters[8].Value.ToString() + ")");

                        command.Connection.Open();
                        await command.ExecuteNonQueryAsync();
                        command.Connection.Close();
                    }
                }
            }
            WriteCommandsToTextFile("Zawodnicy", commandsToTextFile);
            clubIds.Clear();
        }

        public static async void InsertStatisticsToDatabaseAsync(int rows)
        {
            List<string> commandsToTextFile = new List<string>();
            List<int> playerIds = new List<int>();
            playerIds.Clear();
            playerIds = GetIds("id_zawodnika", "Zawodnicy");
            for (int i = 0; i < rows; i++)
            {
                var commandText = "insert into Statystyki (Zawodnicy_id_zawodnika, liczba_sezonow, liczba_meczow, punkty, asy_serwisowe, bloki, skutecznosc) values(:Zawodnicy_id_zawodnika, :liczba_sezonow, :liczba_meczow, :punkty, :asy_serwisowe, :bloki, :skutecznosc)";


                using (OracleConnection connection = new OracleConnection(SqlConnection.connectionString))
                {
                    using (OracleCommand command = new OracleCommand(commandText, connection))
                    {
                        command.Parameters.Add(new OracleParameter("Zawodnicy_id_zawodnika", playerIds[RandomElements.GetRandomNumber(0, playerIds.Count-1)]));
                        command.Parameters.Add(new OracleParameter("liczba_sezonow", RandomElements.GetRandomNumber(1, 15)));
                        command.Parameters.Add(new OracleParameter("liczba_meczow", RandomElements.GetRandomNumber(10, 200)));
                        command.Parameters.Add(new OracleParameter("punkty", RandomElements.GetRandomNumber(10, 500)));
                        command.Parameters.Add(new OracleParameter("asy_serwisowe", RandomElements.GetRandomNumber(1, 70)));
                        command.Parameters.Add(new OracleParameter("bloki", RandomElements.GetRandomNumber(1, 60)));
                        command.Parameters.Add(new OracleParameter("skutecznosc", RandomElements.GetRandomDouble(30.0, 80.0, 1)));
                        commandsToTextFile.Add("insert into Statystyki (Zawodnicy_id_zawodnika, liczba_sezonow, liczba_meczow, punkty, asy_serwisowe, bloki, skutecznosc) " +
                            "values(" + command.Parameters[0].Value.ToString() + ", " + command.Parameters[1].Value.ToString() + ", " + command.Parameters[2].Value.ToString() +
                             ", " + command.Parameters[3].Value.ToString() + ", " + command.Parameters[4].Value.ToString() + ", " + command.Parameters[5].Value.ToString()
                             + ", " + command.Parameters[6].Value.ToString() + ")");

                        command.Connection.Open();
                        await command.ExecuteNonQueryAsync();
                        command.Connection.Close();
                        playerIds.Remove((int)command.Parameters[0].Value);
                    }
                }
            }
            WriteCommandsToTextFile("Statystyki", commandsToTextFile);
            playerIds.Clear();
        }

        public static async void InsertGymStuffsToDatabaseAsync(int rows)
        {
            List<string> commandsToTextFile = new List<string>();
            List<int> gymIds = new List<int>();
            List<int> foreignKeys = new List<int>();
            gymIds.Clear();
            foreignKeys.Clear();
            gymIds = GetIds("id_hali", "Hale_sportowe");
            foreignKeys = GetIds("Hale_sportowe_id_hali", "Zarzad_hali");
            gymIds.RemoveAll(i => foreignKeys.Contains(i));
            for (int i = 0; i < rows; i++)
            {
                var commandText = "insert into Zarzad_hali (Hale_sportowe_id_hali, wozny, kierownik) values(:Hale_sportowe_id_hali, :wozny, :kierownik)";

                using (OracleConnection connection = new OracleConnection(SqlConnection.connectionString))
                {
                    using (OracleCommand command = new OracleCommand(commandText, connection))
                    {
                        command.Parameters.Add(new OracleParameter("Hale_sportowe_id_hali", gymIds[RandomElements.GetRandomNumber(0, gymIds.Count-1)]));
                        command.Parameters.Add(new OracleParameter("wozny", SD.people[RandomElements.GetRandomNumber(0, SD.people.Count-1)]));
                        command.Parameters.Add(new OracleParameter("kierownik", SD.people[RandomElements.GetRandomNumber(0, SD.people.Count-1)]));
                        commandsToTextFile.Add("insert into Zarzad_hali (Hale_sportowe_id_hali, wozny, kierownik) " +
                            "values(" + command.Parameters[0].Value.ToString() + ", " + command.Parameters[1].Value.ToString() + ", " + command.Parameters[2].Value.ToString() + ")");

                        command.Connection.Open();
                        await command.ExecuteNonQueryAsync();
                        command.Connection.Close();
                        gymIds.Remove((int)command.Parameters[0].Value);
                    }
                }
            }
            WriteCommandsToTextFile("Zarzad_hali", commandsToTextFile);
            gymIds.Clear();
            foreignKeys.Clear();
        }

        public static async void InsertGamesToDatabaseAsync(int rows)
        {
            List<string> commandsToTextFile = new List<string>();
            for (int i = 0; i < rows; i++)
            {
                var commandText = "insert into Kolejka (numer_kolejki) values(:numer_kolejki)";

                using (OracleConnection connection = new OracleConnection(SqlConnection.connectionString))
                {
                    using (OracleCommand command = new OracleCommand(commandText, connection))
                    {
                        command.Parameters.Add(new OracleParameter("numer_kolejki", RandomElements.GetRandomNumber(1, 99)));
                        commandsToTextFile.Add("insert into Kolejka (numer_kolejki) " +
                            "values(" + command.Parameters[0].Value.ToString() + ")");

                        command.Connection.Open();
                        await command.ExecuteNonQueryAsync();
                        command.Connection.Close();
                    }
                }
            }
            WriteCommandsToTextFile("Kolejka", commandsToTextFile);

        }

        public static async void InsertSponsorsToDatabaseAsync(int rows)
        {
            List<string> commandsToTextFile = new List<string>();
            List<int> clubIds = new List<int>();
            clubIds.Clear();
            clubIds = GetIds("id_klubu", "Kluby");
            for (int i = 0; i < rows; i++)
            {
                var commandText = "insert into Sponsorzy (nazwa_sponsora, Kluby_id_klubu, wklad_pieniezny) values(:nazwa_sponsora, :Kluby_id_klubu, :wklad_pieniezny)";

                using (OracleConnection connection = new OracleConnection(SqlConnection.connectionString))
                {
                    using (OracleCommand command = new OracleCommand(commandText, connection))
                    {
                        command.Parameters.Add(new OracleParameter("nazwa_sponsora", SD.sponsors[RandomElements.GetRandomNumber(0, SD.sponsors.Count-1)]));
                        command.Parameters.Add(new OracleParameter("Kluby_id_klubu", clubIds[RandomElements.GetRandomNumber(0, clubIds.Count-1)]));                       
                        command.Parameters.Add(new OracleParameter("wklad_pieniezny", RandomElements.GetRandomNumber(20000, 900000)));
                        commandsToTextFile.Add("insert into Sponsorzy (nazwa_sponsora, Kluby_id_klubu, wklad_pieniezny) " +
                            "values(" + command.Parameters[0].Value.ToString() + ", " + command.Parameters[1].Value.ToString() + ", " + command.Parameters[2].Value.ToString() + ")");

                        command.Connection.Open();
                        await command.ExecuteNonQueryAsync();
                        command.Connection.Close();
                    }
                }
            }
            WriteCommandsToTextFile("Sponsorzy", commandsToTextFile);
            clubIds.Clear();
        }

        public static async void InsertMatchesToDatabaseAsync(int rows)
        {
            List<string> commandsToTextFile = new List<string>();
            List<int> clubIds = new List<int>();
            List<int> gameIds = new List<int>();

            clubIds.Clear();
            gameIds.Clear();

            clubIds = GetIds("id_klubu", "Kluby");
            gameIds = GetIds("id_kolejki", "Kolejka");

            for (int i = 0; i < rows; i++)
            {
                var commandText = "insert into Mecze (Kolejka_id_kolejki, Kluby_id_klubu, id_gosc, data_i_godzina, wynik) values(:Kolejka_id_kolejki, :Kluby_id_klubu, :id_gosc, :data_i_godzina, :wynik)";

                using (OracleConnection connection = new OracleConnection(SqlConnection.connectionString))
                {
                    using (OracleCommand command = new OracleCommand(commandText, connection))
                    {
                        command.Parameters.Add(new OracleParameter("Kolejka_id_kolejki", gameIds[RandomElements.GetRandomNumber(0, gameIds.Count-1)]));
                        command.Parameters.Add(new OracleParameter("Kluby_id_klubu", clubIds[RandomElements.GetRandomNumber(0, clubIds.Count-1)]));
                        command.Parameters.Add(new OracleParameter("id_gosc", clubIds[RandomElements.GetRandomNumber(0, clubIds.Count-1)]));
                        command.Parameters.Add(new OracleParameter("data_i_godzina", RandomElements.GetRandomDate(2018, 2021, "yyyy-MM-dd HH:mm:ss")));
                        command.Parameters.Add(new OracleParameter("wynik", Convert.ToString(RandomElements.GetRandomResult())));
                        commandsToTextFile.Add("insert into Mecze (Kolejka_id_kolejki, Kluby_id_klubu, id_gosc, data_i_godzina, wynik) " +
                            "values(" + command.Parameters[0].Value.ToString() + ", " + command.Parameters[1].Value.ToString() + ", " + command.Parameters[2].Value.ToString() + ", " + command.Parameters[3].Value.ToString() + ", " + command.Parameters[4].Value.ToString() + ")");

                        command.Connection.Open();
                        await command.ExecuteNonQueryAsync();
                        command.Connection.Close();
                        
                    }
                }
            }
            WriteCommandsToTextFile("Mecze", commandsToTextFile);
            clubIds.Clear();
        }

        private static void WriteCommandsToTextFile(string tableName, List<string> inserts)
        {
           string fileName = tableName + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
           File.WriteAllLines(fileName, inserts);
        }


    }
}
