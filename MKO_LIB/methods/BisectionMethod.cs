using System;
using System.Collections.Generic;

namespace MKO_LIB
{
    public class BisectionStep
    {
        public int Iteration { get; set; }
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }
        public double FC { get; set; }
        public double Precision { get; set; }
    }

    public class BisectionMethod
    {
        private readonly Func<double, double> _function;

        public BisectionMethod(Func<double, double> function)
        {
            _function = function;
        }

        public BisectionResult Solve(double a, double b, double delta)
        {
            if (_function(a) * _function(b) > 0)
            {
                throw new ArgumentException("f(a) та f(b) повинні мати різні знаки!");
            }

            double c;
            int iterations = 0;
            var steps = new List<BisectionStep>();

            do
            {
                c = (a + b) / 2;
                iterations++;
                double fc = _function(c);

                steps.Add(new BisectionStep
                {
                    Iteration = iterations,
                    A = a,
                    B = b,
                    C = c,
                    FC = fc,
                    Precision = Math.Abs(b - a)
                });

                if (_function(a) * fc > 0)
                {
                    a = c;
                }
                else
                {
                    b = c;
                }

            } while (Math.Abs(b - a) >= delta);

            return new BisectionResult
            {
                Root = c,
                FunctionValue = _function(c),
                Iterations = iterations,
                Precision = Math.Abs(b - a),
                Steps = steps
            };
        }
    }

    public class BisectionResult
    {
        public double Root { get; set; }
        public double FunctionValue { get; set; }
        public int Iterations { get; set; }
        public double Precision { get; set; }
        public List<BisectionStep> Steps { get; set; } = new List<BisectionStep>();
    }
}