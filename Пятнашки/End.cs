using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Пятнашки
{
    public partial class End : Form
    {
        public End()
        {
            InitializeComponent();
        }

        private void End_Load(object sender, EventArgs e)
        {
            label1.Text = "Поздравляю!\nВы прошли отбор в орифлейм!";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
