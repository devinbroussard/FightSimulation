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
        bool gameOver = false;
        Monster currentMonsterOne;
        Monster currentMonsterTwo;
        int currentMonsterIndex = 1;

        void Battle()
        {
            //prints stats
            PrintStats(currentMonsterOne);
            PrintStats(currentMonsterTwo);
            Console.ReadKey(true);
            Console.Clear();

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

        public void Run()
        {
            //created monster one object, and gave its stats
            wompus.name = "Wompus";
            wompus.health = 10.0f;
            wompus.attack = 5.0f;
            wompus.defense = 5.0f;

            //Created monster two object, and gave its stats
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

            Update();

        }

        Monster GetMonster(int monsterIndex)
        {
            Monster monster;
            monster.name = "None";
            monster.attack = 1;
            monster.defense = 1;
            monster.health = 1;

            if (monsterIndex == 1)
                monster = unclePhil;
            else if (monsterIndex == 2)
                monster = backupWompus;
            else if (monsterIndex == 3)
                monster = wompus;
            else if (monsterIndex == 4)
                monster = thwompus;

            return monster;
        }

        void Update()
        {
            Battle();
            UpdateCurrentMonsters();
        }

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
            if (currentMonsterTwo.name == "None" || currentMonsterOne.name == "None" && currentMonsterIndex >= 4)
            {
                Console.WriteLine("Simulation Over");
                gameOver = true;
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
                Console.ReadKey(true);
                Console.Clear();

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
    }
}
