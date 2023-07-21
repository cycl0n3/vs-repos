using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionToEquationsInOneVariable.Techniques
{
    internal class SecantMethod
    {
        static Tuple<double, int> Solve(Func<double, double> f, double p0, double p1, double eps = 1E-6, int n0 = 25)
        {
            double p = 0;
            int i = 2;

            double q0 = f(p0);
            double q1 = f(p1);

            while (i <= n0)
            {
                p = p1 - q1 * (p1 - p0) / (q1 - q0);

                if (Math.Abs(p - p1) < eps)
                {
                    return new Tuple<double, int>(p, i);
                }

                i++;

                p0 = p1;
                q0 = q1;

                p1 = p;
                q1 = f(p);
            }

            return new Tuple<double, int>(p, n0);
        }

        public static void Run()
        {
            Func<double, double> f;

            // f = cos(x) - x/2
            f = x => Math.Cos(x) - x / 2;

            var result = Solve(f, -3, 3);

            Console.WriteLine("f(x) = cos(x) - x/2");
            Console.WriteLine("Root: {0}", result.Item1);
            Console.WriteLine("Iterations: {0}", result.Item2);
        }
    }
}
