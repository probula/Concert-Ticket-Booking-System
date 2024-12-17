using System.Security.Cryptography;
interface IConcert
{
    string Name { get; set; }
    string Location { get; set; }
    string Date { get; set; }
    int AvailableSeats { get; set; }
    decimal TicketPrice { get; set; }
}

class Concert : IConcert
{
    public string Name {get; set;}
    public string Date {get; set;}
    public string Location {get; set;}
    public int AvailableSeats {get; set;}
    public decimal TicketPrice {get; set;}
}

class RegularConcert : Concert {}

class VIPConcert : Concert
{ 
    public string Spotkanie { get; set; }
}

class OnLineConcert : Concert
{
    public string Name { get; set; }
    public string Location { get; set; }
    public string Date { get; set; }
    public int AvailableSeats { get; set; }
    public decimal TicketPrice { get; set; }
    public string Platforma { get; set; }
}

class PrivateConcert : Concert
{
    public string Zaproszenie { get; set; }
}

class BookingSystem
{
    List<IConcert> concerts = new List<IConcert>();

    public void Dodaj()
    {
        Console.WriteLine("Ile chcesz dodać nowych koncertów?: ");
        int num = int.Parse(Console.ReadLine());

        for (int i = 0; i < num; i++)
        {
            Console.WriteLine("Jaki typ koncertu chcesz dodać?: (Regular, VIP, Online, Private): ");
            string type = Console.ReadLine().ToLower();

            IConcert concert = null;

            if (type == "regular")
            {
                concert = new RegularConcert();
            }
            else if (type == "vip")
            {
                concert = new VIPConcert();
            }
            else if (type == "online")
            {
                concert = new OnLineConcert();
            }
            else if (type == "private")
            {
                concert = new PrivateConcert();
            }

            if (concert != null)
            {
                Console.WriteLine("Podaj nazwe koncertu: ");
                concert.Name = Console.ReadLine();

                Console.WriteLine("Podaj date koncertu: ");
                concert.Date = Console.ReadLine();

                Console.WriteLine("Podaj lokalizacje koncertu: ");
                concert.Location = Console.ReadLine().ToLower();

                Console.WriteLine("Podaj ilość miejsc: ");
                concert.AvailableSeats = int.Parse(Console.ReadLine());

                Console.WriteLine("Podaj cene biletu [zl]: ");
                concert.TicketPrice = decimal.Parse(Console.ReadLine());

                if (concert is VIPConcert vipConcert)
                {
                    Console.WriteLine("Sczegóły spotkań: ");
                    vipConcert.Spotkanie = Console.ReadLine();
                }
                else if (concert is OnLineConcert onlineConcert)
                {
                    Console.WriteLine("Platforma na której się odbędzie koncert: ");
                    onlineConcert.Platforma = Console.ReadLine();
                }
                else if (concert is PrivateConcert privateConcert)
                {
                    Console.WriteLine("Specjalne zaproszenie: ");
                    privateConcert.Zaproszenie = Console.ReadLine();
                }

                concerts.Add(concert);
            }
            else
            {
                Console.WriteLine("Zły typ koncertu!");
            }
        }
    }

