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

        protected virtual Point positionA { get; set; }
        protected virtual Point positionB { get; set; }
        protected virtual Point positionC { get; set; } 
        protected virtual Point positionD { get; set; }       

        // Konstruktor
        public Rectangle(Point positionA, Point positionB,Point positionC, Point positionD,  string name, System.Drawing.Color c, string type)
        {
            this.positionA = positionA;
            this.positionB = positionB;
            this.positionC = positionC;
            this.positionD = positionD;
            this.Name = name;
            this.color = c;
            this.type = type;
        }

        public override int getWidth()
        {
            return Convert.ToInt16(positionB.X - positionA.X);
        }

        public override int getHeight()
        {
            return Convert.ToInt16(positionB.Y - positionC.Y);
        }

        public override Point getA()
        {
            return positionA;
        }

        public override Point getB()
        {
            return positionB;
        }

        public override Point getC()
        {
            return positionC;
        }

        public override Point getD()
        {
            return positionD;
        }

        //Ausgabe in InfoBox
        public override string ausgabe()
        {
            return "Eigenschaften:" + Environment.NewLine + Environment.NewLine +
                "Typ: Rechteck" + Environment.NewLine +
                "Name: " + Name + Environment.NewLine +
                "A: " + positionA.ToString() + Environment.NewLine +
                "B: " + positionB.ToString() + Environment.NewLine +
                "C: " + positionC.ToString() + Environment.NewLine +
                "D: " + positionD.ToString() + Environment.NewLine +
                "Farbe: " + color.R + ";" + color.G + ";" + color.B;
        }

    }
}
