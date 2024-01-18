using System;
using System.Linq;
using System.Collections.Generic;
using static SDL2.SDL;
using static Clangen.Utility;

namespace Clangen.Cats
{
    public enum PeltLength : byte
    {
        Short, Medium, Long
    }



    public enum SpriteType : byte
    {
        Kit0,
        Kit1,
        Kit2,
        Adolescent0,
        Adolescent1,
        Adolescent2,
        YoungShort,
        AdultShort,
        SeniorShort,
        YoungLong,
        AdultLong,
        SeniorLong,
        Senior0,
        Senior1,
        Senior2,
        ParalyzedShort,
        ParalyzedLong,
        ParalyzedYoung,
        SickAdult,
        SickYoung,
        Newborn
    }



    public static class LooksGroups // ENTIRELY VERY temporary
    {
        public static string[] EyeColours = {
            "Yellow", "Amber", "Hazel", "Palegreen", "Green", "Blue", "Darkblue", "Grey", "Cyan", "Emerald", 
            "Paleblue", "Paleyellow", "Gold", "Heatherblue", "Copper", "Sage", "Cobalt", "Sunlitice", "Greenyellow", "Bronze", 
            "Silver"
        };
        public static string[] YellowEyes = {
            "Yellow", "Amber", "Paleyellow", "Gold", "Copper", "Greenyellow", "Bronze", "Silver"
        };
        public static string[] BlueEyes = {
            "Blue", "Darkblue", "Cyan", "Paleblue", "Heatherblue", "Cobalt", "Sunlitice", "Grey"
        };
        public static string[] GreenEyes = {
            "Palegreen", "Green", "Emerald", "Sage", "Hazel"
        };

        public static string[] Vitiligo = { "Vitiligo", "Vitiligotwo", "Moon", "Phantom", "Karpati", "Powder", "Bleached", "Smokey" };

        public static string[] TortieBases = { 
            "Single", "Tabby", "Bengal", "Marbled", "Ticked", "Smoke", "Rosette", "Speckled", "Mackerel", "Classic", "Sokoke", "Agouti", "Singlestripe", "Masked"
        };
        public static string[] TortiePatterns = { 
            "One", "Two", "Three", "Four", "Redtail", "Delilah", "Minimalone", "Minimaltwo", "Minimalthree", "Minimalfour", "Half", "Oreo", "Swoop", "Mottled", "Sidemask", "Eyedot", "Bandana", "Pacman", "Streamstrike", 
            "Oriole", "Chimera", "Daub", "Ember", "Blanket", "Robin", "Brindle", "Paige", "Rosetail", "Safi", "Smudged", "Dapplenight", "Streak", "Mask", "Chest", "Armtail", "Smoke", "Grumpyface", "Brie", "Beloved", 
            "Body", "Shiloh", "Freckled", "Heartbeat"
        };

        public static string[] PeltColours = { 
            "White", "Palegrey", "Silver", "Grey", "Darkgrey", "Ghost", "Black", "Cream", "Paleginger", "Golden", "Ginger", "Darkginger", "Sienna", "Lightbrown", "Lilac", "Brown", "Goldenbrown", "Darkbrown", "Chocolate"
        };

        public static string[] Tabbies = { "Tabby", "Ticked", "Mackerel", "Classic", "Sokoke", "Agouti" };
        public static string[] Spotted = { "Speckled", "Rosette" };
        public static string[] Plain = { "SingleColour", "Singlestripe", "TwoColour", "Smoke" };
        public static string[] Exotic = { "Bengal", "Marbled", "Masked" };
        public static string[] Torties = { "Tortie", "Calico" };
        public static string[][] PeltCategories = { Tabbies, Spotted, Plain, Exotic, Torties };

        public static string[] GingerColours = { "Cream", "Paleginger", "Golden", "Ginger", "Darkginger", "Sienna" };
        public static string[] BlackColours = { "Grey", "Darkgrey", "Ghost", "Black" };
        public static string[] WhiteColours = { "White", "Palegrey", "Silver" };
        public static string[] BrownColours = { "Lightbrown", "Lilac", "Brown", "Goldenbrown", "Darkbrown", "Chocolate" };
        public static string[][] ColourCategories = { GingerColours, BlackColours, WhiteColours, BrownColours };

        public static string[] PointMarkings = { "Colourpoint", "Ragdoll", "Sepiapoint", "Minkpoint", "Sealpoint" };

        public static string[] LittleWhite = {
            "Little", "LightTuxedo", "Buzzardfang", "Tip", "Blaze", "Bib", "Vee", "Paws", "Belly", "Tailtip", "Toes", "Brokenblaze", "Liltwo", "Scourge", "Toestail", "Ravenpaw", 
            "Honey", "Luna", "Extra", "Mustache", "Reverseheart", "Sparkle", "Rightear", "Leftear", "Estrella", "ReverseEye", "Backspot", "Eyebags", "Locket", "Blazemask", "Tears"
        };
        public static string[] MiddleWhite = {
            "Tuxedo", "Fancy", "Unders", "Damien", "Skunk", "Mitaine", "Squeaks", "Star", "Wings", "Diva", "Savannah", "Fadespots", "Beard", "Dapplepaw", "Topcover", "Woodpecker",
            "Miss", "Bowtie", "Vest", "Fadebelly", "Digit", "Fctwo", "Fcone", "Mia", "Rosina", "Princess", "Dougie"
        };
        public static string[] HighWhite = {
            "Any", "Anytwo", "Broken", "Freckles", "Ringtail", "HalfFace", "Pantstwo", "Goatee", "Prince", "Farofa", "Mister", "Pants", "Reversepants", "Halfwhite", "Appaloosa", "Piebald", 
            "Curved", "Glass", "Maskmantle", "Mao", "Painted", "Shibainu", "Owl", "Bub", "Sparrow", "Trixie", "Sammy", "Front", "Blossomstep", "Bullseye", "Finn", "Scar", "Buster",
            "Hawkblaze", "Cake"
        };
        public static string[] MostlyWhite = {
            "Van", "OneEar", "Lightsong", "Tail", "Heart", "Moorish", "Apron", "Capsaddle", "Chestspeck", "Blackstar", "Petal", "HeartTwo", "Pebbleshine", "Boots", "Cow", "Cowtwo", "Lovebug", 
            "Shootingstar", "Eyespot", "Pebble", "Tailtwo", "Buddy", "Kropka"
        };

