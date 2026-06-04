using System;
using System.Collections.Generic;

namespace MKO_LIB
{
    public class ChordStep
    {
        public int Iteration { get; set; }
        public double A { get; set; }
        public double B { get; set; }
        public double X { get; set; }
        public double FX { get; set; }
    }

    public class ChordMethod
    {
        private readonly Func<double, double> _function;

        public ChordMethod(Func<double, double> function)
        {
            _function = function;
        }

        public ChordResult Solve(double a, double b, double epsilon)
        {
            if (_function(a) * _function(b) >= 0)
            {
                throw new ArgumentException("Функція повинна мати різні знаки на кінцях відрізка f(a)*f(b) < 0!");
            }

            double x = 0;
            int iterations = 0;
            var steps = new List<ChordStep>();

            while (true)
            {
                iterations++;

                double fa = _function(a);
                double fb = _function(b);

                x = a - (fa * (b - a)) / (fb - fa);
                double fx = _function(x);

                steps.Add(new ChordStep
                {
                    Iteration = iterations,
                    A = a,
                    B = b,
                    X = x,
                    FX = fx
                });

                if (Math.Abs(fx) < epsilon)
                {
                    break;
                }

                if (fa * fx < 0)
                {
                    b = x;
                }
                else
                {
                    a = x;
                }
            }

            return new ChordResult
            {
                Root = x,
                FunctionValue = _function(x),
                Iterations = iterations,
                Steps = steps
            };
        }

        public ChordResult SolveAlternative(double a, double b, double epsilon)
        {
            double aFixed = a;
            double fa = _function(aFixed);
            double xn = b; 
            double xn1 = xn;
            int iterations = 0;
            var steps = new List<ChordStep>();

            while (true)
            {
                iterations++;
                double fxn = _function(xn);

                xn1 = xn - (fxn * (xn - aFixed)) / (fxn - fa);
                double fxn1 = _function(xn1);

                steps.Add(new ChordStep
                {
                    Iteration = iterations,
                    A = aFixed,
                    B = xn,
                    X = xn1,
                    FX = fxn1
                });

                if (Math.Abs(xn1 - xn) < epsilon)
                {
                    break;
                }

                xn = xn1;
            }

            return new ChordResult
            {
                Root = xn1,
                FunctionValue = _function(xn1),
                Iterations = iterations,
                Steps = steps
            };
        }
    }

    public class ChordResult
    {
        public double Root { get; set; }
        public double FunctionValue { get; set; }
        public int Iterations { get; set; }
        public List<ChordStep> Steps { get; set; } = new List<ChordStep>();
    }
}