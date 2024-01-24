using System;
using System.Collections.Generic;
using static SDL2.SDL;
using static SDL2.SDL_image;
using static SDL2.SDL_ttf;
using Clangen.UI;
using Clangen.Cats;
using Clangen.Events;
using Clangen.Managers;
using static Clangen.Utility;
using YamlDotNet.Serialization;
using VYaml.Parser;
using System.IO;
using System.Linq;

namespace Clangen
{
    [Flags]
    internal enum RenderAction : byte
    {
        Clear = 1,
        Present = 2,
        Rebuild = 4,
    }

    public class TranslationFile
    {
        private Dictionary<string, string> Translations;
        public readonly string Name;


        public TranslationFile(string Name)
        {
            Translations = new Dictionary<string, string>();
            this.Name = Name;
        }

        public void Patch(Dictionary<string, string> Translations)
        {
            foreach (string Key in Translations.Keys)
                this.Translations[Key] = Translations[Key];
        }

        public string this[string Key]
        {
            get {
                if (Translations.TryGetValue(Key, out string Value))
                    return Value;
                else
                    return Key;
            }
        }
    }

    public class Text
    {
        private static Deserializer YamlParser = new Deserializer();
        private Dictionary<string, TranslationFile> Translations;
        private TranslationFile CurrentTranslation;

        public void LoadTranslationFile(string Path)
        {
            if (!File.Exists(Path))
            {
                Log($"Cant load TranslationFile \"{Path}\"");
                return;
            }

            var Data = YamlParser.Deserialize<Dictionary<string, Dictionary<string, string>>>(File.ReadAllText(Path));

            foreach (string Language in Data.Keys)
            {
                if (!Translations.ContainsKey(Language))
                {
                    Translations[Language] = new TranslationFile(Language);
                }

                Translations[Language].Patch(Data[Language]);
            }
        }

        public Text()
        {
            Translations = new Dictionary<string, TranslationFile>();

            string[] TranslationFiles = Directory.GetFiles("Common\\Localisation", "*.*", SearchOption.AllDirectories);
            for (int i = 0; i < TranslationFiles.Length; i++)
                LoadTranslationFile(TranslationFiles[i]);

            CurrentTranslation = Translations["LEnglish"];
        }

        public string this[string Key]
        {
            get
            {
                return CurrentTranslation[Key];
            }
        }
    }

    /// <summary>
    /// Represents a class where shared and/or commonly used declarations that both third and first body developers can use
    /// </summary>
    public static class Context
    {
        public static EventPipeline Events = new EventPipeline();

        /// <summary>
        /// Represents the Version and Build of the game
        /// </summary>
        public static class Version
        {
            public const byte Major = 0;
            public const byte Minor = 0;
            public const ushort Revision = 1;

            /// <summary>
            /// A nice little label that represents the current major/minor update
            /// </summary>
            public const string Label = "Primordial Soup";

            /// <summary>
            /// A 32-bit int summarising the version, useful in save data and error logging
            /// </summary>
            public const uint Build = (Major << 24) + (Minor << 16) + Revision;
        }

        /// <summary>
        /// See <see cref="ImageManager"/> for more info
        /// </summary>
        public static ImageManager Images = new ImageManager();

        /// <summary>
        /// See <see cref="FontManager"/> for more info
        /// </summary>
        public static FontManager Fonts = new FontManager();

        /// <summary>
        /// See <see cref="ScreenManager"/> for more info
        /// </summary>
        public static ScreenManager Screens = new ScreenManager();

        public static SpriteLoader Sprites = new SpriteLoader();

        public static CatManager Cats = new CatManager();

        /// <summary>
        /// Pointer to SDL_Renderer*
        /// </summary>
        public static Renderer Renderer;


        public static Text Text = new Text();


        public static void Clear()
        {
            InternalContext.RenderState |= RenderAction.Clear;
        }
        public static void Present()
        {
            InternalContext.RenderState |= RenderAction.Present;
        }

