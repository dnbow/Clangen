using System.Linq;

namespace Clangen.Parsers
{
    public static class Eval
    {
        public static object StringToObject(string Value) // NEED TO OPTIMISE DESPERATELY
        {
            string Internal = Value.Trim();

            switch (Internal)
            {
                case "true":
                case "True":
                    return true;

                case "false":
                case "False":
                    return false;

                default:
                    break;
            }

            if (Value.StartsWith("\"") && Value.EndsWith("\"") || Value.StartsWith("'") && Value.EndsWith("'"))
            {
                Internal = Value.Substring(1, Value.Length - 2);
                if (Internal.StartsWith("#"))
                    return new Color(Value);

                return Internal;
            }
            else if (Value.All(c => 47 < c && c < 58))
            {
                return int.Parse(Value);
            }
            else if (Value.All(c => 47 < c && c < 58 || c == 46) || Value.Contains("."))
            {
                return double.Parse(Value);
            }

            return null; // Fallthrough fallacy -> we can check if this wasnt set easily
        }
    }
}
