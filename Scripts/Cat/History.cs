using System.Collections.Generic;

namespace Clangen.Cats
{
    public readonly struct HistoryStamp
    {
        public readonly CatRef Source;
        public readonly ushort Start;
        public readonly ushort End;

        public HistoryStamp(CatRef Source, ushort Start, ushort End)
        {
            this.Source = Source;
            this.Start = Start;
            this.End = End;
        }
    }

    public class History
    {
        public List<HistoryStamp> Mates;
        public List<HistoryStamp> Mentors;
        public List<HistoryStamp> Apprentices;
    }
}