        /// <summary>
        /// Render a texture to the screen
        /// </summary>
        /// <param name="Texture">The Source texture</param>
        /// <returns>Whether or not the Texture was rendered</returns>
        /// <remarks></remarks>
        public static void Render(Image Texture, SDL_Rect? SourceRect = null, SDL_Rect? DestinationRect = null)
        {
            SDL_Rect srcrect, dstrect;

            if (SourceRect is not null && DestinationRect is not null)
            {
                srcrect = SourceRect.Value;
                dstrect = DestinationRect.Value;
                SDL_RenderCopy(Renderer, Texture, ref srcrect, ref dstrect);
            }
            else if (SourceRect is not null && DestinationRect is null)
            {
                srcrect = SourceRect.Value;
                SDL_RenderCopy(Renderer, Texture, ref srcrect, IntPtr.Zero);
            }
            else if (SourceRect is null && DestinationRect is not null)
            {
                dstrect = DestinationRect.Value;
                SDL_RenderCopy(Renderer, Texture, IntPtr.Zero, ref dstrect);
            }
            else if (SourceRect is null && DestinationRect is null)
            {
                SDL_RenderCopy(Renderer, Texture, IntPtr.Zero, IntPtr.Zero);
            }

            InternalContext.RenderState |= RenderAction.Present;
        }

        public static void RenderEx(Image Texture, SDL_Rect? SourceRect = null, SDL_Rect? DestinationRect = null, bool HorizontalFlip = false, bool VerticalFlip = false)
        {
            SDL_RendererFlip Flip = (HorizontalFlip ? SDL_RendererFlip.SDL_FLIP_HORIZONTAL : 0) | (VerticalFlip ? SDL_RendererFlip.SDL_FLIP_VERTICAL : 0);
            SDL_Rect srcrect, dstrect;

            if (SourceRect is not null && DestinationRect is not null)
            {
                srcrect = SourceRect.Value;
                dstrect = DestinationRect.Value;
                SDL_RenderCopyEx(Renderer, Texture, ref srcrect, ref dstrect, 0, IntPtr.Zero, Flip);
            }
            else if (SourceRect is not null && DestinationRect is null)
            {
                srcrect = SourceRect.Value;
                SDL_RenderCopyEx(Renderer, Texture, ref srcrect, IntPtr.Zero, 0, IntPtr.Zero, Flip);
            }
            else if (SourceRect is null && DestinationRect is not null)
            {
                dstrect = DestinationRect.Value;
                SDL_RenderCopyEx(Renderer, Texture, IntPtr.Zero, ref dstrect, 0, IntPtr.Zero, Flip);
            }
            else if (SourceRect is null && DestinationRect is null)
            {
                SDL_RenderCopyEx(Renderer, Texture, IntPtr.Zero, IntPtr.Zero, 0, IntPtr.Zero, Flip);
            }

            InternalContext.RenderState |= RenderAction.Present;
        }

        public static void RenderOnto(Image Source, Image Destination, SDL_Rect? SourceRect = null, SDL_Rect? DestinationRect = null)
        {
            using var Target = new RenderTarget(Destination);
            Render(Source, SourceRect, DestinationRect);
        }
        

        /// <summary>
        /// Represents the games UI, Composition and Color
        /// </summary>
        public static ThemeManager Theme = new ThemeManager();

        /// <summary>
        /// Represents the Application Window currently being used
        /// </summary>
        public static Window Window;

        public static CRandom Random = new CRandom();


        public static class Config
        {
            public static class CatAges
            {
                public static int Newborn = 1;
                public static int Kitten = 6;
                public static int Adolescent = 12;
                public static int YoungAdult = 48;
                public static int Adult = 96;
                public static int SeniorAdult = 120;
                public static int Senior = 301;
            }
        }



        public static Clan Clan;

        public static readonly List<Element> HoveredElements = new List<Element>();

        public static EventHandlerEmpty OnQuit;
    }

