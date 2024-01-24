using System.IO;
using static SDL2.SDL;
using Clangen.Cats;
using System;
using Clangen.UI;
using System.Diagnostics;

namespace Clangen
{
    public class Debug : IScreen
    {
        public int CatSize = 14 * 16;

        public Image Texture { get; set; }

        public void Construct() 
        {
            var Cats = Context.Cats;

            for (ushort i = 0; i < CatSize; i++)
            {
                Cats.CreateNewCat();
                Cats[i].Looks.Expand();
            }
        }

        public void Render()
        {
            var Cats = Context.Cats;
            int Width = Context.Window.Width / 50;

            using (var Target = new RenderTarget(Texture))
            {
                var Timer = Stopwatch.StartNew();
                for (ushort i = 0; i < CatSize; i++)
                {
                    Cats[i].Looks.RenderTo(new Rect { X = 50 * (i % Width), Y = 50 * (i / Width), W = 50, H = 50 });
                }
                Timer.Stop();
                Console.WriteLine($"Time taken to render {CatSize} kittys: {Timer.ElapsedMilliseconds / 1000.0d} seconds : Avg {(Timer.ElapsedMilliseconds / 1000.0d) / CatSize} seconds");
            }
        }

        public void Deconstruct()
        {

        }

        public void Tick(SDL_Event Event)
        {

        }
    }


    internal class Program
    {
        internal static int Main(string[] Args)
        {
            Directory.SetCurrentDirectory("C:\\Users\\dnbow\\OneDrive\\Desktop\\ClangenNET"); // TEMPORARY FIX

            InternalContext.Prepare();
            InternalContext.Load();

            Context.Screens.SetScreen("Debug");

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
