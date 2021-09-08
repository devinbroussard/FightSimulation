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
        Monster wompus;
        Monster thwompus;
        Monster backupWompus;
        Monster unclePhil;

        //Initializing
        bool gameOver = false;
        Monster currentMonsterOne;
        Monster currentMonsterTwo;
        int currentMonsterIndex = 0;
        int currentScene = 0;

        /// <summary>
        /// Simulates one turn in the current monster fight
        /// </summary>
        void Battle()
        {
            //prints stats
            PrintStats(currentMonsterOne);
            PrintStats(currentMonsterTwo);

            //Monster 1 attacks monster 2
            float damageTaken = Fight(currentMonsterOne, ref currentMonsterTwo);
            Console.Write(currentMonsterTwo.name + " has taken " + damageTaken);
            Console.WriteLine(" damage!");

            //Monster 2 attack monster 1
            damageTaken = Fight(currentMonsterTwo, ref currentMonsterOne);
            Console.Write(currentMonsterOne.name + " has taken " + damageTaken);
            Console.WriteLine(" damage!\n");
            Console.ReadKey(true);
            Console.Clear();
        }

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

            ResetCurrentMonsters();
        }

        void ResetCurrentMonsters()
        {

            currentMonsterIndex = 0;
            //Sets current monsters
            currentMonsterOne = GetMonster(currentMonsterIndex);
            currentMonsterIndex++;
            currentMonsterTwo = GetMonster(currentMonsterIndex);
        }

        /// <summary>
        /// Gets the current monster to be used in battle
        /// </summary>
        /// <param name="monsterIndex"></param>
        /// <returns></returns>
        Monster GetMonster(int monsterIndex)
        {
            Monster monster;
            monster.name = "None";
            monster.attack = 1;
            monster.defense = 1;
            monster.health = 1;

            switch (monsterIndex)
            {
                case 0:
                    monster = unclePhil;
                    break;
                case 1:
                    
                    monster = backupWompus;
                    break;
                case 2:
                    monster = wompus;
                    break;
                case 3:
                    monster = thwompus;
                    break;
            }

            return monster;
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
                    StartBattle(currentMonsterOne, ref currentMonsterTwo);
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
                currentMonsterOne = GetMonster(currentMonsterIndex);
            }
            //If monster two dies...
            if (currentMonsterTwo.health <= 0)
            {
                //..sets current monsterTwo to the next monster in roster
                currentMonsterIndex++;
                currentMonsterTwo = GetMonster(currentMonsterIndex);
            }
            //if the monster is set to the default monster, or the monster roster is empy...
            if (currentMonsterTwo.name == "None" || currentMonsterOne.name == "None" && currentMonsterIndex >= 4)
            {
                //..Then go to the restart menu
                currentScene = 2;
            }    
        }

        string StartBattle(Monster monsterOne, ref Monster monsterTwo)
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
            Start();
            while (!gameOver)
                Update();
        }

    }
}
