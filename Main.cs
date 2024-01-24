using System;
using System.IO;
using System.Diagnostics;
using static SDL2.SDL;
using Clangen.UI;
using Clangen.Cats;



namespace Clangen
{
    public class Debug : IScreen
    {
        public const int XStep = 15, YStep = 15;
        public static readonly int Width = Context.Window.Width / XStep;
        public static readonly int Height = Context.Window.Height / YStep;
        public static readonly int CatAmount = Width * Height;

        public Image Texture { get; set; }

        public void Construct() 
        {
            var Cats = Context.Cats;

            Console.WriteLine("CAT RENDER TEST");
            Console.WriteLine($"Rendering {CatAmount} Cat(s) . . .");

            for (ushort i = 0; i < CatAmount; i++)
            {
                Cats.NewCat();
                Cats[i].Looks.Expand();
            }
        }

        public void Render()
        {
            var Cats = Context.Cats;

            using (var Target = new RenderTarget(Texture))
            {
                Stopwatch Timer = Stopwatch.StartNew();
                for (ushort i = 0; i < CatAmount; i++)
                {
                    Cats[i].Looks.RenderTo(new Rect { X = XStep * (i % Width), Y = YStep * (i / Width), W = 50, H = 50 });

                    if (i % (CatAmount / 100) == 0)
                    {
                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.Write($"Rendered {i / (CatAmount / 100)}% . . .");
                    }
                }
                Timer.Stop();
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write($"Rendered 100% . . .\nTime taken to render {CatAmount} kittys: {Timer.ElapsedMilliseconds / 1000.0d} seconds : Avg {(Timer.ElapsedMilliseconds / 1000.0d) / CatAmount} seconds");
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
            Directory.SetCurrentDirectory("C:\\Users\\dnbow\\OneDrive\\Desktop\\ClangenNET"); // TEMP FIX

            InternalContext.Prepare();
            InternalContext.Load();

            Context.Screens.SetScreen("Debug");

            InternalContext.RunningState = true;
            while (InternalContext.RunningState)
            {
                InternalContext.Tick();
                InternalContext.Render();

                SDL_Delay(16); // TEMP fix for a 60 fps limiter
            }

            return 0;
        }
    }
}
