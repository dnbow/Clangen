using System;
using System.Runtime.InteropServices;
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
    }
}
