#nullable enable
using System.Collections.Generic;
using UnityEngine;

namespace GeneralInterfaceAssembly
{
    public class PermutationCalculator
    {
        public List<int> GeneratePermutations(List<int> elements,int randomProbability)
        {
            var result = new List<List<int>>();
            GeneratePermutationsHelper(elements.Count, elements, result);
            // foreach (var list in result)
            // {
            //     foreach (var num in list)
            //     {
            //         Debug.Log(num);
            //     }
            //     Debug.Log(",");
            // }
            return result[randomProbability];
        }

        void GeneratePermutationsHelper(int n, List<int> elements, List<List<int>> result)
        {
            if (n == 1)
            {
                result.Add(new List<int>(elements));
            }
            else
            {
                for (int i = 0; i < n - 1; i++)
                {
                    GeneratePermutationsHelper(n - 1, elements, result);
                    Swap(elements, n % 2 == 0 ? i : 0, n - 1);
                }
                GeneratePermutationsHelper(n - 1, elements, result);
            }
        }

        static void Swap(List<int> elements, int i, int j)
        {
            (elements[i], elements[j]) = (elements[j], elements[i]);
        }
    }
}