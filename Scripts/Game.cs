using System;
using static SDL2.SDL;
using static SDL2.SDL_image;

namespace Clangen
{
    public class GameState
    {
        public IntPtr Window;
        public IntPtr Renderer;

        public bool isRunning = false;

        public int Init()
        {
            SDL_SetHint(SDL_HINT_WINDOWS_DISABLE_THREAD_NAMING, "1");
            if (SDL_Init(SDL_INIT_VIDEO) < 0)
            {
                Console.WriteLine($"There was an issue initilizing SDL. {SDL_GetError()}");
                return 10;
            }

            Window = SDL_CreateWindow("SDL .NET 6 Tutorial", SDL_WINDOWPOS_UNDEFINED, SDL_WINDOWPOS_UNDEFINED, 640, 480, SDL_WindowFlags.SDL_WINDOW_SHOWN);
            if (Window == IntPtr.Zero)
            {
                Console.WriteLine($"There was an issue creating the window. {SDL_GetError()}");
                return 11;
            }

            Renderer = SDL_CreateRenderer(Window, -1, SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);
            if (Renderer == IntPtr.Zero)
            {
                Console.WriteLine($"There was an issue creating the renderer. {SDL_GetError()}");
                return 12;
            }

            if (IMG_Init(IMG_InitFlags.IMG_INIT_PNG) == 0)
            {
                Console.WriteLine($"There was an issue initilizing SDL2_Image {IMG_GetError()}");
                return 13;
            }

            SDL_SetRenderDrawColor(Renderer, 135, 206, 235, 255);
            return 0;
        }
    }
}
