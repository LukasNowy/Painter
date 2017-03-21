using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
    class ColorRessource
    {       
        //Eigenschaften

        private string Name { get; set; }
        private byte R { get; set; }
        private byte G { get; set; }
        private byte B { get; set; }

        //Konstruktor

        public ColorRessource(string name, byte R, byte G, byte B)
        {
            this.Name = name;
            this.R = R;
            this.G = G;
            this.B = B;
        }

        //Methoden

        public string getName()
        {
            return this.Name;
        }

        public byte getR()
        {
            return this.R;
        }

        public byte getG()
        {
            return this.G;
        }

        public byte getB()
        {
            return this.B;
        }

    }
}
