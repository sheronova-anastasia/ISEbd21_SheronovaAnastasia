using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP1
{
    public partial class FormPlane : Form
    {
        private Bomber plane;
        public FormPlane()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Метод отрисовки самолёта
        /// </summary>
        private void Draw()
        {
            Bitmap bmp = new Bitmap(pictureBoxBombers.Width, pictureBoxBombers.Height);
            Graphics gr = Graphics.FromImage(bmp);
            plane.DrawPlane(gr);
            pictureBoxBombers.Image = bmp;
        }
        /// <summary>
        /// Обработка нажатия кнопки "Создать"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCreate_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            plane = new Bomber(rnd.Next(100, 300), rnd.Next(1000, 2000), Color.Green,
           Color.Brown, true, true, true);
            plane.SetPosition(rnd.Next(10, 100), rnd.Next(10, 100), pictureBoxBombers.Width,
           pictureBoxBombers.Height);
            Draw();
        }
        /// <summary>
        /// Обработка нажатия кнопок управления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMove_Click(object sender, EventArgs e)
        {
            //получаем имя кнопки
            string name = (sender as Button).Name;
            switch (name)
            {
                case "buttonUp":
                    plane.MoveTransport(Direction.Up);
                    break;
                case "buttonDown":
                    plane.MoveTransport(Direction.Down);
                    break;
                case "buttonLeft":
                    plane.MoveTransport(Direction.Left);
                    break;
                case "buttonRight":
                    plane.MoveTransport(Direction.Right);
                    break;
            }
            Draw();
        }
    }
}
