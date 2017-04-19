using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Drawing;

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

        public virtual System.Windows.Point getA()
        {
            return new System.Windows.Point(0, 0);
        }

        public virtual System.Windows.Point getB()
        {
            return new System.Windows.Point(0, 0);
        }

        public virtual System.Windows.Point getC()
        {
            return new System.Windows.Point(0, 0);
        }

        public virtual System.Windows.Point getD()
        {
            return new System.Windows.Point(0, 0);
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

        public virtual PointF PgetA()
        {
            return new PointF(0, 0);
        }

        public virtual PointF PgetB()
        {
            return new PointF(0, 0);
        }

        public virtual PointF PgetC()
        {
            return new PointF(0, 0);
        }

    }
}
