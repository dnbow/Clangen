using System.Collections.Generic;
using System.Linq;
using System;
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
        Young,
        Adult,
        Senior,
        YoungLong,
        AdultLong,
        SeniorLong,
        Elder,
        ElderLong,
        ElderApprentice,
        AdultParalyzed,
        YoungParalyzed,
        NewbornParalyzed,
        AdultSick,
        YoungSick,
        Newborn
    }



    public static class LooksGroups // ENTIRELY VERY temporary
    {
        public static string[] EyeColours = {

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

        public static string[] TortieBases = { };
        public static string[] TortiePatterns = { };

        public static string[] PeltColours = { };

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

        public static string[] LittleWhite;
        public static string[] MiddleWhite;
        public static string[] HighWhite;
        public static string[] MostlyWhite;

        public static string[] SkinSprites = {
            "Black", "Pink", "Darkbrown", "Brown", "Lightbrown", "Dark", "Darkgrey", "Grey", "Darksalmon",
            "Salmon", "Peach", "Darkmarbled", "Marbled", "Lightmarbled", "Darkblue", "Blue", "Lightblue", "Red"
        };

        public static string[] Scars = {

        };
        public static string[] MissingScars = {

        };
        public static string[] SpecialScars = {

        };

        public static string[] AccessoriesPlant = {

        };
        public static string[] AccessoriesWild = {

        };
    }



    public static class CatTint // VERY TEMPORARY
    {

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



        public Looks()
        {
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
                ChosenTortieBase = ChosenTortieBase == "TwoColour" || ChosenTortieBase == "SingleColour" ? "single" : ChosenPelt.ToLower();
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
                ChosenTortieBase = ChosenTortieBase == "TwoColour" || ChosenTortieBase == "SingleColour" ? "single" : ChosenPelt.ToLower();
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




        public void Expand(int Seed)
        {
            CRandom RNG = Seed != -1 ? new CRandom(Seed) : new CRandom();
            Cat ThisCat = Source;

            // InitPatternColor
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
            SpriteAdultSick = SpriteType.AdultSick;
            SpriteYoungSick = SpriteType.YoungSick;

            if (Length == PeltLength.Long)
            {
                SpriteAdult = (SpriteType)RNG.Next(9, 12);
                SpriteAdultParalyzed = SpriteType.AdultParalyzed;
            }
            else
            {
                SpriteAdult = (SpriteType)RNG.Next(6, 9);
                SpriteAdultParalyzed = SpriteType.YoungParalyzed;
            }

            SpriteYoungParalyzed = SpriteType.YoungParalyzed;
            SpriteYoungAdult = SpriteSeniorAdult = SpriteAdult;

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
                    Accessory = RNG.ChooseFrom(RNG.Choose() ? LooksGroups.AccessoriesPlant : LooksGroups.AccessoriesWild);
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

        }
        public void Expand(int Seed, CatRef FirstParent, CatRef SecondParent)
        {
            CRandom RNG = Seed != -1 ? new CRandom(Seed) : new CRandom();
            Cat ThisCat = Source;

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
            SpriteAdultSick = SpriteType.AdultSick;
            SpriteYoungSick = SpriteType.YoungSick;

            if (Length == PeltLength.Long)
            {
                SpriteAdult = (SpriteType)RNG.Next(9, 12);
                SpriteAdultParalyzed = SpriteType.AdultParalyzed;
            }
            else
            {
                SpriteAdult = (SpriteType)RNG.Next(6, 9);
                SpriteAdultParalyzed = SpriteType.YoungParalyzed;
            }

            SpriteYoungParalyzed = SpriteType.YoungParalyzed;
            SpriteYoungAdult = SpriteSeniorAdult = SpriteAdult;

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
                    Accessory = RNG.ChooseFrom(RNG.Choose() ? LooksGroups.AccessoriesPlant : LooksGroups.AccessoriesWild);
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
                        if (TortieBase == "singlestripe" || TortieBase == "smoke" || TortieBase == "single")
                            TortiePattern = RNG.ChooseFrom(new string[] { "tabby", "mackerel", "classic", "single", "smoke", "agouti", "ticked" });
                        else
                            TortiePattern = RNG.Chance(.97) ? TortieBase : "single";

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
                {
                    TortieColour = "Golden";
                }
            }
        }
    }
}
