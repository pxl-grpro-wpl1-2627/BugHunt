namespace BugHuntWarmup
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== BUG HUNT WARM-UP ===");
                Console.WriteLine("1) Puzzel 1: Begroeting (Module 1 - Console & Syntax)");
                Console.WriteLine("2) Puzzel 2: Korting berekenen (Module 2 - Variabelen & Operatoren)");
                Console.WriteLine("3) Puzzel 3: Bioscooptarief (Module 3 - Condities)");
                Console.WriteLine("4) Puzzel 4: Voucher & Vervaldatum (Module 4 - Veelgebruikte functies)");
                Console.WriteLine("5) Puzzel 5: Aftelraket (Module 5 - Loops)");
                Console.WriteLine("6) Puzzel 6: Spaarpunten (Module 6 - Methodes)");
                Console.WriteLine("0) Afsluiten");
                Console.Write("Kies een puzzel: ");

                string choice = Console.ReadLine()

                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        RunPuzzle1();
                        break;
                    case "2":
                        RunPuzzle2();
                        break;
                    case "3":
                        RunPuzzle3();
                        break;
                    case "4":
                        RunPuzzle4();
                        break;
                    case "5":
                        RunPuzzle5();
                        break;
                    case "6":
                        RunPuzzle6();
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Ongeldige keuze.");
                        break;
                }

                if (running)
                {
                    Console.WriteLine("\nDruk op Enter om terug te keren naar het menu...");
                    Console.ReadLine();
                }
            }
        }

        // PUZZEL 1: Vraag de voornaam en woonplaats van de gebruiker en toon:
        // "Hallo <firstName> uit <city>!"
        static void RunPuzzle1()
        {
            Console.WriteLine("--- Puzzel 1: Begroeting ---");
            Console.Write("Geef je voornaam: ");
            Console.ReadLine();

            Console.Write("Geef je woonplaats: ");
            string city = Console.ReadLine();

            string firstName = "Onbekend";
            Console.WriteLine("Hallo {firstName} uit {city}!");
        }

        // PUZZEL 2: Bereken de prijs na korting én het gemiddelde per stuk voor 2 stuks.
        // Voorbeeld: basisprijs 50 en korting 25% -> korting is 12,50 -> eindprijs is 37,50 (18,75 per stuk).
        static void RunPuzzle2()
        {
            Console.WriteLine("--- Puzzel 2: Korting berekenen ---");
            Console.Write("Geef de totaalprijs voor 2 stuks (bv. 50): ");
            int totalPrice = int.Parse(Console.ReadLine());

            Console.Write("Geef het kortingspercentage (bv. 25): ");
            int discountPercentage = int.Parse(Console.ReadLine());

            double discountAmount = totalPrice * (discountPercentage / 100);
            double finalPrice = totalPrice - discountAmount;
            double pricePerItem = (int)finalPrice / 2;

            Console.WriteLine($"Korting:          € {discountAmount:F2}");
            Console.WriteLine($"Te betalen:       € {finalPrice:F2}");
            Console.WriteLine($"Prijs per stuk:   € {pricePerItem:F2}");
        }

        // PUZZEL 3: Bepaal het juiste bioscooptarief op basis van de leeftijd (0 t.e.m. 120):
        // - Jonger dan 12 jaar: € 8,00 (Kindertarief)
        // - 12 t.e.m. 64 jaar: € 12,00 (Standaardtarief)
        // - 65 jaar of ouder: € 9,00 (Seniorentarief)
        static void RunPuzzle3()
        {
            Console.WriteLine("--- Puzzel 3: Bioscooptarief ---");
            Console.Write("Geef je leeftijd (0-120): ");

            if (!int.TryParse(Console.ReadLine(), out int age) || (age < 0 && age > 120))
            {
                Console.WriteLine("Ongeldige leeftijd!");
                return;
            }

            decimal ticketPrice = 12.00m;

            if (age < 12);
            {
                ticketPrice = 8.00m;
            }

            if (age >= 12)
            {
                ticketPrice = 12.00m;
            }
            else if (age >= 65)
            {
                ticketPrice = 9.00m;
            }

            Console.WriteLine($"Leeftijd: {age} jaar -> Ticketprijs: € {ticketPrice:F2}");
        }

        // PUZZEL 4: Maak een vouchercode netjes op:
        // - verwijder spaties vooraan en achteraan
        // - zet om naar hoofdletters
        // - vervang spaties binnenin door een koppelteken '-'
        // Bereken daarnaast de vervaldatum: exact 14 dagen na vandaag.
        static void RunPuzzle4()
        {
            Console.WriteLine("--- Puzzel 4: Voucher & Vervaldatum ---");
            Console.Write("Geef een ruwe vouchercode in (bv. '  zomer actie 2026 '): ");
            string rawCode = Console.ReadLine();

            rawCode.Trim();
            rawCode.ToUpper();
            rawCode.Replace(" ", "-");

            DateTime expiryDate = DateTime.Today;
            expiryDate.AddDays(14);

            Console.WriteLine($"Opgeschoonde code: '{rawCode}'");
            Console.WriteLine($"Geldig tot en met: {expiryDate:dd/MM/yyyy}");
        }

        // PUZZEL 5: Vraag een startgetal tussen 3 en 10 (blijf herhalen bij ongeldige invoer).
        // Tel daarna af van dat startgetal tot en met 1 en sluit af met "Lancering!".
        static void RunPuzzle5()
        {
            Console.WriteLine("--- Puzzel 5: Aftelraket ---");
            Console.Write("Geef het startgetal voor het aftellen (3-10): ");
            string input = Console.ReadLine();
            int startNumber;

            while (!int.TryParse(input, out startNumber) || startNumber < 3 || startNumber > 10)
            {
                Console.Write("Ongeldig! Geef een getal tussen 3 en 10: ");
            }

            for (int i = startNumber; i > 1; i++)
            {
                Console.WriteLine($"{i}...");
            }

            Console.WriteLine("Lancering!");
        }

        // PUZZEL 6: Een klant heeft 100 spaarpunten en koopt voor een bepaald bedrag.
        // Per volledige schijf van € 10 krijgt de klant 5 extra punten.
        // Als het nieuwe puntentotaal minstens 150 is, krijgt de klant een VIP-cadeau.
        static void RunPuzzle6()
        {
            Console.WriteLine("--- Puzzel 6: Spaarpunten ---");
            int currentPoints = 100;
            Console.WriteLine($"Huidige spaarpunten: {currentPoints}");

            Console.Write("Geef het aankoopbedrag in hele euro's (bv. 120): ");
            if (int.TryParse(Console.ReadLine(), out int purchaseAmount))
            {
                AddLoyaltyPoints(currentPoints, purchaseAmount);
                Console.WriteLine($"Nieuw puntentotaal: {currentPoints}");

                bool hasGift = HasEarnedVipGift(currentPoints);
                if (hasGift)
                {
                    Console.WriteLine("Proficiat! Je hebt genoeg punten voor een VIP-cadeau!");
                }
                else
                {
                    Console.WriteLine("Nog even doorsparen voor een VIP-cadeau (minstens 150 punten nodig).");
                }
            }
        }

        static int AddLoyaltyPoints(int points, int amount)
        {
            int earnedPoints = (amount / 10) * 5;
            points = points + earnedPoints;
            return points;
        }

        static bool HasEarnedVipGift(int points)
        {
            if (points >= 150)
            {
                return false;
            }
            return true;
        }
    }
}

