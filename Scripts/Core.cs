using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using static SDL2.SDL;
using static SDL2.SDL_ttf;
using static SDL2.SDL_image;
using Clangen.UI;
using static Clangen.Utility;
using System.Net.Http;
using Clangen.Parsers;

namespace Clangen
{
    /// <summary>
    /// Represents a Color in RGBA format
    /// </summary>
    /// <remarks>Interchangable with <see cref="SDL_Color"/></remarks>
    public struct Color
    {
        public byte Red;
        public byte Green;
        public byte Blue;
        public byte Alpha;

        public Color(byte Red, byte Green, byte Blue, byte Alpha = 255)
        {
            this.Red = Red;
            this.Green = Green;
            this.Blue = Blue;
            this.Alpha = Alpha;
        }
        public Color(uint ColorValue)
        {
            Red = (byte)(ColorValue & 0xFF000000 >> 24);
            Green = (byte)(ColorValue & 0xFF0000 >> 16);
            Blue = (byte)(ColorValue & 0xFF00 >> 8);
            Alpha = (byte)(ColorValue & 0xFF);
        }
        public Color(string HexValue)
        {
            string CleanHex = new string(HexValue.Replace("0x", "").Where(i => 47 < i && i < 58 || 64 < i && i < 71 || 96 < i && i < 103).ToArray()); // Accounts for X000000, #000000 formats etc by getting only 0 - 9 and A - F

            Red = Green = Blue = 0;
            Alpha = 255;

            if (CleanHex.Length == 6)
                Value = (Convert.ToUInt32(CleanHex, 16) << 8) | 0xFF;
            else if (CleanHex.Length == 8)
                Value = Convert.ToUInt32(CleanHex, 16);
            else
                throw new Exception("Couldnt convert \"{HexValue}\" to Color"); // TEMP
        }

        public uint Value
        {
            get => (uint)((Red << 24) | (Green << 16) | (Blue << 8) | Alpha);
            set
            {
                Red = (byte)((value & 0xFF000000) >> 24);
                Green = (byte)((value & 0xFF0000) >> 16);
                Blue = (byte)((value & 0xFF00) >> 8);
                Alpha = (byte)(value & 0xFF);
            }
        }

        public override string ToString() => $"Color({Red}, {Green}, {Blue}, {Alpha})";
        public static implicit operator SDL_Color(Color Value) => new SDL_Color() { r = Value.Red, b = Value.Blue, g = Value.Green, a = Value.Alpha };
        public static implicit operator Color(SDL_Color Value) => new Color(Value.r, Value.g, Value.b, Value.a);
    }

    /// <summary>
    /// Represents a Rectangle
    /// </summary>
    /// <remarks>Interchangable with <see cref="SDL_Rect"/></remarks>
    public struct Rect
    {
        public int X;
        public int Y;
        public int W;
        public int H;

        public int R
        {
            get => X + W;
            set => W = value - X;
        }
        public int B
        {
            get => Y + H;
            set => H = value - Y;
        }

        public (int X, int Y) Center
        {
            get => (X: X + (W / 2), Y: Y + (H / 2));
            set { X = value.X; Y = value.Y; }
        }

        public static implicit operator SDL_Rect(Rect Value) => new SDL_Rect { x = Value.X, y = Value.Y, w = Value.W, h = Value.H };
        public static implicit operator Rect(SDL_Rect Value) => new Rect { X = Value.x, Y = Value.y, W = Value.w, H = Value.h };
    }

    /// <summary>
    /// Represents a 2D Vector
    /// </summary>
    public struct Vector2
    {
        public int X;
        public int Y;

        public static implicit operator (int, int)(Vector2 Value) => (Value.X, Value.Y);
        public static implicit operator Vector2((int, int) Value) => new Vector2 { X = Value.Item1, Y = Value.Item2 };
    }
    
    /// <summary>
    /// Represents a SDL_Window object in a safe context, with additional functionality
    /// </summary>
    public class Window
    {
        public const SDL_WindowFlags DEFAULT_FLAGS = SDL_WindowFlags.SDL_WINDOW_SHOWN;
        public SDL_WindowFlags ActiveFlags => (SDL_WindowFlags)SDL_GetWindowFlags(Pointer);

        private readonly IntPtr Pointer;
        private readonly SDL_Rect _Rect;

