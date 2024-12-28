using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeRunners
{
    public partial class Initial : Form
    {
        public Initial()
        {
            InitializeComponent();
        }

        public bool startGame = false;

        private void startButton_Click(object sender, EventArgs e)
        {
            startGame = true;
            this.Close();
        }
    }
}
