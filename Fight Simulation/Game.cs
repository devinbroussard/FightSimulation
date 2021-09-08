using System;
using System.Collections.Generic;
using System.Text;

namespace Fight_Simulation
{
    //struct that creates the Monster type
    public struct Monster
    {
        public string name;
        public float health;
        public float attack;
        public float defense;
    }

    class Game
    {
        //initializes monsters and variables
        public Monster wompus;
        public Monster thwompus;
        public Monster backupWompus;
        public Monster unclePhil;

        //Initializing
        bool gameOver = false;
        Monster currentMonsterOne;
        Monster currentMonsterTwo;
        int currentMonsterIndex = 0;
        int currentScene = 0;
        Monster[] monsters;

        /// <summary>
        /// Function that gets and returns user data
        /// </summary>
        /// <param name="description"></param>
        /// <param name="optionOne"></param>
        /// <param name="optionTwo"></param>
        /// <param name="pauseInvalid"></param>Optional argument that calls a ReadKey after invalids on true
        /// <returns>A number representing the selected choice</returns>
        int GetInput(string description, string optionOne, string optionTwo, bool pauseInvalid = false)
        {
            //Displays options and gets player input
            Console.WriteLine(description);
            Console.WriteLine("1. " + optionOne);
            Console.WriteLine("2. " + optionTwo);
            Console.Write("\n> ");
            string input = Console.ReadLine();
            int choice = 0;

            //Returns either 1 or 2 depending on users input
            if (input == "1")
                choice = 1;

            else if (input == "2")
                choice = 2;

            //Called if player gives invalid input
            else 
            {
                Console.WriteLine("Invalid input!");
                //Only called in the player added the optional argument
                if (pauseInvalid)
                {
                    Console.ReadKey(true);
                    Console.Clear();
                }
            }
           
            return choice;
        }

        /// <summary>
        /// Initializes monsters and sets current monsters
        /// </summary>
        void Start()
        {
            //Initializes monsters
            wompus.name = "Wompus";
            wompus.health = 10.0f;
            wompus.attack = 5.0f;
            wompus.defense = 5.0f;

            thwompus.name = "Thwompus";
            thwompus.health = 20.0f;
            thwompus.attack = 10.0f;
            thwompus.defense = 5.0f;

            backupWompus.name = "Backup Wompus";
            backupWompus.attack = 25.6f;
            backupWompus.defense = 5.0f;
            backupWompus.health = 3.0f;

            unclePhil.name = "Uncle Phil";
            unclePhil.attack = 50.0f;
            unclePhil.defense = 20.0f;
            unclePhil.health = 50.0f;

            monsters = new Monster[4] { wompus, thwompus, backupWompus, unclePhil };

            ResetCurrentMonsters();
        }

        void ResetCurrentMonsters()
        {

            currentMonsterIndex = 0;
            //Sets current monsters
            currentMonsterOne = monsters[currentMonsterIndex];
            currentMonsterIndex++;
            currentMonsterTwo = monsters[currentMonsterIndex];
        }

        /// <summary>
        /// Updates current scene that is displayed in main update loop
        /// </summary>
        void UpdateCurrentScene()
        {
            switch (currentScene)
            {
                case 0:
                    DisplayStartMenu();
                    break;
                case 1:
                    StartBattle(ref currentMonsterOne, ref currentMonsterTwo);
                    UpdateCurrentMonsters();
                    break;
                case 2:
                    DisplayRestartMenu();
                    break;
            }
        }

        /// <summary>
        /// Displays start menu that allows player to start the game, or leave
        /// </summary>
        void DisplayStartMenu()
        {
            int choice = GetInput("Welcome to Monster Fight Simulator, featuring Uncle Phil!", "Start Simulation", "Quit Application", true);

            if (choice == 1)
                currentScene = 1;

            else if (choice == 2)
                gameOver = true;
        }

        /// <summary>
        /// Displays restart menu that allows player to restart the game, or leave
        /// </summary>
        void DisplayRestartMenu()
        {
            //Gets player input
            int choice = GetInput("The simulation is over. Would you like to play again?", "Yes", "No", true);

            //If choice is one, restart game
            if (choice == 1)
            {
                ResetCurrentMonsters();
                currentScene = 0;
            }

            //If choice is two, end application
            if (choice == 2)
                gameOver = true;
        }

        void End()
        {
            Console.WriteLine("\nGoodbye!");
        }