        /// <summary>
        /// Gets a Rect of the Window
        /// </summary>
        public SDL_Rect Rect => _Rect;

        /// <summary>
        /// Gets the Width of the Window the game is running in
        /// </summary>
        public int Width => _Rect.w;

        /// <summary>
        /// Gets the Height of the Window the game is running in
        /// </summary>
        public int Height => _Rect.h;

        /// <summary>
        /// Gets whether or not the Window is fullscreen
        /// </summary>
        /// <remarks>Returns True for both normal fullscreen and bordered fullscreen</remarks>
        public bool Fullscreen
        {
            get => (ActiveFlags & SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP) != 0;
        }

        public bool Exists => Pointer != null;

        public Window(string Title, int X, int Y, int W, int H, SDL_WindowFlags? flags = null)
        {
            Pointer = SDL_CreateWindow(Title, X, Y, W, H, flags ?? DEFAULT_FLAGS);
            _Rect = new SDL_Rect { x = X, y = Y, w = W, h = H };
        }
        public Window(IntPtr Pointer)
        {
            this.Pointer = Pointer;
        }
        ~Window() => Destroy();

        public void Destroy()
        {
            SDL_DestroyWindow(Pointer);
        }

        public Image Screenshot()
        {
            SDL_GetRendererOutputSize(Context.Renderer, out int W, out int H);
            IntPtr Screenshot = SDL_CreateRGBSurface(0, W, H, 32, 0x000000FF, 0x0000FF00, 0x00FF0000, 0xFF000000);
            Image Texture;
            unsafe
            {
                SDL_Surface* Surface = (SDL_Surface*)Screenshot;
                SDL_Rect rect = new SDL_Rect { x = 0, y = 0, w = Context.Window.Width, h = Context.Window.Height };

                SDL_LockSurface(Screenshot);
                SDL_RenderReadPixels(SDL_GetRenderer(Context.Window), ref rect, ((SDL_PixelFormat*)(Surface->format))->format, Surface->pixels, Surface->pitch);
                SDL_UnlockSurface(Screenshot);
                Texture = SDL_CreateTextureFromSurface(Context.Renderer, Screenshot);
                SDL_FreeSurface(Screenshot);
            }

            return Texture;
        }

        public static implicit operator IntPtr(Window Value) => Value == null ? IntPtr.Zero : Value.Pointer;
        public static implicit operator Window(IntPtr Value) => new Window(Value);
    }

    /// <summary>
    /// Represents a SDL_Renderer object in a safe context, with additional functionality
    /// </summary>
    public class Renderer
    {
        public const SDL_RendererFlags DEFAULT_FLAGS = SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC;
        public SDL_RendererFlags ActiveFlags => (SDL_RendererFlags)Info.flags;
        public SDL_RendererInfo Info
        {
            get
            {
                SDL_GetRendererInfo(Pointer, out SDL_RendererInfo info);
                return Info;
            }
        }

        private readonly IntPtr Pointer;

        public bool Exists => Pointer != null;

        public Renderer(Window window, SDL_RendererFlags? flags = null)
        {
            Pointer = SDL_CreateRenderer(window, -1, (flags ?? DEFAULT_FLAGS));
            if (Pointer == null)
            {
                Log($"Error on Renderer creation: {SDL_GetError()}");
                return;
            }
        }
        public Renderer(IntPtr Pointer)
        {
            this.Pointer = Pointer;
        }
        ~Renderer() => Destroy();


        public void Destroy()
        {
            SDL_DestroyRenderer(Pointer);
        }

        public static implicit operator IntPtr(Renderer Value) => Value == null ? IntPtr.Zero : Value.Pointer;
        public static implicit operator Renderer(IntPtr Value) => new Renderer(Value);
    }

    /// <summary>
    /// Represents a SDL_Texture object in a safe context, with additional functionality
    /// </summary>
    public class Image
    {
        private readonly IntPtr Pointer;
        private SDL_Rect _Rect;

        public SDL_Rect Rect => _Rect;

