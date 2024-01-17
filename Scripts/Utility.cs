using Clangen.Cats;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using static Clangen.Utility;
using static SDL2.SDL;

namespace Clangen
{
    public delegate void EventHandlerEmpty();
    /// <summary>
    /// A class that wraps one-time and/or miscellaneous declarations into one package
    /// </summary>
    public static class Utility
    {
        public static void Log(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Represents a <see cref="System.Random"/> Class with additional features
        /// </summary>
        public class CRandom : System.Random
        {
            public CRandom(int Seed) : base(Seed) { }
            public CRandom() : base() { }

            public bool Chance(double Chance)
            {
                return Sample() <= Chance;
            }

            public bool Choose()
            {
                return Sample() <= .5;
            }

            public T ChooseFrom<T>(T[] Array)
            {
                return Array[(int)(Sample() * Array.Length)];
            }
            public T ChooseFrom<T>(T[] Array, int[] Weights)
            {
                if (Weights.Length != Array.Length)
                    throw new ArgumentException("Weights length must be the same as the given Arrays' length");

                int CumulativeWeight = 0;

                for (int i = 0; i < Weights.Length; i++)
                    CumulativeWeight += Weights[i];
                

                int Choice = (int)(Sample() * CumulativeWeight);

                for (int i = 0; i < Weights.Length; i++)
                    if (Choice <= Weights[i])
                        return Array[Choice];
                    else
                        Choice -= Weights[i];

                // Just here because im not 100% sold this function works
                throw new Exception("This shouldnt be raised, dimmie oopsied somewhere");
            }
            public T ChooseFrom<T>(List<T> List)
            {
                return ChooseFrom(List.ToArray());
            }
            public T ChooseFrom<T>(List<T> List, int[] Weights)
            {
                return ChooseFrom(List.ToArray(), Weights);
            }
        }
    }
}
