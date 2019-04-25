using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Пятнашки
{
    public partial class Game : Form
    {
        public Game()
        {
            InitializeComponent();
        }
        Button[,] field;//Объявление локального массива типа кнопка для поля 
        int[] mas;//массив чисел для облегчения проверки на валидность
        int range = 5;//промежутки между клетками
        int size = 100;//размер клеток
        Button arrowUp = new Button();
        Button arrowDown = new Button();
        Button arrowLeft = new Button();
        Button arrowRight = new Button();
        Label hod = new Label();
        int h = 0;
        static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }

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
            arrowDown.Click += clickDown;
            arrowUp.Click += clickUp;
            arrowLeft.Click += clickLeft;
            arrowRight.Click += clickRight;
            Controls.Add(arrowDown);
            Controls.Add(arrowUp);
            Controls.Add(arrowLeft);
            Controls.Add(arrowRight);
            #endregion
            this.Focus();
            int n =0;
            field = new Button[4, 4];
            mas = new int[4*4];
            this.Location= new Point(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / 2-(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / 4), System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / 2 - (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / 4));
            for(int j=0;j<4;j++)
                for(int i=0;i<4;i++)
                {
                    field[i, j] = new Button();//инициализация кнопок
                    field[i, j].TabStop = false;
                    field[i,j].FlatAppearance.BorderSize = 0;
                    field[i, j].FlatStyle = FlatStyle.Flat;
                    field[i, j].Click += clickButton;//Добавляю обработку нажатия на кнопку
                    field[i, j].Font = new Font("Arial", 20);//изменяю размер 
                    
                    Controls.Add(field[i, j]);//добавление кнопки на форму
                    field[i, j].Location = new Point(i * size + range, j * size + range);//задаю положение кнопки
                    field[i, j].FlatAppearance.BorderSize = 0;//толщина границы кнопки = 0
                    field[i, j].Size = new Size(size, size);//размер ячейки

                    if (n != 15)
                        field[i, j].Image = ScaleImage(Image.FromFile("Resourses/"+(n+1)+".png"), size - 1, size - 1);//изменение размера картинки
                    if (n == 15)
                        field[i, j].Text = "";
                    if(n!=15)
                        field[i, j].Text =" ";//отображение цифер на кнопках
                    mas[n] = n+1;//массив цифер
                    n++;
                }
            this.Show();
            do
            {
                List<int> a = new List<int>();
                for (int i = 0; i < 16; i++)
                {
                    if (i != 15)
                        a.Add(i + 1);
                    else
                        a.Add(0);
                }
                Random rand = new Random();
                int k;
                k = 0;
                for (int i = 0; i < 16; i++)
                {
                    int rng = rand.Next(a.Count());
                    mas[i] = a[rng];
                    if (a[rng] != 0)
                    {
                        field[i % 4, k].Text = " ";
                        field[i % 4, k].Image = ScaleImage(Image.FromFile("Resourses/" + (a[rng]) + ".png"), size - 1, size - 1);
                    }
                    else
                    {
                        field[i % 4, k].Text = "";
                        field[i % 4, k].Image = ScaleImage(Image.FromFile("Resourses/" + (0) + ".png"), size - 1, size - 1);
                    }
                    if ((i + 1) % 4 == 0)
                        k++;
                    a.Remove(a[rng]);
                }
            } while (check(mas) != true);

            PictureBox mini = new PictureBox();
            mini.Image = ScaleImage(Image.FromFile("Resourses/bg.jpg"), 300, 300);
            mini.Location= new Point(4 * size+15, range);
            mini.Size = new Size(300, 300);
            hod.Text = "Ходов: " + h;
            hod.Font = new Font("Arial", 12);
            hod.Location = new Point(4 * size + 15,mini.Size.Height+2*range);

            Controls.Add(mini);
            Controls.Add(hod);
            this.Size = new Size((4 * size) + 5*range + mini.Size.Width+18, (4 * size) + 10*range);
        }

        private void clickDown(object sender, EventArgs e)
        {
            Button tmp = sender as Button;
            int x = (tmp.Location.X - range) / size;
            int y = (tmp.Location.Y - range) / size;
            HideArrows();
            try
            {
                
                Button temp = new Button();
                
                temp.Text = field[x, y - 1].Text;
                temp.Image = field[x, y - 1].Image;
                field[x, y - 1].Text = field[x, y].Text;
                field[x, y - 1].Image = field[x, y].Image;
                field[x, y].Text = temp.Text;
                field[x, y].Image = temp.Image;
               
                Swap<int>(ref mas[4 * y + x], ref mas[4 * (y - 1) + x]);
                HideArrows();
                temp.Dispose();
                h++;
                hod.Text = "Ходов: " + h;
            }
            catch (Exception ex)
            { }
        }
        private void clickUp(object sender, EventArgs e)
        {
            Button tmp = sender as Button;
            int x = (tmp.Location.X - range) / size;
            int y = (tmp.Location.Y - range) / size;
            
            try
            {
                Button temp = new Button();
                temp.Text= field[x, y + 1].Text;
                temp.Image = field[x, y + 1].Image;
                field[x, y + 1].Text = field[x, y].Text;
                field[x, y + 1].Image = field[x, y].Image;
                field[x, y].Text = temp.Text;
                field[x, y].Image = temp.Image;
                HideArrows();
                temp.Dispose();
                h++;
                hod.Text = "Ходов: " + h;
            }
            catch (Exception ex)
            { }
        }
        private void clickLeft(object sender, EventArgs e)
        {
            Button tmp = sender as Button;
            int x = (tmp.Location.X - range) / size;
            int y = (tmp.Location.Y - range) / size;

            try
            {
                Button temp = new Button();
                temp.Text = field[x+1, y].Text;
                temp.Image = field[x+1, y].Image;
                field[x+1, y].Text = field[x, y].Text;
                field[x+1, y].Image = field[x, y].Image;
                field[x, y].Text = temp.Text;
                field[x, y].Image = temp.Image;
                HideArrows();
                temp.Dispose();
                h++;
                hod.Text = "Ходов: " + h;
            }
            catch (Exception ex)
            { }
        }
        private void clickRight(object sender, EventArgs e)
        {
            Button tmp = sender as Button;
            int x = (tmp.Location.X - range) / size;
            int y = (tmp.Location.Y - range) / size;

            try
            {
                Button temp = new Button();
                temp.Text = field[x - 1, y].Text;
                temp.Image = field[x - 1, y].Image;
                field[x - 1, y].Text = field[x, y].Text;
                field[x - 1, y].Image = field[x, y].Image;
                field[x, y].Text = temp.Text;
                field[x, y].Image = temp.Image;
                HideArrows();
                temp.Dispose();
                h++;
                hod.Text = "Ходов: " + h;
            }
            catch (Exception ex)
            { }
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
            try
            {
                if (field[x + 1, y].Text == "")
                {
                    arrowRight.Location = new Point((x + 1) * size + range, y * size + range);
                    arrowRight.Visible = true;
                    arrowRight.BringToFront();
                }
            }
            catch (Exception ex)
            { }
            try
            {
                if (field[x, y - 1].Text == "")
                {
                    arrowUp.Location = new Point(x * size + range, (y - 1) * size + range);
                    arrowUp.Visible = true;
                    arrowUp.BringToFront();
                }
            }
            catch (Exception ex)
            { }
            try
            {
                if (field[x, y + 1].Text == "")
                {
                    arrowDown.Location = new Point(x * size + range, (y + 1) * size + range);
                    arrowDown.Visible = true;
                    arrowDown.BringToFront();
                }
            }
            catch (Exception ex)
            { }
        }
        private void HideArrows()
        {
            arrowDown.Visible = false;
            arrowUp.Visible = false;
            arrowLeft.Visible = false;
            arrowRight.Visible = false;
            win();
        }

        private bool check(int[] mas)
        {
            int sum = 0;
            int zero = -1;
            int n = 0;
            for (int i = 0; i < 14; i++)
            {
                n = 0;
                for (int j = i + 1; j < 16; j++)
                {
                    if (mas[i] == 0)
                    {
                        zero = i;
                        break;
                    }
                    else
                    {
                        if (mas[j] < mas[i])
                        {
                            n++;
                        }
                    }

                }
                sum += n;
            }
            sum += zero;
            if (sum % 2 == 0)
                return true;
            else
                return false;
        }
        private void win()
        {
            for(int i=0;i<16;i++)
            {
                if (mas[i] != i)
                    return;
            }
            End end = new End();
            end.Location = this.Location;
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

        private void Game_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
