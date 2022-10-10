namespace HuntingTheManticore
{
    internal class Program
    {
        static void Header()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;            
            Console.WriteLine(@" ___      ___    ____   ___     ___ _____________ ___ _________ _________ ________   _________ ");
            Console.WriteLine(@"|   \    /   |  /    \  |   \  |   |             |   |    __   |         |        \  |        |");
            Console.WriteLine(@"|    \  /    | /  /\  \ |    \ |   |____     ____|   |   |  |  |   ___   |   |\    | |   _____|");
            Console.WriteLine(@"|     \/     |/  /__\  \|     \|   |    |   |    |   |   |  |__|  |   |  |   |/    / |   |__   ");
            Console.WriteLine(@"|            |          |          |    |   |    |   |   |   __|  |   |  |        /  |    __|  ");
            Console.WriteLine(@"|   |\  /|   |    __    |   |\     |    |   |    |   |   |  |  |  |___|  |   |\   \  |   |____ ");
            Console.WriteLine(@"|   | \/ |   |   |  |   |   | \    |    |   |    |   |   |__|  |         |   | \   \ |        |");
            Console.WriteLine(@"|___|    |___|___|  |___|___|  \___|    |___|    |___|_________|_________|___|  \___\|________|");
            Console.ForegroundColor = ConsoleColor.White;
            EmptyRows(2);
        }

        static void EmptyRows(int rows)
        {
            for (int i = 0; i < rows; i++) Console.WriteLine();
        }

        static int Selectnumber(string player1)
        {
            
            while (true)
            {

                Header();                
                
                Console.WriteLine($"Hey there {player1}. Enter the manticores distance to the city of Consolas (between 0 and 100)");

                Console.Write("Distance: ");
                string distanceText = Console.ReadLine();
                int distance;
                bool success = Int32.TryParse(distanceText, out distance);

                if (success && distance >= 0 && distance <= 100) return distance;

                else if (success && (distance > 100 || distance < 0))
                {

                    Console.WriteLine("Your number was out of range, press any key to try again...");
                    Console.ReadKey();


                }
                else if (!success)
                {
                    Console.WriteLine("Did you enter a number? Press any key to try again...");
                    Console.ReadKey();
                }
            }
        }

        static string HitText(int manti, int guess)
        {
            if (guess > manti) return "That round OVERSHOT the target";
            else if (guess < manti) return "That round FELL SHORT of the target";
            else if (guess == manti) return "That round was a DIRECT HIT";
            else return "Error";
        }

        static bool WasTargetHit(int manti, int guess) => guess == manti ? true : false;

        static int DamageDone(int round)
        {
            if (round % 3 == 0 && round % 5 == 0) return 10;
            else if (round % 3 == 0 || round % 5 == 0) return 3;
            else return 1;
        }
        
        
        
        static void Main(string[] args)
        {
            //Variables for city current/max hp
            int cityMaxHp = 15;
            int cityCurrentHp = 15;

            //Variables for manticore current/max hp
            int mantiMaxHp = 10;
            int mantiCurrentHp = 10;

            //Distance of the manticore
            int mantiDistance;
            // Players distance - guess
            int playersGuess;

            //Keep track of current round
            int round = 1;            

            //Bool to check if target was hit
            bool isHit;

            //Player names
            string player1 = "";
            string player2 = "";

            
            
            Header();
            Console.WriteLine("Welcome to \"Hunting the Manticore\"!");
            Console.Write("Player one, please enter your name: ");
            player1 = Console.ReadLine();

            Header();
            Console.Write("Player two, please enter your name: ");
            player2 = Console.ReadLine();

            //Player 1 get to pick a distance to the Manticore.
            mantiDistance =Selectnumber(player1);


            Header();
            Console.WriteLine($"{player2}, the time has come for you to hunt down the Manticore. Press any key to start hunting...");
            Console.ReadKey();            
            Header();

            while (cityCurrentHp > 0 && mantiCurrentHp > 0)
            {
                EmptyRows(1);
                
                Console.WriteLine($"STATUS: Round: {round}  City: {cityCurrentHp}/{cityMaxHp}  Manticore: {mantiCurrentHp}/{mantiMaxHp} ");
                Console.WriteLine($"The cannon is expected to deal {DamageDone(round)} this round");
                Console.Write("Enter desired cannon range: ");
                                
                bool isValid = Int32.TryParse(Console.ReadLine(), out playersGuess);
                if (!isValid || (playersGuess > 100 || playersGuess < 0))
                {
                    Console.WriteLine("Invalid distance. Distance need to be between 0 and 100\nPress any key to try again...");
                    Console.ReadKey();
                    continue;
                }
                Console.WriteLine(HitText(mantiDistance, playersGuess));

                isHit = WasTargetHit(mantiDistance, playersGuess);
                if (isHit)
                {
                    mantiCurrentHp -= DamageDone(round);
                }
                else
                {
                    cityCurrentHp -= 1;
                }

                

                round++;
                Console.WriteLine("--------------------------");
                
            }        
            Header();
            
            if(mantiCurrentHp<=0) Console.WriteLine($"{player2} won!");
            else if (cityCurrentHp <= 0) Console.WriteLine($"{player1} won!");
            Console.ReadKey();
        }
    }
}