    /// <summary>
    /// Represents a class inherently unsafe declarations or ones that shouldnt be accessable to anybody but first body developers
    /// </summary>
    internal static class InternalContext
    {
        internal static bool RunningState = false;
        internal static RenderAction RenderState;
        internal static ulong GameTick;

        /// <summary>
        /// Set up important systems
        /// </summary>
        /// <returns>0 on success, anything else on failure</returns>
        public static int Prepare()
        {
            SDL_SetHint(SDL_HINT_WINDOWS_DISABLE_THREAD_NAMING, "1"); // Allows for .NET Threads
            if (SDL_Init(SDL_INIT_VIDEO) < 0)
            {
                Log($"Library SDL Uninitialised -> {SDL_GetError()}");
                return -10;
            }

            if (IMG_Init(IMG_InitFlags.IMG_INIT_PNG) == 0)
            {
                Log($"Library SDL_IMAGE Uninitialised -> {IMG_GetError()}");
                return -11;
            }

            if (TTF_Init() < 0)
            {
                Log($"Library SDL_TTF Uninitialised -> {TTF_GetError()}");
                return -12;
            }

            Context.Window = new Window("Dnbows' Clangen", SDL_WINDOWPOS_UNDEFINED, SDL_WINDOWPOS_UNDEFINED, 800, 700, SDL_WindowFlags.SDL_WINDOW_SHOWN);
            if (!Context.Window.Exists)
            {
                Log($"Window Creation failed: {SDL_GetError()}");
                return -13;
            }

            Context.Renderer = SDL_CreateRenderer(Context.Window, -1, SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);
            if (!Context.Renderer.Exists)
            {
                Log($"SDL_CreateRenderer failed: {SDL_GetError()}");
                return -14;
            }

            return 0;
        }


        internal static int Load()
        {
            Context.Fonts.Reload();
            Context.Theme.Reload();
            Context.Screens.Reload();
            Context.Sprites.Load();
            return 0;
        }

        internal static void Quit()
        {
            RunningState = false;

            Context.OnQuit?.Invoke();

            Context.Window.Destroy();
            Context.Renderer.Destroy();

            foreach (Font Font in Context.Fonts)
                Font.Destroy();
            foreach (Image Image in Image.References)
                Image.Destroy();

            TTF_Quit();
            IMG_Quit();
            SDL_Quit();
        }

        internal static void Render()
        {
            if (RenderState == 0) 
                return;

            if ((RenderState & RenderAction.Clear) != 0)
            {
                SDL_RenderClear(Context.Renderer);
            }

            else if ((RenderState & RenderAction.Rebuild) != 0)
            {
                Color Background = Context.Theme.Background;
                SDL_SetRenderDrawColor(Context.Renderer, Background.Red, Background.Green, Background.Blue, Background.Alpha);
                SDL_RenderClear(Context.Renderer);
                Context.Screens.Current.Construct();
                Context.Screens.Current.Render();
                Context.Render(Context.Screens.Current.Texture);
            }

            SDL_RenderPresent(Context.Renderer);
            RenderState = 0;
        }


        internal static void Tick()
        {
            GameTick = SDL_GetTicks64();

            while (SDL_PollEvent(out SDL_Event Event) == 1)
            {
                switch (Event.type)
                {
                    case SDL_EventType.SDL_QUIT:
                        Quit();
                        break;

                    case SDL_EventType.SDL_KEYUP:
                        switch (Event.key.keysym.sym)
                        {
                            case SDL_Keycode.SDLK_F12:
                                DateTime Now = DateTime.Now;
                                Context.Window.Screenshot().Save($"Screenshots/Screenshot{Now.Year,4}{Now.Month,2}{Now.Day,2}{Now.Hour,2}{Now.Minute,2}{Now.Second,2}{Now.Millisecond,4}.png".Replace(" ", "0"));
                                break;

                            default:
                                break;
                        }
                        break;

                    default:
                        break;
                }

                Context.Screens.Current?.Tick(Event);
            }
        }
    } 
}
