

using Clangen.Cats;
using System.Collections.Generic;

namespace Clangen.Cats
{
    public readonly struct HistoryStamp
    {
        public readonly CatRef Cat;
        public readonly ushort Start;
        public readonly ushort End;

        public HistoryStamp(CatRef Cat, ushort Start, ushort End)
        {
            this.Cat = Cat;
            this.Start = Start;
            this.End = End;
        }

        public ushort Duration
        {
            get => (ushort)(End - Start);
        }
    }

    public class History
    {
        public List<HistoryStamp> Mates;
        public List<HistoryStamp> Mentors;
        public List<HistoryStamp> Apprentices;
    }
}
