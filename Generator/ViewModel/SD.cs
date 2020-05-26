using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.ViewModel
{
    public class SD
    {
        public static List<string> tables { get; } = new List<string>() { 
            "Kluby", 
            "Hale sportowe", 
            "Sponsorzy" , 
            "Sztaby drużyn",
            "Adresy",
            "Zawodnicy",
            "Statystyki",
            "Zarząd hali",
            "Kolejka",
            "Mecze"};

        public static List<string> clubs { get; } = new List<string>()
        {
            "AKS",
            "Gwardia",
            "UKS",
            "Stal",
            "LUK",
            "Zaksa",
            "MCKiS",
            "Uni",
            "KPS",
            "Lechia",
            "Norwid",
            "Slepsk",
            "Krispol",
            "Victoria",
            "Czarni",
            "Chrobry",
            "BKS",
            "Wisla",
            "SMS",
            "Olimpia",
            "BAS"
        };

        public static List<string> people { get; } = new List<string>()
        {
            "Roman Palikot",
            "Andrzej Kaczynski",
            "Robert Szparka",
            "Arkadiusz Miedzianowski",
            "Erynfryd Nowak",
            "Leopold Konewka",
            "Dariusz Wyczesany",
            "Stanislaw Pacha",
            "Krystian Meczywor",
            "Marek Rura",
            "Cezary Porazka",
            "Lech Kowalski",
            "Adam Nowak",
            "Orest Cymbalko",
            "Apoloniusz Kwasigroch",
            "Bartosz Zur",
            "Seweryn Kielbasa",
            "Dezyderiusz Siekierka",
            "Lukasz Posepny",
            "Piotr Ptaszek",
            "Rafal Kupka",
            "Jacek Potrzeba",
            "Zenon Pucybut",
            "Mateusz Janusz",
            "Janusz Tracz",
            "Krzysztof Ibisz",
            "Krzysztof Kononowicz",
            "Rafal Lysol",
            "Adrian Stolc",
            "Max Kolonko",
            "Zbigniew Stonoga"
        };

        public static List<string> sponsors { get; } = new List<string>()
        {
            "Lotos",
            "Orlen",
            "Shell",
            "Nokia",
            "Samsung",
            "Nike",
            "Adidas",
            "Puma",
            "Reebook",
            "Colo",
            "PGE",
            "Asseco",
            "Audi",
            "Volkswagen",
            "Toyota",
            "Mercedes",
            "BMW",
            "Opel",
            "KFC",
            "Enea",
            "Rolex",
            "Dell",
            "Apple",
            "Huawei",
            "Mikasa",
            "Errea",
            "Coca Cola",
            "Pepsi"
        };

        public static List<string> gyms { get; } = new List<string>()
        {
            "Hala Sportowa",
            "Orlen Arena",
            "Hala Energia",
            "Hala MOSiR",
            "Atlas Arena",
            "Hala Azoty",
            "Stadion Narodowy",
            "Pepsi Arena",
            "Hala Stulecia",
            "ERGO Arena",
            "Stegu Arena",
            "o2 Arena",
            "Tauron Arena",
            "Spodek",
            "Luczniczka",
            "Torwar",
            "Podpromie",
            "Hala widowiskowo-sportowa",
            "Artego Arena",
            "Hala Polonia",
            "Hala Urania",
            "Netto Arena",
            "Okraglak"
        };

        public static List<string> streets { get; } = new List<string>()
        {
            "Czekoladowa",
            "Waniliowa",
            "Pistacjowa",
            "Bananowa",
            "Truskawkowa",
            "Wisniowa",
            "Karmelowa",
            "Jablkowa",
            "Arbuzowa",
            "Jagodowa",
            "Morelowa",
            "A. Mickiewicza",
            "B. Prusa",
            "L. Walesy",
            "Kollataja",
            "H. Sienkiewicza",
            "Katowicka",
            "3. Maja",
            "Rynek",
            "Koktajlowa",
            "Wojska Polskiego",
            "Wroclawska",
            "Niemodlinska",
            "Pruszkowska",
            "K. Wielkiego"
        };

        public static List<string> emails { get; } = new List<string>()
        {
            "example1@gmail.com",
            "example2@gmail.com",
            "example3@gmail.com",
            "example4@gmail.com",
            "example5@gmail.com",
            "example6@gmail.com",
            "example7@gmail.com",
            "example8@gmail.com",
            "example9@gmail.com",
            "example10@gmail.com",
            "example11@gmail.com",
            "example12@gmail.com",
            "example13@gmail.com",
            "example14@gmail.com",
            "example15@gmail.com",
            "example16@gmail.com",
            "example17@gmail.com",
            "example18@gmail.com",
            "example19@gmail.com",
            "example20@gmail.com",
            "example21@gmail.com",
            "example22@gmail.com",
            "example23@gmail.com",
            "example24@gmail.com",
            "example25@gmail.com",
        };

        public static List<string> websites { get; } = new List<string>()
        {
            "www.example.com",
            "www.example1.com",
            "www.example2.com",
            "www.example3.com",
            "www.example4.com",
            "www.example5.com",
            "www.example6.com",
            "www.example7.com",
            "www.example8.com",
            "www.example9.com",
            "www.example10.com",
            "www.example11.com",
            "www.example12.com",
            "www.example13.com",
            "www.example14.com",
            "www.example15.com",
            "www.example16.com",
            "www.example17.com",
            "www.example18.com",
            "www.example19.com",
            "www.example20.com"
        };

        public static List<string> cities { get; } = new List<string>()
        {
            "Szczebrzeszyn",
            "Gdzies tam",
            "Daleko",
            "Alfonsow",
            "Dygow",
            "Warszawa",
            "Wroclaw",
            "Opole",
            "Gdansk",
            "Katowice",
            "Rzeszow",
            "Strzelce Op.",
            "Kluczbork",
            "Wrzesnia",
            "Tomaszow Maz.",
            "Belchatow",
            "Bydgoszcz",
            "Nysa",
            "Bielsko",
            "Lublin",
            "Sulecin",
            "Czestochowa",
            "Spala",
            "Bedzin",
            "Sosnowiec",
            "Krapkowice",
            "Poznan",
            "Lodz"
        };

        public static List<string> positions = new List<string>()
        {
            "atakujacy",
            "przyjmujacy",
            "rozgrywajacy",
            "libero",
            "srodkowy",
            "uniwersalny"
        };
    }
}
