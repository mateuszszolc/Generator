using Generator.SqlMethods;
using Generator.ViewModel;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Generator
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //OracleConnection conn = new OracleConnection(SqlConnection.connectionString);
        List<int> test = new List<int>();
        public MainWindow()
        {
            InitializeComponent();
            tableComboBox.ItemsSource = SD.tables;
            tableComboBox.SelectedIndex = 0;
            rowCountTextBox.Text = "10";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int rowCount = Convert.ToInt32(rowCountTextBox.Text);
                if(rowCount > 1000 || rowCount < 1)
                {
                    throw new ArgumentOutOfRangeException();
                }
                int clubRows;
                int foreignKeyRows;
                switch (tableComboBox.SelectedItem.ToString())
                {
                    case "Kluby":
                        SqlHelper.InsertClubsToDatabaseAsync(rowCount);
                        MessageBox.Show("Dodano rekord/y", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    case "Hale sportowe":
                        clubRows = SqlHelper.GetRowCount("id_klubu", "Kluby");
                        foreignKeyRows = SqlHelper.GetRowCount("Kluby_id_klubu", "Hale_sportowe");
                        clubRows = clubRows - foreignKeyRows;
                        if(rowCount > clubRows)
                        {
                            throw new TooMuchRowsException();
                        };
                        SqlHelper.InsertGymsToDatabaseAsync(rowCount);
                        MessageBox.Show("Dodano rekord/y", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    case "Sponsorzy":
                        SqlHelper.InsertSponsorsToDatabaseAsync(rowCount);
                        MessageBox.Show("Dodano rekord/y", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    case "Sztaby drużyn":
                        clubRows = SqlHelper.GetRowCount("id_klubu", "Kluby");
                        foreignKeyRows = SqlHelper.GetRowCount("Kluby_id_klubu", "Sztaby_druzyn");
                        clubRows = clubRows - foreignKeyRows;
                        if (rowCount > clubRows)
                        {
                            throw new TooMuchRowsException();
                        }
                        SqlHelper.InsertTeamStuffsToDatabaseAsync(rowCount);
                        MessageBox.Show("Dodano rekord/y", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    case "Adresy":
                        clubRows = SqlHelper.GetRowCount("id_klubu","Kluby");
                        foreignKeyRows = SqlHelper.GetRowCount("Kluby_id_klubu", "Adresy");
                        clubRows = clubRows - foreignKeyRows;
                        if (rowCount > clubRows)
                        {
                            throw new TooMuchRowsException();
                        }
                        SqlHelper.InsertAddressesToDatabaseAsync(rowCount);
                        MessageBox.Show("Dodano rekord/y", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    case "Zawodnicy":
                        SqlHelper.InsertPlayersToDatabaseAsync(rowCount);
                        MessageBox.Show("Dodano rekord/y", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    case "Statystyki":
                        int playerRows = SqlHelper.GetRowCount("id_zawodnika", "Zawodnicy");
                        
                       
                    
                        SqlHelper.InsertStatisticsToDatabaseAsync(rowCount);
                        MessageBox.Show("Dodano rekord/y", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    case "Zarząd hali":
                        int gymRows = SqlHelper.GetRowCount("id_hali", "Hale_sportowe");
                        foreignKeyRows = SqlHelper.GetRowCount("Hale_sportowe_id_hali", "Zarzad_hali");
                        if (rowCount > gymRows)
                        {
                            throw new TooMuchRowsException();
                        }
                        SqlHelper.InsertGymStuffsToDatabaseAsync(rowCount);
                        MessageBox.Show("Dodano rekord/y", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    case "Kolejka":
                        SqlHelper.InsertGamesToDatabaseAsync(rowCount);
                        MessageBox.Show("Dodano rekord/y", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    case "Mecze":
                        SqlHelper.InsertMatchesToDatabaseAsync(rowCount);
                        MessageBox.Show("Dodano rekord/y", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    default:
                        Console.WriteLine("Default case");
                        break;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Podana ilość rekordów powinna być liczbą całkowitą z przedziału <1, 1000>.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (TooMuchRowsException rowsException)
            {
                MessageBox.Show(rowsException.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            //catch (Exception)
            //{
            //    MessageBox.Show("Nie udało się dodać danych.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }

        private void tableComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