        public Image(string Path)
        {
            Pointer = IMG_LoadTexture(Context.Renderer, Path);

            SDL_QueryTexture(Pointer, out uint _, out int _, out _Rect.w, out _Rect.h);
        }
        public Image(IntPtr Pointer)
        {
            this.Pointer = Pointer;

            SDL_QueryTexture(Pointer, out uint _, out int _, out _Rect.w, out _Rect.h);
        }
        public Image(int Width, int Height, SDL_Color? Fill = null)
        {
            Pointer = SDL_CreateTexture(Context.Renderer, SDL_PIXELFORMAT_RGBA8888, (int)SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET, Width, Height);
            SDL_SetRenderTarget(Context.Renderer, Pointer);
            SDL_SetRenderDrawColor(Context.Renderer, Fill?.r ?? 0, Fill?.g ?? 0, Fill?.b ?? 0, Fill?.a ?? 0);
            SDL_RenderClear(Context.Renderer);
            SDL_SetRenderTarget(Context.Renderer, IntPtr.Zero);

            SDL_QueryTexture(Pointer, out uint _, out int _, out _Rect.w, out _Rect.h);
        }
        public Image(SDL_Color Fill) : this(Context.Window.Width, Context.Window.Height, Fill) {}
        ~Image() => Destroy();

        public void Destroy()
        {
            if (Pointer != null)
                SDL_DestroyTexture(Pointer);
        }

        public void Save(string Path)
        {
            unsafe
            {
                SDL_QueryTexture(Pointer, out uint format, out int _, out int W, out int H);
                IntPtr TempTexture = SDL_CreateTexture(Context.Renderer, SDL_PIXELFORMAT_RGBA8888, (int)SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET, W, H);

                SDL_SetRenderTarget(Context.Renderer, TempTexture);
                SDL_SetRenderDrawColor(Context.Renderer, 0x00, 0x00, 0x00, 0x00);
                SDL_RenderClear (Context.Renderer);

                SDL_RenderCopy(Context.Renderer, Pointer, IntPtr.Zero, IntPtr.Zero);

                IntPtr SurfacePointer = SDL_CreateRGBSurface(0, W, H, 32, 0xFF000000, 0xFF0000, 0xFF00, 0xFF);
                SDL_Surface* Surface = (SDL_Surface*)SurfacePointer;
                SDL_Rect SurfaceRect = new SDL_Rect { x = 0, y = 0, w = W, h = H };

                SDL_RenderReadPixels(Context.Renderer, ref SurfaceRect, SDL_PIXELFORMAT_RGBA8888, Surface->pixels, Surface->pitch);

                IMG_SavePNG(SurfacePointer, Path);

                SDL_FreeSurface(SurfacePointer);
                SDL_DestroyTexture(TempTexture);

                SDL_SetRenderTarget(Context.Renderer, IntPtr.Zero);
            }

        }

        public static implicit operator IntPtr(Image Value) => Value == null ? IntPtr.Zero : Value.Pointer;
        public static implicit operator Image(IntPtr Value) => new Image(Value);
    }

    /// <summary>
    /// Represents a TTF_Font object in a safe context, with additional functionality
    /// </summary>
    public class Font
    {
        public string Name { get; private set; }

        private readonly IntPtr Face;

        public Font(string Name, IntPtr Face)
        {
            this.Face = Face;
            this.Name = Name;
        }
        ~Font() => Destroy();

        public void Destroy()
        {
            if (Face != null)
                TTF_CloseFont(Face);
        }

        public IntPtr Render(string Text, int Size = -1)
        {
            return IntPtr.Zero;
        }
        public Image Render(string Text, SDL_Rect BoundingBox, int? Size = null, SDL_Color? Color = null)
        {

            TTF_SetFontSize(Face, Size ?? Context.Theme.FontSize);
            IntPtr Surface = TTF_RenderText_Solid_Wrapped(Face, Text, Color ?? Context.Theme.FontColor, (uint)BoundingBox.w);
            Image Texture = SDL_CreateTextureFromSurface(Context.Renderer, Surface);
            SDL_FreeSurface(Surface);

            return Texture;
        }
    }

    /// <summary>
    /// Represents a Font with branches such as a Bold version, Light version or italics
    /// </summary>
    public class FontCollection
    {

    }

    public class ThemeTemplate
    {
        public string Name;
        private Dictionary<string, object> Fields = new Dictionary<string, object>(); // Didnt want to make it so ambiguous but if i dont then everything needs to be statically typed which makes it stinky for modders

        public ThemeTemplate(Dictionary<string, object> Fields)
        {
            this.Fields = Fields;

            Name = Fields.ContainsKey("Name") ? (string)Fields["Name"] : "unnamed_theme";
        }

