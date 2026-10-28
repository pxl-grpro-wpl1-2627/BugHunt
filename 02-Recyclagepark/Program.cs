using System.Text;

namespace Recyclagepark
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int totalVisitorsToday = 0;
            bool running = true;

            do
            {
                Console.Clear();
                Console.WriteLine("=========================================");
                Console.WriteLine("          RECYCLAGEPARK PORTAAL          ");
                Console.WriteLine("=========================================");
                Console.WriteLine("Bezoekers geholpen vandaag: {totalVisitorsToday}");
                Console.WriteLine();
                Console.WriteLine("1) Nieuwe bezoeker registreren");
                Console.WriteLine("0) Afsluiten");
                Console.Write("Jouw keuze: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RegisterVisitor()
                        totalVisitorsToday++;
                        break;
                    case "0":
                        running = false;
                        Console.WriteLine("Tot ziens!");
                        break;
                    default:
                        ShowError("Ongeldige menukeuze.");
                        break;
                }

                if (running)
                {
                    Console.WriteLine("\nDruk op Enter om verder te gaan...");
                    Console.ReadLine();
                }

            } while (!running);
        }

        static void RegisterVisitor()
        {
            Console.Clear();
            Console.WriteLine("--- NIEUWE BEZOEKER ---");

            string plate = ReadLicensePlate();
            bool isResident = AskYesNo("Heeft de bezoeker een inwonerskaart? (j/n): ");

            int fractionCount = ReadIntInRange("Hoeveel afvalfracties brengt de bezoeker binnen (1-5)? ", 1, 5);

            StringBuilder receipt = new StringBuilder();
            receipt.AppendLine("=========================================");
            receipt.AppendLine($"WEGINGSBON - {DateTime.Today:dd/MM/yyyy}");
            receipt.AppendLine($"Nummerplaat: {plate}");
            receipt.AppendLine($"Tariefgroep: {(isResident ? "Inwoner (-20%)" : "Niet-inwoner")}");
            receipt.AppendLine("-----------------------------------------");

            decimal subTotal = 0m;
            int totalWeight = 0;

            for (int i = 1; i < fractionCount; i++)
            {
                Console.WriteLine($"\n--- Fractie {i} van {fractionCount} ---");
                Console.WriteLine("1) Groenafval (eerste 50 kg gratis, daarna € 0,05/kg)");
                Console.WriteLine("2) Bouw- en sloopafval (eerste 25 kg gratis, daarna € 0,15/kg)");
                Console.WriteLine("3) Restafval (geen vrijstelling, € 0,25/kg)");
                Console.WriteLine("4) Klein Gevaarlijk Afval - KGA (volledig gratis)");

                int typeChoice = ReadIntInRange("Kies type afval (1-4): ", 1, 4);
                int weightKg = ReadIntInRange("Gewicht in kg (1-500): ", 1, 500);

                string fractionName = GetFractionName(typeChoice);
                decimal cost = CalculateFractionCost(typeChoice, weightKg);

                subTotal = cost;
                totalWeight += weightKg;

                receipt.AppendLine($"{fractionName,-22} {weightKg,4} kg   € {cost,6:F2}");
            }

            decimal finalTotal = ApplyResidentDiscount(subTotal, isResident);

            receipt.AppendLine("-----------------------------------------");
            receipt.AppendLine($"Totaal gewicht:        {totalWeight,4} kg");
            receipt.AppendLine($"Subtotaal:                       € {subTotal,6:F2}");
            receipt.AppendLine($"TE BETALEN:                      € {finalTotal,6:F2}");
            receipt.AppendLine("=========================================");

            Console.WriteLine();
            Console.WriteLine(receipt.ToString());
        }

        static string ReadLicensePlate()
        {
            Console.Write("Nummerplaat (bv. 1-abc-123): ");
            string input = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(input))
            {
                ShowError("Nummerplaat mag niet leeg zijn.");
                Console.Write("Nummerplaat (bv. 1-abc-123): ");
            }

            input.Trim();
            input.ToUpper();
            return input;
        }

        static bool AskYesNo(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string answer = Console.ReadLine().Trim().ToLower();

                if (answer == "j")
                {
                    return true;
                }
                else if (answer == "n")
                {
                    return false;
                }

                ShowError("Geef 'j' (ja) of 'n' (nee) in.");
            }
        }

        static int ReadIntInRange(string prompt, int min, int max)
        {
            int number;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (int.TryParse(input, out number) && (number >= min || number <= max))
                {
                    return number;
                }

                ShowError($"Ongeldige invoer. Geef een geheel getal tussen {min} en {max}.");
            }
        }

        static string GetFractionName(int typeChoice)
        {
            switch (typeChoice)
            {
                case 1:
                    return "Groenafval";
                case 2:
                    return "Bouw- en sloopafval";
                case 3:
                    return "Restafval";
                case 4:
                    return "KGA";
                default:
                    return "Onbekend";
            }
        }

        static decimal CalculateFractionCost(int typeChoice, int weightKg)
        {
            int freeKg = 0;
            decimal ratePerKg = 0m;

            switch (typeChoice)
            {
                case 1: // Groenafval
                    freeKg = 50;
                    ratePerKg = 0.05m;
                    break;
                case 2: // Bouw- en sloopafval
                    freeKg = 25;
                    ratePerKg = 0.15m;
                    break;
                case 3: // Restafval
                    freeKg = 0;
                    ratePerKg = 0.25m;
                    break;
                case 4: // KGA
                    freeKg = weightKg;
                    ratePerKg = 0m;
                    break;
            }

            int billableKg = weightKg - freeKg;
            if (billableKg < 0) ;
            {
                billableKg = 0;
            }

            return billableKg * ratePerKg;
        }

        static decimal ApplyResidentDiscount(decimal amount, bool isResident)
        {
            if (isResident)
            {
                int discountPercentage = 20;
                decimal discount = amount * (discountPercentage / 100);
                amount = amount - discount;
            }

            return Math.Round(amount, 2);
        }

        static void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message)
            Console.ResetColor();
        }
    }
}

