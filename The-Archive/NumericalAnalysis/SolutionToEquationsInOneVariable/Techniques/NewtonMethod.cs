using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionToEquationsInOneVariable.Techniques
{
    internal class NewtonMethod
    {
        static Tuple<double, int> Solve(Func<double, double> f, Func<double, double> df, double x0, double eps = 1e-6, int n0 = 25)
        {
            double x = x0;

            int i = 1;

            while (i <= n0)
            {
                double fx = f(x);
                double dfx = df(x);

                if (Math.Abs(dfx) < eps)
                {
                    throw new Exception("NewtonMethod: f'(x) is too small");
                }

                double x1 = x - fx / dfx;

                if (Math.Abs(x1 - x) < eps)
                {
                    return Tuple.Create(x1, i);
                }

                x = x1;
                i++;
            }

            throw new Exception($"NewtonMethod: maximum number of iterations {n0} exceeded");
        }

        public static void Run()
        {
            // f(x) = cos(x) - x, x0 = 1
            Func<double, double> f = x => Math.Cos(x) - x;
            Func<double, double> df = x => -Math.Sin(x) - 1;

            var result = Solve(f, df, 1);
            Console.WriteLine(result);

            // Let f(x) = x^2 − 6 and p0 = 1. Use Newton’s method to find p2.
            f = x => Math.Pow(x, 2) - 6;
            df = x => 2 * x;

            result = Solve(f, df, 1);
            Console.WriteLine(result);
        }
    }
}