        public static string[] SkinSprites = {
            "Black", "Pink", "Darkbrown", "Brown", "Lightbrown", "Dark", "Darkgrey", "Grey", "Darksalmon",
            "Salmon", "Peach", "Darkmarbled", "Marbled", "Lightmarbled", "Darkblue", "Blue", "Lightblue", "Red"
        };

        public static string[] Scars = {
            "One", "Two", "Three", "Tailscar", "Snout", "Cheek", "Side", "Throat", "Tailbase", "Belly", 
            "Legbite", "Neckbite", "Face", "Manleg", "Brightheart", "Mantail", "Bridge", "Rightblind", "Leftblind", "Bothblind", 
            "Beakcheek", "Beaklower", "Catbite", "Ratbite", "Quillchunk", "Quillscratch"
        };
        public static string[] MissingScars = {
            "Leftear", "Rightear", "Notail", "Halftail", "Nopaw", "Noleftear", "Norightear", "Noear"
        };
        public static string[] SpecialScars = {
            "Snake", "Toetrap", "Burnpaws", "Burntail", "Burnbelly", "Burnrump", "Frostface", "FrostTail", "Frostmitt"
        };

        public static string[] PlantAccessories = {
            "Mapleleaf", "Holly", "Blueberries", "Forgetmenots", "Ryestalk", "Laurel", "Bluebells", "Nettle", "Poppy", "Lavender", 
            "Herbs", "Petals", "Dryherbs", "Oakleaves", "Catmint", "Mapleseed", "Juniper"
        };
        public static string[] WildAccessories = {
            "Redfeathers", "Bluefeathers", "Jayfeathers", "Mothwings", "Cicadawings"
        };


        public static string[] Collars = {
            "Crimson", "Blue", "Yellow", "Cyan", "Red", "Lime", "Green", "Rainbow", "Black", "Spikes", "White", "Pink", "Purple", "Multi", "Indigo"
        };
    }



    public static class Tints // VERY TEMPORARY inplace of dynamic tints
    {
        public static Dictionary<string, string> ColourGroups = new Dictionary<string, string>()
        {
            { "White", "White" },
            { "Palegrey", "Cool" },
            { "Silver", "Cool" },
            { "Grey", "Monochrome" },
            { "Darkgrey", "Monochrome" },
            { "Ghost", "Monochrome" },
            { "Black", "Monochrome" },
            { "Cream", "Warm" },
            { "Paleginger", "Warm" },
            { "Golden", "Warm" },
            { "Ginger", "Warm" },
            { "Darkginger", "Warm" },
            { "Sienna", "Brown" },
            { "Lightbrown", "Brown" },
            { "Lilac", "Brown" },
            { "Brown", "Brown" },
            { "Goldenbrown", "Brown" },
            { "Darkbrown", "Brown" },
            { "Chocolate", "Brown" },
        };

        public static Dictionary<string, string[]> PossibleTints = new Dictionary<string, string[]>
        {
            { "Basic", new string[]{ "Pink", "Grey", "Red", "Orange" } },
            { "Warm", new string[]{ "Yellow", "Purple" } },
            { "Cool", new string[]{ "Blue", "Purple", "Black" } },
            { "White", new string[]{ "Yellow" } },
            { "Monochrome", new string[]{ "Blue", "Black" } },
            { "Brown", new string[]{ "Yellow", "Purple", "Black" } }
        };

        public static Dictionary<string, Color> Colors = new Dictionary<string, Color>()
        {
            { "Pink", new Color(253, 237, 237) },
            { "Grey", new Color(225, 225, 225) },
            { "Red", new Color(248, 226, 228) },
            { "Black", new Color(195, 195, 195) },
            { "Orange", new Color(255, 247, 235) },
            { "Yellow", new Color(250, 248, 225) },
            { "Purple", new Color(235, 225, 244) },
            { "Blue", new Color(218, 237, 245) },
        };

        public static class WhitePatches
        {
            public static Dictionary<string, string> ColourGroups = new Dictionary<string, string>()
            {
                { "Palegrey", "Grey" },
                { "Silver", "Grey" },
                { "Grey", "Grey" },
                { "Darkgrey", "Black" },
                { "Ghost", "Black" },
                { "Black", "Black" },
                { "Cream", "Ginger" },
                { "Paleginger", "Ginger" },
                { "Golden", "Ginger" },
                { "Ginger", "Ginger" },
                { "Darkginger", "Ginger" },
                { "Sienna", "Ginger" },
                { "Lightbrown", "Brown" },
                { "Lilac", "Brown" },
                { "Brown", "Brown" },
                { "Goldenbrown", "Brown" },
                { "Darkbrown", "Brown" },
                { "Chocolate", "Brown" },
            };

            public static Dictionary<string, string[]> PossibleTints = new Dictionary<string, string[]>()
            {
                { "Basic", new string[]{ "Offwhite" } },
                { "Black", new string[]{ "Grey", "Darkcream", "Cream" } },
                { "Grey", new string[]{ "Grey" } },
                { "Ginger", new string[]{ "Darkcream", "Cream", "Pink" } },
                { "Brown", new string[]{ "Darkcream", "Cream" } }
            };

            public static Dictionary<string, Color> Colors = new Dictionary<string, Color>()
            {
                { "Darkcream", new Color(236, 229, 208) },
                { "Cream", new Color(247, 241, 225) },
                { "Offwhite", new Color(238, 249, 252) },
                { "Grey", new Color(208, 225, 229) },
                { "Pink", new Color(254, 248, 249) },
            };
        }
    }




    public class Looks
    {
        private readonly CatRef Source;

        public string Name;
        public string Colour;
        public string Pattern;
        public PeltLength Length;
        public string TortieBase;
        public string TortiePattern;
        public string TortieColour;
        public string WhitePatches;
        public string EyeColour;
        public string EyeColour2;
        public string Vitiligo;
        public string Points;
        public string Accessory;
        public List<string> Scars;
        public string Tint;
        public string WhitePatchesTint;
        public string Skin;

        public SpriteType SpriteNewborn;
        public SpriteType SpriteKitten;
        public SpriteType SpriteAdolescent;
        public SpriteType SpriteYoungAdult;
        public SpriteType SpriteYoungSick;
        public SpriteType SpriteYoungParalyzed;
        public SpriteType SpriteAdult;
        public SpriteType SpriteAdultSick;
        public SpriteType SpriteAdultParalyzed;
        public SpriteType SpriteSeniorAdult;
        public SpriteType SpriteSenior;

        public byte Opacity;

        public bool Paralyzed;
        public bool Reversed;

        public bool White => WhitePatches is null && Points is null;



