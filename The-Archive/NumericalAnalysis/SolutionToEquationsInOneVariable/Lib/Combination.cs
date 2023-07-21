using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionToEquationsInOneVariable.Lib
{
    internal class Combination<T>
    {
        private readonly T[] _items;
        private int _itemsPerSet;
        private readonly int[] _indexes;
        private readonly int[] _cycles;

        public Combination(T[] items, int itemsPerSet)
        {
            _items = items;
            _itemsPerSet = itemsPerSet;
            _indexes = new int[_itemsPerSet];
            _cycles = new int[_itemsPerSet];
        }

        // Get all combinations of itemsPerSet items from items
        public IEnumerable<T[]> GetCombinations()
        {
            for (int i = 0; i < _itemsPerSet; i++)
            {
                _indexes[i] = i;
                _cycles[i] = i;
            }

            yield return _indexes.Select(i => _items[i]).ToArray();

            int j = 0;

            while (j < _itemsPerSet)
            {
                if (_indexes[j] < _items.Length - 1 - (_itemsPerSet - 1 - j))
                {
                    _indexes[j]++;
                    j = 0;
                    yield return _indexes.Select(i => _items[i]).ToArray();
                }
                else
                {
                    _indexes[j] = _cycles[j];
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
            var temp = _items[i];
            _items[i] = _items[j];
            _items[j] = temp;
        }

        public static void Run()
        {
            var items = new[] { 1, 2, 3, 4 };
            var combination = new Combination<int>(items, 2);

            foreach (var item in combination.GetCombinations())
            {
                Console.WriteLine(ConvertToString(item));
            }
        }
    }
}
