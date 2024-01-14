using System.IO;
using System.Collections.Generic;
using static SDL2.SDL;
using Newtonsoft.Json;
using System.Linq;
using System;

namespace Clangen.Cats
{
    public class SpriteLoader
    {
        private Dictionary<string, Spritesheet> Spritesheets;

        public SpriteLoader()
        {
            Spritesheets = new Dictionary<string, Spritesheet>();
        }

        internal void Load() // TEMPORARY UNTIL DYNAMICISMS ARE ADDED
        {
            foreach (string Path in Directory.GetFiles("Common\\Sprites", "*.*", SearchOption.AllDirectories))
            {
                string File = Path.Substring(15, Path.Length - 19);

                if (File.StartsWith("Colours"))
                {
                    var CurrentSheet = Spritesheets[File.Substring(7, File.Length - 7)] = new Spritesheet(Path);

                    for (int i = 0; i < 420; i++)
                    {
                        string SpriteID = "";
                        int X = 0, Y = 0;

                        if (i / 21 == 13)
                            continue;

                        switch (i / 21)
                        {
                            case 0: SpriteID = "White"; break;
                            case 1: SpriteID = "PaleGray"; X = 1; break;
                            case 2: SpriteID = "Silver"; X = 2; break;
                            case 3: SpriteID = "Grey"; X = 3; break;
                            case 4: SpriteID = "DarkGrey"; X = 4; break;
                            case 5: SpriteID = "Ghost"; X = 5; break;
                            case 6: SpriteID = "Black"; X = 6; break;
                            case 7: SpriteID = "Cream"; Y = 1; break;
                            case 8: SpriteID = "PaleGinger"; X = 1; Y = 1; break;
                            case 9: SpriteID = "Golden"; X = 2; Y = 1; break;
                            case 10: SpriteID = "Ginger"; X = 3; Y = 1; break;
                            case 11: SpriteID = "DarkGinger"; X = 4; Y = 1; break;
                            case 12: SpriteID = "Sienna"; X = 5; Y = 1; break;
                            case 14: SpriteID = "LightBrown"; Y = 2; break;
                            case 15: SpriteID = "Lilac"; X = 1; Y = 2; break;
                            case 16: SpriteID = "Brown"; X = 2; Y = 2; break;
                            case 17: SpriteID = "GoldenBrown"; X = 3; Y = 2; break;
                            case 18: SpriteID = "DarkBrown"; X = 4; Y = 2; break;
                            case 19: SpriteID = "Chocolate"; X = 5; Y = 2; break;
                        }

                        X *= 3; 
                        Y *= 7;
                        SpriteID += ".";

                        switch (i % 21)
                        {
                            case 0: SpriteID += "Kit0"; break;
                            case 1: SpriteID += "Kit1"; X += 1; break;
                            case 2: SpriteID += "Kit2"; X += 2; break;
                            case 3: SpriteID += "Adolescent0"; Y += 1; break;
                            case 4: SpriteID += "Adolescent1"; X += 1; Y += 1; break;
                            case 5: SpriteID += "Adolescent2"; X += 2; Y += 1; break;
                            case 6: SpriteID += "Young"; Y += 2; break;
                            case 7: SpriteID += "Adult"; X += 1; Y += 2; break;
                            case 8: SpriteID += "Senior"; X += 2; Y += 2; break;
                            case 9: SpriteID += "YoungLong"; Y += 3; break;
                            case 10: SpriteID += "AdultLong"; X += 1; Y += 3; break;
                            case 11: SpriteID += "SeniorLong"; X += 2; Y += 3; break;
                            case 12: SpriteID += "Elder"; Y += 4; break;
                            case 13: SpriteID += "ElderLong"; X += 1; Y += 4; break;
                            case 14: SpriteID += "ElderApp"; X += 2; Y += 4; break;
                            case 15: SpriteID += "AdultParalyzed"; Y += 5; break;
                            case 16: SpriteID += "YoungParalyzed"; X += 1; Y += 5; break;
                            case 17: SpriteID += "NewbornParalyzed"; X += 2; Y += 5; break;
                            case 18: SpriteID += "AdultSick"; Y += 6; break;
                            case 19: SpriteID += "YoungSick"; X += 1; Y += 6; break;
                            case 20: SpriteID += "Newborn"; X += 2; Y += 6; break;
                        }

                        CurrentSheet.Sprites[SpriteID] = new SDL_Rect() { x = X * 50, y = Y * 50, w = 50, h = 50 };
                    }
                }
            }
        }

        public Image this[string Identifier] 
        {
            get
            {
                string[] Path = Identifier.Split('.');
                return Spritesheets[Path[0]][$"{Path[1]}.{Path[2]}"];
            }
        }
    }

    public class Spritesheet
    {
        public readonly Dictionary<string, SDL_Rect> Sprites;
        public readonly Image TextureAtlas;
        public short SpriteSize;

        public Spritesheet(string Path, int Size = 50, Dictionary<string, SDL_Rect>  Sprites = null) 
        {
            this.Sprites = Sprites ?? new Dictionary<string, SDL_Rect>();
            TextureAtlas = new Image(Path);
            SpriteSize = (short)Size;
        }

        public Image this[string Identifer]
        {
            get
            {
                Image Texture = new Image(SpriteSize, SpriteSize);
                Context.RenderOnto(TextureAtlas, Texture, Sprites[Identifer]);
                return Texture;
            }
        }
    }
}