        public Looks(CatRef Cat)
        {
            Source = Cat;
            Scars = new List<string>();
        }



        public bool RandomizePatternColor(CRandom RNG)
        {
            string ChosenPelt = RNG.ChooseFrom(
                RNG.ChooseFrom(LooksGroups.PeltCategories, new int[] { 35, 20, 30, 15, 0 })
            );
            string ChosenPeltColour = RNG.ChooseFrom(RNG.ChooseFrom(LooksGroups.ColourCategories));
            string ChosenTortieBase = null;
            int TortieChanceFemale = 3, TortieChanceMale = 13; // GAMECONFIGCHANCE

            if (RNG.Next(1 << (Source.Value.Gender ? TortieChanceMale : TortieChanceFemale)) == 0)
            {
                ChosenTortieBase = ChosenTortieBase == "TwoColour" || ChosenTortieBase == "SingleColour" ? "Single" : ChosenPelt;
                ChosenPelt = RNG.ChooseFrom(LooksGroups.Torties);
            }

            bool ChosenWhite = RNG.Chance(.4);

            if (ChosenPelt == "SingleColour" || ChosenPelt == "TwoColour")
                ChosenPelt = ChosenWhite ? "TwoColour" : "SingleColour";
            else if (ChosenPelt == "Calico" && !ChosenWhite)
                ChosenPelt = "Tortie";

            Name = ChosenPelt;
            Colour = ChosenPeltColour;
            Length = (PeltLength)RNG.Next(3);
            TortieBase = ChosenTortieBase;
            return ChosenWhite;
        }



        public void RandomizeWhitePatches(CRandom RNG, bool IsWhite)
        {
            if (Name != "Tortie" && RNG.Next(5) == 0) // GAMECONFIGCHANCE
                Points = RNG.ChooseFrom(LooksGroups.PointMarkings);

            int[] Weights = { 10, 10, 10, 10, 1 };
            if (Name == "Tortie")
                Weights = new int[5] { 2, 1, 0, 0, 0 };
            else if (Name == "Calico")
                Weights = new int[5] { 0, 0, 20, 15, 1 };

            switch (RNG.Next(5))
            {
                case 0:
                    WhitePatches = RNG.ChooseFrom(LooksGroups.LittleWhite);
                    return;
                case 1:
                    WhitePatches = RNG.ChooseFrom(LooksGroups.MiddleWhite);
                    return;
                case 2:
                    WhitePatches = RNG.ChooseFrom(LooksGroups.HighWhite);
                    if (Points is not null)
                        Points = null;
                    return;
                case 3:
                    WhitePatches = RNG.ChooseFrom(LooksGroups.MostlyWhite);
                    if (Points is not null)
                        Points = null;
                    return;
                case 4:
                    WhitePatches = "Fullwhite";
                    if (Points is not null)
                        Points = null;
                    return;
            }
        }



        public bool PatternColorInheritance(CRandom RNG, CatRef Parent1, CatRef Parent2)
        {
            Looks FirstLooks = Parent1.Value.Looks, SecondLooks = Parent2.Value.Looks;
            string ChosenPelt, ChosenPeltColour, ChosenTortieBase = null;
            int DirectInheritance = 10; // GAMECONFIGCHANCE

            if (RNG.Chance(1 / DirectInheritance))
            {
                Looks Selected = RNG.Choose() ? FirstLooks : SecondLooks;
                Name = Selected.Name;
                Length = Selected.Length;
                Colour = Selected.Colour;
                TortieBase = Selected.TortieBase;
                return Selected.White;
            }

            int[] Weights = { 0, 0, 0, 0, 0 };

            if (Array.IndexOf(LooksGroups.Tabbies, FirstLooks) > -1)
                Weights = new int[5] { 50, 10, 5, 7, 0 };
            else if (Array.IndexOf(LooksGroups.Spotted, FirstLooks) > -1)
                Weights = new int[5] { 10, 50, 5, 5, 0 };
            else if (Array.IndexOf(LooksGroups.Plain, FirstLooks) > -1)
                Weights = new int[5] { 5, 5, 50, 0, 0 };
            else if (Array.IndexOf(LooksGroups.Exotic, FirstLooks) > -1)
                Weights = new int[5] { 15, 15, 1, 45, 0 };

            if (Array.IndexOf(LooksGroups.Tabbies, SecondLooks) > -1)
                Weights = Weights.Concat(new int[5] { 50, 10, 5, 7, 0 }).ToArray();
            else if (Array.IndexOf(LooksGroups.Spotted, SecondLooks) > -1)
                Weights = Weights.Concat(new int[5] { 10, 50, 5, 5, 0 }).ToArray();
            else if (Array.IndexOf(LooksGroups.Plain, SecondLooks) > -1)
                Weights = Weights.Concat(new int[5] { 5, 5, 50, 0, 0 }).ToArray();
            else if (Array.IndexOf(LooksGroups.Exotic, SecondLooks) > -1)
                Weights = Weights.Concat(new int[5] { 15, 15, 1, 45, 0 }).ToArray();

            ChosenPelt = RNG.ChooseFrom(
                Weights.All(X => X == 0) ? RNG.ChooseFrom(LooksGroups.PeltCategories) : RNG.ChooseFrom(LooksGroups.PeltCategories, Weights)
            );

            int TortieChanceFemale = 3, TortieChanceMale = 13; // GAMECONFIGCHANCE

            if (Array.IndexOf(LooksGroups.Tabbies, FirstLooks.Name) > -1)
                TortieChanceFemale /= 2; TortieChanceMale -= 1;
            if (Array.IndexOf(LooksGroups.Tabbies, SecondLooks.Name) > -1)
                TortieChanceFemale /= 2; TortieChanceMale -= 1;

            if (RNG.Next(1 << (Source.Value.Gender ? TortieChanceMale : TortieChanceFemale)) == 0)
            {
                ChosenTortieBase = ChosenTortieBase == "TwoColour" || ChosenTortieBase == "SingleColour" ? "Single" : ChosenPelt;
                ChosenPelt = RNG.ChooseFrom(LooksGroups.Torties);
            }

            Weights = new int[4] { 0, 0, 0, 0 };

            if (Array.IndexOf(LooksGroups.GingerColours, FirstLooks) > -1)
                Weights = new int[4] { 40, 0, 0, 10 };
            else if (Array.IndexOf(LooksGroups.BlackColours, FirstLooks) > -1)
                Weights = new int[4] { 0, 40, 2, 5 };
            else if (Array.IndexOf(LooksGroups.WhiteColours, FirstLooks) > -1)
                Weights = new int[4] { 0, 5, 40, 0 };
            else if (Array.IndexOf(LooksGroups.BrownColours, FirstLooks) > -1)
                Weights = new int[4] { 10, 5, 0, 35 };

            if (Array.IndexOf(LooksGroups.GingerColours, SecondLooks) > -1)
                Weights = Weights.Concat(new int[4] { 40, 0, 0, 10 }).ToArray();
            else if (Array.IndexOf(LooksGroups.BlackColours, SecondLooks) > -1)
                Weights = Weights.Concat(new int[4] { 0, 40, 2, 5 }).ToArray();
            else if (Array.IndexOf(LooksGroups.WhiteColours, SecondLooks) > -1)
                Weights = Weights.Concat(new int[4] { 0, 5, 40, 0 }).ToArray();
            else if (Array.IndexOf(LooksGroups.BrownColours, SecondLooks) > -1)
                Weights = Weights.Concat(new int[4] { 10, 5, 0, 35 }).ToArray();

            ChosenPeltColour = RNG.ChooseFrom(
                Weights.All(X => X == 0) ? RNG.ChooseFrom(LooksGroups.ColourCategories) : RNG.ChooseFrom(LooksGroups.ColourCategories, Weights)
            );

            switch (FirstLooks.Length)
            {
                case PeltLength.Short: Weights = new int[3] { 50, 10, 2 }; break;
                case PeltLength.Medium: Weights = new int[3] { 25, 50, 25 }; break;
                case PeltLength.Long: Weights = new int[3] { 2, 10, 50 }; break;
            }
            switch (SecondLooks.Length)
            {
                case PeltLength.Short: Weights = Weights.Concat(new int[3] { 50, 10, 2 }).ToArray(); break;
                case PeltLength.Medium: Weights = Weights.Concat(new int[3] { 25, 50, 25 }).ToArray(); break;
                case PeltLength.Long: Weights = Weights.Concat(new int[3] { 2, 10, 50 }).ToArray(); break;
            }

            bool ChosenWhite = RNG.Chance(97);

            if (ChosenPelt == "SingleColour" || ChosenPelt == "TwoColour")
                ChosenPelt = ChosenWhite ? "TwoColour" : "SingleColour";
            else if (ChosenPelt == "Calico" && !ChosenWhite)
                ChosenPelt = "Tortie";

            Name = ChosenPelt;
            Colour = ChosenPeltColour;
            Length = (PeltLength)RNG.Next(3);
            TortieBase = ChosenTortieBase;
            return ChosenWhite;
        }



