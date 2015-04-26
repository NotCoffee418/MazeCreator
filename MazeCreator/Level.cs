using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeCreator
{
    class Level
    {
        public TabPage Tab { get; set; }
        public DataGridView Grid { get; set; }
        public int[] StairsX { get; set; }
        public int[] StairsY { get; set; }
    }
}
