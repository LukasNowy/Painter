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
        private int x { get; set; } // Breite
        private int y { get; set; } // Höhe
        private Point position1 { get; set; } // Position 1
        private Point position2 { get; set; } // Position 2

        // Konstruktor
        public Rectangle(int width, int height, Point position1, Point position2, string name)
        {
            this.x = width;
            this.y = height;
            this.position1 = position1;
            this.position2 = position2;
            Name = name;
        }
    }
}