        public void WhitePatchInheritance(CRandom RNG, CatRef Parent1, CatRef Parent2)
        {
            Looks FirstLooks = Parent1.Value.Looks, SecondLooks = Parent2.Value.Looks;
            int DirectInheritance = 10; // GAMECONFIGCHANCE

            List<string> ParentPoints = new List<string>();
            List<string> ParentWhitePatches = new List<string>();

            if (FirstLooks.WhitePatches is not null) ParentPoints.Add(FirstLooks.WhitePatches); // TEMPORARY
            if (SecondLooks.WhitePatches is not null) ParentPoints.Add(SecondLooks.WhitePatches); // TEMPORARY

            if (FirstLooks.Points is not null) ParentPoints.Add(FirstLooks.Points);
            if (SecondLooks.Points is not null) ParentPoints.Add(SecondLooks.Points);

            if (0 < ParentWhitePatches.Count && RNG.Next(DirectInheritance) == 0)
            {
                List<string> Temp = new List<string>();

                if (Name == "Tortie")
                    for (int i = 0; i < ParentWhitePatches.Count; i++)
                    {
                        var Patch = ParentWhitePatches[i];
                        if (Array.IndexOf(LooksGroups.HighWhite, Patch) == -1 && Array.IndexOf(LooksGroups.MostlyWhite, Patch) == -1 && Patch != "Fullwhite")
                            Temp.Add(Patch);
                    }

                else if (Name == "Calico")
                    for (int i = 0; i < ParentWhitePatches.Count; i++)
                    {
                        var Patch = ParentWhitePatches[i];
                        if (Array.IndexOf(LooksGroups.LittleWhite, Patch) == -1 && Array.IndexOf(LooksGroups.MiddleWhite, Patch) == -1)
                            Temp.Add(Patch);
                    }


                if (0 < Temp.Count)
                {
                    WhitePatches = RNG.ChooseFrom(Temp);

                    if (Name != "Tortie" && 0 < ParentPoints.Count)
                        Points = RNG.ChooseFrom(ParentPoints);

                    return;
                }
            }

            if (Name != "Tortie" && RNG.Next(0 < ParentPoints.Count ? 10 - ParentPoints.Count : 40) == 0)
                Points = RNG.ChooseFrom(LooksGroups.PointMarkings);

            int[] Weights = { 0, 0, 0, 0, 0 };

            for (int i = 0; i < ParentWhitePatches.Count; i++)
            {
                var Patch = ParentWhitePatches[i];
                int[] AddWeights = { 0, 0, 0, 0, 0 };

                if (Array.IndexOf(LooksGroups.LittleWhite, Patch) > -1)
                    AddWeights = new int[5] { 40, 20, 15, 5, 0 };
                else if (Array.IndexOf(LooksGroups.MiddleWhite, Patch) > -1)
                    AddWeights = new int[5] { 10, 40, 15, 10, 0 };
                else if (Array.IndexOf(LooksGroups.HighWhite, Patch) > -1)
                    AddWeights = new int[5] { 15, 20, 40, 10, 1 };
                else if (Array.IndexOf(LooksGroups.MostlyWhite, Patch) > -1)
                    AddWeights = new int[5] { 5, 15, 20, 40, 5 };
                else if (Patch == "Fullwhite")
                    AddWeights = new int[5] { 0, 5, 15, 40, 10 };

                for (int k = 0; k < 5; k++)
                    Weights[k] = AddWeights[k];
            }

            // Any detected white patches WILL alter the second element so this is safe to check whether or not its been altered
            if (Weights[1] != 0)
                Weights = new int[5] { 50, 5, 0, 0, 0 };

            // Adjust weights for torties, since they can't have anything greater than MiddleWhite
            if (Name == "Tortie")
                Weights = Weights[1] != 0 ? new int[5] { 2, 1, 0, 0, 0 } : new int[5] { Weights[0], Weights[1], 0, 0, 0 };

            // Adjust weights for torties, since they can't have anything greater than mid_white
            else if (Name == "Calico")
                Weights = Weights[1] != 0 ? new int[5] { 2, 1, 0, 0, 0 } : new int[5] { Weights[0], Weights[1], 0, 0, 0 };

            switch (RNG.Next(5))
            {
                case 0:
                    WhitePatches = RNG.ChooseFrom(LooksGroups.LittleWhite);
                    return;
                case 1:
                    WhitePatches = RNG.ChooseFrom(LooksGroups.MiddleWhite);
                    return;
                case 2:
                    WhitePatches = RNG.ChooseFrom(LooksGroups.HighWhite);
                    if (Points is not null)
                        Points = null;
                    return;
                case 3:
                    WhitePatches = RNG.ChooseFrom(LooksGroups.MostlyWhite);
                    if (Points is not null)
                        Points = null;
                    return;
                case 4:
                    WhitePatches = "Fullwhite";
                    if (Points is not null)
                        Points = null;
                    return;
            }
        }




