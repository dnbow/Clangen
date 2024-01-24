using Clangen.Managers;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Clangen.Cats
{
    /// <summary>
    /// Represents the current gamemode
    /// </summary>
    public enum GamemodeType : byte
    {
        Classic, Expanded, Cruel
    }

    public enum Herb : byte
    {
        ElderLeaves,
        Cobwebs,
        Daisy,
        Horsetail,
        Juniper,
        Lungwort,
        Mallow,
        Marigold,
        Moss,
        OakLeaves,
        Ragwort,
        Raspberry,
        Tansy,
        Thyme,
        WildGarlic,
        Dandelion,
        Mullein,
        Rosemary,
        Burdock,
        Blackberry,
        Goldenrod,
        Poppy,
        Plantain,
        Catmint
    }

    public class HerbCollection
    {
        private static byte HerbTypeCount;
        private int[] HerbAmounts;
        

        public HerbCollection()
        {
            if (HerbTypeCount != 0)
            {
                int MaxValue = 0;
                foreach (Herb Value in Enum.GetValues(typeof(Herb)))
                    if (MaxValue < (byte)Value)
                        MaxValue = (byte)Value;
            }

            HerbAmounts = new int[HerbTypeCount];
        }


        public bool Has(Herb Type)
        {
            return 0 < HerbAmounts[(byte)Type];
        }
        public bool Has(Herb Type, int Amount)
        {
            return Amount < HerbAmounts[(byte)Type];
        }


        public void Place(Herb Type)
        {
            HerbAmounts[(byte)Type] += 1;
        }
        public void Place(Herb Type, int Amount)
        {
            HerbAmounts[(byte)Type] += Amount;
        }


        public void Take(Herb Type)
        {
            HerbAmounts[(byte)Type] -= 1;
        }
        public void Take(Herb Type, int Amount)
        {
            HerbAmounts[(byte)Type] -= Amount;
            if (HerbAmounts[(byte)Type] < 0)
                HerbAmounts[(byte)Type] = 0;
            
        }


        public void Set(Herb Type, int Amount)
        {
            HerbAmounts[(byte)Type] = Amount;
        }


        public void Remove(Herb Type)
        {
            HerbAmounts[(byte)Type] = 0;
        }
    }

    /// <summary>
    /// Represents the current loaded Save and Clan
    /// </summary>
    public class Clan
    {
        public GamemodeType Gamemode;


        public HerbCollection Herbs;

        public string Name = "???";


        public Cat Leader;
        public byte LeaderLives;
        public byte LeaderPredecessors;

        public Cat Deputy;
        public byte DeputyPredecessors;

        public Cat Instructor;


        public Clan()
        {
            Herbs = new HerbCollection();
        }


        /// <summary>
        /// Makes a Cat a Clan cat, returning a List of any additional Cats that are joining
        /// </summary>
        /// <param name="Cat">The cat to add</param>
        public List<CatRef> AddToClan(Cat Cat)
        {
            Cat.Outside = false;

            if (!Cat.Outside)
                Cat.Clan = Name;

            List<CatRef> Kits = Cat.Kin.Kits;
            List<CatRef> Additional = new List<CatRef>();
            for (int i = 0; i < Cat.Kin.Kits.Count; i++)
            {
                Cat Child = Kits[0];
                if (Child.Outside && !Child.Exiled && Child.Moons < 12)
                {
                    AddToClan(Cat);
                    Additional.Add(Kits[0]);
                }
            }

            return Additional;
        }



        public void AddToOutside(Cat Cat)
        {

        }



        public void RemoveMedCat(Cat Cat)
        {

        }



        public void NewMedicineCat(Cat Cat)
        {

        }
    }
}
