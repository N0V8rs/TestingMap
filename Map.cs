using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingMap
{
    class Map
    {
        private string path;
        private string[] floorMap;
        public char[,] mapLayout;
        public int maxX;
        public int maxY;

        //Spike spike;

        public Map(string filePath)
        {
            path = filePath;
            floorMap = File.ReadAllLines(path);
            maxX = floorMap[0].Length;
            maxY = floorMap.Length;
            CreateMap();
        }

        private void CreateMap()
        {
            mapLayout = new char[maxY, maxX];

            for (int i = 0; i < maxY; i++)
            {
                for (int j = 0; j < maxX; j++)
                {
                    mapLayout[i, j] = floorMap[i][j];
                }
            }
        }

        public void DrawMap(Player player, List<Enemy> enemies, List<Spike> spikes)
        {
            Console.Clear();

            for (int i = 0; i < maxY; i++)
            {
                for (int j = 0; j < maxX; j++)
                {
                    // Drawing logic
                    Console.Write(mapLayout[i, j]);
                }
                Console.WriteLine();
            }

            player.PlayerPosition();
            foreach (var enemy in enemies)
            {
                enemy.EnemyPosition();
            }
            foreach (var spike in spikes)
            {
                spike.Draw();
            }
            player.Draw(spikes);

            Console.SetCursorPosition(0, 0);
        }
        public void UpdateMapTile(int y, int x, char tile)
        {
            mapLayout[y, x] = tile;
        }
    }
}
