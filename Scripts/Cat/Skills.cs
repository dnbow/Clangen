using System.Collections.Generic;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Clangen.Cats
{
    public enum SkillPath : byte
    {
        Teacher,
        Hunter,
        Fighter,
        Runner,
        Climber,
        Swimmer,
        Speaker,
        Mediator,
        Clever,
        Insightful,
        Sense,
        Kit,
        Story,
        Lore,
        Camp,
        Healer,
        Star,
        Dark,
        Omen,
        Dream,
        Clairvoyant,
        Prophet,
        Ghost
    }

    [Flags]
    public enum SkillType : byte {
        Supernatural = 0x1,
        Strong = 0x2,
        Agile = 0x4,
        Smart = 0x8,
        Observant = 0x10,
        Social = 0x20,
    }



    public enum HiddenSkillPath : byte
    {
        Rogue,
        Loner,
        Kittypet
    }



    public struct Skill
    {

    }



    public struct Skills
    {
        public static readonly SkillPath[] UncommonPaths = { SkillPath.Ghost, SkillPath.Prophet, SkillPath.Clairvoyant, SkillPath.Dream, SkillPath.Omen, SkillPath.Star, SkillPath.Healer, SkillPath.Dark };
        public static readonly SkillPath[] AllPaths = Enum.GetValues(typeof(SkillPath)).Cast<SkillPath>().ToArray();

        /// <summary>
        /// Returns a randomly picked Skillpath, with a 1 in 15 percent chance of an uncommon one
        /// </summary>
        public static SkillPath GetRandomSkillPath()
        {
            return Context.Random.ChooseFrom(Context.Random.Next(15) == 0 ? UncommonPaths : AllPaths);
        }

        /// <summary>
        /// Returns a randomly picked Skillpath (excluding the given path), with a 1 in 15 percent chance of an uncommon one
        /// </summary>
        public static SkillPath GetRandomSkillPath(SkillPath ExistingPath) // TEMPORARY -> not really the best code
        {
            SkillPath NewPath;
            while ((NewPath = GetRandomSkillPath()) == ExistingPath);
            return NewPath;
        }


        public SkillPath Primary;
        public int PrimaryPoints;
        public SkillPath Secondary;
        public int SecondaryPoints;
        public HiddenSkillPath Hidden;
        public bool InterestOnly;



        public Skills(SkillPath Primary, int PrimaryPoints, SkillPath Secondary, int SecondaryPoints, HiddenSkillPath Hidden, bool InterestOnly)
        {
            this.Primary = Primary;
            this.PrimaryPoints = PrimaryPoints;
            this.Secondary = Secondary;
            this.SecondaryPoints = SecondaryPoints;
            this.Hidden = Hidden;
            this.InterestOnly = InterestOnly;
        }
    }
}
