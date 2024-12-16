using System.Security.Cryptography;

class Concert() : IConcert
{
    public string Name {get; set;}
    public string Date {get; set;}
    public string Location {get; set;}
    public int AvailableSeats {get; set;}
    public decimal TicketPrice {get; set;}
    
    // public virtual void tekst() 
    // {
    //     Console.WriteLine($"{Name} on {Date} at {Location}. Tickets: {AvailableSeats} available at {TicketPrice} each.");
    // }
    
}
interface IConcert
{
    string Name { get; set; }
    string Location { get; set; }
    string Date { get; set; }
    int AvailableSeats { get; set; }
    decimal TicketPrice { get; set; }
    //void tekst();
}

class RegularConcert : IConcert
{
    public string Name { get; set; }
    public string Location { get; set; }
    public string Date { get; set; }
    public int AvailableSeats { get; set; }
    public decimal TicketPrice { get; set; }

    // public void tekst()
    // {
    //     Console.WriteLine($"Regular Concert: {Name} on {Date} at {Location}. Tickets: {AvailableSeats} available at {TicketPrice} each.");
    // }
}

class VIPConcert : IConcert
{
    public string Name { get; set; }
    public string Location { get; set; }
    public string Date { get; set; }
    public int AvailableSeats { get; set; }
    public decimal TicketPrice { get; set; }
    public string Spotkanie { get; set; }

    // public void tekst()
    // {
    //     Console.WriteLine($"VIP Concert: {Name} on {Date} at {Location}. Tickets: {AvailableSeats} available at {TicketPrice} with {Spotkanie}.");
    // }
}

class OnLineConcert : IConcert
{
    public string Name { get; set; }
    public string Location { get; set; }
    public string Date { get; set; }
    public int AvailableSeats { get; set; }
    public decimal TicketPrice { get; set; }
    public string Platforma { get; set; }

    // public void tekst()
    // {
    //     Console.WriteLine($"Online Concert: {Name} on {Date}. Streaming via {Platforma}. Tickets: {AvailableSeats} available at {TicketPrice} each.");
    // }
}

class PrivateConcert : IConcert
{
    public string Name { get; set; }
    public string Location { get; set; }
    public string Date { get; set; }
    public int AvailableSeats { get; set; }
    public decimal TicketPrice { get; set; }
    public string Zaproszenie { get; set; }

    // public void tekst()
    // {
    //     Console.WriteLine($"Private Concert: {Name} w {Location} dnia {Date} trzeba posiadać: {Zaproszenie}.");
    // }
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
                concert.Location = Console.ReadLine();

                Console.WriteLine("Podaj ilość miejsc: ");
                concert.AvailableSeats = int.Parse(Console.ReadLine());

                Console.WriteLine("Podaj cene biletu: ");
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
                else
                {
                    Console.WriteLine("Niewystarczająca liczba miejsc.");
                }
            }
        }
    }

    public void Wyswietl()
    {
        Console.WriteLine("Dostępne koncerty:");
        foreach (var concert in concerts)
        {
            if(concert is VIPConcert vipConcert){
                Console.WriteLine($"{concert.Name} - {concert.Date} w {concert.Location} ({concert.AvailableSeats} miejsc dostępnych), Spotkania: {vipConcert.Spotkanie}");
            }
            else if (concert is OnLineConcert onLineConcert)
            {
                Console.WriteLine($"{concert.Name} - {concert.Date} w {concert.Location} ({concert.AvailableSeats} miejsc dostępnych, Platforma: {onLineConcert.Platforma})");
            }
            else if (concert is PrivateConcert privateConcert)
            {
                Console.WriteLine($"{concert.Name} - {concert.Date} w {concert.Location} ({concert.AvailableSeats} miejsc dostępnych), Zaproszenie: {privateConcert.Zaproszenie}");
            }
            else if (concert is RegularConcert regularConcert)
            {
                Console.WriteLine($"{concert.Name} - {concert.Date} w {concert.Location} ({concert.AvailableSeats} miejsc dostępnych)");
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
            Console.WriteLine("1. Dodaj koncert\n2. Zarezerwuj bilet\n3. Wyświetl koncerty\n4. Wyjście");
            string wybor = Console.ReadLine();

            switch (wybor)
            {
                case "1":
                    bookingSystem.Dodaj();
                    break;
                case "2":
                    bookingSystem.Rezerwacja();
                    break;
                case "3":
                    bookingSystem.Wyswietl();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Blad!");
                    break;
            }
        }
    }
}