        public void Expand()
        {
            Cat ThisCat = Source;
            CRandom RNG = new CRandom(ThisCat.Seed);

            // InitPatternCOlor
            bool PeltWhite = RandomizePatternColor(RNG);

            // InitWhitePatches
            int VitiligoChance = 1 << 8; // GAMECONFIGCHANCE

            if (RNG.Next(VitiligoChance) == 0)
                Vitiligo = RNG.ChooseFrom(LooksGroups.Vitiligo);

            if (PeltWhite)
                RandomizeWhitePatches(RNG, PeltWhite);

            // InitSprite
            SpriteNewborn = SpriteType.Newborn;
            SpriteKitten = (SpriteType)RNG.Next(3);
            SpriteAdolescent = (SpriteType)RNG.Next(3, 6);
            SpriteSenior = (SpriteType)RNG.Next(12, 15);
            SpriteAdultSick = SpriteType.SickAdult;
            SpriteYoungSick = SpriteType.SickYoung;

            if (Length == PeltLength.Long)
            {
                SpriteAdult = (SpriteType)RNG.Next(9, 12);
                SpriteAdultParalyzed = SpriteType.ParalyzedShort;
            }
            else
            {
                SpriteAdult = (SpriteType)RNG.Next(6, 9);
                SpriteAdultParalyzed = SpriteType.ParalyzedLong;
            }

            SpriteYoungParalyzed = SpriteType.ParalyzedLong;
            SpriteYoungAdult = SpriteSeniorAdult = SpriteAdult;

            Skin = RNG.ChooseFrom(LooksGroups.SkinSprites);
            Reversed = RNG.Choose();

            // InitScars + InitAccessories
            if (ThisCat.Age != Age.Newborn)
            {
                int ScarChance, AccessoryChance;
                switch (ThisCat.Age)
                {
                    case Age.Kitten:
                    case Age.Adolescent:
                        ScarChance = 50; AccessoryChance = 180; break;
                    case Age.YoungAdult:
                    case Age.Adult:
                        ScarChance = 20; AccessoryChance = 100; break;
                    default:
                        ScarChance = 15; AccessoryChance = 80; break;
                }

                if (RNG.Next(ScarChance) == 0)
                    Scars.Add(
                        RNG.ChooseFrom(RNG.Choose() ? LooksGroups.Scars : LooksGroups.SpecialScars)
                    );
                if (RNG.Next(AccessoryChance) == 0)
                    Accessory = RNG.ChooseFrom(RNG.Choose() ? LooksGroups.PlantAccessories : LooksGroups.WildAccessories);
            }

            // InitEyes
            EyeColour = RNG.ChooseFrom(LooksGroups.EyeColours);

            int BaseHeterochromia = 120; // GAMECONFIGCHANCE

            if (Colour == "White" || Array.IndexOf(LooksGroups.HighWhite, WhitePatches) > -1 || Array.IndexOf(LooksGroups.MostlyWhite, WhitePatches) > -1 || WhitePatches == "Fullwhite")
                BaseHeterochromia -= 90;
            if (Colour == "White" || WhitePatches == "Fullwhite")
                BaseHeterochromia -= 10;

            if (RNG.Next(BaseHeterochromia < 0 ? 1 : BaseHeterochromia) == 0)
            {
                if (Array.IndexOf(LooksGroups.YellowEyes, EyeColour) > -1)
                    EyeColour2 = RNG.ChooseFrom(RNG.Choose() ? LooksGroups.BlueEyes : LooksGroups.GreenEyes);
                else if (Array.IndexOf(LooksGroups.BlueEyes, EyeColour) > -1)
                    EyeColour2 = RNG.ChooseFrom(RNG.Choose() ? LooksGroups.YellowEyes : LooksGroups.GreenEyes);
                else if (Array.IndexOf(LooksGroups.GreenEyes, EyeColour) > -1)
                    EyeColour2 = RNG.ChooseFrom(RNG.Choose() ? LooksGroups.BlueEyes : LooksGroups.YellowEyes);
            }

            // InitPattern
            if (Array.IndexOf(LooksGroups.Torties, Name) > -1)
            {
                TortieBase ??= RNG.ChooseFrom(LooksGroups.TortieBases);
                Pattern ??= RNG.ChooseFrom(LooksGroups.TortiePatterns);

                int WildcardChance = 1 << 9; // GAMECONFIGCHANCE

                if (Colour is not null)
                {
                    if (WildcardChance == 0 || RNG.Next(WildcardChance) == 0)
                    {
                        TortiePattern = RNG.ChooseFrom(LooksGroups.TortieBases);

                        var PossibleColours = LooksGroups.PeltColours.ToList();
                        PossibleColours.Remove(Colour);

                        TortieColour = RNG.ChooseFrom(PossibleColours);
                    }
                    else
                    {
                        if (TortieBase == "Singlestripe" || TortieBase == "Smoke" || TortieBase == "Single")
                            TortiePattern = RNG.ChooseFrom(new string[] { "Tabby", "Mackerel", "Classic", "Single", "Smoke", "Agouti", "Ticked" });
                        else
                            TortiePattern = RNG.Chance(.97) ? TortieBase : "Single";

                        if (Colour == "White")
                        {
                            var PossibleColours = LooksGroups.WhiteColours.ToList();
                            PossibleColours.Remove("White");
                            Colour = RNG.ChooseFrom(PossibleColours);
                        }

                        if (Array.IndexOf(LooksGroups.BlackColours, Colour) > -1 || Array.IndexOf(LooksGroups.WhiteColours, Colour) > -1)
                        {
                            TortieColour = RNG.ChooseFrom(RNG.Chance(1 / 3) ? LooksGroups.GingerColours : LooksGroups.BrownColours);
                        }
                        else if (Array.IndexOf(LooksGroups.GingerColours, Colour) > -1)
                        {
                            TortieColour = RNG.ChooseFrom(RNG.Chance(1 / 3) ? LooksGroups.BrownColours : LooksGroups.BrownColours);
                        }
                        else if (Array.IndexOf(LooksGroups.BrownColours, Colour) > -1)
                        {
                            var PossibleColours = LooksGroups.BrownColours.ToList();
                            PossibleColours.Remove(Colour);
                            PossibleColours.AddRange(LooksGroups.BrownColours);
                            PossibleColours.AddRange(LooksGroups.BlackColours);
                            PossibleColours.AddRange(LooksGroups.BlackColours);
                            TortieColour = RNG.ChooseFrom(PossibleColours);
                        }
                        else
                            TortieColour = "Golden";
                    }
                }
                else
                    TortieColour = "Golden";
            }

            // InitTints
            Tints.PossibleTints.TryGetValue("Basic", out string[] BaseTints);
            string[] ColorTints = null;
            if (Tints.ColourGroups.ContainsKey(Colour))
            {
                ColorTints = Tints.PossibleTints[Tints.ColourGroups[Colour]];
            }

            if (BaseTints is not null || ColorTints is not null)
                Tint = RNG.ChooseFrom((RNG.Choose() ? BaseTints : ColorTints) ?? BaseTints ?? ColorTints);

            if (WhitePatches is not null || Points is not null)
            {
                Tints.WhitePatches.PossibleTints.TryGetValue("Basic", out BaseTints);
                ColorTints = null;
                if (Tints.WhitePatches.ColourGroups.ContainsKey(Colour)) // This is following old code, but I cannot tell if its intended to be from CatTint or WhitePatchesTint
                {
                    ColorTints = Tints.WhitePatches.PossibleTints[Tints.WhitePatches.ColourGroups[Colour]];
                }

                if (BaseTints is not null || ColorTints is not null)
                    WhitePatchesTint = RNG.ChooseFrom((RNG.Choose() ? BaseTints : ColorTints) ?? BaseTints ?? ColorTints);
            }
        }
        public void Expand(CatRef FirstParent, CatRef SecondParent)
        {
            Cat ThisCat = Source;
            CRandom RNG = new CRandom(ThisCat.Seed);

            Looks FirstLooks = FirstParent.Value.Looks, SecondLooks = SecondParent.Value.Looks;

            // InitPatternCOlor
            bool PeltWhite = PatternColorInheritance(RNG, FirstParent, SecondParent);

            // InitWhitePatches
            int VitiligoChance = 1 << (8 + (FirstParent.Value.Looks.Vitiligo is null ? 0 : -1) + (SecondParent.Value.Looks.Vitiligo is null ? 0 : -1)); // GAMECONFIGCHANCE

            if (RNG.Next(VitiligoChance) == 0)
                Vitiligo = RNG.ChooseFrom(LooksGroups.Vitiligo);

            if (PeltWhite)
                WhitePatchInheritance(RNG, FirstParent, SecondParent);

            // InitSprite
            SpriteNewborn = SpriteType.Newborn;
            SpriteKitten = (SpriteType)RNG.Next(3);
            SpriteAdolescent = (SpriteType)RNG.Next(3, 6);
            SpriteSenior = (SpriteType)RNG.Next(12, 15);
            SpriteAdultSick = SpriteType.SickAdult;
            SpriteYoungSick = SpriteType.SickYoung;

            if (Length == PeltLength.Long)
            {
                SpriteAdult = (SpriteType)RNG.Next(9, 12);
                SpriteAdultParalyzed = SpriteType.ParalyzedShort;
            }
            else
            {
                SpriteAdult = (SpriteType)RNG.Next(6, 9);
                SpriteAdultParalyzed = SpriteType.ParalyzedLong;
            }

            SpriteYoungParalyzed = SpriteType.ParalyzedLong;
            SpriteYoungAdult = SpriteSeniorAdult = SpriteAdult;

            Skin = RNG.ChooseFrom(LooksGroups.SkinSprites);
            Reversed = RNG.Choose();

            // InitScars + InitAccessories
            if (ThisCat.Age != Age.Newborn)
            {
                int ScarChance, AccessoryChance;
                switch (ThisCat.Age)
                {
                    case Age.Kitten:
                    case Age.Adolescent:
                        ScarChance = 50; AccessoryChance = 180; break;
                    case Age.YoungAdult:
                    case Age.Adult:
                        ScarChance = 20; AccessoryChance = 100; break;
                    default:
                        ScarChance = 15; AccessoryChance = 80; break;
                }

                if (RNG.Next(ScarChance) == 0)
                    Scars.Add(
                        RNG.ChooseFrom(RNG.Choose() ? LooksGroups.Scars : LooksGroups.SpecialScars)
                    );
                if (RNG.Next(AccessoryChance) == 0)
                    Accessory = RNG.ChooseFrom(RNG.Choose() ? LooksGroups.PlantAccessories : LooksGroups.WildAccessories);
            }

            // InitEyes
            EyeColour = RNG.Choose() ? RNG.ChooseFrom(LooksGroups.EyeColours) : (RNG.Choose() ? FirstLooks : SecondLooks).EyeColour;

            int BaseHeterochromia = 120; // GAMECONFIGCHANCE

            if (Colour == "White" || Array.IndexOf(LooksGroups.HighWhite, WhitePatches) > -1 || Array.IndexOf(LooksGroups.MostlyWhite, WhitePatches) > -1 || WhitePatches == "Fullwhite")
                BaseHeterochromia -= 90;
            if (Colour == "White" || WhitePatches == "Fullwhite")
                BaseHeterochromia -= 10;

            BaseHeterochromia += (FirstLooks.EyeColour2 is not null ? -10 : 0) + (SecondLooks.EyeColour2 is not null ? -10 : 0);

            if (RNG.Next(BaseHeterochromia < 0 ? 1 : BaseHeterochromia) == 0)
            {
                if (Array.IndexOf(LooksGroups.YellowEyes, EyeColour) > -1)
                    EyeColour2 = RNG.ChooseFrom(RNG.Choose() ? LooksGroups.BlueEyes : LooksGroups.GreenEyes);
                else if (Array.IndexOf(LooksGroups.BlueEyes, EyeColour) > -1)
                    EyeColour2 = RNG.ChooseFrom(RNG.Choose() ? LooksGroups.YellowEyes : LooksGroups.GreenEyes);
                else if (Array.IndexOf(LooksGroups.GreenEyes, EyeColour) > -1)
                    EyeColour2 = RNG.ChooseFrom(RNG.Choose() ? LooksGroups.BlueEyes : LooksGroups.YellowEyes);
            }

            // InitPattern
            if (Array.IndexOf(LooksGroups.Torties, Name) > -1)
            {
                TortieBase ??= RNG.ChooseFrom(LooksGroups.TortieBases);
                Pattern ??= RNG.ChooseFrom(LooksGroups.TortiePatterns);

                int WildcardChance = 1 << 9; // GAMECONFIGCHANCE

                if (Colour is not null)
                {
                    if (WildcardChance == 0 || RNG.Next(WildcardChance) == 0)
                    {
                        TortiePattern = RNG.ChooseFrom(LooksGroups.TortieBases);

                        var PossibleColours = LooksGroups.PeltColours.ToList();
                        PossibleColours.Remove(Colour);

                        TortieColour = RNG.ChooseFrom(PossibleColours);
                    }
                    else
                    {
                        if (TortieBase == "Singlestripe" || TortieBase == "Smoke" || TortieBase == "Single")
                            TortiePattern = RNG.ChooseFrom(new string[] { "Tabby", "Mackerel", "Classic", "Single", "Smoke", "Agouti", "Ticked" });
                        else
                            TortiePattern = RNG.Chance(.97) ? TortieBase : "Single";

                        if (Colour == "White")
                        {
                            var PossibleColours = LooksGroups.WhiteColours.ToList();
                            PossibleColours.Remove("White");
                            Colour = RNG.ChooseFrom(PossibleColours);
                        }

                        if (Array.IndexOf(LooksGroups.BlackColours, Colour) > -1 || Array.IndexOf(LooksGroups.WhiteColours, Colour) > -1)
                        {
                            TortieColour = RNG.ChooseFrom(RNG.Chance(1 / 3) ? LooksGroups.GingerColours : LooksGroups.BrownColours);
                        }
                        else if (Array.IndexOf(LooksGroups.GingerColours, Colour) > -1)
                        {
                            TortieColour = RNG.ChooseFrom(RNG.Chance(1 / 3) ? LooksGroups.BrownColours : LooksGroups.BrownColours);
                        }
                        else if (Array.IndexOf(LooksGroups.BrownColours, Colour) > -1)
                        {
                            var PossibleColours = LooksGroups.BrownColours.ToList();
                            PossibleColours.Remove(Colour);
                            PossibleColours.AddRange(LooksGroups.BrownColours);
                            PossibleColours.AddRange(LooksGroups.BlackColours);
                            PossibleColours.AddRange(LooksGroups.BlackColours);
                            TortieColour = RNG.ChooseFrom(PossibleColours);
                        }
                        else
                            TortieColour = "Golden";
                    }
                }
                else
                    TortieColour = "Golden";
            }

            // InitTints
            Tints.PossibleTints.TryGetValue("Basic", out string[] BaseTints);
            string[] ColorTints = null;
            if (Tints.ColourGroups.ContainsKey(Colour))
            {
                ColorTints = Tints.PossibleTints[Tints.ColourGroups[Colour]];
            }

            if (BaseTints is not null || ColorTints is not null)
                Tint = RNG.ChooseFrom((RNG.Choose() ? BaseTints : ColorTints) ?? BaseTints ?? ColorTints);
            
            if (WhitePatches is not null || Points is not null)
            {
                Tints.WhitePatches.PossibleTints.TryGetValue("Basic", out BaseTints);
                ColorTints = null;
                if (Tints.ColourGroups.ContainsKey(Colour)) // This is following old code, but I cannot tell if its intended to be from CatTint or WhitePatchesTint
                {
                    ColorTints = Tints.WhitePatches.PossibleTints[Tints.WhitePatches.ColourGroups[Colour]];
                }

                if (BaseTints is not null || ColorTints is not null)
                    WhitePatchesTint = RNG.ChooseFrom((RNG.Choose() ? BaseTints : ColorTints) ?? BaseTints ?? ColorTints);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Destination"></param>
        /// <param name="LifeState"></param>
        /// <param name="HideScars"></param>
        /// <param name="HideAccessorys"></param>
        /// <param name="IsAlwaysLiving"></param>
        /// <param name="IsAlwaysHealthy">True if Cat is to be rendered healthy, False if otherwise</param>
        public void RenderTo(Rect Destination, Age? LifeState = null, bool HideScars = false, bool HideAccessorys = false, bool IsAlwaysLiving = false, bool IsAlwaysHealthy = false)
        {
            Cat ThisCat = Source;
            Age Age = LifeState ?? ThisCat.Age;
            bool Dead = IsAlwaysLiving ? false : ThisCat.Dead;

            SpriteType CatSprite;

            bool AllowSickSprites = true; // GAMECONFIGCHANCE
            bool AllCatsAreNewborn = false;

            if (!IsAlwaysHealthy && !ThisCat.IsAbleToWork && Age != Age.Newborn && AllowSickSprites)
                CatSprite = Age == Age.Kitten || Age == Age.Adolescent ? SpriteType.SickYoung : SpriteType.SickAdult;
            else if (!IsAlwaysHealthy && ThisCat.Looks.Paralyzed)
                CatSprite = Age == Age.Kitten || Age == Age.Adolescent ? SpriteType.ParalyzedYoung : (ThisCat.Looks.Length == PeltLength.Long ? SpriteType.ParalyzedLong : SpriteType.ParalyzedShort);
            else
                CatSprite = AllCatsAreNewborn ? ThisCat.Looks.SpriteNewborn : ThisCat.Looks.GetSpriteType(Age);

            Image Sprite = new Image(50, 50); // TEMPORARY

            using (var Target = new RenderTarget(Sprite))
            {
                if (Name != "Tortie" && Name != "Calico")
                {
                    Context.Render(Context.Sprites[$"{GetSpritesName(Name)}.{Colour}.{CatSprite}"]);
                }
                else
                {
                    Context.Render(Context.Sprites[$"{GetSpritesName(TortieBase)}.{Colour}.{CatSprite}"]);

                    Image Patches = Context.Sprites[$"{GetSpritesName(TortiePattern)}.{TortieColour}.{CatSprite}"];
                    Image TortieMask = Context.Sprites[$"TortieMask.{Pattern}.{CatSprite}"];

                    TortieMask.SetBlendmode(SDL_BlendMode.SDL_BLENDMODE_MUL);

                    Context.RenderOnto(TortieMask, Patches);
                    Context.RenderOnto(Patches, Sprite);
                }

                // Applying Tint
                if (Tint is not null && Tints.Colors.ContainsKey(Tint))
                {
                    Image TintOverlay = new Image(50, 50, Tints.Colors[Tint]);
                    TintOverlay.SetBlendmode(SDL_BlendMode.SDL_BLENDMODE_MUL);
                    Context.RenderOnto(TintOverlay, Sprite);
                }

                // Drawing Whitepatches
                if (WhitePatches is not null)
                {
                    Image WhitePatchLayer = Context.Sprites[$"WhitePatches.{WhitePatches}.{CatSprite}"];

                    // Applying Tint for WhitePatches
                    if (WhitePatchesTint is not null && Tints.WhitePatches.Colors.ContainsKey(WhitePatchesTint))
                    {
                        Image TintOverlay = new Image(50, 50, Tints.WhitePatches.Colors[WhitePatchesTint]);
                        TintOverlay.SetBlendmode(SDL_BlendMode.SDL_BLENDMODE_MUL);
                        Context.RenderOnto(TintOverlay, WhitePatchLayer);
                    }

                    WhitePatchLayer.SetBlendmode(SDL_BlendMode.SDL_BLENDMODE_BLEND);
                    Context.Render(WhitePatchLayer);
                }

                // Drawing points
                if (Points is not null)
                {
                    Image PointsLayer = Context.Sprites[$"WhitePatches.{Points}.{CatSprite}"];

                    // Applying Tint for WhitePatches
                    if (WhitePatchesTint is not null && Tints.WhitePatches.Colors.ContainsKey(WhitePatchesTint))
                    {
                        Image TintOverlay = new Image(50, 50, Tints.WhitePatches.Colors[WhitePatchesTint]);
                        TintOverlay.SetBlendmode(SDL_BlendMode.SDL_BLENDMODE_MUL);
                        Context.RenderOnto(TintOverlay, PointsLayer);
                    }

                    PointsLayer.SetBlendmode(SDL_BlendMode.SDL_BLENDMODE_BLEND);
                    Context.Render(PointsLayer);
                }

                // Drawing vitiligo
                if (Vitiligo is not null)
                    Context.Render(Context.Sprites[$"WhitePatches.{Vitiligo}.{CatSprite}"]);

                // Drawing eyes
                Image EyesLayer = Context.Sprites[$"Eyes.{EyeColour}.{CatSprite}"];

                if (EyeColour2 is not null)
                {
                    Context.RenderOnto(Context.Sprites[$"Eyes2.{EyeColour2}.{CatSprite}"], EyesLayer);
                }

                Context.Render(EyesLayer);

                // Drawing scars and special scars
                for (int i = 0; i < Scars.Count; i++)
                {
                    var Scar = Scars[i];

                    if (LooksGroups.Scars.Contains(Scar) || LooksGroups.SpecialScars.Contains(Scar))
                        Context.Render(Context.Sprites[$"Scars.{Scar}.{CatSprite}"]);
                }

                // Drawing lineart
                if (true && !Dead) // TEMPORARY -> true is the bool value for ShadersOn settings
                {
                    Image Shader = Context.Sprites[$"Shaders.{CatSprite}"];
                    Shader.SetBlendmode(SDL_BlendMode.SDL_BLENDMODE_MUL);
                    Context.Render(Shader);
                    Context.Render(Context.Sprites[$"Lighting.{CatSprite}"]);
                }

                if (!Dead)
                    Context.Render(Context.Sprites[$"Lineart.{CatSprite}"]);
                else if (ThisCat.DarkForest)
                    Context.Render(Context.Sprites[$"LineartDarkforest.{CatSprite}"]);
                else if (Dead)
                    Context.Render(Context.Sprites[$"LineartDead.{CatSprite}"]);

                // Drawing skin
                Context.Render(Context.Sprites[$"Skin.{Skin}.{CatSprite}"]);

                // Drawing missing scars
                for (int i = 0; i < Scars.Count; i++)
                {
                    var Scar = Scars[i];

                    if (LooksGroups.MissingScars.Contains(Scar))
                    {
                        Image MissingScar = Context.Sprites[$"ScarsMissing.{Scar}.{CatSprite}"];
                        MissingScar.SetBlendmode(SDL_BlendMode.SDL_BLENDMODE_MOD);
                        Context.Render(MissingScar);
                    }
                }

                // Draw accessories
                if (Array.IndexOf(LooksGroups.PlantAccessories, Accessory) > -1)
                    Context.Render(Context.Sprites[$"MedcatHerbs.{Accessory}.{CatSprite}"]);
                else if (Array.IndexOf(LooksGroups.Collars, Accessory) > -1)
                    Context.Render(Context.Sprites[$"Collars.{Accessory}.{CatSprite}"]);
            }

            // Reverse the sprite
            if (Reversed)
                Context.RenderEx(Sprite, null, Destination, true);
            else
                Context.Render(Sprite, null, Destination);
            
        }


        public string GetDescription() // IMPLEMENT
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the stored <see cref="SpriteType"/> Value that lines up with the given <see cref="Age"/> Value
        /// </summary>
        /// <param name="Value">The given age</param>
        /// <returns>The <see cref="SpriteType"/> Value stored for the <see cref="Age"/> Value given</returns>
        /// <exception cref="NotImplementedException">Raised when switch-case statement hasnt been updated for any newly added <see cref="Age"/> Values</exception>
        public SpriteType GetSpriteType(Age Value)
        {
            switch (Value)
            {
                case Age.Newborn: 
                    return SpriteNewborn;
                case Age.Kitten: 
                    return SpriteKitten;
                case Age.Adolescent: 
                    return SpriteAdolescent;
                case Age.YoungAdult: 
                    return SpriteYoungAdult;
                case Age.Adult: 
                    return SpriteAdult;
                case Age.SeniorAdult: 
                    return SpriteSeniorAdult;
                case Age.Senior: 
                    return SpriteSenior;
                default: // Only here to tell if a new Age value has been added and not accounted for
                    throw new NotImplementedException();
            }
        }

        public string GetSpritesName(string Value)
        {
            switch (Value)
            {
                case "SingleColour":
                case "TwoColour":
                    return "Single";
                case "Tortie":
                case "Calico":
                    return null; // TEMPORARY
                default:
                    return Value;
            }
        }
    }
}
