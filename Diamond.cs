using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingMap
{
    public class Diamond 
    {
        public int posX;
        public int posY;

        public Diamond(int initialPosX, int initialPosY)
        {
            posX = initialPosX;
            posY = initialPosY;
        }

        public void SetPosition(int newX, int newY)
        {
            posX = newX;
            posY = newY;
        }

       //public void Collect(Player player, Map map)
       //{
       //    // Check if the player's position matches the diamond's position
       //    if (player.posX == posX && player.posY == posY)
       //    {
       //        // Player picks up the diamond
       //        Console.WriteLine("You collected a diamond!");
       //        player.diamonds++; // Increment the player's diamond count
       //        // You can add more logic here, such as updating the map or removing the diamond
       //        map.UpdateMapTile(posY, posX, '-'); // Set the map tile to '-'
       //        SetPosition(-1, -1); // Move the diamond to an invalid position to "remove" it
       //    }
       //}
    }
}

