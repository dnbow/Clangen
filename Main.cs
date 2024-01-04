using System.IO;
using static SDL2.SDL;

namespace Clangen
{
    internal class Program
    {
        static int Main(string[] args)
        {
            Directory.SetCurrentDirectory("C:\\Users\\dnbow\\OneDrive\\Desktop\\ClangenNET");
            GameState gameState = new GameState();

            gameState.Init();
            gameState.Load();
            gameState.Finalise();

            gameState.SetScreen(gameState.Screens["Menu"]);

            gameState.isRunning = true;

            while (gameState.isRunning)
            {
                gameState.Update();
                gameState.Render();
                SDL_Delay(16); // Temporary fix for a 60 fps limiter
            }

            gameState.Quit();

            return 0;
        }
    }
}
