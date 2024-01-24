using System.Collections.Generic;

namespace Clangen.Cats
{
    public static class History
    {
        interface IHistory
        {

        }

        public readonly struct Event : IHistory
        {
            public readonly CatRef Source;
            public readonly ushort Start;
            public readonly ushort End;

            public Event(CatRef Source, ushort Start, ushort End)
            {
                this.Source = Source;
                this.Start = Start;
                this.End = End;
            }
        }
    }
}
