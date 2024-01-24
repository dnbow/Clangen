using Newtonsoft.Json.Bson;
using System.Collections.Generic;
using System.Linq;

namespace Clangen.Cats
{
    public enum GriefType : byte
    {
        None, Minor, Major
    }

    public enum RelationDynamic : byte
    {
        None, Mate, Sibling, Child, Parent
    }

    public enum RelationFacet : byte
    {
        Romantic, Platonic, Trust, Admiration, Comfortable, Jealousy, Dislike
    }

    public struct Relation
    {
        public RelationDynamic Dynamic;
        public byte Romantic;
        public byte Platonic;
        public byte Admiration;
        public byte Trust;
        public byte Comfortable;
        public byte Jealousy;
        public byte Dislike;
    }

    public class Relations
    {
        private readonly CatRef Source;
        private readonly Dictionary<CatRef, Relation> Map;

        /// <summary>
        /// A List of this Cats current Mates
        /// </summary>
        public List<CatRef> Mates;

        /// <summary>
        /// A List of this Cats current Parents, meaning whichever parental figures exist 
        /// </summary>
        public List<CatRef> Parents;

        /// <summary>
        /// Gets a List of Non-Biological parents from Relations.Parents
        /// </summary>
        public List<CatRef> AdoptiveParents
        {

            get
            {
                List<CatRef> Cats = new List<CatRef>();

                Cat Cat = Source;

                for (int i = 0; i < Parents.Count; i++)
                {
                    CatRef Parent = Parents[i];
                    
                    if (Parent != Cat.Kin.Parent1 && Parent != Cat.Kin.Parent2)
                        Cats.Add(Parent);
                }

                return Cats;
            }
        }


        public int Count
        {
            get => Map.Count;
        }


        public CatRef[] Cats()
        {
            return Map.Keys.ToArray();
        }


        public Relations()
        {
            Map = new Dictionary<CatRef, Relation>();
        }



        public Relation this[CatRef Cat]
        {
            get => Map[Cat];
        }



        public void Clear()
        {

        }
    }


    public class KinRelations
    {
        private readonly CatRef Source;

        /// <summary>
        /// A <see cref="CatRef"> pointing to this Cats First Biological Parent
        /// </summary>
        public CatRef Parent1;

        /// <summary>
        /// A <see cref="CatRef"> pointing to this Cats Second Biological Parent
        /// </summary>
        public CatRef Parent2;

        /// <summary>
        /// A List of <see cref="CatRef"> objects that are Kits to this cat
        /// </summary>
        public List<CatRef> Kits;

        /// <summary>
        /// A List of <see cref="CatRef"> objects that are Siblings to this cat
        /// </summary>
        public List<CatRef> Siblings;

        /// <summary>
        /// A List of <see cref="CatRef"> objects that are Siblings to this cats' Parents
        /// </summary>
        public List<CatRef> ParentSiblings
        {
            get
            {
                List<CatRef> Cats = new List<CatRef>();

                if (Parent1 != null)
                    for (int i = 0; i < Parent1.Value.Kin.Siblings.Count; i++)
                        Cats.Add(Parent1.Value.Kin.Siblings[i]);
                if (Parent2 != null)
                    for (int i = 0; i < Parent2.Value.Kin.Siblings.Count; i++)
                        Cats.Add(Parent2.Value.Kin.Siblings[i]);

                return Cats;
            }
        }

        /// <summary>
        /// A List of <see cref="CatRef"> objects that are Mates to this cats' Kits
        /// </summary>
        public List<CatRef> KitsMates
        {
            get
            {
                List<CatRef> Cats = new List<CatRef>();

                for (int i = 0; i < Kits.Count; i++)
                {
                    List<CatRef> Temp = Kits[i].Value.Relations.Mates;

                    for (int k = 0; k < Temp.Count; k++)
                        Cats.Add(Temp[k]);
                }

                return Cats;
            }
        }

        /// <summary>
        /// Gets a List of <see cref="CatRef"/> objects that are Mates of this cats Siblings
        /// </summary>
        public List<CatRef> SiblingsMates
        {
            get
            {
                List<CatRef> Cats = new List<CatRef>();

                for (int i = 0; i < Siblings.Count; i++)
                {
                    List<CatRef> Temp = Siblings[i].Value.Relations.Mates;

                    for (int k = 0; k < Temp.Count; k++)
                        Cats.Add(Temp[k]);
                }

                return Cats;
            }
        }

        /// <summary>
        /// Gets a List of <see cref="CatRef"/> objects that are Niblings of this Cat
        /// </summary>
        public List<CatRef> SiblingsKits
        {
            get
            {
                List<CatRef> Cats = new List<CatRef>();

                for (int i = 0; i < Siblings.Count; i++)
                {
                    List<CatRef> Temp = Siblings[i].Value.Kin.Kits;

                    for (int k = 0; k < Temp.Count; k++)
                        Cats.Add(Temp[k]);
                }

                return Cats;
            }
        }

        /// <summary>
        /// Gets an array of 4 <see cref="CatRef"/> objects, the first 2 are the Two Parents of this Cats' first parent and the last 2 are the Two Parents of this Cats' second parent
        /// </summary>
        public CatRef[] Grandparents
        {
            get
            {
                CatRef[] Cats = new CatRef[4];

                if (Parent1 != null)
                {
                    Cats[0] = Parent1.Value.Kin.Parent1;
                    Cats[1] = Parent1.Value.Kin.Parent2;
                }
                if (Parent2 != null)
                {
                    Cats[2] = Parent2.Value.Kin.Parent1;
                    Cats[3] = Parent2.Value.Kin.Parent2;
                }

                return Cats;
            }
        }

        /// <summary>
        /// Gets a List of <see cref="CatRef"/> objects that are Grandkits of this Cat
        /// </summary>
        public List<CatRef> Grandkits
        {
            get
            {
                List<CatRef> Cats = new List<CatRef>();

                for (int i = 0; i < Kits.Count; i++)
                {
                    List<CatRef> Temp = Kits[i].Value.Kin.Kits;

                    for (int k = 0; k < Temp.Count; k++)
                        Cats.Add(Temp[k]);
                }

                return Cats;
            }
        }

        /// <summary>
        /// Gets a List of <see cref="CatRef"/> objects that are Cousins of this Cat
        /// </summary>
        public List<CatRef> Cousins
        {
            get
            {
                List<CatRef> Cats = new List<CatRef>();

                if (Parent1 != null)
                {
                    List<CatRef> Temp = Parent1.Value.Kin.Siblings;
                    for (int i = 0; i < Temp.Count; i++)
                        Cats.Add(Temp[i]);
                    
                }
                if (Parent2 != null)
                {
                    List<CatRef> Temp = Parent2.Value.Kin.Siblings;
                    for (int i = 0; i < Temp.Count; i++)
                        Cats.Add(Temp[i]);
                    
                }

                return Cats;
            }
        }


        public KinRelations()
        {
            Kits = new List<CatRef>();
            Siblings = new List<CatRef>();
        }


        public void Update()
        {

        }
    }
}
