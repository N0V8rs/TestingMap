using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingMap
{
    internal class Game 
    {
        private Map map;
        private Enemy enemy;
        private Player player;
        private Exit exit;
        private Diamond diamonds;
        private List<Enemy> enemies;
        public List<Spike> spikes;

        public Game()
        {
            map = new Map("TextFile1.txt");
            player = new Player(100, 50);
            exit = new Exit(10, 5);
            enemies = new List<Enemy>
            {
                // Spawns an Enemey
                new Enemy(100,10,2,5),
                new Enemy(20,10,2, 2)
            };
            spikes = new List<Spike>
            {
                 new Spike(6, 5),
                 new Spike(8, 3),
            };

          // diamonds = new List<Diamond>
          // {
          //     new Diamond(6, 5),
          //     new Diamond(8, 3)
          // };
        }

        public void DisplayLegend()
        {
            Console.SetCursorPosition(0, map.maxY + 4);
            Console.WriteLine("| Map Legend");
            Console.SetCursorPosition(0, map.maxY + 5);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("| Player = + ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" Enemy 1 = B ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" Enemy 2 =   |");
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("| Walls = # |");
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("| Floor = - |");
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("| Diamonds = @ ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(" SpikeTrap = ^ ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(" Exit = X |");
            Console.ResetColor();
            Console.WriteLine();
        }
        public void DisplayHUD(Player player, List<Enemy> enemies, Map map)
        {

            Console.SetCursorPosition(0, map.maxY + 1);
            Console.WriteLine($"|| Health: {player.currentHP}/{player.maxHP} | Diamonds Raided: {player.Diamonds} ||");

            foreach (var enemy in enemies)
            {
                Console.Write($"|| Enemy Health: {enemy.currentHP}/{enemy.maxHP} ||");
            }
            DisplayLegend();
        }

        public void Start()
        {
            Console.WriteLine("Now playing Noah Vaters RPG Map");
            Console.WriteLine("\n");
            Console.WriteLine("Get as much Diamonds around the map and get to the exit");
            Console.WriteLine("Watch out for the enemies in the map trying to kill you");
            Console.WriteLine("\n");
            Console.WriteLine("To attack back press space near them");
            Console.WriteLine("Best of luck to you and watch out for the camo.f.... enemies");
            Console.WriteLine("Press any key to start...");
            Console.ReadKey(true);
            Console.Clear();

            while (!player.GameOver && !player.YouWin)
            {
                // Clear the console at the beginning of each frame
                Console.Clear();

                // Draw the map, HUD, and other elements
                map.DrawMap(player, enemies, spikes, exit);
                DisplayHUD(player, enemies, map);

                // Handle player input
                player.PlayerInput(map, enemies,exit);
               
                if (exit.IsPlayerOnExit(player))
                {
                    player.YouWin = true;
                }

                if (player.GameOver)
                {
                    Console.Clear();
                    Console.WriteLine("Game Over!");
                    // Display other game over messages or options...
                    Console.ReadKey(true);
                }

                // Update and draw enemies
                foreach (var enemy in enemies.ToList())
                {
                    enemy.Move(map, player);
                    enemy.Attack(player);
                   //player.CheckForSpikes(spikes);
                   //player.Draw(spikes);

                    if (!enemy.enemyAlive)
                    {
                        enemies.Remove(enemy);
                        //map.mapLayout(enemy.PosX, enemy.PosY, '-');
                    }
                    else
                    {
                        enemy.EnemyPosition();
                    }
                }
                player.CheckForSpikes(spikes);
                player.Draw(spikes);

                // Check if the player is still alive
                if (player.currentHP > 0)
                {
                    player.PlayerPosition();
                }
            }

            Console.Clear();

                if (player.YouWin)
                {
                    Console.WriteLine("YOU WIN!");
                    Console.WriteLine("There's always more Diamonds out there to raid");
                    Console.WriteLine($"\nYou collected : {player.Diamonds} Diamonds!");
                }
                else
                {
                    Console.WriteLine("You Died");
                    Console.WriteLine("Try better");
                }

                Console.ReadKey(true);
            }
        }
    }
