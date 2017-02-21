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

        // 4 Eckpunkte

        private Point positionA { get; set; }
        private Point positionB { get; set; }
        private Point positionC { get; set; } 
        private Point positionD { get; set; }       


        // Konstruktor
        public Rectangle(Point positionA, Point positionB,Point positionC, Point positionD, string name)
        {
            this.positionA = positionA;
            this.positionB = positionB;
            this.positionC = positionC;
            this.positionD = positionD;
            Name = name;
        }

        public Point getPositionA()
        {
            return positionA;
        }

        public Point getPositionB()
        {
            return positionB;
        }

        public Point getPositionC()
        {
            return positionC;
        }

        public Point getPositionD()
        {
            return positionD;
        }
    }
}
