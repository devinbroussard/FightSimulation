using System;
using System.Collections.Generic;
using System.Text;

namespace Fight_Simulation
{
    class Game
    {
        public void Run()
        {
            //created monster one object, and gave its stats
            Monster monsterOne;
            monsterOne.name = "Wompus";
            monsterOne.health = 5.0f;
            monsterOne.attack = 10.0f;
            monsterOne.defense = 5.0f;

            //Created monster two object, and gave its stats
            Monster monsterTwo;
            monsterTwo.name = "Thwompus";
            monsterTwo.health = 5.0f;
            monsterTwo.attack = 10.0f;
            monsterTwo.defense = 5.0f;

            //Prints monster1 stats
            PrintStats(monsterOne);
            Console.ReadKey();
            Console.Clear();

            //Prints monster2 stats
            PrintStats(monsterTwo);
            Console.ReadKey();
            Console.Clear();

            //Monster 1 attacks monster 2
            float damageTaken = CalculateDamage(monsterOne, monsterTwo);
            monsterTwo.health -= damageTaken;
            Console.WriteLine(monsterTwo.name + " has taken " + damageTaken);
            Console.WriteLine();
            Console.ReadKey();

            //Monster 2 attack monster 1
            damageTaken = CalculateDamage(monsterOne, monsterTwo);
            monsterOne.health -= damageTaken;
            Console.WriteLine(monsterOne.name + " has taken " + damageTaken);
            Console.ReadKey();
        }

        void PrintStats(Monster monster)
        {
            Console.WriteLine("Name: " + monster.name);
            Console.WriteLine("Health: " + monster.health);
            Console.WriteLine("Attack: " + monster.attack);
            Console.WriteLine("Defense: " + monster.defense);
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
    public struct Monster
    {
        public string name;
        public float health;
        public float attack;
        public float defense;
    }
}
