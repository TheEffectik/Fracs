using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Fractals {
    internal class Complex {
        public double X { get; set; }
        public double Y { get; set; }
        public Complex(double x, double y) {
            X = x;
            Y = y;
        }
        public Complex() {
            X = 0.0;
            Y = 0.0;
        }

        public static Complex operator + (Complex c1, Complex c2) {
            return new Complex(c1.X + c2.X, c1.Y + c2.Y);
        }
        public static Complex operator - (Complex c1, Complex c2) {
            return new Complex(c1.X - c2.X, c1.Y - c2.Y);
        }
        public static Complex operator * (Complex c1, Complex c2) {
            return new Complex(c1.X * c2.X - c1.Y * c2.Y, c1.X * c2.Y + c1.Y * c2.X);
        }
        public static Complex operator / (Complex c1, Complex c2) {
            return new Complex((c1.X + c2.X + c1.Y * c2.Y) / (c2.X * c2.X + c2.Y * c2.Y), 
                               (c1.Y * c2.X - c1.X * c2.Y) / (c2.X * c2.X + c2.Y * c2.Y));
        }

        public double Abs() {
            return Math.Sqrt(X * X + Y * Y);
        }
        public Complex Sq() {
            return (new Complex(X, Y)) * (new Complex(X, Y));
        }

        public Complex f(Complex c) {
            return (new Complex(X, Y)).Sq() + c;
        }
    }
}
