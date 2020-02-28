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
    public partial class FormHangar : Form
    {
        /// <summary>
        /// Объект от класса-ангара
        /// </summary>
        Hangar<ITransport> hangar;

        public FormHangar()
        {
            InitializeComponent();
            hangar = new Hangar<ITransport>(12, pictureBoxHangar.Width,
pictureBoxHangar.Height);
            Draw();
        }

        /// <summary>
        /// Метод отрисовки ангара
        /// </summary>
        private void Draw()
        {
            Bitmap bmp = new Bitmap(pictureBoxHangar.Width, pictureBoxHangar.Height);
            Graphics gr = Graphics.FromImage(bmp);
            hangar.Draw(gr);
            pictureBoxHangar.Image = bmp;
        }

        /// <summary>
        /// Обработка нажатия кнопки "Разместить самолёт в ангаре"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSetPlane_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var plane = new Plane(100, 1000, dialog.Color);
                int place = hangar + plane;
                Draw();
            }
        }
        /// <summary>
        /// Обработка нажатия кнопки "Разместить бомбардировщик в ангаре"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSetBomber_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ColorDialog dialogDop = new ColorDialog();
                if (dialogDop.ShowDialog() == DialogResult.OK)
                {
                    var plane = new Bomber(100, 1000, dialog.Color, dialogDop.Color,
                   true, true, true);
                    int place = hangar + plane;
                    Draw();
                }
            }
        }
        /// <summary>
        /// Обработка нажатия кнопки "Забрать"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTakePlane_Click(object sender, EventArgs e)
        {
            if (maskedTextBoxPlace.Text != "")
            {
                var plane = hangar - Convert.ToInt32(maskedTextBoxPlace.Text);
                if (plane != null)
                {
                    Bitmap bmp = new Bitmap(pictureBoxTakePlane.Width,
                   pictureBoxTakePlane.Height);
                    Graphics gr = Graphics.FromImage(bmp);
                    plane.SetPosition(5, 5, pictureBoxTakePlane.Width,
                   pictureBoxTakePlane.Height);
                    plane.DrawPlane(gr);
                    pictureBoxTakePlane.Image = bmp;
                }
                else
                {
                    Bitmap bmp = new Bitmap(pictureBoxTakePlane.Width,
                   pictureBoxTakePlane.Height);
                    pictureBoxTakePlane.Image = bmp;
                }
                Draw();
            }
        }
    }
}
