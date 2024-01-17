using System.IO;
using static SDL2.SDL;



namespace Clangen
{
    internal class Program
    {
        internal static int Main(string[] Args)
        {
            Directory.SetCurrentDirectory("C:\\Users\\dnbow\\OneDrive\\Desktop\\ClangenNET"); // TEMPORARY FIX

            InternalContext.Prepare();
            InternalContext.Load();

            Context.Screens.SetScreen("Menu");

            InternalContext.RunningState = true;
            while (InternalContext.RunningState)
            {
                InternalContext.Tick();
                InternalContext.Render();

                SDL_Delay(16); // Temporary fix for a 60 fps limiter
            }

            return 0;
        }
    }
}
