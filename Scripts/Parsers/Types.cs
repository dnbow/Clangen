using System;
using System.Linq;

namespace Clangen.Parsers
{
    public static class Eval
    {
        public static object EvalNull = new object();

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

                case "null":
                case "Null":
                    return null;

                default:
                    break;
            }



            if (Internal.StartsWith("\"") && Internal.EndsWith("\"") || Internal.StartsWith("'") && Internal.EndsWith("'"))
            {
                Internal = Internal.Substring(1, Internal.Length - 2);
                if (Internal.StartsWith("#"))
                    return new Color(Internal);

                return Internal;
            }
            else if (Internal.All(c => 47 < c && c < 58))
            {
                return int.Parse(Internal);
            }
            else if (Internal.All(c => 47 < c && c < 58 || c == 46) || Internal.Contains("."))
            {
                return double.Parse(Internal);
            }

            return EvalNull; // Fallthrough fallacy -> we can check if this wasnt set easily
        }
    }
}
