using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionToEquationsInOneVariable.Techniques
{
    internal class FixedPointIteration
    {
        static double Solve(Func<double, double> g, double p0, double tolerance, int n0)
        {
            int i = 1;

            while (i <= n0)
            {
                double p = g(p0);

                if (Math.Abs(p - p0) < tolerance)
                {
                    return p;
                }

                i++;
                p0 = p;
            }

            throw new Exception($"Method failed after {n0} iterations");
        }

        public static void Run()
        {
            // g = x^4 + 2x^2 − x − 3.
            Func<double, double> g = x => Math.Pow(x, 4) + 2 * Math.Pow(x, 2) - x - 3;
            double p0 = 1;
            double tolerance = 0.0001;
            int n0 = 1000;

            double p = Solve(g, p0, tolerance, n0);

            Console.WriteLine($"p = {p}");
        }
    }
}
