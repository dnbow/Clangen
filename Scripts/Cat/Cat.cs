using System;
using System.Collections.Generic;
using System.Linq;
using Clangen.Cats;
using Clangen.Events;
using Clangen.Managers;
using static Clangen.Utility;

namespace Clangen.Managers
{
    public enum SortType : byte
    {
        Rank
    }

    public class CatManager
    {
        private readonly CRandom InternalRandom;
        private readonly object __Lock;
        private readonly Cat[] KittyArray;
        private ushort Index;

        public SortType SortType = SortType.Rank;

        public CatManager() 
        {
            InternalRandom = new CRandom();
            __Lock = new object();
            KittyArray = new Cat[4096];
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

        public Cat this[ushort Identifer]
        {
            get => KittyArray[Identifer];
        }

        public Cat NewCat()
        {
            var NewCat = new Cat(NextID, (ushort)(InternalRandom.NextDouble() * 65536));
            KittyArray[NewCat.Identifier] = NewCat;
            return NewCat;
        }

        public void Sort()
        {

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
        None, Elder, Apprentice, Warrior, MediatorApprentice, Mediator, MedicineCatApprentice, MedicineCat, Deputy, Leader,
        Exiled, 
    }



    public enum Gender : byte
    {
        Male, Female
    }



    public struct Pronoun
    {
        public string Subject;
        public string Object;
        public string Possessive;
        public string Inpossessive;
        public string Self;

        /// <summary>
        /// Represents True if this pronoun is Singular, False if its plural (they -> plural, he -> singular)
        /// </summary>
        public bool Conjugate;
    }



    /// <summary>
    /// Represents a Reference to a Cat without using pointers for now; it keep things simpler and safer.
    /// You may not see a use for this, but it's to keep memory usage down or when all you need to do is compare <see cref="Cat"/> objects
    /// </summary>
    public readonly struct CatRef
    {
        public readonly ushort Identifier;


        public CatRef(Cat Cat)
        {
            if (Cat is null)
                throw new ArgumentException("Provided Cat object must be not null");

            Identifier = Cat.Identifier;
        }
        public CatRef(ushort Identifier)
        {
            this.Identifier = Identifier;
        }
        public Cat Value
        {
            get => Context.Cats[Identifier];
        }



        public override int GetHashCode() => Identifier;
        public override bool Equals(object obj)
        {
            if (obj is null)
                return Identifier == 0;
            else if (obj is CatRef CatRef) 
                return Identifier == CatRef.Identifier;

            return obj.Equals(this);
        }



        public static bool operator ==(CatRef This, CatRef Other) => This.Identifier == Other.Identifier;
        public static bool operator !=(CatRef This, CatRef Other) => This.Identifier != Other.Identifier;

        public static implicit operator CatRef(Cat Value) => new CatRef(Value.Identifier);
        public static implicit operator Cat(CatRef Value) => Context.Cats[Value.Identifier];
    }



    public class Cat
    {
        
        private ushort __Moons;
        private int __Experience;

        public readonly ushort Seed;
        public readonly ushort Identifier;

        public string Prefix;
        public string Suffix;
        public bool SpecialSuffixHidden;

        public Looks Looks;

        public Age Age;

        public bool Gender;
        public Gender GenderAlign;

        public string Clan; // TEMPORARY

        public Status Status;
        public Pronoun Pronouns;
        public Skills Skills;

        public CatRef Mentor;
        public int PatrolWithMentor; // Not entirely sure what this is for

        public readonly Relations Relations;
        public readonly KinRelations Kin;

        public bool Dead;
        public bool Outside;
        public bool Exiled;

        public List<Condition> Conditions;


        // DETERMINING USAGE FOR VARIABLES BELOW
        public int DeadFor;
        public bool DarkForest;
        public string Backstory;
        public List<CatRef> Apprentices;
        public dynamic Placement;
        public bool Example;
        public string Thought;
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



        public bool IsAbleToWork
        {
            get => true;
        }

        public bool IsPregnant
        {
            get => false;
        }


        public ushort Moons
        {
            get => __Moons;
            set
            {
                __Moons = value;

                Age = GetAgeFromMoons(__Moons);
            }
        }


        public string Name
        {
            get
            {
                return $"{Suffix}{Prefix}";
            }
        }



        public Cat(ushort Identifier, ushort Seed)
        {
            this.Seed = Seed;
            this.Identifier = Identifier;

            Looks = new Looks(this);

            Expand();
        }



        public override int GetHashCode()
        {
            return (Identifier << 16) | Seed;
        }

        public override bool Equals(object obj)
        {
            if (obj is Cat Cat)
            {
                return Identifier == Cat.Identifier;
            }
            else if (obj is CatRef CatRef)
            {
                return Identifier == CatRef.Identifier;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return $"CAT {Identifier} {Seed}";
        }



        public void Expand()
        {
            CRandom RNG = new CRandom(Seed);

            Age = (Age)RNG.Next(7);
        }

        /// <summary>
        /// Make a cat die
        /// </summary>
        /// <param name="BodyRecovered">True if the Cats' body was recovered, False if otherwise. Defaults to True</param>
        /// <returns>Additional text to add on to this death event if applicable, otherwise returns null</returns>
        public string Die(bool BodyRecovered = true)
        {
            string AdditionalText;

            if (Status is Status.Leader && IsPregnant)
            {
                // MISSING - carry pregnant status on to the grave
            }
            else
            {
                Conditions.Clear();
            }

            if (Status is Status.Leader)
            {
                if (Context.Clan.LeaderLives > 0)
                {
                    Thought = Context.Text["Thought.Death.Leader"];
                    return null;
                }
                else
                {
                    Dead = true;
                    Context.Clan.LeaderLives = 0;

                    Thought = Context.Text["Thought.Death"];
                    AdditionalText = $"They've lost their last life and have travelled to {(Context.Clan.Instructor.DarkForest ? "the Dark Forest" : "StarClan")}.";
                }
            }
            else
            {
                Dead = true;
                Thought = Context.Text["Thought.Death"];
            }

            Relations.Clear();

            for (int i = 0; i < Apprentices.Count; i++)
                if (Apprentices[i].Value is Cat Cat)
                    Cat.UpdateMentor();

            return null;
        }


        /// <summary>
        /// Exile a cat, removing their status and giving them <see cref="Status.Exiled"/>
        /// </summary>
        public void Exile()
        {
            Exiled = Outside = true;
            Status = Status.Exiled;

            // MISSING -> Addendum for Vengeful trait "Swears their revenge for being exiled"
            Thought = "Is shocked that they have been exiled";

            for (int i = 0; i < Apprentices.Count; i++)
                if (Apprentices[i].Value is Cat Cat)
                    Cat.UpdateMentor();

            UpdateMentor(); 
        }



        public void Grief(bool BodyStatus = false) // MISSING
        {

        }


        /// <summary>
        /// Makes a Clan cat an "outside" cat. Handles removing them from special positions, and removing mentors and apprentices
        /// </summary>
        public void Gone()
        {
            Outside = true;

            if (Status is Status.Leader || Status is Status.Warrior)
                StatusChange(Status.Warrior);

            for (int i = 0; i < Apprentices.Count; i++)
                if (Apprentices[i].Value is Cat Cat)
                    Cat.UpdateMentor();
            UpdateMentor();

            Context.Clan.AddToOutside(this);

        }


        /// <summary>
        /// Changes the status of a Cat. Additional functions are needed if you wish to make a Cat a Leader or Deputy
        /// </summary>
        /// <param name="New">The new status to switch to</param>
        /// <param name="Resort">True if Cats should be resorted, False if we shouldnt</param>
        public void StatusChange(Status New, bool Resort = false)
        {
            Status Old = Status;
            Status = New;

            for (int i = 0; i < Apprentices.Count; i++)
                if (Apprentices[i].Value is Cat Cat)
                    Cat.UpdateMentor();
            UpdateMentor();

            if (Old is Status.MedicineCat)
                Context.Clan.RemoveMedCat(this);

            switch (Status)
            {
                case Status.Warrior:
                case Status.Elder:
                    if (Old is Status.Leader && Context.Clan.Leader == this)
                    {
                        Context.Clan.Leader = null;
                        Context.Clan.LeaderPredecessors += 1;
                    }

                    if (Context.Clan.Deputy is not null)
                    {
                        Context.Clan.Deputy = null;
                        Context.Clan.DeputyPredecessors += 1;
                    }
                    
                    break;

                case Status.MedicineCat:
                    Context.Clan.NewMedicineCat(this);
                    break;

                default:
                    break;
            }

            if (Context.Cats.SortType is SortType.Rank && Resort)
                Context.Cats.Sort();
        }



        public RelationDynamic GetFamilialRelation(Cat Other)
        {
            return IsParentOf(Other) ? RelationDynamic.Parent : IsChildOf(Other) ? RelationDynamic.Child : IsSiblingOf(Other) ? RelationDynamic.Sibling : RelationDynamic.None;
        }

        public Age GetAgeFromMoons(int Moons)
        {
            if (Moons < Context.Config.CatAges.Newborn)
                return Age.Newborn;
            else if (Moons < Context.Config.CatAges.Kitten)
                return Age.Kitten;
            else if (Moons < Context.Config.CatAges.Adolescent)
                return Age.Adolescent;
            else if (Moons < Context.Config.CatAges.YoungAdult)
                return Age.YoungAdult;
            else if (Moons < Context.Config.CatAges.Adult)
                return Age.Adult;
            else if (Moons < Context.Config.CatAges.SeniorAdult)
                return Age.SeniorAdult;
            else
                return Age.Senior;
        }



        private void RemoveMentor()
        {
            if (this.Mentor.Value is null) return;

            Cat Mentor = this.Mentor;

            Mentor.Apprentices.Remove(this);


            
        }
        public void UpdateMentor()
        {
            if (Dead || Outside || Exiled || Status is Status.Apprentice || Status is Status.MediatorApprentice || Status is Status.MedicineCatApprentice)
            {

            }
        }

        /// <summary>
        /// Returns True if <paramref name="Other"/> is parent of this cat, False if otherwise
        /// </summary>
        /// <param name="Other">The Target Cat</param>
        public bool IsParentOf(Cat Other)
        {
            return Other.Kin.Parent1 == this || Other.Kin.Parent2 == this;
        }

        /// <summary>
        /// Returns True if <paramref name="Other"/> is child of this cat, False if otherwise
        /// </summary>
        /// <param name="Other">The Target Cat</param>
        public bool IsChildOf(Cat Other)
        {
            return Kin.Parent1 == Other || Kin.Parent2 == Other;
        }

        /// <summary>
        /// Returns True if <paramref name="Other"/> is sibling of this cat, False if otherwise
        /// </summary>
        /// <param name="Other">The Target Cat</param>
        public bool IsSiblingOf(Cat Other)
        {
            return Kin.Siblings.Contains(Other);
        }
    }
}
