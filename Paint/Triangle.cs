using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;

namespace Paint
{
    class Triangle : Geometrische_Form
    {
        //Eigenschaften

        protected virtual PointF positionA { get; set; }
        protected virtual PointF positionB { get; set; }
        protected virtual PointF positionC { get; set; }


        //Konstruktor

        public Triangle(PointF posA, PointF posB,PointF posC, string name, System.Drawing.Color c, string type)
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
                "Typ: Dreieck" + Environment.NewLine +
                "Name: " + Name + Environment.NewLine +
                "A: " + positionA.ToString() + Environment.NewLine +
                "B: " + positionB.ToString() + Environment.NewLine +
                "C: " + positionC.ToString() + Environment.NewLine +
                "Farbe: " + color.R + ";" + color.G + ";" + color.B;
        }

        public override PointF PgetA()
        {
            return positionA;
        }

        public override PointF PgetB()
        {
            return positionB;
        }

        public override PointF PgetC()
        {
            return positionC;
        }
              
    }
}
