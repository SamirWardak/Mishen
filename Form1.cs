using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Target
{
    public partial class Form1 : Form
    {
        private float radius = 100;
        private int count;
        private int counts;
        bool starts = false;
        public Form1()
        {
            InitializeComponent();
            richTextBox1.Visible = false;
            button2.Visible = false;
        }

        private void PaintCircle(int x, int y)
        {
            Graphics grf = this.CreateGraphics();

            Graphics gr = pictureBox1.CreateGraphics();
            gr.DrawEllipse(new Pen(Brushes.Red, 5), x, y, 3,3);
    }


        private void Panel1_MouseClick(object sender, MouseEventArgs e)
        {

            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.DrawToBitmap(bmp, pictureBox1.ClientRectangle);
            //bmp.Save("D:\\testBmp111.bmp", ImageFormat.Bmp);
            Color color = bmp.GetPixel(e.X, e.Y);

            


            if ((color.R.ToString() == "211" && color.G.ToString() == "211" && color.B.ToString() == "211")
                || (color.R.ToString() == "255" && color.G.ToString() == "0" && color.B.ToString() == "0")) {
                fToolStripMenuItem.Text = $"Попаданий: {++count}";
            }
            else
            {
                sToolStripMenuItem.Text = $"Промахи: {++counts}";
            }
            chToolStripMenuItem.Text = $"X: {e.X}";
            ptToolStripMenuItem.Text = $"Y: {e.Y}";
            tToolStripMenuItem.Text = $"Всего выстрелов: {count+ counts}";
            PaintCircle(e.X, e.Y);

            


        }


        private void Button2_Click(object sender, System.EventArgs v)
        {

            int res;
            bool isInt = System.Int32.TryParse(richTextBox1.Text, out res);
            if (starts)
            {
                if (isInt)
                {
                    if (float.Parse(richTextBox1.Text) >= pictureBox1.Width / 4 || float.Parse(richTextBox1.Text) < 10)
                    {
                        MessageBox.Show("Radius details error!!!", "Eror");
                    }
                    else
                    {
                        radius = float.Parse(richTextBox1.Text);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter radisu in diapazon 100-250", "Error!!!");
                    return;
                }
                pictureBox1.MouseClick += Panel1_MouseClick;
                chToolStripMenuItem.Text = "";
                ptToolStripMenuItem.Text = "";
                fToolStripMenuItem.Text = "Попаданий 0";
                sToolStripMenuItem.Text = "Промахи 0";
                tToolStripMenuItem.Text = "Всего выстрелов 0";
                pictureBox1.Refresh();
            }

        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (radius != 0 && starts == true)
            {
                e.Graphics.TranslateTransform(pictureBox1.Width / 2f, pictureBox1.Height / 2f);
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;


                using (var brush = new SolidBrush(Color.LightGray))
                {
                    e.Graphics.FillPie(brush, -radius, -radius, radius * 2, radius * 2, -90, 90);
                    e.Graphics.FillPolygon(brush, new PointF[] {
                    new PointF(0, 0),
                    new PointF(0, radius),
                    new PointF(-radius, 0)
                    });
                }

                using (var pen = new Pen(Color.Black) { CustomEndCap = new System.Drawing.Drawing2D.AdjustableArrowCap(2, 10, true) })
                {
                    var w = pictureBox1.Width / 2f;
                    var h = pictureBox1.Height / 2f;
                    e.Graphics.DrawLine(pen, -w, 0, w, 0);
                    e.Graphics.DrawLine(pen, 0, h, 0, -h);
                }
            }
            
        }

        private void StartToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            richTextBox1.Visible = true;
            button2.Visible = true;
            starts = true;
            startToolStripMenuItem.Visible = false;
        }

        private void Form1_Resize(object sender, System.EventArgs e)
        {
            Invalidate();
        }
    }
}
