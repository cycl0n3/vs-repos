using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionToEquationsInOneVariable.Techniques
{
    internal class Bisection
    {
        public static double Solve(Func<double, double> f, double a, double b, double tolerance, int iterations)
        {
            if (f(a) * f(b) > 0)
            {
                throw new ArgumentException("f(a) and f(b) must have opposite signs");
            }

            int i = 1;
            double p = 0.0;
            double FA = f(a);

            // print labels
            Console.WriteLine("i".PadRight(5) + "a".PadRight(20) + "b".PadRight(20) + "p".PadRight(20) + "f(p)".PadRight(20));

            while (i <= iterations)
            {
                p = a + (b - a) / 2;

                double FP = f(p);

                // print values
                Console.WriteLine(i.ToString().PadRight(5) + a.ToString().PadRight(20) + b.ToString().PadRight(20) + p.ToString().PadRight(20) + FP.ToString().PadRight(20));

                if (FP == 0 || Math.Abs(FP) < tolerance || (b - a) / 2 < tolerance)
                {
                    return p;
                }

                if (FA * FP > 0)
                {
                    a = p;
                    FA = FP;
                }
                else
                {
                    b = p;
                }

                i++;
            }

            throw new Exception("Method failed after " + iterations + " iterations");
        }

        public static double Solve(Func<double, double> f, double a, double b, int iterations)
        {
            if (f(a) * f(b) > 0)
            {
                throw new ArgumentException("f(a) and f(b) must have opposite signs");
            }

            int i = 1;
            double p = 0.0;
            double FA = f(a);

            while (i <= iterations)
            {
                p = a + (b - a) / 2;

                double FP = f(p);

                if (FA * FP > 0)
                {
                    a = p;
                    FA = FP;
                }
                else
                {
                    b = p;
                }

                i++;
            }

            return p;
        }

        public static double Solve(Func<double, double> f, double a, double b, double tolerance)
        {
            // find solution withing the given tolerance
            int iterations = GetIterations(a, b, tolerance);
            return Solve(f, a, b, iterations);
        }

        public static int GetIterations(double a, double b, double tolerance)
        {
            return (int)Math.Ceiling(Math.Log((b - a) / tolerance, 2));
        }

        static void Q1()
        {
            Func<double, double> f;
            Tuple<double, double> interval;
            double result;

            // f(x) = root(x) - cos(x), interval = (0, 1)
            f = x => Math.Sqrt(x) - Math.Cos(x);
            interval = Tuple.Create(0.0, 1.0);

            result = Solve(f, interval.Item1, interval.Item2, 3);
            Console.WriteLine("\nFunction f(x) = root(x) - cos(x), interval = (0, 1)");
            Console.WriteLine("Result: " + result);
            Console.WriteLine($"f({result}) = " + f(result));
        }

        static void Q3()
        {
            Func<double, double> f;
            Tuple<double, double> interval;
            double result;

            // Use the Bisection method to find solutions accurate to within 10^−2 for x^3 − 7x^2 + 14x − 6 = 0
            // on the intervals [0, 1]
            f = x => Math.Pow(x, 3) - 7 * Math.Pow(x, 2) + 14 * x - 6;
            interval = Tuple.Create(0.0, 1.0);
            int iterations = GetIterations(interval.Item1, interval.Item2, 0.01);
            result = Solve(f, interval.Item1, interval.Item2, iterations);

            Console.WriteLine("\nFunction f(x) = x^3 − 7x^2 + 14x − 6, interval = (0, 1)");
            Console.WriteLine("Iteratoins: " + iterations);
            Console.WriteLine("Result: " + result);
            Console.WriteLine($"f({result}) = " + f(result));

            interval = Tuple.Create(1.0, 3.2);
            iterations = GetIterations(interval.Item1, interval.Item2, 0.01);
            result = Solve(f, interval.Item1, interval.Item2, iterations);

            Console.WriteLine("\nFunction f(x) = x^3 − 7x^2 + 14x − 6, interval = (1, 3.2)");
            Console.WriteLine("Iteratoins: " + iterations);
            Console.WriteLine("Result: " + result);
            Console.WriteLine($"f({result}) = " + f(result));

            interval = Tuple.Create(3.2, 4.0);
            iterations = GetIterations(interval.Item1, interval.Item2, 0.01);
            result = Solve(f, interval.Item1, interval.Item2, iterations);

            Console.WriteLine("\nFunction f(x) = x^3 − 7x^2 + 14x − 6, interval = (3.2, 4)");
            Console.WriteLine("Iteratoins: " + iterations);
            Console.WriteLine("Result: " + result);
            Console.WriteLine($"f({result}) = " + f(result));
        }

        static void Q5()
        {
            Func<double, double> f;
            Tuple<double, double> interval;
            double tolerance;
            int iterations;
            double result;

            // Use the Bisection method to find solutions accurate to within 10^−5 for x − 2^−x = 0
            // on the intervals [0, 1]
            f = x => x - Math.Pow(2, -x);
            interval = Tuple.Create(0.0, 1.0);
            tolerance = 1E-5;
            iterations = GetIterations(interval.Item1, interval.Item2, tolerance);
            result = Solve(f, interval.Item1, interval.Item2, iterations);

            Console.WriteLine("\nFunction f(x) = x − 2^−x, interval = (0, 1)");
            Console.WriteLine("Tolerance: " + tolerance);
            Console.WriteLine("Iterations: " + iterations);
            Console.WriteLine("Result: " + result);
            Console.WriteLine($"f({result}) = " + f(result));

            // Use the Bisection method to find solutions accurate to within 10^−5 for e^x − x^2 + 3x − 2 = 0
            // on the intervals [0, 1]
            f = x => Math.Pow(Math.E, x) - Math.Pow(x, 2) + 3 * x - 2;
            interval = Tuple.Create(0.0, 1.0);
            tolerance = 1E-5;
            iterations = GetIterations(interval.Item1, interval.Item2, tolerance);
            result = Solve(f, interval.Item1, interval.Item2, iterations);

            Console.WriteLine("\nFunction f(x) = e^x − x^2 + 3x − 2, interval = (0, 1)");
            Console.WriteLine("Tolerance: " + tolerance);
            Console.WriteLine("Iterations: " + iterations);
            Console.WriteLine("Result: " + result);
            Console.WriteLine($"f({result}) = " + f(result));

            // Use the Bisection method to find solutions accurate to within 10^−5 for 2xcos(2x) − (x + 1)^2 = 0
            // on the intervals [-3, -2]
            f = x => 2 * x * Math.Cos(2 * x) - Math.Pow(x + 1, 2);
            interval = Tuple.Create(-3.0, -2.0);
            tolerance = 1E-5;
            iterations = GetIterations(interval.Item1, interval.Item2, tolerance);
            result = Solve(f, interval.Item1, interval.Item2, iterations);

            Console.WriteLine("\nFunction f(x) = 2xcos(2x) − (x + 1)^2, interval = (-3, -2)");
            Console.WriteLine("Tolerance: " + tolerance);
            Console.WriteLine("Iterations: " + iterations);
            Console.WriteLine("Result: " + result);
            Console.WriteLine($"f({result}) = " + f(result));

            // Use the Bisection method to find solutions accurate to within 10^−5 for 2xcos(2x) − (x + 1)^2 = 0
            // on the intervals [-1, 0]
            f = x => 2 * x * Math.Cos(2 * x) - Math.Pow(x + 1, 2);
            interval = Tuple.Create(-1.0, 0.0);
            tolerance = 1E-5;
            iterations = GetIterations(interval.Item1, interval.Item2, tolerance);
            result = Solve(f, interval.Item1, interval.Item2, iterations);

            Console.WriteLine("\nFunction f(x) = 2xcos(2x) − (x + 1)^2, interval = (-1, 0)");
            Console.WriteLine("Tolerance: " + tolerance);
            Console.WriteLine("Iterations: " + iterations);
            Console.WriteLine("Result: " + result);
            Console.WriteLine($"f({result}) = " + f(result));

            // Use the Bisection method to find solutions accurate to within 10^−5 for xcos(x) − 2x^2 + 3x − 1 = 0
            // on the intervals [0.2, 0.3]
            f = x => x * Math.Cos(x) - 2 * Math.Pow(x, 2) + 3 * x - 1;
            interval = Tuple.Create(0.2, 0.3);
            tolerance = 1E-5;
            iterations = GetIterations(interval.Item1, interval.Item2, tolerance);
            result = Solve(f, interval.Item1, interval.Item2, iterations);

            Console.WriteLine("\nFunction f(x) = xcos(x) − 2x^2 + 3x − 1, interval = (0.2, 0.3)");
            Console.WriteLine("Tolerance: " + tolerance);
            Console.WriteLine("Iterations: " + iterations);
            Console.WriteLine("Result: " + result);
            Console.WriteLine($"f({result}) = " + f(result));

            // Use the Bisection method to find solutions accurate to within 10^−5 for xcos(x) − 2x^2 + 3x − 1 = 0
            // on the intervals [1.2, 1.3]
            f = x => x * Math.Cos(x) - 2 * Math.Pow(x, 2) + 3 * x - 1;
            interval = Tuple.Create(1.2, 1.3);
            tolerance = 1E-5;
            iterations = GetIterations(interval.Item1, interval.Item2, tolerance);
            result = Solve(f, interval.Item1, interval.Item2, iterations);

            Console.WriteLine("\nFunction f(x) = xcos(x) − 2x^2 + 3x − 1, interval = (1.2, 1.3)");
            Console.WriteLine("Tolerance: " + tolerance);
            Console.WriteLine("Iterations: " + iterations);
            Console.WriteLine("Result: " + result);
            Console.WriteLine($"f({result}) = " + f(result));
        }

        static void Q13()
        {
            Func<double, double> f;
            Tuple<double, double> interval;
            double tolerance;
            int iterations;
            double result;

            // Find an approximation to 3√25 correct to within 10^−4 using the Bisection Algorithm
            f = x => Math.Pow(x, 3) - 25;
            interval = Tuple.Create(2.0, 3.0);
            tolerance = 1E-4;
            iterations = GetIterations(interval.Item1, interval.Item2, tolerance);
            result = Solve(f, interval.Item1, interval.Item2, iterations);

            Console.WriteLine("Function f(x) = x^3 - 25, interval = (2, 3)");
            Console.WriteLine("Tolerance: " + tolerance);
            Console.WriteLine("Iterations: " + iterations);
            Console.WriteLine("Result: " + result);
            Console.WriteLine($"f({result}) = " + f(result));
        }

        public static void Run()
        {
            //Q5();
            Q13();
        }
    }
}
