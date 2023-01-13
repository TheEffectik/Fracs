using System;
using System.Drawing;
using System.Windows.Forms;

namespace Fractals {
    public partial class Form : System.Windows.Forms.Form {
        private Bitmap MyImage;
        private int width = 700;
        private int height = 700;

        private int K = 100;
        private Complex LeftTop = new Complex(-2, 2);
        private Complex RightDown = new Complex(2, -2);
        private double INF = 2.0;

        public Form() {
            InitializeComponent();

            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Width = width;
            pictureBox1.Height = height;

            createBitmap();

        }

        Complex f(Complex z, Complex c) {
            return z * z + c;
        }
        int iterations(Complex z, Complex c) {
            int res = 0;
            while (res < K && z.Abs() <= INF) {
                z = z.f(c);
                res++;
            }

            return res;
        }

        Complex convert_to_complex(int x, int y) {
            y = height - y;
            Complex z = new Complex {
                X = x / (double)width * (RightDown.X - LeftTop.X) + LeftTop.X,
                Y = y / (double)height * (LeftTop.Y - RightDown.Y) + RightDown.Y
            };
            return z;
        }

        private Color hsv2rgb(double h, double s, double v) {
            Func<double, int> f = delegate (double n)
            {
                double k = (n + h * 6) % 3;
                return (int)((v - (v * s * (Math.Max(0, Math.Min(Math.Min(k, 4 - k), 1))))) * 255);
            };
            return Color.FromArgb(f(20), f(15), f(1));
        }
        Color get_color(int cnt) {
            double hue = 255.0 * cnt / K;
            double saturation = 255.0;
            double value = 255.0;
            if (cnt == K)
                value = 0.0;
            Color res = hsv2rgb(hue / 255, saturation / 255, value / 255);

            return res;
        }

        private void createBitmap() {
            Bitmap bmp = new Bitmap(width, height);

            using (var graphics = Graphics.FromImage(bmp)) {
                graphics.FillRectangle(Brushes.White, 0, 0, bmp.Width, bmp.Height);
                for (int x = 0; x < bmp.Width; x++) {
                    for (int y = 0; y < bmp.Height; y++) {
                        Complex z = convert_to_complex(x, y);
                        
                        int cnt = iterations(new Complex(0, 0), z);
                        
                        Color col = get_color(cnt);
                        bmp.SetPixel(y, x, col);
                    }
                }
            }

            MyImage = bmp;
            pictureBox1.Image = MyImage;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) {
            Complex newCenter = convert_to_complex(e.Y, e.X);
            double pastWidth = (RightDown.X - LeftTop.X) / 2.0;
            double pastHeight = (LeftTop.Y - RightDown.Y) / 2.0;
            double zoom = 2.0;
            LeftTop.Y = newCenter.Y + pastHeight / zoom;
            LeftTop.X = newCenter.X - pastWidth / zoom;
            RightDown.Y = newCenter.Y - pastHeight / zoom;
            RightDown.X = newCenter.X + pastWidth / zoom;
            createBitmap();
        }

        private void Form_Load(object sender, EventArgs e)
        {

        }
    }
}
