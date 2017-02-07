using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Paint
{
    class Rectangle : Geometrische_Form
    {
        public int x { get; set; } // Breite
        public int y { get; set; } // Höhe
        
        public Rectangle(int width, int height, Point positionX, Point positionY, string name)
        {
            this.x = width;
            this.y = height;
            this.positionX = positionX;
            this.positionY = positionY;
            Name = name;
        }
    }
}