        /// <summary>
        /// Called every game loop until the game is over
        /// </summary>
        void Update()
        {
            UpdateCurrentScene();
        }

        /// <summary>
        /// Changes onne of the current fighters to be the next in the if it has died.
        /// Ends the game if all figthters in the list have been used.
        /// </summary>
        void UpdateCurrentMonsters()
        {
            //If monsters one dies...
            if (currentMonsterOne.health <= 0)
            {
                //...sets current monsterOne to next one in roster
                currentMonsterIndex++;
                currentMonsterOne = monsters[currentMonsterIndex];
            }
            //If monster two dies...
            if (currentMonsterTwo.health <= 0)
            {
                //..sets current monsterTwo to the next monster in roster
                currentMonsterIndex++;
                currentMonsterTwo = monsters[currentMonsterIndex];
            }
            //if the monster is set to the default monster, or the monster roster is empy...
            if (currentMonsterTwo.name == "None" || currentMonsterOne.name == "None" && currentMonsterIndex >= 4)
            {
                //..Then go to the restart menu
                currentScene = 2;
            }    
        }

        string StartBattle(ref Monster monsterOne, ref Monster monsterTwo)
        {
            string battleResults = "No Contest";

            while (monsterOne.health > 0 && monsterTwo.health > 0)
            {
                //prints stats
                PrintStats(monsterOne);
                PrintStats(monsterTwo);

                //Monster 1 attacks monster 2
                float damageTaken = Fight(monsterOne, ref monsterTwo);
                Console.Write(monsterTwo.name + " has taken " + damageTaken);
                Console.WriteLine(" damage!");

                //Monster 2 attack monster 1
                damageTaken = Fight(monsterTwo, ref monsterOne);
                Console.Write(monsterOne.name + " has taken " + damageTaken);
                Console.WriteLine(" damage!\n");
                Console.ReadKey(true);
                Console.Clear();
            }

            if (monsterOne.health <= 0 && monsterTwo.health <= 0) 
                battleResults = "Draw";
            else if (monsterOne.health > 0) 
                battleResults = monsterOne.name;
            else if (monsterTwo.health > 0)
                battleResults = monsterTwo.name;

            return battleResults;
        }

        float Fight(Monster attack, ref Monster defender)
        {
            float damageTaken = CalculateDamage(attack, defender);
            defender.health -= damageTaken;

            return damageTaken;
        }

        void PrintStats(Monster monster)
        {
            Console.WriteLine("Name: " + monster.name);
            Console.WriteLine("Health: " + monster.health);
            Console.WriteLine("Attack: " + monster.attack);
            Console.WriteLine("Defense: " + monster.defense);
            Console.WriteLine();
        }

        float CalculateDamage(float attack, float defense)
        {
            float damage = attack - defense;
            if (damage <= 0)
            {
                damage = 0;
            }
            return damage;
        }

        float CalculateDamage(Monster attacker, Monster defender)
        {
            float damage = attacker.attack - defender.defense;
            if (damage <= 0)
            {
                damage = 0;
            }
            return damage;
        }

        public void Run()
        {
            GetArrayInts(4, 2, 3, 4);
            Console.ReadKey();
            Start();

            while (!gameOver)
                Update();

            End();
        }
       
        //Using For
        void GetArray(params int[] numbers)
        {
            int minNumber = numbers[0];
            int maxNumber = numbers[0];

            for (int i = 0; i < numbers.Length; i++)
            {
                if (minNumber > numbers[i])
                    minNumber = numbers[i];
                if (maxNumber < numbers[i])
                    maxNumber = numbers[i];
            }

            Console.WriteLine("Max: " + maxNumber);
            Console.WriteLine("Min: " + minNumber);
            Console.ReadKey();
        }

        //Using ForEach
        void GetArrayInts(params int[] numbers)
        {
            int minNumber = numbers[0];
            int maxNumber = numbers[0];

            foreach (int number in numbers)
            {
                if (minNumber > number)
                    minNumber = number;
                if (maxNumber < number)
                    maxNumber = number;
            }

            Console.WriteLine("Max: " + maxNumber);
            Console.WriteLine("Min: " + minNumber);
        }

        //int[] oneToFive = new int[5] { 1, 2, 3, 4, 5 };

        //void PrintNumbers(int[] arrayUsed)
        //{
        //    for (int number = 0; number < arrayUsed.Length; number++)
        //    {
        //        Console.WriteLine(arrayUsed[number]);
        //    }
        //}
    }
}
