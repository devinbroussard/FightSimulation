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
            monsterOne.health = 10.0f;
            monsterOne.attack = 5.0f;
            monsterOne.defense = 5.0f;

            //Created monster two object, and gave its stats
            Monster monsterTwo;
            monsterTwo.name = "Thwompus";
            monsterTwo.health = 20.0f;
            monsterTwo.attack = 10.0f;
            monsterTwo.defense = 5.0f;

            //Prints monster1 stats
            PrintStats(monsterOne);

            //Prints monster2 stats
            PrintStats(monsterTwo);

            //Monster 1 attacks monster 2
            float damageTaken = Fight(ref monsterOne, ref monsterTwo);
            Console.Write(monsterTwo.name + " has taken " + damageTaken);
            Console.WriteLine(" damage!");

            //Monster 2 attack monster 1
            damageTaken = Fight(ref monsterTwo, ref monsterOne);
            Console.Write(monsterOne.name + " has taken " + damageTaken);
            Console.WriteLine(" damage!");

            //prints stats
            PrintStats(monsterOne);
            PrintStats(monsterTwo);
        }

        float Fight(ref Monster attack, ref Monster defender)
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
    public struct Monster
    {
        public string name;
        public float health;
        public float attack;
        public float defense;
    }
}
