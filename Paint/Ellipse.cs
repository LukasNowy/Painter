﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Paint
{
    class Ellipse : Geometrische_Form
    {
        //Eigenschaften

        protected virtual Point positionA { get; set; }
        protected virtual Point positionB { get; set; }
        protected virtual Point positionC { get; set; }
        protected virtual Point positionD { get; set; }


        //Konstruktor

        public Ellipse(Point posA, Point posB, Point posC, Point PosD, string name, System.Drawing.Color c, string type)
        {
            this.positionA = posA;
            this.positionB = posB;
            this.positionC = posC;
            this.positionD = PosD;
            this.Name = name;
            this.color = c;
            this.type = type;
        }

        //Methoden

        public override string ausgabe()
        {
            return "Eigenschaften:" + Environment.NewLine + Environment.NewLine +
                "Typ: Ellipse" + Environment.NewLine +
                "Name: " + Name + Environment.NewLine +
                "A: " + positionA.ToString() + Environment.NewLine +
                "B: " + positionB.ToString() + Environment.NewLine +
                "C: " + positionC.ToString() + Environment.NewLine +
                "D: " + positionD.ToString() + Environment.NewLine +
                "Radius X: " + Convert.ToInt16(positionC.X - positionD.X) + Environment.NewLine +
                "Radius Y: " + Convert.ToInt16(positionB.Y - positionC.Y) + Environment.NewLine +     
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
            return positionC;
        }

        public override Point getD()
        {
            return positionD;
        }


    }
}
