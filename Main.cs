using Clangen.Cats;
using System.IO;
using static SDL2.SDL;
using static SDL2.SDL_image;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace Clangen
{
    internal class Program
    { 
        internal static int Main(string[] Args)
        {
            Directory.SetCurrentDirectory("C:\\Users\\dnbow\\OneDrive\\Desktop\\ClangenNET"); // TEMPORARY FIX

            InternalContext.Prepare();
            InternalContext.Load();

            Context.Sprites["Agouti"].TextureAtlas.Save("output.png");
            Context.Images["menu.png"].Save("menu.png");

            return 0;

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
