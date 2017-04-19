using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Paint
{
    abstract class Geometrische_Form
    {
        protected virtual string Name { get; set; }

        protected virtual string type { get; set; }

        protected virtual int lineWidth { get; set; }

        public virtual System.Drawing.Color color { get; set; }

        public string getName()
        {
            return Name;
        }

        public string getType()
        {
            return this.type;
        }

        public virtual int getWidth()
        {
            return 0;
        }

        public virtual int getHeight()
        {
            return 0;
        }

        public virtual Point getA()
        {
            return new Point(0, 0);
        }

        public virtual Point getB()
        {
            return new Point(0, 0);
        }

        public virtual Point getC()
        {
            return new Point(0, 0);
        }

        public virtual Point getD()
        {
            return new Point(0, 0);
        }

        public virtual string ausgabe()
        {
            return null;
        }

        public void changeName(string newname)
        {
            this.Name = newname;
        }

        public virtual int getLineWidth()
        {
            return lineWidth;
        }
    }
}
