

using SDL2;
using System;
using static SDL2.SDL;
using static SDL2.SDL_image;

namespace Clangen
{
    internal class Program
    {
        static int Main(string[] args)
        {
            GameState gameState = new GameState();


            while (gameState.isRunning)
            {
                while (SDL_PollEvent(out SDL_Event e) == 1)
                {
                    switch (e.type)
                    {
                        case SDL_EventType.SDL_QUIT:
                            gameState.isRunning = false;
                            break;
                    }
                }

                if (SDL_RenderClear(gameState.Renderer) < 0)
                    Console.WriteLine($"There was an issue with clearing the render surface. {SDL_GetError()}");
                


                SDL_RenderPresent(gameState.Renderer);
            }

            SDL_DestroyRenderer(gameState.Renderer);
            SDL_DestroyWindow(gameState.Window);
            SDL_Quit();

            return 0;
        }
    }
}
