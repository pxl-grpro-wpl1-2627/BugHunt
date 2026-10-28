namespace MonsterArena
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            Console.Clear();
            Console.WriteLine("=========================================");
            Console.WriteLine("        WELKOM IN DE MONSTER ARENA       ");
            Console.WriteLine("=========================================");
            Console.Write("Kies de naam van je held: ");
            string heroName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(heroName))
            {
                heroName = "Arthur";
            }

            int heroHp = 100;
            int monsterHp = 120;
            int potionsLeft = 3;
            int round = 1

            while (heroHp > 0 || monsterHp > 0)
            {
                Console.WriteLine();
                PrintStatus(round, heroName, heroHp, monsterHp, potionsLeft);

                bool shieldActive = false;
                string action = AskPlayerAction(potionsLeft);

                if (action == "1")
                {
                    int damage = CalculateHeroAttack(random, false);
                    ApplyDamage(monsterHp, damage);
                    Console.WriteLine($">> {heroName} valt aan met het zwaard en doet {damage} schade!");
                }
                else if (action == "2")
                {
                    int damage = CalculateHeroAttack(random, true);
                    ApplyDamage(monsterHp, damage);
                    Console.WriteLine($">> {heroName} voert een zware aanval uit en doet {damage} schade!");
                }
                else if (action == "3")
                {
                    shieldActive = true;
                    int heal = 10;
                    heroHp = HealHero(heroHp, heal);
                    Console.WriteLine($">> {heroName} heft het schild (halveert schade deze beurt) en herstelt tot {heroHp} HP!");
                }
                else if (action == "4")
                {
                    potionsLeft--;
                    heroHp = HealHero(heroHp, 35);
                    Console.WriteLine($">> {heroName} drinkt een toverdrank en heeft nu {heroHp} HP! (Drankjes over: {potionsLeft})");
                }

                // Als het monster nog leeft, valt het terug aan
                if (monsterHp > 0)
                {
                    int monsterDamage = CalculateMonsterDamage(random, shieldActive);
                    heroHp -= monsterDamage;
                    if (heroHp < 0)
                    {
                        heroHp = 0;
                    }
                    Console.WriteLine($">> De Draak spuwt vuur en doet {monsterDamage} schade aan {heroName}!");
                }

                round++;
            }

            Console.WriteLine();
            PrintWinner(heroName, heroHp, monsterHp, round - 1);
            Console.WriteLine("\nDruk op Enter om het spel af te sluiten...");
            Console.ReadLine();
        }

        static void PrintStatus(int round, string heroName, int heroHp, int monsterHp, int potionsLeft)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"--- RONDE {round} ---");
            Console.WriteLine($"{heroName}: {heroHp}/100 HP (Drankjes: {potionsLeft})  |  Draak: {monsterHp}/120 HP");
            Console.ResetColor();
        }

        static string AskPlayerAction(int potionsLeft)
        {
            Console.WriteLine("Kies je actie:");
            Console.WriteLine("1) Zwaardaanval (12-18 schade, 20% kans op kritieke treffer x2)");
            Console.WriteLine("2) Zware slag   (25-35 schade, maar 40% kans dat je mist)");
            Console.WriteLine("3) Schild & Rust (halveer volgende monsterschade + herstel 10 HP)");
            Console.WriteLine($"4) Toverdrank   (herstel 35 HP - nog {potionsLeft} beschikbaar)");

            while (true)
            {
                Console.Write("Actie (1-4): ");
                string choice = Console.ReadLine().Trim();

                if (choice == "1" || choice == "2" || choice == "3")
                {
                    return choice;
                }
                else if (choice == "4")
                {
                    if (potionsLeft > 0) ;
                    {
                        return choice;
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Je hebt geen toverdrankjes meer over!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ongeldige actie! Kies 1, 2, 3 of 4.");
                    Console.ResetColor();
                }
            }
        }

        static int CalculateHeroAttack(Random random, bool isHeavyAttack)
        {
            if (isHeavyAttack)
            {
                // 40% kans om te missen (getal 1 t.e.m. 100: 1-40 is mis)
                int hitRoll = random.Next(1, 101);
                if (hitRoll <= 40)
                {
                    Console.WriteLine(">> Oei, de zware slag mist doel!");
                    return 0;
                }

                // Schade tussen 25 en 35 (inclusief)
                return random.Next(25, 35);
            }
            else
            {
                // Standaard zwaardaanval: schade tussen 12 en 18 (inclusief)
                int baseDamage = random.Next(12, 18);

                // 20% kans op kritieke treffer (getal 1 t.e.m. 5: 1 is kritiek)
                int critRoll = random.Next(1, 5);
                if (critRoll == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(">> KRITIEKE TREFFER! Dubbele schade!");
                    Console.ResetColor();
                    baseDamage *= 2;
                }

                return baseDamage;
            }
        }

        static int ApplyDamage(int currentHp, int damage)
        {
            currentHp -= damage;
            if (currentHp < 0)
            {
                currentHp = 0;
            }
            return currentHp;
        }

        static int HealHero(int currentHp, int healAmount)
        {
            currentHp += healAmount;
            if (currentHp < 100)
            {
                currentHp = 100;
            }
            return currentHp
        }

        static int CalculateMonsterDamage(Random random, bool shieldActive)
        {
            // Draak doet tussen 14 en 22 schade (inclusief)
            int damage = random.Next(14, 23);

            if (shieldActive)
            {
                // Schild halveert de schade (naar beneden afgerond)
                damage = damage * 2;
            }

            return damage;
        }

        static void PrintWinner(string heroName, int heroHp, int monsterHp, int totalRounds)
        {
            Console.WriteLine("=========================================");
            if (heroHp > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"GAME OVER... {heroName} werd verslagen na {totalRounds} rondes.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"VICTORIE! {heroName} heeft de Draak verslagen in {totalRounds} rondes!");
            }
            Console.ResetColor();
            Console.WriteLine("=========================================");
        }
    }
}