        public object this[string ID]
        {
            get => Fields[ID];
            set => Fields[ID] = value;
        }
    }

    public class ThemePreset
    {

    }

    public static class Manager
    {
        /// <summary>
        /// Represents a manager for retrieving, caching, modifying and using Images
        /// </summary>
        public class ImageManager
        {
            /// <summary>
            /// A Dictionary with a key of the fonts filename (inluding file extension) and value of an <see cref="Image"/> object
            /// </summary>
            private readonly Dictionary<string, Image> Cache = new Dictionary<string, Image>();

            public void Reload()
            {
                foreach ((string ID, Image Obj) in Cache.Select(i => (i.Key, i.Value)))
                    Obj.Destroy();
                Cache.Clear();
            }

            public Image this[string Identifier]
            {
                get
                {
                    if (Cache.ContainsKey(Identifier))
                        return Cache[Identifier];

                    Image Texture = new Image(IMG_LoadTexture(Context.Renderer, $"Common\\Images\\{Identifier}"));
                    Cache[Identifier] = Texture;
                    return Texture;
                }
            }
        }

        /// <summary>
        /// Represents a manager for retrieving, caching, modifying and using Fonts 
        /// </summary>
        public class FontManager
        {
            /// <summary>
            /// A Dictionary with a key of the fonts filename (inluding file extension) and value of a <see cref="Font"/> object
            /// </summary>
            private static readonly Dictionary<string, Font> Cache = new Dictionary<string, Font>();


            public IEnumerator<Font> GetEnumerator() => Cache.Values.GetEnumerator();


            public void Reload()
            {
                foreach ((string ID, Font Obj) in Cache.Select(i => (i.Key, i.Value)))
                    Obj.Destroy();
                Cache.Clear();

                foreach (string Path in Directory.GetFiles("Common\\Fonts"))
                {
                    if (!Path.EndsWith(".ttf"))
                        continue;

                    string Fontname = Path.Split('\\').Last().Replace(".ttf", "");
                    Utility.Log(Fontname);
                    Cache[Fontname] = new Font(Fontname, TTF_OpenFont(Path, 64));
                }
            }

            public Font this[string Identifier]
            {
                get
                {
                    return Cache.ContainsKey(Identifier) ? Cache[Identifier] : null;
                }
            }
        }

        /// <summary>
        /// Represents a manager for handling, activating and switching Screens
        /// </summary>
        public class ScreenManager
        {
            private BaseScreen[] ScreenArray;
            private readonly List<short> ArrayHistory = new List<short>();

            public BaseScreen Current
            {
                get
                {
                    return (ArrayHistory.Count > 0) ? ScreenArray[ArrayHistory[0]] : null;
                }
                set
                {
                    short idx = (short)Array.FindIndex(ScreenArray, (i) => i == value);

                    if (idx == -1) // If index it wasnt found in ScreenArray, it means somebody did a silly
                        throw new Exception("You cant add Screen classes manually, theyre added automatically!");


                    if (ArrayHistory.Count > 0 && value == ScreenArray[ArrayHistory[0]]) // If its the same, ignore
                        return;

                    if (ArrayHistory.Count > 0)
                    {
                        ScreenArray[ArrayHistory[0]].Deconstruct();
                        ArrayHistory.Insert(0, idx);
                    }
                    else
                        ArrayHistory.Add(idx);

                    value.Construct();
                    InternalContext.RenderState |= RenderAction.Rebuild;
                }
            }

            internal void Reload()
            {
                ScreenArray = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(assembly => assembly.GetTypes())
                    .Where(type => type.IsSubclassOf(typeof(BaseScreen)))
                    .Select(type => Activator.CreateInstance(type) as BaseScreen).ToArray();
            }

            public void SetScreen(string ID)
            {
                Current = ScreenArray.Where((i) => i.GetType().Name == ID).First();
            }
        }

        /// <summary>
        /// Represents a manager for handling, activating and switching Themes
        /// </summary>
        public class ThemeManager
        {
            public Dictionary<string, ThemeTemplate> Themes;
            public ThemeTemplate Current;
            private ThemeTemplate Default;

            public string Name => Current?.Name ?? Default.Name;

