using System;

namespace System
{
    static class RandomExtensions
    {
        public static T NextItemRnd<T>(this Random rnd, params T[] items) => items[rnd.Next(items.Length)];
    }
}
