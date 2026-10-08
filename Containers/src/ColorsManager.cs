using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using c = System.ConsoleColor;

namespace Containers.src
{
    public static class ColorsManager
    {
        public static c DefaultForegroundColor { get; } = c.White;
        public static c DefaultBackgroundColor { get; } = c.Black;
        public static readonly string ArgSplit = "_";
        public static readonly string DefaultArgs = "Default";

        public static List<Text> GetSubstrings(string line)
        {
            string parameters = string.Empty;
            string content = string.Empty;
            Text text = new();
            List<Text> substrings = new();
            bool settingColors = false;

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '<' )
                {
                    if (text.Edited)
                    {
                        text.Content = content;
                        substrings.Add(text);
                        content = string.Empty;
                        text = new();
                    }
                    settingColors = true;
                }
                else if (line[i] == '>')
                {
                    text = ReturnTextStruct(parameters);
                    text.Edited = true;
                    parameters = string.Empty;
                    settingColors = false;
                }
                else if (settingColors)
                {
                    parameters += line[i];
                }
                else content += line[i];
            }

            if(text.Edited)
            {
                text.Content = content;
                substrings.Add(text);
            }

            return substrings;
        }

        public static string ReturnTextOnly(string line)
        {
            string text = string.Empty;
            bool writing = true;
            for(int i = 0; i < line.Length; i++)
            {
                if (line[i] == '<') writing = false;
                if (line[i] == '>')
                {
                    writing = true;
                    i++;
                }
                text += writing && i < line.Length ? line[i] : string.Empty;
            }

            return text;
        }

        private static Text ReturnTextStruct(string parameters)
        {
            Text text = new Text();

            if(parameters == DefaultArgs)
            {
                text.ForegroundColor = DefaultForegroundColor;
                text.BackgroundColor = DefaultBackgroundColor;
            }
            else
            {
                string[] arguments = parameters.Split(ArgSplit).ToArray();
                switch (arguments[0])
                {
                    case "Black":
                        text.ForegroundColor = c.Black;
                        break;
                    case "DarkBlue":
                        text.ForegroundColor = c.DarkBlue;
                        break;
                    case "DarkGreen":
                        text.ForegroundColor = c.DarkGreen;
                        break;
                    case "DarkCyan":
                        text.ForegroundColor = c.DarkCyan;
                        break;
                    case "DarkRed":
                        text.ForegroundColor = c.DarkRed;
                        break;
                    case "DarkMagenta":
                        text.ForegroundColor = c.DarkMagenta;
                        break;
                    case "DarkYellow":
                        text.ForegroundColor = c.DarkYellow;
                        break;
                    case "Gray":
                        text.ForegroundColor = c.Gray;
                        break;
                    case "DarkGray":
                        text.ForegroundColor = c.DarkGray;
                        break;
                    case "Blue":
                        text.ForegroundColor = c.Blue;
                        break;
                    case "Green":
                        text.ForegroundColor = c.Green;
                        break;
                    case "Cyan":
                        text.ForegroundColor = c.Cyan;
                        break;
                    case "Red":
                        text.ForegroundColor = c.Red;
                        break;
                    case "Magenta":
                        text.ForegroundColor = c.Magenta;
                        break;
                    case "Yellow":
                        text.ForegroundColor = c.Yellow;
                        break;
                    case "White":
                        text.ForegroundColor = c.White;
                        break;
                    default:
                        text.ForegroundColor = DefaultForegroundColor;
                        break;
                }

                if (arguments.Length == 2)
                {
                    switch (arguments[1])
                    {
                        case "Black":
                            text.BackgroundColor = c.Black;
                            break;
                        case "DarkBlue":
                            text.BackgroundColor = c.DarkBlue;
                            break;
                        case "DarkGreen":
                            text.BackgroundColor = c.DarkGreen;
                            break;
                        case "DarkCyan":
                            text.BackgroundColor = c.DarkCyan;
                            break;
                        case "DarkRed":
                            text.BackgroundColor = c.DarkRed;
                            break;
                        case "DarkMagenta":
                            text.BackgroundColor = c.DarkMagenta;
                            break;
                        case "DarkYellow":
                            text.BackgroundColor = c.DarkYellow;
                            break;
                        case "Gray":
                            text.BackgroundColor = c.Gray;
                            break;
                        case "DarkGray":
                            text.BackgroundColor = c.DarkGray;
                            break;
                        case "Blue":
                            text.BackgroundColor = c.Blue;
                            break;
                        case "Green":
                            text.BackgroundColor = c.Green;
                            break;
                        case "Cyan":
                            text.BackgroundColor = c.Cyan;
                            break;
                        case "Red":
                            text.BackgroundColor = c.Red;
                            break;
                        case "Magenta":
                            text.BackgroundColor = c.Magenta;
                            break;
                        case "Yellow":
                            text.BackgroundColor = c.Yellow;
                            break;
                        case "White":
                            text.BackgroundColor = c.White;
                            break;
                        default:
                            text.BackgroundColor = DefaultBackgroundColor;
                            break;
                    }
                }
            }

            return text;
        }

        public static string BuildTextCode(string text, c foregroundColor, c backgroundColor)
        {
            string buff = "<";
            switch (foregroundColor)
            {
                case c.Black:
                    buff += "Black";
                    break;
                case c.DarkBlue:
                    buff += "DarkBlue";
                    break;
                case c.DarkGreen:
                    buff += "DarkGreen";
                    break;
                case c.DarkCyan:
                    buff += "DarkCyan";
                    break;
                case c.DarkRed:
                    buff += "DarkRed";
                    break;
                case c.DarkMagenta:
                    buff += "DarkMagenta";
                    break;
                case c.DarkYellow:
                    buff += "DarkYellow";
                    break;
                case c.Gray:
                    buff += "Gray";
                    break;
                case c.DarkGray:
                    buff += "DarkGray";
                    break;
                case c.Blue:
                    buff += "Blue";
                    break;
                case c.Green:
                    buff += "Green";
                    break;
                case c.Cyan:
                    buff += "Cyan";
                    break;
                case c.Red:
                    buff += "Red";
                    break;
                case c.Magenta:
                    buff += "Magenta";
                    break;
                case c.Yellow:
                    buff += "Yellow";
                    break;
                case c.White:
                    buff += "White";
                    break;
            }

            buff += ArgSplit;

            switch (backgroundColor)
            {
                case c.Black:
                    buff += "Black";
                    break;
                case c.DarkBlue:
                    buff += "DarkBlue";
                    break;
                case c.DarkGreen:
                    buff += "DarkGreen";
                    break;
                case c.DarkCyan:
                    buff += "DarkCyan";
                    break;
                case c.DarkRed:
                    buff += "DarkRed";
                    break;
                case c.DarkMagenta:
                    buff += "DarkMagenta";
                    break;
                case c.DarkYellow:
                    buff += "DarkYellow";
                    break;
                case c.Gray:
                    buff += "Gray";
                    break;
                case c.DarkGray:
                    buff += "DarkGray";
                    break;
                case c.Blue:
                    buff += "Blue";
                    break;
                case c.Green:
                    buff += "Green";
                    break;
                case c.Cyan:
                    buff += "Cyan";
                    break;
                case c.Red:
                    buff += "Red";
                    break;
                case c.Magenta:
                    buff += "Magenta";
                    break;
                case c.Yellow:
                    buff += "Yellow";
                    break;
                case c.White:
                    buff += "White";
                    break;
            }

            buff += ">";
            buff += text;

            return buff;
        }

        public static string BuildTextCode(string text, Font font)
        {
            string buff = "<";
            switch (font.f)
            {
                case c.Black:
                    buff += "Black";
                    break;
                case c.DarkBlue:
                    buff += "DarkBlue";
                    break;
                case c.DarkGreen:
                    buff += "DarkGreen";
                    break;
                case c.DarkCyan:
                    buff += "DarkCyan";
                    break;
                case c.DarkRed:
                    buff += "DarkRed";
                    break;
                case c.DarkMagenta:
                    buff += "DarkMagenta";
                    break;
                case c.DarkYellow:
                    buff += "DarkYellow";
                    break;
                case c.Gray:
                    buff += "Gray";
                    break;
                case c.DarkGray:
                    buff += "DarkGray";
                    break;
                case c.Blue:
                    buff += "Blue";
                    break;
                case c.Green:
                    buff += "Green";
                    break;
                case c.Cyan:
                    buff += "Cyan";
                    break;
                case c.Red:
                    buff += "Red";
                    break;
                case c.Magenta:
                    buff += "Magenta";
                    break;
                case c.Yellow:
                    buff += "Yellow";
                    break;
                case c.White:
                    buff += "White";
                    break;
            }

            buff += ArgSplit;

            switch (font.b)
            {
                case c.Black:
                    buff += "Black";
                    break;
                case c.DarkBlue:
                    buff += "DarkBlue";
                    break;
                case c.DarkGreen:
                    buff += "DarkGreen";
                    break;
                case c.DarkCyan:
                    buff += "DarkCyan";
                    break;
                case c.DarkRed:
                    buff += "DarkRed";
                    break;
                case c.DarkMagenta:
                    buff += "DarkMagenta";
                    break;
                case c.DarkYellow:
                    buff += "DarkYellow";
                    break;
                case c.Gray:
                    buff += "Gray";
                    break;
                case c.DarkGray:
                    buff += "DarkGray";
                    break;
                case c.Blue:
                    buff += "Blue";
                    break;
                case c.Green:
                    buff += "Green";
                    break;
                case c.Cyan:
                    buff += "Cyan";
                    break;
                case c.Red:
                    buff += "Red";
                    break;
                case c.Magenta:
                    buff += "Magenta";
                    break;
                case c.Yellow:
                    buff += "Yellow";
                    break;
                case c.White:
                    buff += "White";
                    break;
            }

            buff += ">";
            buff += text;

            return buff;
        }
    }
}