using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Paint
{
    class Line : Geometrische_Form
    {
        //Eigenschaften

        protected virtual Point positionA { get; set; }
        protected virtual Point positionB { get; set; }
        protected virtual Point positionC { get; set; }
        protected virtual Point positionD { get; set; }


        //Konstruktor

        public Line(Point posA, Point posB, string name, System.Drawing.Color c, string type, int lwidth)
        {
            this.positionA = posA;
            this.positionB = posB;
            this.Name = name;
            this.color = c;
            this.type = type;
            this.lineWidth = lwidth;
        }

        //Methoden

        public override string ausgabe()
        {
            return "Eigenschaften:" + Environment.NewLine + Environment.NewLine +
                "Typ: Linie" + Environment.NewLine +
                "Name: " + Name + Environment.NewLine +
                "A: " + positionA.ToString() + Environment.NewLine +
                "B: " + positionB.ToString() + Environment.NewLine +
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


        public override int getLineWidth()
        {
            return lineWidth;
        }
    }
}
