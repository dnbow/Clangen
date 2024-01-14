using System.Collections.Generic;


namespace Clangen.Managers
{
    public class CatManager
    {
        private object __Lock;

        private readonly List<Cats.Cat> KittyArray;
        private ushort Index;

        public CatManager() 
        {
            __Lock = new object();

            KittyArray = new List<Cats.Cat>(1024);
            Index = 0;
        }

        public ushort NextID
        {
            get
            {
                lock (__Lock)
                {
                    return Index++;
                }
            }
        }

        public Cats.Cat this[ushort Identifer]
        {
            get => KittyArray[Identifer];
            set => KittyArray[Identifer] = value;
        }
    }
}

namespace Clangen.Cats
{
    public enum Age : byte
    {
        Newborn, Kitten, Adolescent, YoungAdult, Adult, SeniorAdult, Senior
    }

    public enum Status : byte
    {
        Newborn, Kitten, Elder, Apprentice, Warrior, MediatorApprentice, Mediator, MedicineCatApprentice, MedicineCat, Deputy, Leader
    }



    public struct Pronoun
    {
        public string Subject;
        public string Object;
        public string Possessive;
        public string Inpossessive;
        public string Self;
        public bool Conjugate;
        // False: Plural, True: Singular
    }



    public class Looks
    {
        public string Name;
        public string Colour;
        public string Pattern;
        public string TortieBase;
        public string TortiePattern;
        public string TortieColour;
        public string WhitePatches;
        public string EyeColour;
        public string EyeColour2;
        public string Vitiligo;
        public string Length;
        public string Points;
        public string Accessory;
        public string Scars;
        public string Tint;
        public string WhitePatchesTint;
        public string Skin;

        public byte Opacity;

        public bool Paralyzed;
        public bool Reversed;
    }



    public struct Skills
    {

    }

    public struct Personality
    {

    }

    /// <summary>
    /// Represents a Reference to a Cat without using pointers for now, to keep things simpler and safer
    /// </summary>
    public readonly struct CatRef
    {
        public readonly ushort Identifier;

        public CatRef(Cat Cat)
        {
            Identifier = Cat.Identifier;
        }
        public CatRef(ushort Identifier)
        {
            this.Identifier = Identifier;
        }
        public Cat Value
        {
            get => Context.Clan.Cats[Identifier];
        }

        public override int GetHashCode() => Identifier;
        public override bool Equals(object obj)
        {
            if (obj is null)
                return Identifier == 0;

            return obj.Equals(this);
        }



        public static bool operator ==(CatRef This, CatRef Other) => This.Identifier == Other.Identifier;
        public static bool operator !=(CatRef This, CatRef Other) => This.Identifier != Other.Identifier;

        public static implicit operator CatRef(Cat Value) => new CatRef(Value.Identifier);
        public static implicit operator Cat(CatRef Value) => Value;
    }

    public class Cat
    {
        private readonly ushort Seed;
        private ushort __Moons;
        private int __Experience;

        public readonly ushort Identifier;

        public string Prefix;
        public string Suffix;
        public bool SpecialSuffixHidden;

        public Looks Looks;

        public Status Status;
        public Pronoun Pronouns;
        public Personality Personality;
        public History History;
        public Skills Skills;

        public CatRef Mentor;
        public int PatrolWithMentor; // Not entirely sure what this is for

        public readonly Relations Relations;
        public readonly KinRelations Kin;

        public Age Age;

        public bool Dead;
        public bool Outside;
        public bool Exiled;


        // DETERMINING USE FOR VARIABLES BELOW
        public int DeadFor;
        public bool DarkForest;
        public string Gender;
        public string Backstory;
        public List<CatRef> Apprentice;
        public dynamic Placement;
        public bool Example;
        public string Thought;
        public bool Genderalign;
        public int BirthCooldown;
        public dynamic Illnesses;
        public dynamic Injuries;
        public dynamic HealedCondition;
        public dynamic LeaderDeathHeal;
        public bool AlsoGot;
        public dynamic PermenantCondition;
        public dynamic ExperienceLevel;
        public bool NoKits;
        public bool NoMates;
        public bool NoRetire;
        public bool PreventFading;
        public List<CatRef> FadedOffspring;
        public bool Faded;
        public bool Favourite;
        public int InCamp;
        public Image _Sprite;


        public Cat(ushort Seed, ushort Identifier)
        {
            this.Seed = Seed;
            this.Identifier = Identifier;

            
        }

        public override int GetHashCode()
        {
            return (Identifier << 16) | Seed;
        }
        public override string ToString()
        {
            return $"CatObject {Identifier} : {Seed}";
        }

        public void Expand()
        {

        }
    }
}
