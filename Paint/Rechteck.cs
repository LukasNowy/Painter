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

        public Point positionA { get; set; }
        private Point positionB { get; set; }
        private Point positionC { get; set; } 
        private Point positionD { get; set; }       

        // Konstruktor
        public Rectangle(Point positionA, Point positionB,Point positionC, Point positionD,  string name)
        {
            this.positionA = positionA;
            this.positionB = positionB;
            this.positionC = positionC;
            this.positionD = positionD;
            this.Name = name;
        }

        public override string ausgabe()
        {
            return "Eigenschaften:" + Environment.NewLine + Environment.NewLine +
                "Typ: Rechteck" + Environment.NewLine +
                "Name: " + Name + Environment.NewLine +
                "A: " + positionA.ToString() + Environment.NewLine +
                "B: " + positionB.ToString() + Environment.NewLine +
                "C: " + positionC.ToString() + Environment.NewLine +
                "D: " + positionD.ToString();
        }

    }
}
