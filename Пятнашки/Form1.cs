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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Button[,] field;//Объявление локального массива типа кнопка для поля 

        private void Form1_Load(object sender, EventArgs e)
        {
            int range = 5;//промежутки между клетками
            int size = 100;//размер клеток
            int n=0;
            field = new Button[4, 4];
            this.Location= new Point(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / 2-(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / 4), System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / 2 - (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / 4));
            for(int i=0;i<4;i++)
                for(int j=0;j<4;j++)
                {
                    field[i, j] = new Button();
                    field[i, j].Location = new Point(i * size + range, j * size + range);
                    field[i, j].Size = new Size(size, size);
                    field[i, j].Text =""+ n;
                    field[i, j].Click += clickButton;//Добавляю обработку нажатия на кнопку
                    n++;
                    Controls.Add(field[i,j]);
                }
            this.Size = new Size((4 * size) + 5*range, (4 * size) + 10*range);
        }
        private void clickButton(object sender, EventArgs e)
        {
            Button tmp = sender as Button;
            
        }
    }
}
