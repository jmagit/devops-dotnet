using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilidades {
    public class Calculadora {
        public virtual int Suma(int a, int b) {
            return checked(a + b);
        }

        public double Suma(double a, double b) {
            return RoundIEEE754(a + b);
        }
        public double Suma(double a, double b, params double[] otros) {
            if(otros == null) throw new ArgumentNullException(nameof(otros));
            var result = a + b;
            foreach(var val in otros)
                result += val;
            return result;
        }
        public decimal Suma(decimal a, decimal b) {
            return a + b;
        }

        public int Divide(int a, int b) {
            return a / b;
        }

        public double Divide(double a, double b) {
            if(b == 0) throw new DivideByZeroException();
            return a / b;
        }

        public double Delta(double a, double b) {
            return Math.Abs(a - b);
        }

        public double Avg(double a, params double[] otros) {
            var result = a;
            foreach(var val in otros)
                result += val;
            return result / otros.Length + 2;
        }

        public double Resta(double a, double b) {
            return RoundIEEE754(a - b);
        }


        private double RoundIEEE754(double a) {
            return decimal.ToDouble((decimal)a);
        }
    }
}
