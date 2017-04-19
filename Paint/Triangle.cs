using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Paint
{
    class Triangle : Geometrische_Form
    {
        //Eigenschaften

        protected virtual Point positionA { get; set; }
        protected virtual Point positionB { get; set; }
        protected virtual Point positionC { get; set; }
        protected virtual Point positionD { get; set; }


        //Konstruktor

        public Triangle(Point posA, Point posB,Point posC, string name, System.Drawing.Color c, string type)
        {
            this.positionA = posA;
            this.positionB = posB;
            this.positionC = posC;
            this.Name = name;
            this.color = c;
            this.type = type;
        }

        //Methoden

        public override string ausgabe()
        {
            return "Eigenschaften:" + Environment.NewLine + Environment.NewLine +
                "Typ: Linie" + Environment.NewLine +
                "Name: " + Name + Environment.NewLine +
                "A: " + positionA.ToString() + Environment.NewLine +
                "B: " + positionB.ToString() + Environment.NewLine +
                "C: " + positionC.ToString() + Environment.NewLine +
                "Farbe: " + color.R + ";" + color.G + ";" + color.B;
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
            return positionB;
        }
              
    }
}
