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
        public string Name { get; set; }
        public Point positionX { get; set; }
        public Point positionY { get; set; }
    }
}
