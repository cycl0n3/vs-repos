using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionToEquationsInOneVariable.Lib
{
    internal class Permutation<T>
    {
        private readonly T[] _items;
        private readonly int _n;
        private readonly int[] _indexes;
        private readonly int[] _cycles;

        public Permutation(T[] items)
        {
            _items = items;
            _n = items.Length;
            _indexes = new int[_n];
            _cycles = new int[_n];
        }

        public IEnumerable<T[]> GetPermutations()
        {
            for (int i = 0; i < _n; i++)
            {
                _indexes[i] = 0;
                _cycles[i] = i;
            }

            yield return _items.ToArray();

            int j = 0;

            while (j < _n)
            {
                if (_indexes[j] < j)
                {
                    Swap(j % 2 == 0 ? 0 : _indexes[j], j);
                    _indexes[j]++;
                    j = 0;
                    yield return _items.ToArray();
                }
                else
                {
                    _indexes[j] = 0;
                    j++;
                }
            }
        }

        // Like [1, 2, 3]
        public static string ConvertToString<U>(U[] items)
        {
            return $"[{string.Join(", ", items)}]";
        }

        private void Swap(int i, int j)
        {
            (_items[j], _items[i]) = (_items[i], _items[j]);
        }

        public static void Run()
        {
            var items = new[] { 1, 2, 3 };

            var permutation = new Permutation<int>(items);

            foreach (var p in permutation.GetPermutations())
            {
                Console.WriteLine(ConvertToString(p));
            }
        }
    }
}
