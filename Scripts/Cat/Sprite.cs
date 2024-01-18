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
                if (!Path.EndsWith(".png"))
                    continue;

                string File = Path.Substring(15, Path.Length - 19);
                Spritesheet CurrentSheet;

                switch (File) {
                    case "ColoursAgouti":
                    case "ColoursBengal":
                    case "ColoursClassic":
                    case "ColoursMackerel":
                    case "ColoursMarbled":
                    case "ColoursMasked":
                    case "ColoursRosette":
                    case "ColoursSingle":
                    case "ColoursSinglestripe":
                    case "ColoursSmoke":
                    case "ColoursSokoke":
                    case "ColoursSpeckled":
                    case "ColoursTabby":
                    case "ColoursTicked":
                        CurrentSheet = Spritesheets[File.Substring(7, File.Length - 7)] = new Spritesheet(Path);

                        for (int i = 0; i < 21 * 20; i++)
                        {
                            string SpriteID = "";
                            int X = 0, Y = 0;

                            if (i / 21 == 13)
                                continue;

                            switch (i / 21)
                            {
                                case 0: SpriteID = "White"; break;
                                case 1: SpriteID = "Palegrey"; X = 1; break;
                                case 2: SpriteID = "Silver"; X = 2; break;
                                case 3: SpriteID = "Grey"; X = 3; break;
                                case 4: SpriteID = "Darkgrey"; X = 4; break;
                                case 5: SpriteID = "Ghost"; X = 5; break;
                                case 6: SpriteID = "Black"; X = 6; break;
                                case 7: SpriteID = "Cream"; Y = 1; break;
                                case 8: SpriteID = "Paleginger"; X = 1; Y = 1; break;
                                case 9: SpriteID = "Golden"; X = 2; Y = 1; break;
                                case 10: SpriteID = "Ginger"; X = 3; Y = 1; break;
                                case 11: SpriteID = "Darkginger"; X = 4; Y = 1; break;
                                case 12: SpriteID = "Sienna"; X = 5; Y = 1; break;
                                case 14: SpriteID = "Lightbrown"; Y = 2; break;
                                case 15: SpriteID = "Lilac"; X = 1; Y = 2; break;
                                case 16: SpriteID = "Brown"; X = 2; Y = 2; break;
                                case 17: SpriteID = "Goldenbrown"; X = 3; Y = 2; break;
                                case 18: SpriteID = "Darkbrown"; X = 4; Y = 2; break;
                                case 19: SpriteID = "Chocolate"; X = 5; Y = 2; break;
                            }

                            X *= 3;
                            Y *= 7;
                            SpriteID += ".";

                            Group(i % 21, ref SpriteID, ref X, ref Y);

                            CurrentSheet.Sprites[SpriteID] = new SDL_Rect() { x = X * 50, y = Y * 50, w = 50, h = 50 };
                        }

                        break;



                    case "Skin":
                        CurrentSheet = Spritesheets[File] = new Spritesheet(Path);

                        for (int i = 0; i < 21 * 18; i++)
                        {
                            string SpriteID;
                            int X, Y;

                            switch (i / 21)
                            {
                                case 0: SpriteID = "Black"; X = 0; Y = 0; break;
                                case 1: SpriteID = "Red"; X = 1; Y = 0; break;
                                case 2: SpriteID = "Pink"; X = 2; Y = 0; break;
                                case 3: SpriteID = "Darkbrown"; X = 3; Y = 0; break;
                                case 4: SpriteID = "Brown"; X = 4; Y = 0; break;
                                case 5: SpriteID = "Lightbrown"; X = 5; Y = 0; break;
                                case 6: SpriteID = "Dark"; X = 0; Y = 1; break;
                                case 7: SpriteID = "Darkgrey"; X = 1; Y = 1; break;
                                case 8: SpriteID = "Grey"; X = 2; Y = 1; break;
                                case 9: SpriteID = "Darksalmon"; X = 3; Y = 1; break;
                                case 10: SpriteID = "Salmon"; X = 4; Y = 1; break;
                                case 11: SpriteID = "Peach"; X = 5; Y = 1; break;
                                case 12: SpriteID = "Darkmarbled"; X = 0; Y = 2; break;
                                case 13: SpriteID = "Marbled"; X = 1; Y = 2; break;
                                case 14: SpriteID = "Lightmarbled"; X = 2; Y = 2; break;
                                case 15: SpriteID = "Darkblue"; X = 3; Y = 2; break;
                                case 16: SpriteID = "Blue"; X = 4; Y = 2; break;
                                case 17: SpriteID = "Lightblue"; X = 5; Y = 2; break;
                                default: SpriteID = ""; X = 0; Y = 0; break;
                            }

                            X *= 3;
                            Y *= 7;
                            SpriteID += ".";

                            Group(i % 21, ref SpriteID, ref X, ref Y);

                            CurrentSheet.Sprites[SpriteID] = new SDL_Rect() { x = X * 50, y = Y * 50, w = 50, h = 50 };
                        }

                        break;



                    case "Lineart":
                    case "LineartDead":
                    case "LineartDarkforest":
                    case "Lighting":
                    case "Shaders":
                        CurrentSheet = Spritesheets[File] = new Spritesheet(Path);

                        for (int i = 0; i < 21; i++)
                        {
                            string SpriteID = "";
                            int X = 0, Y = 0;

                            Group(i, ref SpriteID, ref X, ref Y);

                            CurrentSheet.Sprites[SpriteID] = new SDL_Rect() { x = X * 50, y = Y * 50, w = 50, h = 50 };
                        }

                        break;



                    case "Eyes":
                    case "Eyes2":
                        CurrentSheet = Spritesheets[File] = new Spritesheet(Path);

                        for (int i = 0; i < 21 * 21; i++)
                        {
                            string SpriteID;
                            int X, Y;

                            switch (i / 21)
                            {
                                case 0: SpriteID = "Yellow"; X = 0; Y = 0; break;
                                case 1: SpriteID = "Amber"; X = 1; Y = 0; break;
                                case 2: SpriteID = "Hazel"; X = 2; Y = 0; break;
                                case 3: SpriteID = "Palegreen"; X = 3; Y = 0; break;
                                case 4: SpriteID = "Green"; X = 4; Y = 0; break;
                                case 5: SpriteID = "Blue"; X = 5; Y = 0; break;
                                case 6: SpriteID = "Darkblue"; X = 6; Y = 0; break;
                                case 7: SpriteID = "Grey"; X = 7; Y = 0; break;
                                case 8: SpriteID = "Cyan"; X = 8; Y = 0; break;
                                case 9: SpriteID = "Emerald"; X = 9; Y = 0; break;
                                case 10: SpriteID = "Heatherblue"; X = 10; Y = 0; break;
                                case 11: SpriteID = "Sunlitice"; X = 11; Y = 0; break;
                                case 12: SpriteID = "Copper"; X = 0; Y = 1; break;
                                case 13: SpriteID = "Sage"; X = 1; Y = 1; break;
                                case 14: SpriteID = "Cobalt"; X = 2; Y = 1; break;
                                case 15: SpriteID = "Paleblue"; X = 3; Y = 1; break;
                                case 16: SpriteID = "Bronze"; X = 4; Y = 1; break;
                                case 17: SpriteID = "Silver"; X = 5; Y = 1; break;
                                case 18: SpriteID = "Paleyellow"; X = 6; Y = 1; break;
                                case 19: SpriteID = "Gold"; X = 7; Y = 1; break;
                                case 20: SpriteID = "Greenyellow"; X = 8; Y = 1; break;
                                default: SpriteID = ""; X = 0; Y = 0; break;
                            }

                            X *= 3;
                            Y *= 7;
                            SpriteID += ".";

                            Group(i % 21, ref SpriteID, ref X, ref Y);

                            CurrentSheet.Sprites[SpriteID] = new SDL_Rect() { x = X * 50, y = Y * 50, w = 50, h = 50 };
                        }

                        break;



                    case "FadeMask":
                    case "FadeStarclan":
                    case "FadeDarkforest":
                        CurrentSheet = Spritesheets[File] = new Spritesheet(Path);

                        for (int i = 0; i < 21 * 3; i++)
                        {
                            string SpriteID = "";
                            int X, Y = 0;

                            switch (i / 21)
                            {
                                case 0: SpriteID = "0"; X = 0; break;
                                case 1: SpriteID = "1"; X = 3; break;
                                case 2: SpriteID = "2"; X = 6; break;
                                default: SpriteID = ""; X = 0; break;
                            }

                            Group(i % 21, ref SpriteID, ref X, ref Y);

                            CurrentSheet.Sprites[SpriteID] = new SDL_Rect() { x = X * 50, y = Y * 50, w = 50, h = 50 };
                        }

                        break;



                    case "WhitePatches":
                        CurrentSheet = Spritesheets[File] = new Spritesheet(Path);

                        for (int i = 0; i < 21 * 130; i++)
                        {
                            string SpriteID;
                            int X, Y;

                            switch (i / 21)
                            {
                                // Figure out who made these white patches
                                case 0: SpriteID = "Fullwhite"; X = 0; Y = 0; break;
                                case 1: SpriteID = "Any"; X = 1; Y = 0; break;
                                case 2: SpriteID = "Tuxedo"; X = 2; Y = 0; break;
                                case 3: SpriteID = "Little"; X = 3; Y = 0; break;
                                case 4: SpriteID = "Colourpoint"; X = 4; Y = 0; break;
                                case 5: SpriteID = "Van"; X = 5; Y = 0; break;
                                case 6: SpriteID = "Anytwo"; X = 6; Y = 0; break;
                                case 7: SpriteID = "Moon"; X = 7; Y = 0; break;
                                case 8: SpriteID = "Phantom"; X = 8; Y = 0; break;
                                case 9: SpriteID = "Powder"; X = 9; Y = 0; break;
                                case 10: SpriteID = "Bleached"; X = 10; Y = 0; break;
                                case 11: SpriteID = "Savannah"; X = 11; Y = 0; break;
                                case 12: SpriteID = "Fadespots"; X = 12; Y = 0; break;
                                case 13: SpriteID = "Pebbleshine"; X = 13; Y = 0; break;
                                case 14: SpriteID = "Extra"; X = 0; Y = 1; break;
                                case 15: SpriteID = "OneEar"; X = 1; Y = 1; break;
                                case 16: SpriteID = "Broken"; X = 2; Y = 1; break;
                                case 17: SpriteID = "LightTuxedo"; X = 3; Y = 1; break;
                                case 18: SpriteID = "Buzzardfang"; X = 4; Y = 1; break;
                                case 19: SpriteID = "Ragdoll"; X = 5; Y = 1; break;
                                case 20: SpriteID = "Lightsong"; X = 6; Y = 1; break;
                                case 21: SpriteID = "Vitiligo"; X = 7; Y = 1; break;
                                case 22: SpriteID = "Blackstar"; X = 8; Y = 1; break;
                                case 23: SpriteID = "Piebald"; X = 9; Y = 1; break;
                                case 24: SpriteID = "Curved"; X = 10; Y = 1; break;
                                case 25: SpriteID = "Petal"; X = 11; Y = 1; break;
                                case 26: SpriteID = "Shibainu"; X = 12; Y = 1; break;
                                case 27: SpriteID = "Owl"; X = 13; Y = 1; break;

                                // Ryos White Patches
                                case 28: SpriteID = "Tip"; X = 0; Y = 2; break;
                                case 29: SpriteID = "Fancy"; X = 1; Y = 2; break;
                                case 30: SpriteID = "Freckles"; X = 2; Y = 2; break;
                                case 31: SpriteID = "Ringtail"; X = 3; Y = 2; break;
                                case 32: SpriteID = "HalfFace"; X = 4; Y = 2; break;
                                case 33: SpriteID = "Pantstwo"; X = 5; Y = 2; break;
                                case 34: SpriteID = "Goatee"; X = 6; Y = 2; break;
                                case 35: SpriteID = "Vitiligotwo"; X = 7; Y = 2; break;
                                case 36: SpriteID = "Paws"; X = 8; Y = 2; break;
                                case 37: SpriteID = "Mitaine"; X = 9; Y = 2; break;
                                case 38: SpriteID = "Brokenblaze"; X = 10; Y = 2; break;
                                case 39: SpriteID = "Scourge"; X = 11; Y = 2; break;
                                case 40: SpriteID = "Diva"; X = 12; Y = 2; break;
                                case 41: SpriteID = "Beard"; X = 13; Y = 2; break;
                                case 42: SpriteID = "Tail"; X = 0; Y = 3; break;
                                case 43: SpriteID = "Blaze"; X = 1; Y = 3; break;
                                case 44: SpriteID = "Prince"; X = 2; Y = 3; break;
                                case 45: SpriteID = "Bib"; X = 3; Y = 3; break;
                                case 46: SpriteID = "Vee"; X = 4; Y = 3; break;
                                case 47: SpriteID = "Unders"; X = 5; Y = 3; break;
                                case 48: SpriteID = "Honey"; X = 6; Y = 3; break;
                                case 49: SpriteID = "Farofa"; X = 7; Y = 3; break;
                                case 50: SpriteID = "Damien"; X = 8; Y = 3; break;
                                case 51: SpriteID = "Mister"; X = 9; Y = 3; break;
                                case 52: SpriteID = "Belly"; X = 10; Y = 3; break;
                                case 53: SpriteID = "Tailtip"; X = 11; Y = 3; break;
                                case 54: SpriteID = "Toes"; X = 12; Y = 3; break;
                                case 55: SpriteID = "Topcover"; X = 13; Y = 3; break;
                                case 56: SpriteID = "Apron"; X = 0; Y = 4; break;
                                case 57: SpriteID = "Capsaddle"; X = 1; Y = 4; break;
                                case 58: SpriteID = "Maskmantle"; X = 2; Y = 4; break;
                                case 59: SpriteID = "Squeaks"; X = 3; Y = 4; break;
                                case 60: SpriteID = "Star"; X = 4; Y = 4; break;
                                case 61: SpriteID = "Toestail"; X = 5; Y = 4; break;
                                case 62: SpriteID = "Ravenpaw"; X = 6; Y = 4; break;
                                case 63: SpriteID = "Pants"; X = 7; Y = 4; break;
                                case 64: SpriteID = "Reversepants"; X = 8; Y = 4; break;
                                case 65: SpriteID = "Skunk"; X = 9; Y = 4; break;
                                case 66: SpriteID = "Karpati"; X = 10; Y = 4; break;
                                case 67: SpriteID = "Halfwhite"; X = 11; Y = 4; break;
                                case 68: SpriteID = "Appaloosa"; X = 12; Y = 4; break;
                                case 69: SpriteID = "Dapplepaw"; X = 13; Y = 4; break;

                                // Beejean's White Patches + Perrio's point marks, painted, and heart2 + anju's new marks + key's blackstar
                                case 70: SpriteID = "Heart"; X = 0; Y = 5; break;
                                case 71: SpriteID = "Liltwo"; X = 1; Y = 5; break;
                                case 72: SpriteID = "Glass"; X = 2; Y = 5; break;
                                case 73: SpriteID = "Moorish"; X = 3; Y = 5; break;
                                case 74: SpriteID = "Sepiapoint"; X = 4; Y = 5; break;
                                case 75: SpriteID = "Minkpoint"; X = 5; Y = 5; break;
                                case 76: SpriteID = "Sealpoint"; X = 6; Y = 5; break;
                                case 77: SpriteID = "Mao"; X = 7; Y = 5; break;
                                case 78: SpriteID = "Luna"; X = 8; Y = 5; break;
                                case 79: SpriteID = "Chestspeck"; X = 9; Y = 5; break;
                                case 80: SpriteID = "Wings"; X = 10; Y = 5; break;
                                case 81: SpriteID = "Painted"; X = 11; Y = 5; break;
                                case 82: SpriteID = "HeartTwo"; X = 12; Y = 5; break;
                                case 83: SpriteID = "Woodpecker"; X = 13; Y = 5; break;

                                // Acorn's White Patches + Ryos' bub + Fable Lovebug + Frankie Trixie
                                case 84: SpriteID = "Boots"; X = 0; Y = 6; break;
                                case 85: SpriteID = "Miss"; X = 1; Y = 6; break;
                                case 86: SpriteID = "Cow"; X = 2; Y = 6; break;
                                case 87: SpriteID = "Cowtwo"; X = 3; Y = 6; break;
                                case 88: SpriteID = "Bub"; X = 4; Y = 6; break;
                                case 89: SpriteID = "Bowtie"; X = 5; Y = 6; break;
                                case 90: SpriteID = "Mustache"; X = 6; Y = 6; break;
                                case 91: SpriteID = "Reverseheart"; X = 7; Y = 6; break;
                                case 92: SpriteID = "Sparrow"; X = 8; Y = 6; break;
                                case 93: SpriteID = "Vest"; X = 9; Y = 6; break;
                                case 94: SpriteID = "Lovebug"; X = 10; Y = 6; break;
                                case 95: SpriteID = "Trixie"; X = 11; Y = 6; break;
                                case 96: SpriteID = "Sammy"; X = 12; Y = 6; break;
                                case 97: SpriteID = "Sparkle"; X = 13; Y = 6; break;

                                // Acorn's White Patches: the sequel
                                case 98: SpriteID = "Rightear"; X = 0; Y = 7; break;
                                case 99: SpriteID = "Leftear"; X = 1; Y = 7; break;
                                case 100: SpriteID = "Estrella"; X = 2; Y = 7; break;
                                case 101: SpriteID = "Shootingstar"; X = 3; Y = 7; break;
                                case 102: SpriteID = "Eyespot"; X = 4; Y = 7; break;
                                case 103: SpriteID = "ReverseEye"; X = 5; Y = 7; break;
                                case 104: SpriteID = "Fadebelly"; X = 6; Y = 7; break;
                                case 105: SpriteID = "Front"; X = 7; Y = 7; break;
                                case 106: SpriteID = "Blossomstep"; X = 8; Y = 7; break;
                                case 107: SpriteID = "Pebble"; X = 9; Y = 7; break;
                                case 108: SpriteID = "Tailtwo"; X = 10; Y = 7; break;
                                case 109: SpriteID = "Buddy"; X = 11; Y = 7; break;
                                case 110: SpriteID = "Backspot"; X = 12; Y = 7; break;
                                case 111: SpriteID = "Eyebags"; X = 13; Y = 7; break;
                                case 112: SpriteID = "Bullseye"; X = 0; Y = 8; break;
                                case 113: SpriteID = "Finn"; X = 1; Y = 8; break;
                                case 114: SpriteID = "Digit"; X = 2; Y = 8; break;
                                case 115: SpriteID = "Kropka"; X = 3; Y = 8; break;
                                case 116: SpriteID = "Fctwo"; X = 4; Y = 8; break;
                                case 117: SpriteID = "Fcone"; X = 5; Y = 8; break;
                                case 118: SpriteID = "Mia"; X = 6; Y = 8; break;
                                case 119: SpriteID = "Scar"; X = 7; Y = 8; break;
                                case 120: SpriteID = "Buster"; X = 8; Y = 8; break;
                                case 121: SpriteID = "Smokey"; X = 9; Y = 8; break;
                                case 122: SpriteID = "Hawkblaze"; X = 10; Y = 8; break;
                                case 123: SpriteID = "Cake"; X = 11; Y = 8; break;
                                case 124: SpriteID = "Rosina"; X = 12; Y = 8; break;
                                case 125: SpriteID = "Princess"; X = 13; Y = 8; break;
                                case 126: SpriteID = "Locket"; X = 0; Y = 9; break;
                                case 127: SpriteID = "Blazemask"; X = 1; Y = 9; break;
                                case 128: SpriteID = "Tears"; X = 2; Y = 9; break;
                                case 129: SpriteID = "Dougie"; X = 3; Y = 9; break;

                                default: SpriteID = ""; X = 0; Y = 0; break;
                            }

                            X *= 3;
                            Y *= 7;
                            SpriteID += ".";

                            Group(i % 21, ref SpriteID, ref X, ref Y);

                            CurrentSheet.Sprites[SpriteID] = new SDL_Rect() { x = X * 50, y = Y * 50, w = 50, h = 50 };
                        }

                        break;



                    case "TortiePatches":
                        CurrentSheet = Spritesheets["TortieMask"] = new Spritesheet(Path);

                        for (int i = 0; i < 21 * 43; i++)
                        {
                            string SpriteID;
                            int X, Y;

                            switch (i / 21)
                            {
                                case 0: SpriteID = "One"; X = 0; Y = 0; break;
                                case 1: SpriteID = "Two"; X = 1; Y = 0; break;
                                case 2: SpriteID = "Three"; X = 2; Y = 0; break;
                                case 3: SpriteID = "Four"; X = 3; Y = 0; break;
                                case 4: SpriteID = "Redtail"; X = 4; Y = 0; break;
                                case 5: SpriteID = "Delilah"; X = 5; Y = 0; break;
                                case 6: SpriteID = "Half"; X = 6; Y = 0; break;
                                case 7: SpriteID = "Streak"; X = 7; Y = 0; break;
                                case 8: SpriteID = "Mask"; X = 8; Y = 0; break;
                                case 9: SpriteID = "Smoke"; X = 9; Y = 0; break;
                                case 10: SpriteID = "Minimalone"; X = 0; Y = 1; break;
                                case 11: SpriteID = "Minimaltwo"; X = 1; Y = 1; break;
                                case 12: SpriteID = "Minimalthree"; X = 2; Y = 1; break;
                                case 13: SpriteID = "Minimalfour"; X = 3; Y = 1; break;
                                case 14: SpriteID = "Oreo"; X = 4; Y = 1; break;
                                case 15: SpriteID = "Swoop"; X = 5; Y = 1; break;
                                case 16: SpriteID = "Chimera"; X = 6; Y = 1; break;
                                case 17: SpriteID = "Chest"; X = 7; Y = 1; break;
                                case 18: SpriteID = "Armtail"; X = 8; Y = 1; break;
                                case 19: SpriteID = "Grumpyface"; X = 9; Y = 1; break;
                                case 20: SpriteID = "Mottled"; X = 0; Y = 2; break;
                                case 21: SpriteID = "Sidemask"; X = 1; Y = 2; break;
                                case 22: SpriteID = "Eyedot"; X = 2; Y = 2; break;
                                case 23: SpriteID = "Bandana"; X = 3; Y = 2; break;
                                case 24: SpriteID = "Pacman"; X = 4; Y = 2; break;
                                case 25: SpriteID = "Streamstrike"; X = 5; Y = 2; break;
                                case 26: SpriteID = "Smudged"; X = 6; Y = 2; break;
                                case 27: SpriteID = "Daub"; X = 7; Y = 2; break;
                                case 28: SpriteID = "Ember"; X = 8; Y = 2; break;
                                case 29: SpriteID = "Brie"; X = 9; Y = 2; break;
                                case 30: SpriteID = "Oriole"; X = 0; Y = 3; break;
                                case 31: SpriteID = "Robin"; X = 1; Y = 3; break;
                                case 32: SpriteID = "Brindle"; X = 2; Y = 3; break;
                                case 33: SpriteID = "Paige"; X = 3; Y = 3; break;
                                case 34: SpriteID = "Rosetail"; X = 4; Y = 3; break;
                                case 35: SpriteID = "Safi"; X = 5; Y = 3; break;
                                case 36: SpriteID = "Dapplenight"; X = 6; Y = 3; break;
                                case 37: SpriteID = "Blanket"; X = 7; Y = 3; break;
                                case 38: SpriteID = "Beloved"; X = 8; Y = 3; break;
                                case 39: SpriteID = "Body"; X = 9; Y = 3; break;
                                case 40: SpriteID = "Shiloh"; X = 0; Y = 4; break;
                                case 41: SpriteID = "Freckled"; X = 1; Y = 4; break;
                                case 42: SpriteID = "Heartbeat"; X = 2; Y = 4; break;
                                default: SpriteID = ""; X = 0; Y = 0; break;
                            }

                            X *= 3;
                            Y *= 7;
                            SpriteID += ".";

                            Group(i % 21, ref SpriteID, ref X, ref Y);

                            CurrentSheet.Sprites[SpriteID] = new SDL_Rect() { x = X * 50, y = Y * 50, w = 50, h = 50 };
                        }

                        break;



                    case "Scars":
                        CurrentSheet = Spritesheets["Scars"] = new Spritesheet(Path);

                        for (int i = 0; i < 21 * 36; i++)
                        {
                            string SpriteID;
                            int X, Y;

                            switch (i / 21)
                            {
                                case 0: SpriteID = "One"; X = 0; Y = 0; break;
                                case 1: SpriteID = "Two"; X = 1; Y = 0; break;
                                case 2: SpriteID = "Three"; X = 2; Y = 0; break;
                                case 3: SpriteID = "Manleg"; X = 3; Y = 0; break;
                                case 4: SpriteID = "Brightheart"; X = 4; Y = 0; break;
                                case 5: SpriteID = "Mantail"; X = 5; Y = 0; break;
                                case 6: SpriteID = "Bridge"; X = 6; Y = 0; break;
                                case 7: SpriteID = "Rightblind"; X = 7; Y = 0; break;
                                case 8: SpriteID = "Leftblind"; X = 8; Y = 0; break;
                                case 9: SpriteID = "Bothblind"; X = 9; Y = 0; break;
                                case 10: SpriteID = "Burnpaws"; X = 10; Y = 0; break;
                                case 11: SpriteID = "Burntail"; X = 11; Y = 0; break;
                                case 12: SpriteID = "Burnbelly"; X = 0; Y = 1; break;
                                case 13: SpriteID = "Beakcheek"; X = 1; Y = 1; break;
                                case 14: SpriteID = "Beaklower"; X = 2; Y = 1; break;
                                case 15: SpriteID = "Burnrump"; X = 3; Y = 1; break;
                                case 16: SpriteID = "Catbite"; X = 4; Y = 1; break;
                                case 17: SpriteID = "Ratbite"; X = 5; Y = 1; break;
                                case 18: SpriteID = "Frostface"; X = 6; Y = 1; break;
                                case 19: SpriteID = "FrostTail"; X = 7; Y = 1; break;
                                case 20: SpriteID = "Frostmitt"; X = 8; Y = 1; break;
                                case 21: SpriteID = "Frostsock"; X = 9; Y = 1; break;
                                case 22: SpriteID = "Quillchunk"; X = 10; Y = 1; break;
                                case 23: SpriteID = "Quillscratch"; X = 11; Y = 1; break;
                                case 24: SpriteID = "Tailscar"; X = 0; Y = 2; break;
                                case 25: SpriteID = "Snout"; X = 1; Y = 2; break;
                                case 26: SpriteID = "Cheek"; X = 2; Y = 2; break;
                                case 27: SpriteID = "Side"; X = 3; Y = 2; break;
                                case 28: SpriteID = "Throat"; X = 4; Y = 2; break;
                                case 29: SpriteID = "Tailbase"; X = 5; Y = 2; break;
                                case 30: SpriteID = "Belly"; X = 6; Y = 2; break;
                                case 31: SpriteID = "Toetrap"; X = 7; Y = 2; break;
                                case 32: SpriteID = "Snake"; X = 8; Y = 2; break;
                                case 33: SpriteID = "Legbite"; X = 9; Y = 2; break;
                                case 34: SpriteID = "Neckbite"; X = 10; Y = 2; break;
                                case 35: SpriteID = "Face"; X = 11; Y = 2; break;
                                default: SpriteID = ""; X = 11; Y = 0; break;
                            }

                            X *= 3;
                            Y *= 7;
                            SpriteID += ".";

                            Group(i % 21, ref SpriteID, ref X, ref Y);

                            CurrentSheet.Sprites[SpriteID] = new SDL_Rect() { x = X * 50, y = Y * 50, w = 50, h = 50 };
                        }

                        break;



                    case "ScarsMissing":
                        CurrentSheet = Spritesheets["Missing"] = new Spritesheet(Path);

                        for (int i = 0; i < 21 * 8; i++)
                        {
                            string SpriteID;
                            int X, Y = 0;

                            switch (i / 21)
                            {
                                case 0: SpriteID = "Leftear"; X = 0; break;
                                case 1: SpriteID = "Rightear"; X = 1; break;
                                case 2: SpriteID = "Notail"; X = 2; break;
                                case 3: SpriteID = "Noleftear"; X = 3; break;
                                case 4: SpriteID = "Norightear"; X = 4; break;
                                case 5: SpriteID = "Noear"; X = 5; break;
                                case 6: SpriteID = "Halftail"; X = 6; break;
                                case 7: SpriteID = "Nopaw"; X = 7; break;
                                default: SpriteID = ""; X = 0; break;
                            }

                            X *= 3;
                            Y *= 7;
                            SpriteID += ".";

                            Group(i % 21, ref SpriteID, ref X, ref Y);

                            CurrentSheet.Sprites[SpriteID] = new SDL_Rect() { x = X * 50, y = Y * 50, w = 50, h = 50 };
                        }

                        break;



                    case "MedcatHerbs":
                        CurrentSheet = Spritesheets["MedcatHerbs"] = new Spritesheet(Path);

                        for (int i = 0; i < 0; i++)
                        {
                            string SpriteID;
                            int X, Y;

                            switch (i / 21)
                            {
                                case 0: SpriteID = "Mapleleaf"; X = 0; Y = 0; break;
                                case 1: SpriteID = "Holly"; X = 1; Y = 0; break;
                                case 2: SpriteID = "Blueberries"; X = 2; Y = 0; break;
                                case 3: SpriteID = "Forgetmenots"; X = 3; Y = 0; break;
                                case 4: SpriteID = "Ryestalk"; X = 4; Y = 0; break;
                                case 5: SpriteID = "Laurel"; X = 5; Y = 0; break;
                                case 6: SpriteID = "Bluebells"; X = 0; Y = 1; break;
                                case 7: SpriteID = "Nettle"; X = 1; Y = 1; break;
                                case 8: SpriteID = "Poppy"; X = 2; Y = 1; break;
                                case 9: SpriteID = "Lavender"; X = 3; Y = 1; break;
                                case 10: SpriteID = "Herbs"; X = 4; Y = 1; break;
                                case 11: SpriteID = "Petals"; X = 5; Y = 1; break;
                                case 12: SpriteID = "Redfeathers"; X = 0; Y = 2; break;
                                case 13: SpriteID = "Bluefeathers"; X = 1; Y = 2; break;
                                case 14: SpriteID = "Jayfeathers"; X = 2; Y = 2; break;
                                case 15: SpriteID = "Mothwings"; X = 3; Y = 2; break;
                                case 16: SpriteID = "Cicadawings"; X = 4; Y = 2; break;
                                case 17: SpriteID = "Dryherbs"; X = 5; Y = 2; break;
                                case 18: SpriteID = "Oakleaves"; X = 0; Y = 3; break;
                                case 19: SpriteID = "Catmint"; X = 1; Y = 3; break;
                                case 20: SpriteID = "Mapleseed"; X = 2; Y = 3; break;
                                case 21: SpriteID = "Juniper"; X = 3; Y = 3; break;
                                default: SpriteID = ""; X = 0; Y = 0; break;
                            }

                            X *= 3;
                            Y *= 7;
                            SpriteID += ".";

                            Group(i % 21, ref SpriteID, ref X, ref Y);

                            CurrentSheet.Sprites[SpriteID] = new SDL_Rect() { x = X * 50, y = Y * 50, w = 50, h = 50 };
                        }

                        break;


                    case "Collars":
                    case "CollarsBell":
                    case "CollarsBow":
                    case "CollarsNylon":
                        CurrentSheet = Spritesheets[File] = new Spritesheet(Path);

                        for (int i = 0; i < 0; i++)
                        {
                            string SpriteID;
                            int X, Y;

                            switch (i / 21)
                            {
                                case 0: SpriteID = "Crimson"; X = 0; Y = 0; break;
                                case 1: SpriteID = "Blue"; X = 1; Y = 0; break;
                                case 2: SpriteID = "Yellow"; X = 2; Y = 0; break;
                                case 3: SpriteID = "Cyan"; X = 3; Y = 0; break;
                                case 4: SpriteID = "Red"; X = 4; Y = 0; break;
                                case 5: SpriteID = "Lime"; X = 5; Y = 0; break;
                                case 6: SpriteID = "Green"; X = 0; Y = 1; break;
                                case 7: SpriteID = "Rainbow"; X = 1; Y = 1; break;
                                case 8: SpriteID = "Black"; X = 2; Y = 1; break;
                                case 9: SpriteID = "Spikes"; X = 3; Y = 1; break;
                                case 10: SpriteID = "White"; X = 4; Y = 1; break;
                                case 11: SpriteID = "Pink"; X = 0; Y = 2; break;
                                case 12: SpriteID = "Purple"; X = 1; Y = 2; break;
                                case 13: SpriteID = "Multi"; X = 2; Y = 2; break;
                                case 14: SpriteID = "Indigo"; X = 3; Y = 2; break;
                                default: SpriteID = ""; X = 0; Y = 0; break;
                            }

                            X *= 3;
                            Y *= 7;
                            SpriteID += ".";

                            Group(i % 21, ref SpriteID, ref X, ref Y);

                            CurrentSheet.Sprites[SpriteID] = new SDL_Rect() { x = X * 50, y = Y * 50, w = 50, h = 50 };
                        }

                        break;



                    case "":
                        CurrentSheet = Spritesheets[""] = new Spritesheet(Path);

                        for (int i = 0; i < 0; i++)
                        {
                            string SpriteID;
                            int X, Y;

                            switch (i / 21)
                            {
                                default: SpriteID = ""; X = 0; Y = 0; break;
                            }

                            X *= 3;
                            Y *= 7;
                            SpriteID += ".";

                            Group(i % 21, ref SpriteID, ref X, ref Y);

                            CurrentSheet.Sprites[SpriteID] = new SDL_Rect() { x = X * 50, y = Y * 50, w = 50, h = 50 };
                        }

                        break;



                    default:
                        Utility.Log($"Unaccounted for file: {File}");
                        break;
                }

            }
        }

        private void Group(int Iteration, ref string SpriteID, ref int X, ref int Y)
        {
            switch (Iteration)
            {
                case 0: SpriteID += "Kit0"; break;
                case 1: SpriteID += "Kit1"; X += 1; break;
                case 2: SpriteID += "Kit2"; X += 2; break;
                case 3: SpriteID += "Adolescent0"; Y += 1; break;
                case 4: SpriteID += "Adolescent1"; X += 1; Y += 1; break;
                case 5: SpriteID += "Adolescent2"; X += 2; Y += 1; break;
                case 6: SpriteID += "YoungShort"; Y += 2; break;
                case 7: SpriteID += "AdultShort"; X += 1; Y += 2; break;
                case 8: SpriteID += "SeniorShort"; X += 2; Y += 2; break;
                case 9: SpriteID += "YoungLong"; Y += 3; break;
                case 10: SpriteID += "AdultLong"; X += 1; Y += 3; break;
                case 11: SpriteID += "SeniorLong"; X += 2; Y += 3; break;
                case 12: SpriteID += "Senior0"; Y += 4; break;
                case 13: SpriteID += "Senior1"; X += 1; Y += 4; break;
                case 14: SpriteID += "Senior2"; X += 2; Y += 4; break;
                case 15: SpriteID += "ParalyzedShort"; Y += 5; break;
                case 16: SpriteID += "ParalyzedLong"; X += 1; Y += 5; break;
                case 17: SpriteID += "ParalyzedYoung"; X += 2; Y += 5; break;
                case 18: SpriteID += "SickAdult"; Y += 6; break;
                case 19: SpriteID += "SickYoung"; X += 1; Y += 6; break;
                case 20: SpriteID += "Newborn"; X += 2; Y += 6; break;
            }
        }

        public Image this[string Identifier] 
        {
            get
            {
                List<string> Path = Identifier.Split('.').ToList();
                string Root = Path[0];

                Path.RemoveAt(0);

                return Spritesheets[Root][string.Join(".", Path)];
            }
        }
    }

    public class Spritesheet
    {
        public readonly Dictionary<string, SDL_Rect> Sprites;
        public readonly Image TextureAtlas;
        public short SpriteSize;

        public Spritesheet(string Path, int Size = 50, Dictionary<string, SDL_Rect> Sprites = null) 
        {
            this.Sprites = Sprites ?? new Dictionary<string, SDL_Rect>();
            TextureAtlas = new Image(Path);
            SpriteSize = (short)Size;
        }

        public Image this[string Identifer] // TEMPORARY (need to measure whats better -> holding individual images or the spritesheets and blitting them onto images when needed)
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
