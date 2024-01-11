using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Clangen.Parsers
{
    public static class INI
    {
        public static Dictionary<string, object> Load(string Path) // NEED TO FIX
        {
            Dictionary<string, object> Fields = new Dictionary<string, object>();

            bool Definition = false;
            bool Comment = false;
            bool Assignment = false;
            bool DelimitedString = false;
            bool DelimitedStringSingle = false;

            List<string> Definitions = new List<string>();

            string Phrase = "";
            string LastKey = "";

            using (var reader = new StreamReader(Path))
            {
                int Byte;
                char Char = default;

                while ((Byte = reader.Read()) != -1)
                {
                    Char = (char)Byte;
                    switch (Char)
                    {
                        case '#':
                        case ';':
                            if (!DelimitedString && !DelimitedStringSingle)
                            {
                                Comment = true;

                                if (Assignment && LastKey != "")
                                {
                                    Fields[LastKey] = Eval.StringToObject(Phrase.Substring(2));
                                    LastKey = "";
                                }
                            }

                            break;

                        case '\n':
                            if (Assignment && LastKey != "")
                            {
                                Fields[LastKey.Trim()] = Eval.StringToObject(Phrase.Substring(2));
                                LastKey = "";
                            }

                            Definition = Comment = Assignment = DelimitedString = DelimitedStringSingle = false;
                            Phrase = "";

                            break;

                        case '"':
                            if (!DelimitedStringSingle)
                            {
                                if (DelimitedString && Assignment && LastKey != "")
                                {
                                    Fields[LastKey.Trim()] = Eval.StringToObject(Phrase.Substring(2) + '"');
                                    LastKey = "";
                                }

                                DelimitedString = !DelimitedString;
                            }

                            break;

                        case '\'':
                            if (!DelimitedString)
                            {
                                if (DelimitedStringSingle && Assignment && LastKey != "")
                                {
                                    Fields[LastKey.Trim()] = Eval.StringToObject(Phrase.Substring(2) + '\'');
                                    LastKey = "";
                                }

                                DelimitedStringSingle = !DelimitedStringSingle;
                            }

                            break;

                        case '[':
                            if (!(Comment || Assignment || DelimitedString || DelimitedStringSingle))
                            {
                                Definition = true;
                                Phrase = "";
                            }

                            break;

                        case ']':
                            if (!(Comment || Assignment || DelimitedString || DelimitedStringSingle))
                            {
                                Definition = false;
                                Definitions.Add(Phrase.Substring(1));
                            }

                            break;

                        case '=':
                            if (!Comment)
                            {
                                Assignment = true;
                                LastKey = Phrase;
                                Phrase = "";
                            }

                            break;

                        default:
                            break;
                    }

                    if (!Comment && Char != '\n')
                        Phrase += Char;
                }

                if (Assignment && LastKey != "")
                {
                    Fields[LastKey.Trim()] = Eval.StringToObject(Phrase.Substring(2));
                }
            }

            return Fields;
        }
    }
}
