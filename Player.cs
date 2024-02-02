using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingMap
{
    class Player : Character
    {
        public int maxHP;
        public int currentHP;
        private int damage;
        private int diamonds;
        private bool youWin;
        private bool gameOver;

        public Player(int maxHP, int damage) : base(10, 10)
        {
            this.maxHP = maxHP;
            currentHP = maxHP;
            this.damage = damage;
            diamonds = 0;
            youWin = false;
            gameOver = false;
        }

        public bool YouWin => youWin;
        public bool GameOver => gameOver;
        public int Diamonds => diamonds;

        public void PlayerPosition()
        {
            Console.SetCursorPosition(posX, posY);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("+");
            Console.ResetColor();
  
        }
        public Player(int maxHP, int initialPosX, int initialPosY) : base(initialPosX, initialPosY)
        {
            this.maxHP = maxHP;
            currentHP = maxHP;
        }

        public void CheckForSpikes(List<Spike> spikes)
        {
            foreach (Spike spike in spikes)
            {
                if (posX == spike.posX && posY == spike.posY)
                {
                    TakeDamageFromSpike();
                    break; // If there is a spike at the player's position, no need to check further spikes
                }
            }
        }
        public void Draw(List<Spike> spikes)
        {
            // Draw spikes first
            foreach (var spike in spikes)
            {
                spike.Draw();
            }

            // Check if the player is on a spike
            bool onSpike = spikes.Any(spike => posX == spike.posX && posY == spike.posY);

            // Draw player if not on a spike
            if (!onSpike)
            {
                Console.SetCursorPosition(posX, posY);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("+");
                Console.ResetColor();
            }
        }


        private void TakeDamageFromSpike()
        {
            Console.WriteLine("Ouch! You stepped on a spike!");
            currentHP --; // Reduce player health when hit by a spike

            if (currentHP <= 0)
            {
                Console.WriteLine("Game Over - You ran out of health!");
                gameOver = true;
            }
        }


        public void ReceiveDamage(int damage)
        {
            currentHP -= damage;

            if (currentHP <= 0)
            {
                currentHP = 0; // Ensure HP doesn't go negative
                gameOver = true; // Set the GameOver flag when player health reaches 0
            }
        }


        public void AttackEnemy(List<Enemy> enemies)
        {
            // Perform player attack only if there are enemies nearby
            foreach (var enemy in enemies)
            {
                if (Math.Abs(posX - enemy.posX) <= 1 && Math.Abs(posY - enemy.posY) <= 1)
                {
                    // Perform player attack on the enemy
                    enemy.ReceiveDamage(damage);
                }
            }
        }

        private void AttackEnemy(Enemy enemy)
        {
            // Perform player attack on the enemy
            enemy.ReceiveDamage(damage);
        }

        public void PlayerInput(Map map, List<Enemy> enemies)
        {
            bool moved = false;

            int movementX = posX;
            int movementY = posY;

            ConsoleKeyInfo playerMovement = Console.ReadKey(true);

            // Attack Key for the player
            if (playerMovement.Key == ConsoleKey.Spacebar)
            {
                AttackEnemy(enemies);
                moved = true;
            }

            // Handle movement only if the player didn't attack
            if (!moved)
            {
                // Calculate new position based on user input
                switch (playerMovement.Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        movementY = Math.Max(posY - 1, 0);
                        break;

                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        movementY = Math.Min(posY + 1, map.maxY);
                        break;

                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                        movementX = Math.Max(posX - 1, 0);
                        break;

                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                        movementX = Math.Min(posX + 1, map.maxX);
                        break;
                }

                // Update new position if it's a valid move
                if (map.mapLayout[movementY, movementX] != '#')
                {
                    posX = movementX;
                    posY = movementY;
                }

                // Handle collisions and interactions with other elements on the map
                // ...

                // Exit game using a key
                if (playerMovement.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(1);
                }
            }
        }
    }
}
