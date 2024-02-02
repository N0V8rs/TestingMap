using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace TestingMap
{
    class Enemy : Character
    {
        public int maxHP;
        public int currentHP;
        private int damage;
        public  bool enemyAlive;
        private Map map;
        private Random randomRoll = new Random();

        public Enemy(int maxHP, int damage, int initialPosX, int initialPosY) : base(initialPosX, initialPosY)
        {
            this.maxHP = maxHP;
            currentHP = maxHP;
            this.damage = damage;
            enemyAlive = true;
            this.map = map;
        }

        public void EnemyPosition()
        {
            Console.SetCursorPosition(posX, posY);

            if (enemyAlive)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("B");
            }
            else
            {
                Console.Write("-"); // Draw an empty space for defeated enemies
            }

            Console.ResetColor();
        }


        public void Move(Map map, Player player)
        {
            // If the enemy is not alive, no need to move
            if (!enemyAlive)
            {
                return;
            }

            int newEnemyPositionX;
            int newEnemyPositionY;

            do
            {
                int rollResult = randomRoll.Next(1, 5);

                // Calculate the potential new position based on the roll
                switch (rollResult)
                {
                    case 1:
                        newEnemyPositionY = Math.Min(posY + 1, map.maxY);
                        newEnemyPositionX = posX;
                        break;
                    case 2:
                        newEnemyPositionY = Math.Max(posY - 1, 0);
                        newEnemyPositionX = posX;
                        break;
                    case 3:
                        newEnemyPositionX = Math.Max(posX - 1, 0);
                        newEnemyPositionY = posY;
                        break;
                    case 4:
                        newEnemyPositionX = Math.Min(posX + 1, map.maxX);
                        newEnemyPositionY = posY;
                        break;
                    default:
                        newEnemyPositionX = posX;
                        newEnemyPositionY = posY;
                        break;
                }

                // Check for collisions with specific map tiles and player position
                if (map.mapLayout[newEnemyPositionY, newEnemyPositionX] != '#' &&
                    map.mapLayout[newEnemyPositionY, newEnemyPositionX] != '^' &&
                    map.mapLayout[newEnemyPositionY, newEnemyPositionX] != 'X' &&
                    !(newEnemyPositionX == player.posX && newEnemyPositionY == player.posY))
                {
                    // Update the new position if there are no collisions
                    map.mapLayout[posY, posX] = '-'; // Reset old position

                    // Set the enemy symbol at the new position only if the enemy is alive
                    if (enemyAlive)
                    {
                        map.mapLayout[newEnemyPositionY, newEnemyPositionX] = 'B';
                        posX = newEnemyPositionX;
                        posY = newEnemyPositionY;
                    }
                    else
                    {
                        // If the enemy is not alive, set the map layout to '-'
                        map.mapLayout[newEnemyPositionY, newEnemyPositionX] = '-';
                    }

                    break; // Exit the loop if a valid position is found
                }
            } while (true); // Retry until a valid position is found
        }

        public void Attack(Player player)
        {
            // If the enemy is not alive, no need to attack
            if (!enemyAlive)
            {
                return;
            }

            // Check if the player is adjacent (horizontally or vertically)
            if ((Math.Abs(player.posX - posX) == 1 && player.posY == posY) ||
                (Math.Abs(player.posY - posY) == 1 && player.posX == posX))
            {
                // Player is nearby, perform the attack
                player.ReceiveDamage(damage);
            }
        }

        public void ReceiveDamage(int damage)
        {
            currentHP -= damage;
            if (currentHP <= 0)
            {
                // Enemy is defeated, mark it as not alive
                enemyAlive = false;
            }
        }
    }
}
  
