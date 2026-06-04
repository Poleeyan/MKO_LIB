using System;
using System.Collections.Generic;
using System.Numerics;

namespace MKO_LIB
{
    public class NewtonStep
    {
        public int Iteration { get; set; }
        public double XPrev { get; set; }
        public double FXPrev { get; set; }
        public double DFxPrev { get; set; }
        public double XCurr { get; set; }
        public double Precision { get; set; }
    }

    public class ComplexNewtonStep
    {
        public int Iteration { get; set; }
        public Complex ZPrev { get; set; }
        public Complex ZCurr { get; set; }
        public double Precision { get; set; }
    }

    public class ComplexNewtonResult
    {
        public Complex Root { get; set; }
        public int Iterations { get; set; }
        public List<ComplexNewtonStep> Steps { get; set; } = new List<ComplexNewtonStep>();
    }

    public class NewtonMethod
    {
        private readonly Func<double, double> _function;
        private readonly Func<double, double>? _derivative;

        public NewtonMethod(Func<double, double> function, Func<double, double> derivative)
        {
            _function = function;
            _derivative = derivative;
        }

        public NewtonMethod(Func<double, double> function)
        {
            _function = function;
            _derivative = null;
        }

        public NewtonResult Solve(double x, double delta)
        {
            if (_derivative == null)
            {
                throw new InvalidOperationException("Похідна не визначена. Використовуйте конструктор з похідною для методу Ньютона.");
            }

            double x0;
            double fDerivative;
            int iterations = 0;
            double precision;
            var steps = new List<NewtonStep>();

            do
            {
                x0 = x;
                fDerivative = _derivative(x0);
                
                if (Math.Abs(fDerivative) < 1e-10)
                {
                    throw new InvalidOperationException("Похідна близька до нуля. Метод дотичних не збігається.");
                }

                double fx0 = _function(x0);
                x = x0 - fx0 / fDerivative;

                iterations++;
                precision = Math.Abs(x - x0);

                steps.Add(new NewtonStep
                {
                    Iteration = iterations,
                    XPrev = x0,
                    FXPrev = fx0,
                    DFxPrev = fDerivative,
                    XCurr = x,
                    Precision = precision
                });

            } while (precision > delta);

            return new NewtonResult
            {
                Root = x,
                FunctionValue = _function(x),
                Iterations = iterations,
                Precision = precision,
                Steps = steps
            };
        }

        public NewtonResult SolveSecant(double x, double h, double delta)
        {
            double x0;
            double fDerivative;
            int iterations = 0;
            double precision;
            var steps = new List<NewtonStep>();

            do
            {
                x0 = x;
                double fx0 = _function(x0);
                fDerivative = (_function(x0 + h) - fx0) / h;

                if (Math.Abs(fDerivative) < 1e-10)
                {
                    throw new InvalidOperationException("Числова похідна близька до нуля. Метод січних не збігається.");
                }

                x = x0 - fx0 / fDerivative;

                iterations++;
                precision = Math.Abs(x - x0);

                steps.Add(new NewtonStep
                {
                    Iteration = iterations,
                    XPrev = x0,
                    FXPrev = fx0,
                    DFxPrev = fDerivative,
                    XCurr = x,
                    Precision = precision
                });

            } while (precision > delta);

            return new NewtonResult
            {
                Root = x,
                FunctionValue = _function(x),
                Iterations = iterations,
                Precision = precision,
                Steps = steps
            };
        }

        public static ComplexNewtonResult SolveComplex(
            Func<Complex, Complex> function,
            Func<Complex, Complex> derivative,
            Complex z0,
            double epsilon,
            int maxIterations = 100)
        {
            Complex z = z0;
            int iterations = 0;
            double precision;
            var steps = new List<ComplexNewtonStep>();

            do
            {
                iterations++;
                Complex zPrev = z;

                Complex fz = function(z);
                Complex dfz = derivative(z);

                z = z - fz / dfz;

                precision = Complex.Abs(z - zPrev);

                steps.Add(new ComplexNewtonStep
                {
                    Iteration = iterations,
                    ZPrev = zPrev,
                    ZCurr = z,
                    Precision = precision
                });

            } while (precision > epsilon && iterations < maxIterations);

            return new ComplexNewtonResult
            {
                Root = z,
                Iterations = iterations,
                Steps = steps
            };
        }
    }

    public class NewtonResult
    {
        public double Root { get; set; }
        public double FunctionValue { get; set; }
        public int Iterations { get; set; }
        public double Precision { get; set; }
        public List<NewtonStep> Steps { get; set; } = new List<NewtonStep>();
    }
}