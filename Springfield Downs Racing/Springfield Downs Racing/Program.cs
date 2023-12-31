﻿class RacingProgram
{
    const string BAR = "------------------------------------------------"; // Used for organising text
    static int balance; // Keeps track of the user's currency whilst the program is active
    static string file_name;
    static string[] dog_names = {"Raised by Wolves", "Not A Chance", "Obedience School Dropout", "Dogbiscuit", "Drool Britannia!" };
    // Dictionary holding the percentage chance each dog will win the race, and how much money that dog pays out
    static Dictionary<int, int> odds = new Dictionary<int, int>
    {{50, 6000 }, {25, 12000 }, {15, 20000 }, {8, 40000 }, {2, 200000 }};
    static int[] chances = {50, 25, 15, 8, 2 };
    static Random random = new Random();    



    // Display how much currency the user has
    static void ViewBalance()
    {
        Console.WriteLine($"You have: £{balance}");
    }



    // Display a random dialogue to build up suspension
    static void DisplayRandomDialogue(string winning_dog, string losing_dog)
    {
        int random_num = random.Next(0, 10);
        
        switch (random_num)
        {
            case 0:
                Console.WriteLine($"{losing_dog} is out like a shot! Now he turns, and explains to the others that the rabbit they're chasing " +
                    $"is not infact real.");
                Thread.Sleep(6000);
                Console.WriteLine("The dogs are humiliated... They're looking for someone to blame... I believe they've chosen me!");
                Thread.Sleep(4000);
                Console.WriteLine($"Yes, here they come! In my final moments, I'd like to award the race to {winning_dog}.");
                break;

            case 1:
                Console.WriteLine($"{losing_dog} opens up a commanding lead. He heads into the third turn, and... WHAT'S THIS?");
                Thread.Sleep(4000);
                Console.WriteLine($"It appears a little girl has fallen in love with him. Her father is filling out adoption papers, and... {losing_dog} HAS " +
                    "A HOME!!!");
                Thread.Sleep(6000);
                Console.WriteLine($"Oh, and {winning_dog} wins the race.");
                break;

            case 2:
                Console.WriteLine("In protest of dog racing's declining popularity, the racers solemnly link forelegs and cross the finish line as one.");
                Thread.Sleep(6000);
                Console.WriteLine($"Still, somebody has to win, so let's say... {winning_dog}.");
                break;

            case 3:
                Console.WriteLine($"{losing_dog} leads into the first bend.");
                Thread.Sleep(4000);
                Console.WriteLine("But other dogs are gaining fast.");
                Thread.Sleep(4000);
                Console.WriteLine($"And now Fat Tony informs me that {winning_dog} who is currently napping on the backstretch, is your winner!");
                break;

            case 4:
                Console.WriteLine($"{winning_dog} is out of the gate and wins the inaugural springfield downs 1-meter sprint!");
                break;

            case 5:
                Console.WriteLine("A stray laser light beam has wandered onto the track.");
                Thread.Sleep(4000);
                Console.WriteLine($"Hours later, {winning_dog} is the first to realise the futility of chasing the beam, and gets the victory!");
                break;

            case 6:
                Console.WriteLine($"{losing_dog} is first out of the gates.");
                Thread.Sleep(4000);
                Console.WriteLine($"Then, on the basis of a random number generated by this program, the \"race\" is awarded to {winning_dog}.");
                break;

            case 7:
                Console.WriteLine($"{losing_dog} blazes into the lead, but appears to have thrown his cat jockey.");
                Thread.Sleep(4000);
                Console.WriteLine("In fact, NONE of the dogs has a cat jockey on its back.");
                Thread.Sleep(4000);
                Console.WriteLine("There ARE cat jockeys in this sport, right?");
                Thread.Sleep(4000);
                Console.WriteLine($"Anyway, {winning_dog} wins.");
                break;

            case 8:
                Console.WriteLine($"{losing_dog} raises ten dollars.");
                Thread.Sleep(4000);
                Console.WriteLine($"{winning_dog} calls his bluff, turns over three sixes, and wins the hand!");
                Thread.Sleep(4000);
                Console.WriteLine("These dogs can really play poker!");
                break;

            case 9:
                Console.WriteLine($"As they enter the backstretch, it's {losing_dog}'s race to lose.");
                Thread.Sleep(4000);
                Console.WriteLine($"And he does. {winning_dog} is the winner!");
                break;
        }

    }

    // Picks out a winning dog and simulates the race through text. Adds coins to the user's account if they picked the winning dog
    static void SimulateBet(Dictionary<int, string> dog_bets, string betted_dog)
    {
        int random_num = random.Next(1, 101); // Generate a random number from 1-100
        int[] chance_thresholds = {50, 25, 10, 2, 0}; // The chance of a dog winning is index[0] (50) or (index[n] - index[n+1]) where n <= 3
        int winning_chance = 0;
        int losing_chance = 0;


        /* The higest chance-threshold number that the random number is higher than is the 'winning chance-threshold', meaning that
        the corresponding win-chance for that chance-threshold is the 'winning chance' meaning the dog with that win-chance is the winning dog */
        for (int index = 0; index < chance_thresholds.Length; index++)
        {
            if (random_num > chance_thresholds[index])
            {
                winning_chance = chances[index];
                random_num = random.Next(1, 5); // Generate a random number for the 'losing dog'
                index = (index + random_num) % 5; // The sum of the random number and the index of the winning-chance may be > 5 so get the modulus
                losing_chance = chances[index];
                break;
            }
        }

        // Lookup the win-chances in the dictionary of dog-bets to get the names of the winning/losing dogs
        string winning_dog = dog_bets[winning_chance];
        string losing_dog = dog_bets[losing_chance];

        DisplayRandomDialogue(winning_dog, losing_dog);

        /*
        Console.WriteLine($"As they enter the backstretch, it's {losing_dog}'s race to lose.");
        Thread.Sleep(4000); // Pause the program - gives the user time to read the text and makes the bet seem live
        Console.WriteLine($"And he does. {winning_dog} is the winner!");
        */

        Thread.Sleep(4000);
        if (betted_dog == winning_dog)
        {
            Console.WriteLine("You won!");
            balance += odds[winning_chance]; // If user picked the winning dog, retrieve the amount of coins that the dog's odds pays off and add it to the user's balance
        }
        else
        {
            Console.WriteLine("You lost.");
        }
        File.WriteAllText(file_name, balance.ToString()); // Write the user's new balance to their text file
        Thread.Sleep(4000);
        
    }



    // Randomise the list of dog bets and lets the user pick their dog
    static void Bet()
    {
        Dictionary<int, string> dog_bets = new Dictionary<int, string>();

        string dog_name = dog_names[random.Next(0, 5)]; // Pick out a random dog name
        foreach (int chance in chances)
        {
            // For each win-chance, assign it a random dog name. If the dog has already been assigned to a win-chance, pick another dog
            while (dog_bets.ContainsValue(dog_name))
            {
                dog_name = dog_names[random.Next(0, 5)];
            }
            // Add the win-chance and the dog name; each time a bet is placed, the chance of each dog winning is randomised
            dog_bets.Add(chance, dog_name);
        }

        int id = 1;
        Console.WriteLine($"{BAR}\nChoose the ID of the dog you'd like to bet on:\n{BAR}");
        foreach (KeyValuePair<int, string> bet in dog_bets){
            // Display the ID(1-5), the name of each dog and its chance of winning
            Console.WriteLine($"{id}: {bet.Value}. Odds = {bet.Key}%");
            id ++;
        }
        
        Console.WriteLine($"Your balance: £{balance}\n{BAR}");
        int choice = int.Parse(Console.ReadLine());
        string betted_dog = dog_bets[chances[choice - 1]]; // Retrieve the name of the betted dog by looking up its chance-value
        Console.WriteLine($"You chose {betted_dog}");
        balance -= 2000; // Subtract 2000 coins (price of a bet)

        Thread.Sleep(2000); // Gives the user time to read the screen
        SimulateBet(dog_bets, betted_dog);
    }



    // Allow the user to gain £2000 so they can place a bet
    static void GenerateMoney()
    {
        Console.WriteLine("Keep pressing space to earn money. Each tap will generate £10\nAlternatively, hold enter to generate the money quicker.");
        for (int i = 1; i < 201; i++) // The user needs to make £2000, and each tap generates £10, so the user needs to tap enter 200 times
        {
            Console.ReadLine();
            Console.Write($"£{i * 10} earned");
        }
        balance += 2000;
        File.WriteAllText(file_name, balance.ToString()); // Write the user's new balance to their text file
        Console.WriteLine("\nYou have generated £2000! Heading back to the main menu...");
        Thread.Sleep(4000);
    }


    // Display the main menu
    static void DisplayMenu()
    {
        string choice = "";
        // 4 is the exit option - until the user enters 4, they can use all the features of the program
        while (choice != "4") {
            Console.WriteLine(BAR);
            Console.WriteLine("---MAIN MENU---");
            Console.WriteLine("Please choose one of the following options:");
            Console.WriteLine("1: Place a bet down\n2: View your balance\n3: Generate £2000\n4 Exit the program");
            Console.WriteLine(BAR);
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    if (balance >= 2000) // A bet costs 2000 in-game currency
                    {
                        Console.WriteLine("Loading betting menu..");
                        Bet();
                    }
                    else
                    {
                        Console.WriteLine("You don't have enough money! Choose option 3 to manually generate £2000");
                    }
                    break;

                case "2":
                    Console.WriteLine("Loading balance..");
                    ViewBalance();
                    break;

                case "3":
                    Console.WriteLine("Loading money generator...");
                    GenerateMoney();
                    break;

                case "4":
                    Console.WriteLine("Exting program..");
                    break;

                default:
                    Console.WriteLine("Invalid option, returning to menu..");
                    break;
            }
        }
    }



    // Load the user's balance up or set it with a default of value 0 if the user is new
    static void LogIn()
    {
        Console.WriteLine("Please enter your username to continue:");
        string username = Console.ReadLine().ToLower();
        file_name = $"{username}.txt"; // Each user's balance is stored in a text file named after their username
        if (!(File.Exists(file_name))){
            File.WriteAllText(file_name, "0"); // If user hasn't played before, create a new text file for them with a balance of 0
        }
        balance = int.Parse(File.ReadAllText(file_name)); // Retrives the user's balance from their text file
    }

    

    public static void Main(string[] args)
    {
        Console.WriteLine("Program loading..");
        LogIn();
        DisplayMenu();
    }
}
