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
        protected string Name { get; set; }

        public string getName()
        {
            return Name;
        }

        public virtual string ausgabe()
        {
            return null;
        }

        public void changeName(string newname)
        {
            this.Name = newname;
        }
    }
}
