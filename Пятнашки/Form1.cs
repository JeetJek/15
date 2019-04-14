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
        int[,] nums;//массив чисел для облегчения проверки на валидность
        int range = 5;//промежутки между клетками
        int size = 100;//размер клеток
        Image cell = Image.FromFile("Resourses/Cells.png");
        Image up = Image.FromFile("Resourses/arrowUp.png");

        Button arrowUp = new Button();
        Button arrowDown = new Button();
        Button arrowLeft = new Button();
        Button arrowRight = new Button();

        private void Form1_Load(object sender, EventArgs e)
        {
            #region Стрелки
            arrowDown.BackgroundImage = Image.FromFile("Resourses/arrowDown.png");
            arrowUp.BackgroundImage = Image.FromFile("Resourses/arrowUp.png");
            arrowLeft.BackgroundImage = Image.FromFile("Resourses/arrowLeft.png");
            arrowRight.BackgroundImage = Image.FromFile("Resourses/arrowRight.png");
            arrowUp.Visible = false;
            arrowDown.Visible = false;
            arrowRight.Visible = false;
            arrowLeft.Visible = false;
            arrowDown.Size = new Size(size, size);
            arrowUp.Size = new Size(size, size);
            arrowLeft.Size = new Size(size, size);
            arrowRight.Size = new Size(size, size);
            Controls.Add(arrowDown);
            Controls.Add(arrowUp);
            Controls.Add(arrowLeft);
            Controls.Add(arrowRight);
            #endregion
            int n =0;
            field = new Button[4, 4];
            nums = new int[4, 4];
            this.Location= new Point(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / 2-(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / 4), System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / 2 - (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / 4));
            for(int i=0;i<4;i++)
                for(int j=0;j<4;j++)
                {
                    field[i, j] = new Button();//инициализация кнопок
                    if (n != 0)
                        field[i, j].Image= ScaleImage(cell,size-10,size-10);//изменение размера картинки
                    field[i, j].Location = new Point(i * size + range, j * size + range);//задаю положение кнопки
                    field[i, j].FlatAppearance.BorderSize = 0;//толщина границы кнопки = 0
                    field[i, j].Size = new Size(size, size);//размер ячейки
                    if (n == 0)
                        field[i, j].Text = "";
                    if(n!=0)
                        field[i, j].Text =""+ n;//отображение цифер на кнопках
                    nums[i, j] = n;//массив цифер

                    field[i, j].Click += clickButton;//Добавляю обработку нажатия на кнопку
                    field[i, j].Font = new Font("Arial", 30);//изменяю размер 
                    n++;
                    Controls.Add(field[i,j]);//добавление кнопки на форму
                }
            this.Size = new Size((4 * size) + 5*range, (4 * size) + 10*range);
        }

        private void clickButton(object sender, EventArgs e)
        {
            Button tmp = sender as Button;
            int x = (tmp.Location.X - range) / size;
            int y = (tmp.Location.Y - range) / size;
            //Нужна проверка ячеек на существование и заполненность
            try
            {
                if (field[x - 1, y].Text == "")
                {
                    arrowLeft.Location = new Point((x - 1) * size + range, y * size + range);
                    arrowLeft.Visible = true;
                    arrowLeft.BringToFront();
                }
            }
            catch (Exception ex)
            { }

        }
        static Image ScaleImage(Image source, int width, int height)
        {

            Image dest = new Bitmap(width, height);
            using (Graphics gr = Graphics.FromImage(dest))
            {
                gr.FillRectangle(Brushes.White, 0, 0, width, height);  // Очищаем экран
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                float srcwidth = source.Width;
                float srcheight = source.Height;
                float dstwidth = width;
                float dstheight = height;

                if (srcwidth <= dstwidth && srcheight <= dstheight)  // Исходное изображение меньше целевого
                {
                    int left = (width - source.Width) / 2;
                    int top = (height - source.Height) / 2;
                    gr.DrawImage(source, left, top, source.Width, source.Height);
                }
                else if (srcwidth / srcheight > dstwidth / dstheight)  // Пропорции исходного изображения более широкие
                {
                    float cy = srcheight / srcwidth * dstwidth;
                    float top = ((float)dstheight - cy) / 2.0f;
                    if (top < 1.0f) top = 0;
                    gr.DrawImage(source, 0, top, dstwidth, cy);
                }
                else  // Пропорции исходного изображения более узкие
                {
                    float cx = srcwidth / srcheight * dstheight;
                    float left = ((float)dstwidth - cx) / 2.0f;
                    if (left < 1.0f) left = 0;
                    gr.DrawImage(source, left, 0, cx, dstheight);
                }

                return dest;
            }
        }
    }
}