            public Color Background => (Color)(Current?["Background"] ?? Default["Background"]);
            public Color BackgroundHovered => (Color)(Current?["BackgroundHovered"] ?? Default["BackgroundHovered"]);
            public Color BackgroundDisabled => (Color)(Current?["BackgroundDisabled"] ?? Default["BackgroundDisabled"]);
            public Color BackgroundSelected => (Color)(Current?["BackgroundSelected"] ?? Default["BackgroundSelected"]);
            public Color BackgroundActive => (Color)(Current?["BackgroundActive"] ?? Default["BackgroundActive"]);

            public Color Border => (Color)(Current?["Border"] ?? Default["Border"]);
            public Color BorderHovered => (Color)(Current?["BorderHovered"] ?? Default["BorderHovered"]);
            public Color BorderDisabled => (Color)(Current?["BorderDisabled"] ?? Default["BorderDisabled"]);
            public Color BorderSelected => (Color)(Current?["BorderSelected"] ?? Default["BorderSelected"]);
            public Color BorderActive => (Color)(Current?["BorderActive"] ?? Default["BorderActive"]);
            public int BorderWidth => (int)(Current?["BorderWidth"] ?? Default["BorderWidth"]);

            public Color Text => (Color)(Current?["Text"] ?? Default["Text"]);
            public Color TextHovered => (Color)(Current?["TextHovered"] ?? Default["TextHovered"]);
            public Color TextDisabled => (Color)(Current?["TextDisabled"] ?? Default["TextDisabled"]);
            public Color TextSelected => (Color)(Current?["TextSelected"] ?? Default["TextSelected"]);
            public Color TextCursor => (Color)(Current?["TextCursor"] ?? Default["TextCursor"]);

            public Color Hyperlink => (Color)(Current?["Hyperlink"] ?? Default["Hyperlink"]);
            public Color HyperlinkHovered => (Color)(Current?["HyperlinkHovered"] ?? Default["HyperlinkHovered"]);
            public Color HyperlinkDisabled => (Color)(Current?["HyperlinkDisabled"] ?? Default["HyperlinkDisabled"]);
            public Color HyperlinkSelected => (Color)(Current?["HyperlinkSelected"] ?? Default["HyperlinkSelected"]);

            public Font FontType => Context.Fonts[(string)(Current?["FontType"] ?? Default["FontType"])];
            public Color FontColor => (Color)(Current?["FontColor"] ?? Default["FontColor"]);
            public int FontSize => (int)(Current?["FontSize"] ?? Default["FontSize"]);
            public int FontBold => (int)(Current?["FontBold"] ?? Default["FontBold"]);

            public Color EntryBox => (Color)(Current?["EntryBox"] ?? Default["EntryBox"]);
            public Color EntryBoxHovered => (Color)(Current?["EntryBoxHovered"] ?? Default["EntryBoxHovered"]);
            public Color EntryBoxDisabled => (Color)(Current?["EntryBoxDisabled"] ?? Default["EntryBoxDisabled"]);
            public Color EntryBoxActive => (Color)(Current?["EntryBoxActive"] ?? Default["EntryBoxActive"]);
            public bool EntryBoxVisible => (bool)(Current?["EntryBoxActive"] ?? Default["EntryBoxActive"]);

            public string Alignment => (string)(Current?["Alignment"] ?? Default["Alignment"]);
            public string AlignmentVertical => (string)(Current?["AlignmentVertical"] ?? Default["AlignmentVertical"]);
            public int AlignmentVerticalPadding => (int)(Current?["AlignmentVerticalPadding"] ?? Default["AlignmentVerticalPadding"]);

            public double VerticalSpacing => (double)(Current?["VerticalSpacing"] ?? Default["VerticalSpacing"]);



            public void Reload()
            {
                Themes = new Dictionary<string, ThemeTemplate>();

                foreach (string Path in Directory.GetFiles("Common\\Themes"))
                {
                    if (!Path.EndsWith(".ini"))
                        continue;

                    foreach (ThemeTemplate template in Load(Path))
                    {
                        Themes[template.Name] = template;

                        if (template.Name == "Original")
                            Default = template;
                    }
                }
            }

            public ThemeTemplate[] Load(string Path) 
            {
                List<ThemeTemplate> templates = new List<ThemeTemplate>();

                Dictionary<string, object> Fields = INI.Load(Path);

                templates.Add(new ThemeTemplate(Fields)); // TEMP

                return templates.ToArray();
            }

            public object this[string ID]
            {
                get => Current?[ID] ?? Default[ID];
            }
        }
    }
}