using System;
using System.Collections.Generic;
using System.Text;

namespace SlotMachine
{
    public static class Extensions
    {
        // Extension method to called to print the rows of the table during a spin 
        public static string Print<T>(this List<T> list)
        {
            string s = string.Empty;
            for (int i = 0; i < list.Count; i++)
                s += list[i].ToString();

            return s;
        }
    }
}