    public void Szukaj()
        {
            Console.WriteLine("Aby wyszukać koncert na podstawie daty, wpisz: 1 \nAby wyszukać na podstawie lokalizacji, wpisz: 2 \nAby wyszukać na podstawie ceny, wpisz: 3");
            string wybor = Console.ReadLine();

            switch (wybor)
            {
                case "1":
                    Console.WriteLine("Podaj datę w formacie DD-MM-YYYY:");
                    string data = Console.ReadLine();
                    
                    var koncertyData = concerts.Where(c => c.Date == data).ToList();

                    if (koncertyData.Any())
                    {
                        foreach (var concert in koncertyData)
                        {
                            Console.WriteLine($"Koncerty dnia {data}: \n {concert.Name} - {concert.Date} - {concert.Location}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Brak koncertow");
                    }
                    break;
                
                case "2":
                    Console.WriteLine("Podaj miejsce koncertu w formacie: Miasto, Kraj");
                    string miejsce = Console.ReadLine().ToLower();
                    
                    var koncertyLokalizacja = concerts.Where(c => c.Location == miejsce).ToList();

                    if (koncertyLokalizacja.Any())
                    {
                        foreach (var concert in koncertyLokalizacja)
                        {
                            Console.WriteLine($"Koncerty w {miejsce}: \n {concert.Name} - {concert.Date} - {concert.Location}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Brak koncertow");
                    }
                    break;
                case "3":
                    Console.WriteLine("Podaj max cene koncertu [zl]");
                    decimal cena = Decimal.Parse(Console.ReadLine());
                    
                    var koncertyCena = concerts.Where(c => c.TicketPrice <= cena).ToList();

                    if (koncertyCena.Any())
                    {
                        foreach (var concert in koncertyCena)
                        {
                            Console.WriteLine($"Koncerty w cenie max {cena}: \n {concert.Name} - {concert.Date} - {concert.Location}, cena: {concert.TicketPrice}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Brak koncertow");
                    }
                    break;
                default:
                    Console.WriteLine("Blad!");
                    break;
            }
        }
    

    public void Rezerwacja()
    {
        Console.WriteLine("Podaj nazwę koncertu, na który chcesz zarezerwować bilety:");
        string concertName = Console.ReadLine();
        foreach (var concert in concerts)
        {
            if (concert.Name == concertName)
            {
                Console.WriteLine("Podaj liczbę miejsc do zarezerwowania:");
                int seats = int.Parse(Console.ReadLine());

                if (seats <= concert.AvailableSeats)
                {
                    concert.AvailableSeats -= seats;
                    Console.WriteLine($"Zarezerwowano {seats} miejsc. Pozostało {concert.AvailableSeats} miejsc.");
                }

                if (concert.AvailableSeats <= 10)
                {
                    Console.WriteLine("Uwaga! Pozostało mniej niz 10 biletów!");
                }
                else
                {
                    Console.WriteLine("Niewystarczająca liczba miejsc.");
                }
            }
            else
            {
                Console.WriteLine("Nieprawidłowa nazwa!");
            }
        }
    }

    public void AnulujRezerwacja()
    {
        Console.WriteLine("Podaj nazwę koncertu, na który chcesz anulować rezerwację:");
        string nazwaAnulujKoncert = Console.ReadLine();

        foreach (var concert in concerts)
        {
            if (concert.Name == nazwaAnulujKoncert)
            {
                Console.WriteLine("Podaj liczbę miejsc do anulowania:");
                int miejsca = int.Parse(Console.ReadLine());

                if (miejsca > 0 && miejsca <= concert.AvailableSeats)
                {
                    concert.AvailableSeats += miejsca;
                    Console.WriteLine($"Anulowano {miejsca} miejsc. Pozostało {concert.AvailableSeats} miejsc.");
                }
                else
                {
                    Console.WriteLine("Blad!");
                }
            }
            else
            {
                Console.WriteLine("Nieprawidłowa nazwa!");
            }
        }
    }
    public void Wyswietl()
    {
        Console.WriteLine("Dostępne koncerty:");
        foreach (var concert in concerts)
        {
            if(concert is VIPConcert vipConcert){
                Console.WriteLine($"{concert.Name} - {concert.Date} w {concert.Location} ({concert.AvailableSeats} miejsc dostępnych, cena: {concert.TicketPrice}), Spotkania: {vipConcert.Spotkanie}");
            }
            else if (concert is OnLineConcert onLineConcert)
            {
                Console.WriteLine($"{concert.Name} - {concert.Date} w {concert.Location} ({concert.AvailableSeats} miejsc dostępnych, cena: {concert.TicketPrice}, Platforma: {onLineConcert.Platforma})");
            }
            else if (concert is PrivateConcert privateConcert)
            {
                Console.WriteLine($"{concert.Name} - {concert.Date} w {concert.Location} ({concert.AvailableSeats} miejsc dostępnych), cena: {concert.TicketPrice} Zaproszenie: {privateConcert.Zaproszenie}");
            }
            else if (concert is RegularConcert)
            {
                Console.WriteLine($"{concert.Name} - {concert.Date} w {concert.Location} ({concert.AvailableSeats} miejsc dostępnych, cena: {concert.TicketPrice})");
            }
            else
            {
                Console.WriteLine("Podany typ koncertu nie istnieje!");
            }
        }
    }
}
internal class Program
{
    public static void Main(string[] args)
    {
        BookingSystem bookingSystem = new BookingSystem();
        Console.WriteLine("Wybierz opcje i kliknij enter");
        while (true)
        {
            Console.WriteLine("1. Dodaj koncert\n2. Wyświetl koncerty\n3. Zarezerwuj bilet\n4. Anuluj rezerwację \n5. Filtruj koncerty \n6. Wyjście");
            string wybor = Console.ReadLine();

            switch (wybor)
            {
                case "1":
                    bookingSystem.Dodaj();
                    break;
                case "2":
                    bookingSystem.Wyswietl();
                    break;
                case "3":
                    bookingSystem.Rezerwacja();
                    break;
                case "4":
                    bookingSystem.AnulujRezerwacja();
                    break;
                case "5":
                    bookingSystem.Szukaj();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Blad!");
                    break;
            }
        }
    }
}