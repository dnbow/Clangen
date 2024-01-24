using System.Collections.Generic;

namespace Clangen.Events
{
    public enum EventType : byte
    {
        Herb, 
        Ceremony,
        Death,
    }


    public struct Event
    {
        public readonly EventType Type;
        public readonly string Text;

        public Event(EventType Type, string Text)
        {
            this.Type = Type;
            this.Text = Text;
        }
    }


    public class EventPipeline
    {
        private Stack<Event> Pipeline;

        public void Push(Event Event)
        {
            Pipeline.Push(Event);
        }


        public Event Pop()
        {
            return Pipeline.Pop();
        }


        public Event Peek()
        {
            return Pipeline.Peek();
        }
    }
}
